using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Network_Packet_Analyzer.Connections.DNS
{
    /// <summary>
    /// Class to store information about DNS records
    /// </summary>
    public class DNS_Record
    {
        // Type of the DNS record (A, AAAA, CNAME, MX, etc.)
        public string Type { get; set; }

        // Time to live (TTL) of the DNS record
        public int TimeToLive { get; set; }

        // Address associated with the DNS record
        public IPAddress Address { get; set; }

        // Additional data (e.g., priority for MX records)
        public int? Priority { get; set; }

        // TTL expiration date
        public DateTime Expiration { get; set; }
    }
}
