using System.Runtime.InteropServices;

namespace Network_Packet_Traffic.Connections.IPNET
{
    //[StructLayout(LayoutKind.Sequential)]
    //public struct MIB_IPNETROW
    //{
    //    public int dwIndex;
    //    public int dwPhysAddrLen;

    //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
    //    public byte[] bPhysAddr;

    //    public int dwAddr;
    //    public int dwType;
    //}

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

    //[StructLayout(LayoutKind.Sequential)]
    //public struct IpNetEntry
    //{
    //    public int dwIndex;
    //    public int dwAddr;
    //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    //    public byte[] macAddr;
    //    public int dwType;
    //}
}
