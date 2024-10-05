using Network_Packet_Traffic.Connections.Enums;

namespace Network_Packet_Traffic.Connections.Structs
{
    public struct PacketConnectionInfo
    {
        public string LocalAddress;
        public string MacAddress;
        public int LocalPort;
        public string RemoteAddress;
        public int RemotePort;
        public int ProcessId;
        public StateType State;
        public ProtocolType Protocol;
    }
}
