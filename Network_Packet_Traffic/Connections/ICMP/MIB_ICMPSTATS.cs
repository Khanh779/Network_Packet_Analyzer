using System.Runtime.InteropServices;

namespace Network_Packet_Traffic.Connections.ICMP
{
    /// <summary>
    /// Represents the ICMP statistics structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_ICMPSTATS
    {
        public int dwMsgs;             // Total number of ICMP messages
        public int dwErrors;           // Total number of errors
        public int dwDestUnreachs;     // Number of destination unreachable messages
        public int dwTimeExcds;        // Number of time exceeded messages
        public int dwParmProbs;        // Number of parameter problems
        public int dwSrcQuenchs;       // Number of source quench messages
        public int dwRedirects;        // Number of redirects
        public int dwEchos;            // Number of echo requests
        public int dwEchoReps;         // Number of echo replies
        public int dwTimestamps;       // Number of timestamp requests
        public int dwTimestampReps;    // Number of timestamp replies
        public int dwAddrMasks;        // Number of address mask requests
        public int dwAddrMaskReps;     // Number of address mask replies
    }


    /// <summary>
    /// Represents individual ICMP statistics (extended version).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_ICMPSTATS_EX
    {
        public int dwMsgs;              // Number of messages
        public int dwErrors;            // Number of errors
        public int dwDestUnreachs;      // Number of destination unreachable messages
        public int dwTimeExcds;         // Number of time exceeded messages
        public int dwParmProbs;          // Number of parameter problem messages
        public int dwSrcQuenchs;         // Number of source quench messages
        public int dwRedirects;          // Number of redirects
        public int dwEchos;              // Number of echo requests
        public int dwEchoReps;           // Number of echo replies
        public int dwTimestamps;         // Number of timestamp requests
        public int dwTimestampReps;      // Number of timestamp replies
        public int dwAddrMasks;          // Number of address mask requests
        public int dwAddrMaskReps;       // Number of address mask replies
        public int dwInfoRequests;       // Number of info requests
        public int dwInfoReps;           // Number of info replies
    }

}
