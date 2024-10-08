using System;
using System.Net;
using System.Net.Sockets;

namespace Network_Packet_Analyzer.Connections.DNS
{
    public class DNS_Info
    {
        // IP address of the DNS server used for the query
        public IPAddress DnsServerIp { get; private set; }

        // The queried IP address
        public IPAddress QueriedIPAddress { get; private set; }

        // The resolved hostname
        public string HostName { get; private set; }

        // List of IP addresses associated with the hostname
        public IPAddress[] HostAddresses { get; private set; }

        // Response time for the DNS query
        public TimeSpan ResponseTime { get; private set; }

        // Record type (A, AAAA, CNAME, MX, etc.)
        public string RecordType { get; private set; }

        // Time to live (TTL) for the DNS record
        public int TimeToLive { get; private set; }

        // A list of DNS records associated with the hostname
        public DNS_Record[] DnsRecords { get; private set; }

        // A flag indicating if the resolution was successful
        public bool IsResolved { get; private set; }

        // Additional properties
        public string[] Aliases { get; private set; } // List of CNAMEs (aliases)
        public DateTime LastResolvedTime { get; private set; } // Last time the DNS was resolved
        public int QueryCount { get; private set; } // Count of queries made

        // Constructor with optional parameters
        public DNS_Info(string dnsServerIp = "")
        {
            // Convert the DNS server IP string to an IPAddress object using NetHelper
            DnsServerIp = NetHelper.ConvertFromStringToIPAddress(dnsServerIp); // Changed to IPAddress

            // Initialize other properties
            QueriedIPAddress = new IPAddress(0); // The queried IP address, initialized as an empty address
            HostName = string.Empty; // The resolved hostname, initialized as an empty string
            HostAddresses = Array.Empty<IPAddress>(); // Initialize as an empty array
            DnsRecords = Array.Empty<DNS_Record>(); // Initialize as an empty array
            ResponseTime = TimeSpan.Zero; // Initialize response time to zero
            RecordType = string.Empty; // The type of DNS record, initialized as an empty string
            TimeToLive = 0; // TTL initialized to zero
            IsResolved = false; // Initially set to false, as the DNS has not been resolved yet
            Aliases = Array.Empty<string>(); // Initialize aliases
            LastResolvedTime = DateTime.MinValue; // Initialize last resolved time
            QueryCount = 0; // Initialize query count

            // Optional: Automatically resolve the DNS for the server IP
            if (DnsServerIp != null)
            {
                ResolveDnsFromIp(DnsServerIp.ToString());
            }
        }

        // Method to resolve DNS information from an IP address
        public void ResolveDnsFromIp(string ipAddress)
        {
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                var entry = Dns.GetHostEntry(ipAddress);
                watch.Stop();

                HostName = entry.HostName;
                HostAddresses = entry.AddressList;
                ResponseTime = watch.Elapsed;
                IsResolved = true;
                LastResolvedTime = DateTime.Now; // Update last resolved time
                QueryCount++; // Increment query count

                // Example of record details
                RecordType = "A"; // Default record type (you can modify based on your logic)
                TimeToLive = 3600; // Default TTL (you can modify based on your logic)

                // Create DNS record objects (example)
                DnsRecords = new DNS_Record[]
                {
                    new DNS_Record { Type = RecordType, TimeToLive = TimeToLive, Address = NetHelper.ConvertFromStringToIPAddress(ipAddress) }
                };

                // Get aliases (CNAMEs)
                Aliases = entry.Aliases;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                IsResolved = false;
            }
        }

        // Method to resolve DNS information from a hostname
        public void ResolveDnsFromHostName(string hostName)
        {
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                var entry = Dns.GetHostEntry(hostName);
                watch.Stop();

                HostName = entry.HostName;
                HostAddresses = entry.AddressList;
                ResponseTime = watch.Elapsed;
                IsResolved = true;
                LastResolvedTime = DateTime.Now; // Update last resolved time
                QueryCount++; // Increment query count

                // Example of record details
                RecordType = "A"; // Default record type (you can modify based on your logic)
                TimeToLive = 3600; // Default TTL (you can modify based on your logic)

                // Create DNS record objects (example)
                DnsRecords = new DNS_Record[]
                {
                    new DNS_Record { Type = RecordType, TimeToLive = TimeToLive, Address = HostAddresses.Length > 0 ? NetHelper.ConvertFromStringToIPAddress(HostAddresses[0].ToString()) : null }
                };

                // Get aliases (CNAMEs)
                Aliases = entry.Aliases;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                IsResolved = false;
            }
        }

        // Method to get all IP addresses of the hostname
        public string[] GetIpAddresses()
        {
            return Array.ConvertAll(HostAddresses, ip => ip.ToString());
        }
    }

}
