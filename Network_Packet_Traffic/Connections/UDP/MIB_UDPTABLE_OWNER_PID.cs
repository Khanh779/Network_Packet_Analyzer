using System.Runtime.InteropServices;

namespace Network_Packet_Analyzer.Connections.UDP
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_UDPTABLE_OWNER_PID
    {
        public uint dwNumEntries;
        public MIB_UDPROW_OWNER_PID[] table;
    }
}
