using System.Runtime.InteropServices;

namespace Network_Packet_Traffic.Connections.UDP
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_UDPSTATS
    {
        public int dwInDatagrams;
        public int dwNoPorts;
        public int dwInErrors;
        public int dwOutDatagrams;
        public int dwNumAddrs;
    }
}
