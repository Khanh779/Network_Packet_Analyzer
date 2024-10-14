using System.Runtime.InteropServices;

namespace Network_Packet_Analyzer.Connections.ICMP
{
    /// <summary>
    /// Represents an individual ICMP entry.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_ICMPENTRY
    {
        public int Address;       // The IP address of the entry
        public uint ReceiveCount;  // Number of ICMP messages received
        public uint SendCount;     // Number of ICMP messages sent
        public uint Errors;        // Number of errors encountered
    }
}
