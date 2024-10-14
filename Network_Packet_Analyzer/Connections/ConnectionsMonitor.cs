using Network_Packet_Analyzer.Connections.ARP;
using Network_Packet_Analyzer.Connections.Enums;
using Network_Packet_Analyzer.Connections.IPNET;
using Network_Packet_Analyzer.Connections.Structs;
using Network_Packet_Analyzer.Connections.TCP;
using Network_Packet_Analyzer.Connections.UDP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Network_Packet_Analyzer.Connections.NetHelper;

namespace Network_Packet_Analyzer.Connections
{
    /// <summary>
    /// Delegate for handling the event when a new packet connection load occurs.
    /// </summary>
    public delegate void NewPacketsConnectionLoadHandler(object sender, PacketConnectionInfo[] packets);

    /// <summary>
    /// Delegate for handling the event when a new packet connection starts.
    /// </summary>
    public delegate void NewPacketConnectionStartedHandler(object sender, PacketConnectionInfo packet);

    /// <summary>
    /// Delegate for handling the event when a packet connection ends.
    /// </summary>
    public delegate void NewPacketConnectionEndedHandler(object sender, PacketConnectionInfo packet);

    public class ConnectionsMonitor
    {
        /// <summary>
        /// Event triggered when a new packet connection starts.
        /// </summary>
        public event NewPacketConnectionStartedHandler NewPacketConnectionStarted;

        /// <summary>
        /// Event triggered when new packet connections are loaded.
        /// </summary>
        public event NewPacketsConnectionLoadHandler NewPacketsConnectionLoad;

        /// <summary>
        /// Event triggered when a packet connection ends.
        /// </summary>
        public event NewPacketConnectionEndedHandler NewPacketConnectionEnded;

        private HashSet<PacketConnectionInfo> _previousPackets;
        private Thread _monitorThread = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionsMonitor"/> class.
        /// </summary>
        /// <param name="autoReload">Indicates whether the monitor should auto-reload periodically.</param>
        public ConnectionsMonitor(bool autoReload = true)
        {
            _previousPackets = new HashSet<PacketConnectionInfo>();
            IsAutoReload = autoReload;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the monitor should auto-reload.
        /// </summary>
        public bool IsAutoReload { get; set; } = true;

        /// <summary>
        /// Gets or sets the interval for auto-reloading.
        /// </summary>
        public int Interval { get; set; } = 4000;

        /// <summary>
        /// Starts listening for new packet connections.
        /// </summary>
        public void StartListening()
        {
            _shouldStop = false;
            if (_monitorThread == null || _monitorThread.ThreadState != ThreadState.Running) // Chỉ cần kiểm tra không chạy
            {
                _monitorThread = new Thread(MonitorPacketConnections);

                _monitorThread.IsBackground = true; // Đặt thread là background nếu chưa
                _monitorThread.Start();

                Console.WriteLine("Monitor started");
            }
        }


        /// <summary>
        /// Gets a value indicating whether the monitor is currently running.
        /// </summary>
        public bool IsRunning => _monitorThread != null && _monitorThread.ThreadState == ThreadState.Running;

        /// <summary>
        /// Stops listening for new packet connections.
        /// </summary>
        public void StopListening()
        {
            _shouldStop = true;
            if (_monitorThread.ThreadState == ThreadState.Running) // Chỉ cần kiểm tra đang chạy
            {
                _monitorThread.Abort(); // Sau đó hủy thread
                Console.WriteLine("Monitor stopped");
            }
        }

        /// <summary>
        /// Restarts the listening for new packet connections.
        /// </summary>
        public void RestartListening()
        {
            StopListening();
            StartListening();
            Console.WriteLine("Monitor restarted");
        }


        /// <summary>
        /// Pauses the listening for new packet connections.
        /// </summary>
        public void PauseListening()
        {
            if (_monitorThread.ThreadState == ThreadState.Running)
                _monitorThread.Suspend();

        }

        /// <summary>
        /// Resumes the listening for new packet connections.
        /// </summary>
        public void ResumeListening()
        {
            if (_monitorThread.ThreadState == ThreadState.Suspended)
                _monitorThread.Resume();

        }


        ProtocolType _ProtocolFilter = ProtocolType.All;

        /// <summary>
        /// Gets or sets the protocol filter for the packet connections.
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        public ProtocolType ProtocolFilter
        {
            get { return _ProtocolFilter; }
            set { _ProtocolFilter = value; }
        }


        HashSet<PacketConnectionInfo> GetBasePacket(ProtocolType protocolFilter)
        {
            HashSet<PacketConnectionInfo> packetConnectionInfos = new HashSet<PacketConnectionInfo>();

            if (protocolFilter.HasFlag(ProtocolType.TCP))
            {
                MIB_TCPTABLE_OWNER_PID tcpTable = TCP.TCP_Info.GetTcpTable();
                for (int i = 0; i < tcpTable.dwNumEntries; i++)
                {
                    MIB_TCPROW_OWNER_PID tcpRow = tcpTable.table[i];
                    packetConnectionInfos.Add(new PacketConnectionInfo
                    {
                        LocalAddress = ConvertIpAddress(tcpRow.dwLocalAddr),
                        LocalPort = tcpRow.dwLocalPort,
                        RemoteAddress = ConvertIpAddress(tcpRow.dwRemoteAddr),
                        RemotePort = tcpRow.dwRemotePort,
                        ProcessId = tcpRow.dwOwningPid,
                        Protocol = ProtocolType.TCP,
                        State = GetState(tcpRow.dwState)
                    });
                }
            }

            if (protocolFilter.HasFlag(ProtocolType.UDP))
            {
                MIB_UDPTABLE_OWNER_PID udpTable = UDP.UDP_Info.GetUdpTable();
                for (int i = 0; i < udpTable.dwNumEntries; i++)
                {
                    MIB_UDPROW_OWNER_PID udpRow = udpTable.table[i];
                    packetConnectionInfos.Add(new PacketConnectionInfo
                    {
                        LocalAddress = ConvertIpAddress(udpRow.dwLocalAddr),
                        LocalPort = udpRow.dwLocalPort,
                        RemoteAddress = IPAddress.None,
                        RemotePort = 0,
                        State = GetState(-1),
                        ProcessId = udpRow.dwOwningPid,
                        Protocol = ProtocolType.UDP
                    });
                }
            }

            if (protocolFilter.HasFlag(ProtocolType.IPNET))
            {
                MIB_IPNETTABLE ipNetTable = IPNET.IPNET_Info.GetIpNetTable();
                for (int i = 0; i < ipNetTable.dwNumEntries; i++)
                {
                    MIB_IPNETROW ipnetRow = ipNetTable.table[i];
                    packetConnectionInfos.Add(new PacketConnectionInfo
                    {
                        LocalAddress = ConvertIpAddress(ipnetRow.dwAddr),
                        LocalPort = 0,
                        RemoteAddress = IPAddress.None,
                        MacAddress = ConvertMacAddress(ipnetRow.bPhysAddr),
                        RemotePort = 0,
                        State = GetState(-1),
                        ProcessId = 0,
                        Protocol = ProtocolType.IPNET
                    });
                }
            }

            if (protocolFilter.HasFlag(ProtocolType.ARP))
            {
                MIB_ARPTABLE arpTable = ARP.ARP_Info.GetARPTable();
                for (int i = 0; i < arpTable.NumberOfEntries; i++)
                {
                    MIB_ARPROW arpRow = arpTable.arpTable[i];
                    packetConnectionInfos.Add(new PacketConnectionInfo
                    {
                        LocalAddress = ConvertIpAddress(arpRow.dwAddr),
                        LocalPort = 0,
                        MacAddress = ConvertMacAddress(arpRow.bPhysAddr),
                        RemoteAddress = IPAddress.None,
                        RemotePort = 0,
                        State = GetState(-1),
                        ProcessId = 0,
                        Protocol = ProtocolType.ARP
                    });
                }
            }

            return packetConnectionInfos;
        }



        /// <summary>
        /// Gets the packet connections based on the selected <see cref="ProtocolFilter"/>.
        /// </summary>
        /// <param name="protocolFilter">Filter to select specific protocols or all of them.</param>
        /// <returns>List of <see cref="PacketConnectionInfo"/> based on the protocol filter.</returns>
        public List<PacketConnectionInfo> GetPacketConnections(ProtocolType protocolFilter)
        {
            return GetBasePacket(protocolFilter).ToList();
        }

        /// <summary>
        /// Gets all packet connections based on the <see cref="ProtocolFilter"/>.
        /// </summary>
        /// <returns>List of <see cref="PacketConnectionInfo"/> for all protocols.</returns>
        public List<PacketConnectionInfo> GetPacketConnections()
        {
            return GetBasePacket(ProtocolType.All).ToList();
        }



        bool _shouldStop = false;

        #region Connection Monitoring

        /// <summary>
        /// Monitors all packet connections by fetching the TCP, UDP, IPNET, and ARP tables.
        /// </summary>
        private void MonitorPacketConnections()
        {
            while (IsAutoReload)
            {
                if (_shouldStop)
                    break;

                HashSet<PacketConnectionInfo> packetConnectionInfos = GetBasePacket(ProtocolFilter);

                // Trigger event for new packets
                NewPacketsConnectionLoad?.Invoke(this, packetConnectionInfos.ToArray());

                // Check for ended packet connections
                foreach (var oldPacket in _previousPackets)
                {
                    if (!packetConnectionInfos.Contains(oldPacket))
                    {
                        NewPacketConnectionEnded?.Invoke(this, oldPacket);
                    }
                }

                // Check for new packet connections
                foreach (var newPacket in packetConnectionInfos)
                {
                    if (!_previousPackets.Contains(newPacket))
                    {
                        NewPacketConnectionStarted?.Invoke(this, newPacket);
                    }
                }

                // Update previous packets
                _previousPackets = packetConnectionInfos; // No need to clear first

                // Sleep before the next iteration
                Thread.Sleep(Interval);
            }
        }


        #endregion



    }
}
