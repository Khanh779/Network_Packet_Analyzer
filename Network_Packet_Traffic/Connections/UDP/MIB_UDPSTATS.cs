using System.Runtime.InteropServices;

namespace Network_Packet_Analyzer.Connections.UDP
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_UDPSTATS
    {
        public uint dwInDatagrams;
        public ushort dwNoPorts;
        public uint dwInErrors;
        public uint dwOutDatagrams;
        public uint dwNumAddrs;
    }
}
