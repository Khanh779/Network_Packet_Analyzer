using System.Runtime.InteropServices;

namespace Network_Packet_Traffic.Connections.IPNET
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_IPSTATS
    {
        public int dwForwarding;
        public int dwDefaultTTL;
        public int dwInReceives;
        public int dwInHdrErrors;
        public int dwInAddrErrors;
        public int dwForwDatagrams;
        public int dwInUnknownProtos;
        public int dwInDiscards;
        public int dwInDelivers;
        public int dwOutRequests;
        public int dwRoutingDiscards;
        public int dwOutDiscards;
        public int dwOutNoRoutes;
        public int dwReasmTimeout;
        public int dwReasmReqds;
        public int dwReasmOks;
        public int dwReasmFails;
        public int dwFragOks;
        public int dwFragFails;
        public int dwFragCreates;
        public int dwNumIf;
        public int dwNumAddr;
        public int dwNumRoutes;
    }
}
