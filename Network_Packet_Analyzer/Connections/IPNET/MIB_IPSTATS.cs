using System.Runtime.InteropServices;

namespace Network_Packet_Analyzer.Connections.IPNET
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_IPSTATS
    {
        public uint dwForwarding;
        public uint dwDefaultTTL;
        public uint dwInReceives;
        public uint dwInHdrErrors;
        public uint dwInAddrErrors;
        public uint dwForwDatagrams;
        public uint dwInUnknownProtos;
        public uint dwInDiscards;
        public uint dwInDelivers;
        public uint dwOutRequests;
        public uint dwRoutingDiscards;
        public uint dwOutDiscards;
        public uint dwOutNoRoutes;
        public uint dwReasmTimeout;
        public uint dwReasmReqds;
        public uint dwReasmOks;
        public uint dwReasmFails;
        public uint dwFragOks;
        public uint dwFragFails;
        public uint dwFragCreates;
        public uint dwNumIf;
        public uint dwNumAddr;
        public uint dwNumRoutes;
    }
}
