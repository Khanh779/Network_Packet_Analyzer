namespace Network_Packet_Analyzer.Connections.ARP
{
    public struct MIB_ARPSTATS
    {
        public ForwardingStatus Forwarding { get; set; }
        public uint DefaultTTL { get; set; }
        public uint InReceives { get; set; }
        public uint InHeaderErrors { get; set; }
        public uint InAddressErrors { get; set; }
        public uint ForwardedDatagrams { get; set; }
        public uint InUnknownProtocols { get; set; }
        public uint InDiscards { get; set; }
        public uint InDelivers { get; set; }
        public RequestsStatus OutRequests { get; set; }
        public uint RoutingDiscards { get; set; }
        public uint OutDiscards { get; set; }
        public uint OutNoRoutes { get; set; }
        public uint NumberOfInterfaces { get; set; }
        public uint ReassemblyTimeout { get; set; }
        public RequestsStatus ReassemblyRequests { get; set; }
        public uint ReassemblyOks { get; set; }
        public uint ReassemblyFails { get; set; }
        public uint FragmentOks { get; set; }
        public uint FragmentFails { get; set; }
        public uint FragmentCreates { get; set; }
        public uint NumberOfAddresses { get; set; }
        public uint NumberOfRoutes { get; set; }
    }
}
