namespace Network_Packet_Traffic.Connections.ARP
{
    public struct MIB_ARPTABLE
    {
        public int NumberOfEntries { get; set; }
        public ARP.MIB_ARPROW[] ARPConnections { get; set; }


    }
}
