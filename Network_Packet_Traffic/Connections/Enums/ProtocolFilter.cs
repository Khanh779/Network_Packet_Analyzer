using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network_Packet_Traffic.Connections.Enums
{
    /// <summary>
    /// Enum used to define the filtering options for network protocols in the application.
    /// This allows the user to select specific protocols to filter, 
    /// making it easier to manage and analyze network traffic based on protocol type.
    /// </summary>
    [Flags]
    public enum ProtocolFilter
    {
        /// <summary>
        /// Unknown protocol filter.
        /// </summary>
        UNKNOWN = -1,

        /// <summary>
        /// Filter for all protocols, no specific filtering is applied.
        /// </summary>
        All,

        /// <summary>
        /// Filter for TCP (Transmission Control Protocol) packets only.
        /// </summary>
        TCP,

        /// <summary>
        /// Filter for UDP (User Datagram Protocol) packets only.
        /// </summary>
        UDP,

        /// <summary>
        /// Filter for ICMP (Internet Control Message Protocol) packets only.
        /// </summary>
        ICMP,

        /// <summary>
        /// Filter for ARP (Address Resolution Protocol) packets only.
        /// </summary>
        ARP,

        /// <summary>
        /// Filter for DNS (Domain Name System) protocol packets only.
        /// </summary>
        DNS,

        /// <summary>
        /// Filter for DHCP (Dynamic Host Configuration Protocol) packets only.
        /// </summary>
        DHCP,

        /// <summary>
        /// Filter for IPNET (Internet Protocol Network) packets only.
        /// </summary>
        IPNET


    }

}
