using System.Runtime.InteropServices;

namespace Network_Packet_Analyzer.Connections.IPNET
{


    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_IPNETROW
    {
        [MarshalAs(UnmanagedType.U4)]
        public int dwIndex;
        [MarshalAs(UnmanagedType.U4)]
        public int dwPhysAddrLen;
        [MarshalAs(UnmanagedType.U4)]
        public int dwAddr;
        [MarshalAs(UnmanagedType.U4)]
        public int dwType;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] bPhysAddr;
    }
}

