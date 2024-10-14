using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Network_Packet_Analyzer.Connections.DHCP
{
    public class DHCP_Info
    {
        /// <summary>
        /// The IP address of the DHCP server
        /// </summary>
        public IPAddress DhcpServerIp { get; private set; }

        /// <summary>
        /// The IP address assigned by the DHCP server
        /// </summary>
        public IPAddress AssignedIPAddress { get; private set; }

        /// <summary>
        /// The MAC address of the device
        /// </summary>
        public string MacAddress { get; private set; }

        /// <summary>
        /// Lease time for the assigned IP address
        /// </summary>
        public TimeSpan LeaseTime { get; private set; }

        /// <summary>
        /// The start time of the lease
        /// </summary>
        public DateTime LeaseStartTime { get; private set; }

        /// <summary>
        /// A flag indicating if the DHCP lease is valid
        /// </summary>
        public bool IsLeaseValid { get; private set; }

        /// <summary>
        /// Constructor with optional parameters
        /// </summary>
        /// <param name="dhcpServerIp"></param>
        public DHCP_Info(string dhcpServerIp = "")
        {
            // Convert the DHCP server IP string to an IPAddress object using NetHelper
            DhcpServerIp = NetHelper.ConvertFromStringToIPAddress(dhcpServerIp);

            // Initialize other properties
            AssignedIPAddress = IPAddress.None; // No IP assigned initially
            MacAddress = string.Empty; // Initialize MAC address as empty
            LeaseTime = TimeSpan.Zero; // Lease time initialized to zero
            LeaseStartTime = DateTime.MinValue; // Lease start time initialized to the minimum value
            IsLeaseValid = false; // Initially set to false
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="macAddress"></param>
        public void RequestLease(string macAddress)
        {
            try
            {
                // Simulate a DHCP request (this is a placeholder for actual DHCP client logic)
                MacAddress = macAddress;
                AssignedIPAddress = RequestIpFromDhcpServer(DhcpServerIp, MacAddress);

                // Simulate lease time (e.g., 24 hours)
                LeaseTime = TimeSpan.FromHours(24);
                LeaseStartTime = DateTime.Now; // Set lease start time to now
                IsLeaseValid = true; // Mark lease as valid
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error requesting DHCP lease: {ex.Message}");
                IsLeaseValid = false; // Mark lease as invalid if there's an error
            }
        }

        // Simulated method to request an IP address from the DHCP server
        private IPAddress RequestIpFromDhcpServer(IPAddress dhcpServerIp, string macAddress)
        {
            // Simulate DHCP server logic (for demonstration purposes)
            // In a real scenario, this would involve network programming to send a DHCPDISCOVER message.
            Console.WriteLine($"Requesting IP address from DHCP server {dhcpServerIp} for MAC {macAddress}...");

            // Return a dummy IP address for demonstration
            return IPAddress.Parse("192.168.1.100");
        }

        // Method to release the DHCP lease
        public void ReleaseLease()
        {
            if (IsLeaseValid)
            {
                // Simulate lease release (this is a placeholder for actual DHCP client logic)
                Console.WriteLine($"Releasing lease for IP {AssignedIPAddress}...");
                AssignedIPAddress = IPAddress.None; // Clear the assigned IP
                IsLeaseValid = false; // Mark lease as invalid
            }
            else
            {
                Console.WriteLine("No valid lease to release.");
            }
        }

        // Method to renew the DHCP lease
        public void RenewLease()
        {
            if (IsLeaseValid)
            {
                // Simulate lease renewal (this is a placeholder for actual DHCP client logic)
                Console.WriteLine($"Renewing lease for IP {AssignedIPAddress}...");

                // Simulate lease time extension
                LeaseTime += TimeSpan.FromHours(24); // Extend lease by another 24 hours
                LeaseStartTime = DateTime.Now; // Update lease start time
            }
            else
            {
                Console.WriteLine("No valid lease to renew.");
            }
        }
    }
}
