using Network_Packet_Traffic.Connections.Enums;
using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Network_Packet_Traffic.Connections
{
    public static class NetHelper
    {

        public const byte NO_ERROR = 0;
        public const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x00000100;
        public const int FORMAT_MESSAGE_IGNORE_INSERTS = 0x00000200;
        public const int FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000;

        public static int dwFlags = FORMAT_MESSAGE_ALLOCATE_BUFFER |
            FORMAT_MESSAGE_FROM_SYSTEM |
            FORMAT_MESSAGE_IGNORE_INSERTS;


        public const byte MIB_TCP_RTO_CONSTANT = 2;
        public const byte MIB_TCP_RTO_OTHER = 1;
        public const byte MIB_TCP_RTO_RSRE = 3;
        public const byte MIB_TCP_RTO_VANJ = 4;

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetProcessHeap();


        [DllImport("kernel32.dll")]
        public static extern int FormatMessage(int flags, IntPtr source, int messageId, int languageId, StringBuilder buffer, int size, IntPtr arguments);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        static extern int SendARP(int DestIP, int SrcIP, byte[] pMacAddr, ref uint PhyAddrLen);

        [DllImport("Ws2_32.dll")]
        public static extern Int32 inet_addr(string ip);

        public static string GetMacAddress(string ipAddress)
        {
            try
            {
                int dest = inet_addr(ipAddress);
                byte[] macAddr = new byte[6];
                uint macAddrLen = (uint)macAddr.Length;

                // Gọi API SendARP để lấy địa chỉ MAC
                int result = SendARP(dest, 0, macAddr, ref macAddrLen);

                if (result != 0)
                {
                    MessageBox.Show("Error: Unable to retrieve ARP info");
                    return null;
                }

                StringBuilder macAddressString = new StringBuilder();
                for (int i = 0; i < macAddrLen; i++)
                {
                    if (i > 0)
                        macAddressString.Append(":");
                    macAddressString.AppendFormat("{0:X2}", macAddr[i]);
                }

                return macAddressString.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
        }

        public static string GetAPIErrorMessageDescription(int ApiErrNumber)
        {
            StringBuilder sError = new StringBuilder(512);
            int lErrorMessageLength;
            lErrorMessageLength = FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM, IntPtr.Zero, ApiErrNumber, 0, sError, sError.Capacity, IntPtr.Zero);

            if (lErrorMessageLength > 0)
            {
                string strgError = sError.ToString();
                strgError = strgError.Substring(0, strgError.Length - 2);
                return strgError + " (" + ApiErrNumber.ToString() + ")";
            }
            return "none";
        }

        public static StateType GetState(int numState)
        {
            // Nếu tồn tại số uint strong trong enum StateType thì trả về giá trị tương ứng, ngược lại, trả về Unknown
            if (Enum.IsDefined(typeof(StateType), numState))
                return (StateType)numState;
            else
                return StateType.UNKNOWN;
        }

        public static IPAddress ConvertIpAddress(int ipAddress)
        {

            byte[] ipBytes = BitConverter.GetBytes(ipAddress);
            Array.Reverse(ipBytes);
            return new IPAddress(ipBytes);
        }
    }
}
