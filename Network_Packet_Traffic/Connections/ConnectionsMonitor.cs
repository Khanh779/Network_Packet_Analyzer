using Network_Packet_Traffic.Connections.ARP;
using Network_Packet_Traffic.Connections.Enums;
using Network_Packet_Traffic.Connections.IPNET;
using Network_Packet_Traffic.Connections.Structs;
using Network_Packet_Traffic.Connections.TCP;
using Network_Packet_Traffic.Connections.UDP;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static Network_Packet_Traffic.Connections.NetHelper;

namespace Network_Packet_Traffic.Connections
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

        private PacketConnectionInfo[] _previousPackets = new PacketConnectionInfo[0];
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
        /// Reload the packet connections.
        /// </summary>
        public List<PacketConnectionInfo> GetPacketConnections()
        {
            MIB_TCPTABLE_OWNER_PID tcpTable = TCP.TCP_Info.GetTcpTable();
            MIB_UDPTABLE_OWNER_PID udpTable = UDP.UDP_Info.GetUdpTable();
            MIB_IPNETTABLE ipNetTable = IPNET.IPNET_Info.GetIpNetable();
            var arpTable = ARP.ARP_Info.GetARPTable();

            // Combine all packet connection information from TCP, UDP, IPNET, and ARP tables
            PacketConnectionInfo[] packetConnectionInfos = new PacketConnectionInfo[tcpTable.dwNumEntries + udpTable.dwNumEntries + ipNetTable.dwNumEntries + arpTable.NumberOfEntries];

            // Populate TCP connection data
            for (int i = 0; i < tcpTable.dwNumEntries; i++)
            {
                MIB_TCPROW_OWNER_PID tcpRow = tcpTable.table[i];
                PacketConnectionInfo packet = new PacketConnectionInfo
                {
                    LocalAddress = ConvertIpAddress((int)tcpRow.dwLocalAddr),
                    LocalPort = tcpRow.dwLocalPort,
                    RemoteAddress = ConvertIpAddress((int)tcpRow.dwRemoteAddr),
                    RemotePort = tcpRow.dwRemotePort,
                    ProcessId = tcpRow.dwOwningPid,
                    Protocol = ProtocolType.TCP,
                    State = GetState((int)tcpRow.dwState)
                };
                packetConnectionInfos[i] = packet;
            }

            // Populate UDP connection data
            for (int i = 0; i < udpTable.dwNumEntries; i++)
            {
                MIB_UDPROW_OWNER_PID udpRow = udpTable.table[i];
                PacketConnectionInfo packet = new PacketConnectionInfo
                {
                    LocalAddress = ConvertIpAddress((int)udpRow.dwLocalAddr),
                    LocalPort = udpRow.dwLocalPort,
                    RemoteAddress = ConvertIpAddress((int)udpRow.dwRemoteAddr),
                    RemotePort = udpRow.dwRemotePort,
                    State = GetState(-1),
                    ProcessId = udpRow.dwOwningPid,
                    Protocol = ProtocolType.UDP
                };
                packetConnectionInfos[i + tcpTable.dwNumEntries] = packet;
            }

            // Populate IPNET (ARP-like) connection data
            for (int i = 0; i < ipNetTable.dwNumEntries; i++)
            {
                MIB_IPNETROW arpRow = ipNetTable.table[i];
                PacketConnectionInfo packet = new PacketConnectionInfo
                {
                    LocalAddress = ConvertIpAddress(arpRow.dwAddr),
                    LocalPort = 0,
                    RemoteAddress = ConvertIpAddress(arpRow.dwPhysAddrLen),
                    RemotePort = 0,
                    State = GetState(-1),
                    ProcessId = 0,
                    Protocol = ProtocolType.IPNET
                };
                packetConnectionInfos[i + tcpTable.dwNumEntries + udpTable.dwNumEntries] = packet;
            }

            // Populate ARP table data
            for (int i = 0; i < arpTable.NumberOfEntries; i++)
            {
                var arpRow = arpTable.ARPConnections[i];
                PacketConnectionInfo packet = new PacketConnectionInfo
                {
                    LocalAddress = arpRow.Address,
                    LocalPort = 0,
                    MacAddress = arpRow.PhysicalAddress.ToString(),
                    RemoteAddress = new System.Net.IPAddress(0),
                    RemotePort = 0,
                    State = GetState(-1),
                    ProcessId = 0,
                    Protocol = ProtocolType.ARP
                };
                packetConnectionInfos[i + tcpTable.dwNumEntries + udpTable.dwNumEntries + ipNetTable.dwNumEntries] = packet;
            }
            return packetConnectionInfos.ToList();
        }


        #region Connection Monitoring

        /// <summary>
        /// Monitors all packet connections by fetching the TCP, UDP, IPNET, and ARP tables.
        /// </summary>
        private void MonitorPacketConnections()
        {
            do
            {

                PacketConnectionInfo[] packetConnectionInfos = GetPacketConnections().ToArray();

                // Trigger the event for loading new packet connections
                NewPacketsConnectionLoad?.Invoke(this, packetConnectionInfos);

                // Check and trigger events for new and ended connections
                foreach (var packet in packetConnectionInfos)
                {
                    if (!_previousPackets.Contains(packet))
                    {
                        NewPacketConnectionStarted?.Invoke(this, packet);
                    }
                }

                foreach (var oldPacket in _previousPackets)
                {
                    if (!packetConnectionInfos.Contains(oldPacket))
                    {
                        NewPacketConnectionEnded?.Invoke(this, oldPacket);
                    }
                }

                Thread.Sleep(Interval);
                _previousPackets = packetConnectionInfos;
            }
            while (IsAutoReload);
        }

        #endregion
    }
}
