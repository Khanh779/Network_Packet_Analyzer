using Network_Packet_Analyzer.Connections.ARP;
using System.Net;
using System.Runtime.InteropServices;

namespace Network_Packet_Analyzer.Connections.ARP
{


    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_ARPROW
    {
        [MarshalAs(UnmanagedType.U4)]
        public int dwIndex;
        [MarshalAs(UnmanagedType.U4)]
        public int dwPhysAddrLen;
        [MarshalAs(UnmanagedType.U4)]
        public int dwAddr;
        [MarshalAs(UnmanagedType.U4)]
        public MIB_ARP_TYPE dwType;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] bPhysAddr;
    }
}
