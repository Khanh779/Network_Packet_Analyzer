using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network_Packet_Analyzer.Connections.ARP
{
    public enum MIB_ARP_TYPE
    {
        Other = 1, //MIB_IPNET_TYPE_OTHER
        Invalid = 2, //MIB_IPNET_TYPE_INVALID
        Dynamic = 3, //MIB_IPNET_TYPE_DYNAMIC
        Static = 4 //MIB_IPNET_TYPE_STATIC
    }
}
