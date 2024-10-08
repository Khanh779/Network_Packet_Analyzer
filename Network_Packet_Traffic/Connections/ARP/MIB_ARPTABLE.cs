using System.Runtime.InteropServices;

namespace Network_Packet_Analyzer.Connections.ARP
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_ARPTABLE
    {
        public uint NumberOfEntries { get; set; }
        public ARP.MIB_ARPROW[] arpTable { get; set; }


    }
}
