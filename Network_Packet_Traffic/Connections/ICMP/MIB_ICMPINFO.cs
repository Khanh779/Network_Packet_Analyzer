using System.Runtime.InteropServices;

namespace Network_Packet_Analyzer.Connections.ICMP
{
    /// <summary>
    /// Represents the ICMP information structure containing both incoming and outgoing statistics.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_ICMPINFO
    {
        public MIB_ICMPSTATS icmpInStats;  // Incoming ICMP statistics
        public MIB_ICMPSTATS icmpOutStats; // Outgoing ICMP statistics
    }


    /// <summary>
    /// Represents ICMP statistics including inbound and outbound (extended version).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_ICMPINFO_EX
    {
        public MIB_ICMPSTATS_EX icmpInStats;   // Inbound statistics
        public MIB_ICMPSTATS_EX icmpOutStats;  // Outbound statistics
    }
}
