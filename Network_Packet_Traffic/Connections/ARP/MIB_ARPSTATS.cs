namespace Network_Packet_Traffic.Connections.ARP
{
    public struct MIB_ARPSTATS
    {
        public ForwardingStatus Forwarding { get; set; }
        public int DefaultTTL { get; set; }
        public int InReceives { get; set; }
        public int InHeaderErrors { get; set; }
        public int InAddressErrors { get; set; }
        public int ForwardedDatagrams { get; set; }
        public int InUnknownProtocols { get; set; }
        public int InDiscards { get; set; }
        public int InDelivers { get; set; }
        public RequestsStatus OutRequests { get; set; }
        public int RoutingDiscards { get; set; }
        public int OutDiscards { get; set; }
        public int OutNoRoutes { get; set; }
        public int ReassemblyTimeout { get; set; }
        public RequestsStatus ReassemblyRequests { get; set; }
        public int ReassemblyOks { get; set; }
        public int ReassemblyFails { get; set; }
        public int FragmentOks { get; set; }
        public int FragmentFails { get; set; }
        public int FragmentCreates { get; set; }
        public int NumberOfInterfaces { get; set; }
        public int NumberOfAddresses { get; set; }
        public int NumberOfRoutes { get; set; }
    }
}
