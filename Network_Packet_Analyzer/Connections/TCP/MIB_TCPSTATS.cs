using System.Runtime.InteropServices;

namespace Network_Packet_Analyzer.Connections.TCP
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_TCPSTATS
    {
        public uint dwRtoAlgorithm;
        public uint dwRtoMin;
        public uint dwRtoMax;
        public uint dwMaxConn;
        public uint dwActiveOpens;
        public uint dwPassiveOpens;
        public uint dwAttemptFails;
        public uint dwEstabResets;
        public uint dwCurrEstab;
        public uint dwInSegs;
        public uint dwOutSegs;
        public uint dwRetransSegs;
        public uint dwInErrs;
        public uint dwOutRsts;
        public uint dwNumConns;
    }
}
