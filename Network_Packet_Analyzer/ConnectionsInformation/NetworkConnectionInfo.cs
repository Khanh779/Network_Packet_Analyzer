using System.Net;
using System.Net.NetworkInformation;

namespace Network_Packet_Analyzer.ConnectionsInformation
{
    /// <summary>
    /// This class retrieves various network connection statistics and properties.
    /// </summary>
    public class NetworkConnectionInfo
    {
        private IPGlobalProperties _networkProperties;
        private IPGlobalStatistics _ipv4Statistics;
        private IPGlobalStatistics _ipv6Statistics;

        private NetworkInterface[] _networkInterfaces;
        //private IPInterfaceStatistics _interfaceStatistics;

        private TcpStatistics _tcpIPv4Statistics;
        private TcpStatistics _tcpIPv6Statistics;
        private UdpStatistics _udpIPv4Statistics;
        private UdpStatistics _udpIPv6Statistics;

        private IcmpV4Statistics _icmpV4Statistics;
        private IcmpV6Statistics _icmpV6Statistics;

        private TcpConnectionInformation[] _tcpConnections;

        public NetworkConnectionInfo()
        {
            _networkProperties = IPGlobalProperties.GetIPGlobalProperties();

            _ipv4Statistics = _networkProperties.GetIPv4GlobalStatistics();
            _ipv6Statistics = _networkProperties.GetIPv6GlobalStatistics();

            _tcpIPv4Statistics = _networkProperties.GetTcpIPv4Statistics();
            _tcpIPv6Statistics = _networkProperties.GetTcpIPv6Statistics();
            _tcpConnections = _networkProperties.GetActiveTcpConnections();
            _udpIPv4Statistics = _networkProperties.GetUdpIPv4Statistics();
            _udpIPv6Statistics = _networkProperties.GetUdpIPv6Statistics();

            _icmpV4Statistics = _networkProperties.GetIcmpV4Statistics();
            _icmpV6Statistics = _networkProperties.GetIcmpV6Statistics();

            _networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
        }

        /// <summary>
        /// Gets the active TCP listeners.
        /// </summary>
        /// <returns>An array of IPEndPoints representing active TCP listeners.</returns>
        public IPEndPoint[] GetActiveTcpListeners()
        {
            return _networkProperties.GetActiveTcpListeners();
        }

        /// <summary>
        /// Gets the active UDP listeners.
        /// </summary>
        /// <returns>An array of IPEndPoints representing active UDP listeners.</returns>
        public IPEndPoint[] GetActiveUdpListeners()
        {
            return _networkProperties.GetActiveUdpListeners();
        }

        /// <summary>
        /// Gets the active TCP connections.
        /// </summary>
        /// <returns>An array of TcpConnectionInformation representing active TCP connections.</returns>
        public TcpConnectionInformation[] GetTcpConnections()
        {
            return _tcpConnections;
        }

        /// <summary>
        /// Gets the IP interface statistics for a specified network interface index.
        /// </summary>
        /// <param name="index">The index of the network interface.</param>
        /// <returns>IPInterfaceStatistics for the specified network interface.</returns>
        public IPInterfaceStatistics GetIPInterfaceStatistics(int index)
        {
            return _networkInterfaces[index].GetIPStatistics();
        }

        /// <summary>
        /// Gets the IP interface properties for a specified network interface index.
        /// </summary>
        /// <param name="index">The index of the network interface.</param>
        /// <returns>IPInterfaceProperties for the specified network interface.</returns>
        public IPInterfaceProperties GetIPInterfaceProperties(int index)
        {
            return _networkInterfaces[index].GetIPProperties();
        }

        /// <summary>
        /// Gets the IPv4 interface statistics for a specified network interface index.
        /// </summary>
        /// <param name="index">The index of the network interface.</param>
        /// <returns>IPv4InterfaceStatistics for the specified network interface.</returns>
        public IPv4InterfaceStatistics GetIPv4InterfaceStatistics(int index)
        {
            return _networkInterfaces[index].GetIPv4Statistics();
        }

        /// <summary>
        /// Gets the TCP IPv4 statistics.
        /// </summary>
        /// <returns>TcpStatistics for IPv4.</returns>
        public TcpStatistics GetTcpIPv4Statistics()
        {
            return _tcpIPv4Statistics;
        }

        /// <summary>
        /// Gets the TCP IPv6 statistics.
        /// </summary>
        /// <returns>TcpStatistics for IPv6.</returns>        
        public TcpStatistics GetTcpIPv6Statistics()
        {
            return _tcpIPv6Statistics;
        }

        /// <summary>
        /// Gets the UDP IPv4 statistics.
        /// </summary>
        /// <returns>UdpStatistics for IPv4.</returns>
        public UdpStatistics GetUdpIPv4Statistics()
        {
            return _udpIPv4Statistics;
        }

        /// <summary>
        /// Gets the UDP IPv6 statistics.
        /// </summary>
        /// <returns>UdpStatistics for IPv6.</returns>
        public UdpStatistics GetUdpIPv6Statistics()
        {
            return _udpIPv6Statistics;
        }

        /// <summary>
        /// Gets the IPv4 global statistics.
        /// </summary>
        /// <returns>IPGlobalStatistics for IPv4.</returns>
        public IPGlobalStatistics GetIPv4Statistics()
        {
            return _ipv4Statistics;
        }

        /// <summary>
        /// Gets the IPv6 global statistics.
        /// </summary>
        /// <returns>IPGlobalStatistics for IPv6.</returns>
        public IPGlobalStatistics GetIPv6Statistics()
        {
            return _ipv6Statistics;
        }

        /// <summary>
        /// Gets the ICMP IPv4 statistics.
        /// </summary>
        /// <returns>IcmpV4Statistics.</returns>
        public IcmpV4Statistics GetIcmpV4Statistics()
        {
            return _icmpV4Statistics;
        }

        /// <summary>
        /// Gets the ICMP IPv6 statistics.
        /// </summary>
        /// <returns>IcmpV6Statistics.</returns>
        public IcmpV6Statistics GetIcmpV6Statistics()
        {
            return _icmpV6Statistics;
        }
    }
}
