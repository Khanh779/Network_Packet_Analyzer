using Network_Packet_Analyzer.Connections.Enums;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Network_Packet_Analyzer.Connections
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

   

        /// <summary>
        /// Retrieves the error message description for a specified API error number.
        /// </summary>
        /// <param name="ApiErrNumber">The API error number for which to retrieve the description.</param>
        /// <returns>
        /// A string containing the error message description, including the error number, 
        /// or "none" if the message could not be retrieved.
        /// </returns>
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

        /// <summary>
        /// Retrieves the state type corresponding to a numeric state value.
        /// </summary>
        /// <param name="numState">The numeric state value to convert.</param>
        /// <returns>
        /// A <see cref="StateType"/> representing the state, or 
        /// <see cref="StateType.UNKNOWN"/> if the value is not defined.
        /// </returns>
        public static StateType GetState(int numState)
        {
            // If the number is a valid StateType, return it
            if (Enum.IsDefined(typeof(StateType), numState))
                return (StateType)numState;
            else
                return StateType.UNKNOWN;
        }


        /// <summary>
        /// Retrieves the name of the process based on its process ID.
        /// </summary>
        /// <param name="processId">The ID of the process to retrieve the name for.</param>
        /// <returns>
        /// A string containing the name of the process if found; 
        /// otherwise, returns "Unknown" in case of an exception.
        /// </returns>
        public static string GetProcessName(int processId)
        {
            try
            {
                // Get the process using the provided process ID
                System.Diagnostics.Process process = System.Diagnostics.Process.GetProcessById(processId);
                return process.ProcessName; // Return the process name
            }
            catch (Exception)
            {
                // In case of any exceptions (e.g., process not found), return "Unknown"
                return "Unknown";
            }
        }

        /// <summary>
        /// Retrieves the <see cref="Process"/> object corresponding to the specified process ID.
        /// </summary>
        /// <param name="processId">The ID of the process to retrieve.</param>
        /// <returns>
        /// A <see cref="Process"/> object representing the specified process; 
        /// or null if the process could not be found or an error occurred.
        /// </returns>
        public static Process GetProcessInformation(int processId)
        {
            try
            {
                // Get the process using the provided process ID
                Process process = Process.GetProcessById(processId);
                return process; // Return the Process object
            }
            catch (ArgumentException)
            {
                // Process with the specified ID does not exist
                Console.WriteLine($"Error: No process with ID {processId} exists.");
            }
            catch (Exception ex)
            {
                // Handle other potential exceptions
                Console.WriteLine($"Error: {ex.Message}");
            }

            return null; // Return null if the process could not be found
        }


        /// <summary>
        /// Converts an integer representation of an IP address to an <see cref="IPAddress"/> object.
        /// </summary>
        /// <param name="ipAddress">The integer representation of the IP address.</param>
        /// <returns>
        /// An <see cref="IPAddress"/> object corresponding to the provided integer IP address.
        /// </returns>
        public static IPAddress ConvertIpAddress(int ipAddress)
        {
            // Convert the integer IP address to a byte array
            byte[] ipBytes = BitConverter.GetBytes(ipAddress);

            // Reverse the byte array to match the correct IP address format (little-endian to big-endian)
            Array.Reverse(ipBytes);

            // Create and return an IPAddress object using the byte array
            return new IPAddress(ipBytes);
        }

        /// <summary>
        /// Get the IP address from a string.
        /// </summary>
        /// <param name="ipAddress">The IP address in string format.</param>
        /// <returns>An IPAddress object if the conversion is successful; otherwise, null.</returns>
        /// <exception cref="FormatException">Thrown when the string is not a valid IP address format.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the input string is null.</exception>
        public static IPAddress ConvertFromStringToIPAddress(string ipAddress)
        {
            // Check if the input string is null or empty
            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                throw new ArgumentNullException(nameof(ipAddress), "IP address string cannot be null or empty.");
            }

            try
            {
                // Convert the string IP address to an IPAddress object
                return IPAddress.Parse(ipAddress);
            }
            catch (FormatException)
            {
                // Handle format exception for invalid IP address format
                Console.WriteLine($"Error: '{ipAddress}' is not a valid IP address format.");
                return null; // Optionally, return null or throw the exception based on your preference
            }
        }

        /// <summary>
        /// Converts a integer representation of an MAC address to a string.
        /// </summary>
        public static string ConvertMacAddress(byte[] macAddress)
        {
            // Create a PhysicalAddress from the byte array
            PhysicalAddress physicalAddress = new PhysicalAddress(macAddress);

            // Convert the PhysicalAddress to a string in the format "XX-XX-XX-XX-XX-XX"
            string macAddressString = BitConverter.ToString(physicalAddress.GetAddressBytes()).Replace("-", ":");
            return macAddressString;
        }


        /// <summary>
        /// Retrieves the MAC address associated with a given IP address.
        /// </summary>
        /// <param name="ipAddress">The IP address for which to retrieve the MAC address.</param>
        /// <returns>
        /// A string representing the MAC address in hexadecimal format, or 
        /// null if the MAC address could not be retrieved.
        /// </returns>
        public static string GetMacAddress(string ipAddress)
        {
            string macAddress = string.Empty;
            try
            {
                int remoteMachine = BitConverter.ToInt32(System.Net.IPAddress.Parse(ipAddress).GetAddressBytes(), 0);
                byte[] macAddr = new byte[6];
                uint macAddrLen = (uint)macAddr.Length;
                if (SendARP(remoteMachine, 0, macAddr, ref macAddrLen) == 0)
                {
                    string[] str = new string[(int)macAddrLen];
                    for (int i = 0; i < macAddrLen; i++)
                        str[i] = macAddr[i].ToString("x2");
                    macAddress = string.Join(":", str);
                }
            }
            catch (Exception ex)
            {
                macAddress = "Error: " + ex.Message;
            }
            return macAddress;
        }

       
        //public static string GetMacAddress(string ipAddress)
        //{
        //    try
        //    {
        //        int dest = inet_addr(ipAddress);
        //        byte[] macAddr = new byte[6];
        //        uint macAddrLen = (uint)macAddr.Length;

        //        // Gọi API SendARP để lấy địa chỉ MAC
        //        int result = SendARP(dest, 0, macAddr, ref macAddrLen);

        //        if (result != 0)
        //        {
        //            MessageBox.Show("Error: Unable to retrieve ARP info");
        //            return null;
        //        }

        //        StringBuilder macAddressString = new StringBuilder();
        //        for (int i = 0; i < macAddrLen; i++)
        //        {
        //            if (i > 0)
        //                macAddressString.Append(":");
        //            macAddressString.AppendFormat("{0:X2}", macAddr[i]);
        //        }

        //        return macAddressString.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message);
        //        return null;
        //    }
        //}
    }
}
