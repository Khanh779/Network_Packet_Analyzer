using Network_Packet_Analyzer.Connections.DHCP;
using Network_Packet_Analyzer.Connections.DNS;
using Network_Packet_Analyzer.Connections.Enums;
using System.Net;

namespace Network_Packet_Analyzer.Connections.Structs
{
    public struct PacketConnectionInfo
    {
        /// <summary>
        /// Local IP address of the connection.
        /// </summary>
        public IPAddress LocalAddress;

        /// <summary>
        /// MAC address of the local device.
        /// </summary>
        public string MacAddress;

        /// <summary>
        /// Local port used in the connection.
        /// </summary>
        public ushort LocalPort;

        /// <summary>
        /// Remote IP address for the connection.
        /// </summary>
        public IPAddress RemoteAddress;

        /// <summary>
        /// MAC address of the remote device (if applicable).
        /// </summary>
        public string RemoteMacAddress;

        /// <summary>
        /// Remote port used in the connection.
        /// </summary>
        public ushort RemotePort;

        /// <summary>
        /// Process ID associated with the connection.
        /// </summary>
        public uint ProcessId;

        /// <summary>
        /// Current state of the connection.
        /// </summary>
        public StateType State;

        /// <summary>
        /// Protocol used for the connection (e.g., TCP, UDP).
        /// </summary>
        public ProtocolType Protocol;

        /// <summary>
        /// DNS info related to the connection (if applicable).
        /// </summary>
        public DNS_Info DnsInfo; // Optional: Include DNS info

        /// <summary>
        /// DHCP info related to the connection (if applicable).
        /// </summary>
        public DHCP_Info DhcpInfo; // Optional: Include DHCP info
    }
}
