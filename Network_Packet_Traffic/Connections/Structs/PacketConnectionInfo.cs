using Network_Packet_Traffic.Connections.Enums;
using System.Net;

namespace Network_Packet_Traffic.Connections.Structs
{
    public struct PacketConnectionInfo
    {
        public IPAddress LocalAddress;
        public string MacAddress;
        public ushort LocalPort;
        public IPAddress RemoteAddress;
        public string MACAddress;
        public ushort RemotePort;
        public uint ProcessId;
        public StateType State;
        public ProtocolType Protocol;
    }
}
