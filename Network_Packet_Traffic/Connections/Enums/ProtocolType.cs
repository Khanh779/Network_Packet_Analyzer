namespace Network_Packet_Traffic.Connections.Enums
{
    public enum ProtocolType
    {
        /// <summary>
        /// Unknown protocol filter.
        /// </summary>
        UNKNOWN = -1,

        /// <summary>
        /// Transmission Control Protocol (TCP) - A connection-oriented protocol 
        /// that ensures reliable, ordered, and error-checked delivery of data over the network.
        /// </summary>
        TCP,

        /// <summary>
        /// User Datagram Protocol (UDP) - A connectionless protocol 
        /// that sends packets without ensuring reliability or order, but offers faster transmission.
        /// </summary>
        UDP,

        /// <summary>
        /// Internet Control Message Protocol (ICMP) - A protocol used for error messages 
        /// and operational information, such as ping requests.
        /// </summary>
        ICMP,

        /// <summary>
        /// Address Resolution Protocol (ARP) - A protocol for mapping IP network addresses 
        /// to physical MAC addresses on local networks.
        /// </summary>
        ARP,

        /// <summary>
        /// IP Network (IPNET) - Represents general network-level IP communications.
        /// </summary>
        IPNET,

        /// <summary>
        /// Dynamic Host Configuration Protocol (DHCP) - A protocol used for automatically assigning 
        /// IP addresses to devices in a network.
        /// </summary>
        DHCP,

        /// <summary>
        /// Domain Name System (DNS) - A protocol used to resolve human-readable domain names 
        /// into IP addresses.
        /// </summary>
        DNS,

        /// <summary>
        /// Unknown protocol - Used for any unrecognized or unsupported protocol.
        /// </summary>
        Unknown = -1
    }

}
