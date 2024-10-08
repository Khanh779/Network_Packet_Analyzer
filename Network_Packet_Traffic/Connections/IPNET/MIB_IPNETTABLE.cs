using System.Runtime.InteropServices;

namespace Network_Packet_Analyzer.Connections.IPNET
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_IPNETTABLE
    {
        public uint dwNumEntries;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public MIB_IPNETROW[] table;
    }
}
