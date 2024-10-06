using System.Runtime.InteropServices;

namespace Network_Packet_Traffic.Connections.UDP
{

    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_UDPROW_OWNER_PID
    {
        //public uint dwLocalAddr;
        //public int dwLocalPort;
        //public int dwOwningPid;

        public uint dwLocalAddr;
        public int dwLocalPort;
        public uint dwRemoteAddr; // Địa chỉ remote
        public int dwRemotePort; // Cổng remote
        public int dwOwningPid;
    }
}
