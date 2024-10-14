using System;

namespace Network_Packet_Analyzer.Connections.Enums
{
    [Flags]
    public enum ProtocolType
    {
        /// <summary>
        /// Unknown protocol filter.
        /// </summary>
        UNKNOWN = -1,

        /// <summary>
        /// All protocols are selected.
        /// </summary>
        All = TCP | UDP | ICMP | ARP | IPNET | DHCP | DNS,

        /// <summary>
        /// Transmission Control Protocol (TCP) - A connection-oriented protocol 
        /// that ensures reliable, ordered, and error-checked delivery of data over the network.
        /// </summary>
        TCP = 1,

        /// <summary>
        /// User Datagram Protocol (UDP) - A connectionless protocol 
        /// that sends packets without ensuring reliability or order, but offers faster transmission.
        /// </summary>
        UDP = 2,

        /// <summary>
        /// Internet Control Message Protocol (ICMP) - A protocol used for error messages 
        /// and operational information, such as ping requests.
        /// </summary>
        ICMP = 4,

        /// <summary>
        /// Address Resolution Protocol (ARP) - A protocol for mapping IP network addresses 
        /// to physical MAC addresses on local networks.
        /// </summary>
        ARP = 8,

        /// <summary>
        /// IP Network (IPNET) - Represents general network-level IP communications.
        /// </summary>
        IPNET = 16,

        /// <summary>
        /// Dynamic Host Configuration Protocol (DHCP) - A protocol used for automatically assigning 
        /// IP addresses to devices in a network.
        /// </summary>
        DHCP = 32,

        /// <summary>
        /// Domain Name System (DNS) - A protocol used to resolve human-readable domain names 
        /// into IP addresses.
        /// </summary>
        DNS = 64,
    }


}
