using Network_Packet_Traffic.Connections.Enums;
using System.Net;

namespace Network_Packet_Traffic.Connections.Structs
{
    public struct PacketConnectionInfo
    {
        public IPAddress LocalAddress;
        public string MacAddress;
        public int LocalPort;
        public IPAddress RemoteAddress;
        public int RemotePort;
        public int ProcessId;
        public StateType State;
        public ProtocolType Protocol;
    }
}
