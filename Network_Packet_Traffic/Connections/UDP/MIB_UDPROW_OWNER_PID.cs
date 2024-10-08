using System.Runtime.InteropServices;

namespace Network_Packet_Analyzer.Connections.UDP
{

    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_UDPROW_OWNER_PID
    {
        //public uint dwLocalAddr;
        //public int dwLocalPort;
        //public int dwOwningPid;

        public int dwLocalAddr;
        public ushort dwLocalPort;
        public int dwRemoteAddr; // Địa chỉ remote
        public ushort dwRemotePort; // Cổng remote
        public uint dwOwningPid;
    }
}
