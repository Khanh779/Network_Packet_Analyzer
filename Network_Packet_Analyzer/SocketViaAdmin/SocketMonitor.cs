using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using static Network_Packet_Analyzer.SocketViaAdmin.SocketMonitor;

namespace Network_Packet_Analyzer.SocketViaAdmin
{
    /// <summary>
    /// Structure representing the IP header of a packet.
    /// </summary>
    public struct IPHeader
    {
        public string IPSourceAddress; // Source IP Address
        public string IPDestinationAddress; // Destination IP Address
        public Protocol Protocol; // Protocol used
        public int SourcePort; // Source Port
        public int DestinationPort; // Destination Port
    }

    /// <summary>
    /// Delegate for the event when a packet is received.
    /// </summary>
    public delegate void OnPacketReceivedEventHandler(object sender, IPHeader ipHeader);


    /// <summary>
    /// Monitors socket activity and captures incoming packets.
    /// </summary>
    public class SocketMonitor
    {
        private Socket socket;
        private bool isListening = false;

        public event OnPacketReceivedEventHandler PacketReceived; // Event triggered on packet receipt

        public SocketMonitor()
        {
            // Get host entry and determine local IP addresses
            var hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            var localAddresses = hostEntry.AddressList.Where((h) => h.AddressFamily == AddressFamily.InterNetwork).ToList();

            // Initialize the socket for raw IP packets
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
            }
            catch
            {
                Console.Write("You need Administrator user privileges to execute this program.");
                return;
            }

            isListening = false;
            socket.Bind(new IPEndPoint(localAddresses[0], 0)); // Bind socket to local address
            socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AcceptConnection, 1);

            // Set socket to receive all packets
            byte[] receiveAll = new byte[] { 1, 0, 0, 0 };
            byte[] outputBuffer = new byte[] { 0, 0, 0, 0 };
            socket.IOControl(IOControlCode.ReceiveAll, receiveAll, outputBuffer); // SIO_RCVALL

        }

        /// <summary>
        /// Gets whether the monitor is currently listening for packets.
        /// </summary>
        public bool IsListening { get { return isListening; } }

        private List<IPHeader> ipHeaderList = new List<IPHeader>();

        /// <summary>
        /// Gets the list of IP headers received.
        /// </summary>
        public IPHeader[] IPHeaders
        {
            get
            {
                return ipHeaderList.ToArray();
            }
        }

        //static void Main(string[] args)
        //{
        //    var socketMonitor = new SocketMonitor();
        //    socketMonitor.PacketReceived += (sender, ipHeader) =>
        //      {
        //          Console.WriteLine($"Source IP: {ipHeader.IPSourceAddress}");
        //          Console.WriteLine($"Destination IP: {ipHeader.IPDestinationAddress}");
        //          Console.WriteLine($"Protocol: {ipHeader.Protocol}");
        //          Console.WriteLine($"Source Port: {ipHeader.SourcePort}");
        //          Console.WriteLine($"Destination Port: {ipHeader.DestinationPort}");
        //          Console.WriteLine("_________________________");
        //      };

        //    socketMonitor.StartListen();
        //    Console.ReadLine();
        //    socketMonitor.StopListen();
        //}

        /// <summary>
        /// Starts listening for incoming packets.
        /// </summary>
        public void StartListen()
        {
            if (!isListening)
            {
                isListening = true;
                ipHeaderList = new List<IPHeader>();
                Task.Run(() =>
                {
                    while (isListening)
                    {
                        byte[] buffer = new byte[4096];

                        IAsyncResult asyncResult = socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, null, null);
                        var length = socket.EndReceive(asyncResult);
                        var sourcePort = (buffer[20] << 8) + buffer[21];
                        var destinationPort = (buffer[22] << 8) + buffer[23];
                        int headerLength = (buffer[0] & 0x0F) * 4; // Header length in bytes
                        var sourcePort1 = buffer[headerLength] * 256 + buffer[headerLength + 1];
                        var destinationPort1 = buffer[headerLength + 2] * 256 + buffer[headerLength + 3];

                        IPHeader ipHeader = new IPHeader
                        {
                            IPSourceAddress = ConvertToIp(buffer, 12),
                            IPDestinationAddress = ConvertToIp(buffer, 16),
                            Protocol = (Protocol)buffer[9],
                            SourcePort = sourcePort1,
                            DestinationPort = destinationPort1
                        };

                        ipHeaderList.Add(ipHeader);
                        PacketReceived?.Invoke(this, ipHeader); // Invoke the event if there are subscribers

                    }
                });
            }
            else
            {
                Console.WriteLine("Already listening.");
            }
        }



        /// <summary>
        /// Stops listening for incoming packets.
        /// </summary>
        public void StopListen()
        {
            if (isListening)
            {
                isListening = false;
                ipHeaderList.Clear(); // Clear the list of headers
                Console.WriteLine("Listening stopped.");
            }
            else
            {
                Console.WriteLine("Not currently listening.");
            }
        }

        /// <summary>
        /// Converts byte array to string representation of an IP address.
        /// </summary>
        /// <param name="buffer">The byte array containing IP address data.</param>
        /// <param name="index">The starting index of the IP address in the byte array.</param>
        /// <returns>A string representation of the IP address.</returns>
        private string ConvertToIp(byte[] buffer, int index)
        {
            return string.Format("{0}.{1}.{2}.{3}", buffer[index], buffer[index + 1], buffer[index + 2], buffer[index + 3]);
        }

        /// <summary>
        /// Gets the protocol name based on the provided byte.
        /// </summary>
        /// <param name="protocolByte">The byte representing the protocol.</param>
        /// <returns>A string representation of the protocol.</returns>
        private string GetProtocolName(byte protocolByte)
        {
            return Enum.GetName(typeof(Protocol), protocolByte) ?? "Unknown";
        }

        #region Protocol

        /// <summary>
        /// Enum representing various protocols.
        /// </summary>
        public enum Protocol
        {
            HOPOPT = 0,
            ICMP = 1,
            IGMP = 2,
            GGP = 3,
            IP_in_IP = 4,
            ST = 5,
            TCP = 6,
            CBT = 7,
            EGP = 8,
            IGP = 9,
            BBN_RCC_MON = 10,
            NVP_II = 11,
            PUP = 12,
            ARGUS = 13,
            EMCON = 14,
            XNET = 15,
            CHAOS = 16,
            UDP = 17,
            MUX = 18,
            DCN_MEAS = 19,
            HMP = 20,
            PRM = 21,
            XNS_IDP = 22,
            TRUNK_1 = 23,
            TRUNK_2 = 24,
            LEAF_1 = 25,
            LEAF_2 = 26,
            RDP = 27,
            IRTP = 28,
            ISO_TP4 = 29,
            NETBLT = 30,
            MFE_NSP = 31,
            MERIT_INP = 32,
            DCCP = 33,
            _3PC = 34,
            IDPR = 35,
            XTP = 36,
            DDP = 37,
            IDPR_CMTP = 38,
            TPPlusPlus = 39,
            IL = 40,
            IPv6 = 41,
            SDRP = 42,
            IPv6_Route = 43,
            IPv6_Frag = 44,
            IDRP = 45,
            RSVP = 46,
            GRE = 47,
            DSR = 48,
            BNA = 49,
            ESP = 50,
            AH = 51,
            I_NLSP = 52,
            SWIPE = 53,
            NARP = 54,
            MOBILE = 55,
            TLSP = 56,
            SKIP = 57,
            IPv6_ICMP = 58,
            IPv6_NoNxt = 59,
            IPv6_Opts = 60,
            AHIP = 61,
            CFTP = 62,
            ALN = 63,
            SAT_EXPAK = 64,
            KRYPTOLAN = 65,
            RVD = 66,
            IPPC = 67,
            ADFS = 68,
            SAT_MON = 69,
            VISA = 70,
            IPCU = 71,
            CPNX = 72,
            CPHB = 73,
            WSN = 74,
            PVP = 75,
            BR_SAT_MON = 76,
            SUN_ND = 77,
            WB_MON = 78,
            WB_EXPAK = 79,
            ISO_IP = 80,
            VMTP = 81,
            SECURE_VMTP = 82,
            VINES = 83,
            TTP = 84,
            IPTM = 84,
            NSFNET_IGP = 85,
            DGP = 86,
            TCF = 87,
            EIGRP = 88,
            OSPF = 89,
            Sprite_RPC = 90,
            LARP = 91,
            MTP = 92,
            AX_25 = 93,
            OS = 94,
            MICP = 95,
            SCC_SP = 96,
            ETHERIP = 97,
            ENCAP = 98,
            APES = 99,
            GMTP = 100,
            IFMP = 101,
            PNNI = 102,
            PIM = 103,
            ARIS = 104,
            SCPS = 105,
            QNX = 106,
            A_N = 107,
            IPComp = 108,
            SNP = 109,
            Compaq_Peer = 110,
            IPX_in_IP = 111,
            VRRP = 112,
            PGM = 113,
            AHOP = 114,
            L2TP = 115,
            DDX = 116,
            IATP = 117,
            STP = 118,
            SRP = 119,
            UTI = 120,
            SMP = 121,
            SM = 122,
            PTP = 123,
            IS_IS_Over_IPv4 = 124,
            FIRE = 125,
            CRTP = 126,
            CRUDP = 127,
            SSCOPMCE = 128,
            IPLT = 129,
            SPS = 130,
            PIPE = 131,
            SCTP = 132,
            FC = 133,
            RSVP_E2E_IGNORE = 134,
            Mobility_Header = 135,
            UDPLite = 136,
            MPLS_in_IP = 137,
            MANET = 138,
            HIP = 139,
            Shim6 = 140,
            WESP = 141,
            ROHC = 142,
            Unknown = -1
        }

        #endregion
    }
}
