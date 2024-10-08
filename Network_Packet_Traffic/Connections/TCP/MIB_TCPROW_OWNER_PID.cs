using System.Runtime.InteropServices;

namespace Network_Packet_Analyzer.Connections.TCP
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_TCPROW_OWNER_PID
    {
        public int dwState;
        public int dwLocalAddr;
        public ushort dwLocalPort;
        public int dwRemoteAddr;
        public ushort dwRemotePort;
        public uint dwOwningPid;
    }
}
