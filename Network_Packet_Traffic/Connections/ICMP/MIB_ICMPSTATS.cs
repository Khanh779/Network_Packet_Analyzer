using System.Runtime.InteropServices;

namespace Network_Packet_Analyzer.Connections.ICMP
{
    /// <summary>
    /// Represents the ICMP statistics structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_ICMPSTATS
    {
        public uint dwMsgs;             // Total number of ICMP messages
        public uint dwErrors;           // Total number of errors
        public uint dwDestUnreachs;     // Number of destination unreachable messages
        public uint dwTimeExcds;        // Number of time exceeded messages
        public uint dwParmProbs;        // Number of parameter problems
        public uint dwSrcQuenchs;       // Number of source quench messages
        public uint dwRedirects;        // Number of redirects
        public uint dwEchos;            // Number of echo requests
        public uint dwEchoReps;         // Number of echo replies
        public uint dwTimestamps;       // Number of timestamp requests
        public uint dwTimestampReps;    // Number of timestamp replies
        public uint dwAddrMasks;        // Number of address mask requests
        public uint dwAddrMaskReps;     // Number of address mask replies
    }


    /// <summary>
    /// Represents individual ICMP statistics (extended version).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_ICMPSTATS_EX
    {
        public uint dwMsgs;              // Number of messages
        public uint dwErrors;            // Number of errors
        public uint dwDestUnreachs;      // Number of destination unreachable messages
        public uint dwTimeExcds;         // Number of time exceeded messages
        public uint dwParmProbs;          // Number of parameter problem messages
        public uint dwSrcQuenchs;         // Number of source quench messages
        public uint dwRedirects;          // Number of redirects
        public uint dwEchos;              // Number of echo requests
        public uint dwEchoReps;           // Number of echo replies
        public uint dwTimestamps;         // Number of timestamp requests
        public uint dwTimestampReps;      // Number of timestamp replies
        public uint dwAddrMasks;          // Number of address mask requests
        public uint dwAddrMaskReps;       // Number of address mask replies
        public uint dwInfoRequests;       // Number of info requests
        public uint dwInfoReps;           // Number of info replies
    }

}
