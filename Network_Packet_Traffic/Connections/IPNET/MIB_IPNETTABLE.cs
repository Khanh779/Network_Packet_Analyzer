using System.Runtime.InteropServices;

namespace Network_Packet_Traffic.Connections.IPNET
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_IPNETTABLE
    {
        public uint dwNumEntries;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public MIB_IPNETROW[] table;
    }
}
