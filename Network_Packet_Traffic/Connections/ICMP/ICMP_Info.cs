using System;
using System.Runtime.InteropServices;

namespace Network_Packet_Analyzer.Connections.ICMP
{
    /// <summary>
    /// Class to handle ICMP statistics retrieval and management.
    /// </summary>
    public class ICMP_Info
    {
        // Importing the GetIcmpStatistics function from iphlpapi.dll
        [DllImport("iphlpapi.dll")]
        public extern static int GetIcmpStatistics(ref MIB_ICMPINFO pStats);

        [DllImport("iphlpapi.dll")]
        public extern static int GetIcmpStatisticsEx(ref MIB_ICMPINFO_EX pStats);

        /// <summary>
        /// Method to retrieve extended ICMP statistics.
        /// </summary>
        public static MIB_ICMPINFO_EX RetrieveExtendedIcmpStatistics()
        {
            MIB_ICMPINFO_EX icmpStatsEx = new MIB_ICMPINFO_EX();
            int result = GetIcmpStatisticsEx(ref icmpStatsEx);

            if (result != 0)
            {
                throw new System.ComponentModel.Win32Exception(result);
            }


            return icmpStatsEx;
        }

        /// <summary>
        /// Method to retrieve ICMP statistics.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.ComponentModel.Win32Exception"></exception>
        public static MIB_ICMPINFO RetrieveIcmpStatistics()
        {
            MIB_ICMPINFO icmpStats = new MIB_ICMPINFO();
            int result = GetIcmpStatistics(ref icmpStats);

            if (result != 0)
            {
                throw new System.ComponentModel.Win32Exception(result);
            }

            return icmpStats;
        }


        /// <summary>
        /// Retrieves ICMP statistics and displays them.
        /// </summary>
        public void RetrieveAndDisplayStatistics()
        {
            MIB_ICMPINFO icmpInfo = new MIB_ICMPINFO();
            int result = GetIcmpStatistics(ref icmpInfo);

            if (result == 0) // 0 indicates success
            {
                Console.WriteLine("ICMP Incoming Statistics:");
                Console.WriteLine($"Messages: {icmpInfo.icmpInStats.dwMsgs}");
                Console.WriteLine($"Errors: {icmpInfo.icmpInStats.dwErrors}");
                Console.WriteLine($"Destination Unreachable: {icmpInfo.icmpInStats.dwDestUnreachs}");
                Console.WriteLine($"Time Exceeded: {icmpInfo.icmpInStats.dwTimeExcds}");
                Console.WriteLine($"Parameter Problems: {icmpInfo.icmpInStats.dwParmProbs}");
                Console.WriteLine($"Source Quench: {icmpInfo.icmpInStats.dwSrcQuenchs}");
                Console.WriteLine($"Redirects: {icmpInfo.icmpInStats.dwRedirects}");
                Console.WriteLine($"Echo Requests: {icmpInfo.icmpInStats.dwEchos}");
                Console.WriteLine($"Echo Replies: {icmpInfo.icmpInStats.dwEchoReps}");
                Console.WriteLine($"Timestamps: {icmpInfo.icmpInStats.dwTimestamps}");
                Console.WriteLine($"Timestamp Replies: {icmpInfo.icmpInStats.dwTimestampReps}");
                Console.WriteLine($"Address Masks: {icmpInfo.icmpInStats.dwAddrMasks}");
                Console.WriteLine($"Address Mask Replies: {icmpInfo.icmpInStats.dwAddrMaskReps}");

                Console.WriteLine("\nICMP Outgoing Statistics:");
                Console.WriteLine($"Messages: {icmpInfo.icmpOutStats.dwMsgs}");
                Console.WriteLine($"Errors: {icmpInfo.icmpOutStats.dwErrors}");
                Console.WriteLine($"Destination Unreachable: {icmpInfo.icmpOutStats.dwDestUnreachs}");
                Console.WriteLine($"Time Exceeded: {icmpInfo.icmpOutStats.dwTimeExcds}");
                Console.WriteLine($"Parameter Problems: {icmpInfo.icmpOutStats.dwParmProbs}");
                Console.WriteLine($"Source Quench: {icmpInfo.icmpOutStats.dwSrcQuenchs}");
                Console.WriteLine($"Redirects: {icmpInfo.icmpOutStats.dwRedirects}");
                Console.WriteLine($"Echo Requests: {icmpInfo.icmpOutStats.dwEchos}");
                Console.WriteLine($"Echo Replies: {icmpInfo.icmpOutStats.dwEchoReps}");
                Console.WriteLine($"Timestamps: {icmpInfo.icmpOutStats.dwTimestamps}");
                Console.WriteLine($"Timestamp Replies: {icmpInfo.icmpOutStats.dwTimestampReps}");
                Console.WriteLine($"Address Masks: {icmpInfo.icmpOutStats.dwAddrMasks}");
                Console.WriteLine($"Address Mask Replies: {icmpInfo.icmpOutStats.dwAddrMaskReps}");
            }
            else
            {
                // Handle error
                Console.WriteLine($"Failed to retrieve ICMP statistics. Error code: {result}");
            }
        }
    }
}
