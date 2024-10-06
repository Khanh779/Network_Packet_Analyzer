using Network_Packet_Traffic.Connections.IPNET;
using System.Net;

namespace Network_Packet_Traffic.Connections.ARP
{
    public class MIB_ARPROW
    {
        public int Index { get; set; }
        public int PhysicalAddressLength { get; set; }
        public byte[] PhysicalAddress { get; set; }
        public IPAddress Address { get; set; }
        public MIB_IPNET_TYPE Type { get; set; }

    }
}
