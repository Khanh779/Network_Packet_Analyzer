using Network_Packet_Analyzer.Connections.ARP;
using Network_Packet_Analyzer.Connections.Enums;
using Network_Packet_Analyzer.Connections.IPNET;
using Network_Packet_Analyzer.Connections.Structs;
using Network_Packet_Analyzer.Connections.TCP;
using Network_Packet_Analyzer.Connections.UDP;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
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

        private HashSet<PacketConnectionInfo> _previousPackets = new HashSet<PacketConnectionInfo>();
        private Thread _monitorThread = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionsMonitor"/> class.
        /// </summary>
        /// <param name="autoReload">Indicates whether the monitor should auto-reload periodically.</param>
        public ConnectionsMonitor(bool autoReload = true)
        {
            _monitorThread = new Thread(MonitorPacketConnections);
            IsAutoReload = autoReload;
            _monitorThread.IsBackground = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the monitor should auto-reload.
        /// </summary>
        public bool IsAutoReload { get; set; } = true;

        /// <summary>
        /// Gets or sets the interval for auto-reloading.
        /// </summary>
        public int Interval { get; set; } = 5000;

        /// <summary>
        /// Starts listening for new packet connections.
        /// </summary>
        public void StartListening()
        {
            if (_monitorThread != null && _monitorThread.ThreadState != ThreadState.Running)
                _monitorThread.Start();
        }

        /// <summary>
        /// Gets a value indicating whether the monitor is currently running.
        /// </summary>
        public bool IsRunning => _monitorThread.ThreadState == ThreadState.Running;

        /// <summary>
        /// Stops listening for new packet connections.
        /// </summary>
        public void StopListening()
        {
            if (_monitorThread.ThreadState == ThreadState.Running)
                _monitorThread.Abort();
        }

        /// <summary>
        /// Gets or sets the protocol filter for the packet connections.
        /// </summary>
        public ProtocolFilter ProtocolFilter { get; set; } = ProtocolFilter.All;


        HashSet<PacketConnectionInfo> GetBasePacket(ProtocolFilter protocolFilter = ProtocolFilter.All)
        {
            HashSet<PacketConnectionInfo> packetConnectionInfos = new HashSet<PacketConnectionInfo>();

            // Avoid loading unnecessary data based on the protocol filter
            if (protocolFilter == ProtocolFilter.All || protocolFilter == ProtocolFilter.TCP)
            {
                // Populate TCP connection data only if TCP is selected or all protocols
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

            if (protocolFilter == ProtocolFilter.All || protocolFilter == ProtocolFilter.UDP)
            {
                // Populate UDP connection data only if UDP is selected or all protocols
                MIB_UDPTABLE_OWNER_PID udpTable = UDP.UDP_Info.GetUdpTable();
                for (int i = 0; i < udpTable.dwNumEntries; i++)
                {
                    MIB_UDPROW_OWNER_PID udpRow = udpTable.table[i];
                    packetConnectionInfos.Add(new PacketConnectionInfo
                    {
                        LocalAddress = ConvertIpAddress(udpRow.dwLocalAddr),
                        LocalPort = udpRow.dwLocalPort,
                        RemoteAddress = IPAddress.None, // No remote address for listening sockets
                        RemotePort = 0, // Same for remote port in UDP listening
                        State = GetState(-1),
                        ProcessId = udpRow.dwOwningPid,
                        Protocol = ProtocolType.UDP
                    });
                }
            }

            if (protocolFilter == ProtocolFilter.All || protocolFilter == ProtocolFilter.IPNET)
            {
                // Populate IPNET (similar to ARP) connection data
                MIB_IPNETTABLE ipNetTable = IPNET.IPNET_Info.GetIpNetTable();
                for (int i = 0; i < ipNetTable.dwNumEntries; i++)
                {
                    MIB_IPNETROW ipnetRow = ipNetTable.table[i];
                    packetConnectionInfos.Add(new PacketConnectionInfo
                    {
                        LocalAddress = ConvertIpAddress(ipnetRow.dwAddr),
                        LocalPort = 0,
                        RemoteAddress = IPAddress.None, // No remote address, mapping to MAC address
                        MacAddress = ConvertMacAddress(ipnetRow.bPhysAddr),
                        RemotePort = 0,
                        State = GetState(-1),
                        ProcessId = 0,
                        Protocol = ProtocolType.IPNET
                    });
                }
            }

            if (protocolFilter == ProtocolFilter.All || protocolFilter == ProtocolFilter.ARP)
            {
                // Populate ARP table data only if ARP is selected or all protocols
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
        public List<PacketConnectionInfo> GetPacketConnections(ProtocolFilter protocolFilter)
        {
            return GetBasePacket(protocolFilter).ToList();
        }

        /// <summary>
        /// Gets all packet connections based on the <see cref="ProtocolFilter"/>.
        /// </summary>
        /// <returns>List of <see cref="PacketConnectionInfo"/> for all protocols.</returns>
        public List<PacketConnectionInfo> GetPacketConnections()
        {
            return GetBasePacket(ProtocolFilter.All).ToList();
        }



        #region Connection Monitoring

        /// <summary>
        /// Monitors all packet connections by fetching the TCP, UDP, IPNET, and ARP tables.
        /// </summary>
        private void MonitorPacketConnections()
        {
            while (IsAutoReload)
            {
                HashSet<PacketConnectionInfo> packetConnectionInfos = GetBasePacket(ProtocolFilter);

                // Trigger event for all new packets
                foreach (var packet in packetConnectionInfos)
                {
                    if (_previousPackets.Add(packet))
                    {
                        NewPacketConnectionStarted?.Invoke(this, packet);
                    }
                }

                // Check for disconnected packets
                var disconnectedPackets = _previousPackets.Where(oldPacket => !packetConnectionInfos.Contains(oldPacket)).ToList();
                foreach (var oldPacket in disconnectedPackets)
                {
                    NewPacketConnectionEnded?.Invoke(this, oldPacket);
                    _previousPackets.Remove(oldPacket);
                }

                // Trigger event for new packets
                NewPacketsConnectionLoad?.Invoke(this, packetConnectionInfos.ToArray());

                Thread.Sleep(Interval);
            }
        }


        #endregion



    }
}
