using System.Runtime.InteropServices;

namespace Network_Packet_Analyzer.Connections.TCP
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_TCPTABLE_OWNER_PID
    {
        public uint dwNumEntries;
        public MIB_TCPROW_OWNER_PID[] table;
    }
}
