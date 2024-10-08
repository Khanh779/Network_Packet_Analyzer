using System.Runtime.InteropServices;

namespace Network_Packet_Analyzer.Connections.ICMP
{
    /// <summary>
    /// Represents a single entry in an ICMP table.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ICMPEntry
    {
        public int Address;            // The IP address of the host.
        public uint ReceiveCount;       // Number of ICMP messages received from this address.
        public uint SendCount;          // Number of ICMP messages sent to this address.
        public int Errors;             // Number of errors encountered.
        public uint TimeStamp;          // Last time a packet was received or sent.
    }
}
