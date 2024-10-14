# Network_Packet_Analyzer
The "Network Packet Analyzer" project is a network traffic monitoring and packet analysis tool that captures, analyzes, and displays information about data packets transmitted over the network.

## Overview
![image](https://github.com/Khanh779/Network_Packet_Analyzer/blob/master/ScreenShot/Screenshot_0.png)
![image](https://github.com/Khanh779/Network_Packet_Analyzer/blob/master/ScreenShot/Screenshot_1.png)

## Features
- Port Scanner
- Ping Sniffer
- Analyze and display detailed network packet information.
- Support for TCP, UDP, ARP, IP, ICMP, DHCP, DNS protocols.
- Real-time monitoring of active and closed connections.
- Events for new and ended connections.

## Installation
- .NET Framework 4.7.2 or higher is required.
- Run the application with admin privileges for full access.
- Use Visual Studio to build and run the project.

## Usage
- Use the `ConnectionsMonitor` class to listen for network packet changes.
- The tool automatically retrieves packet information and tracks ongoing traffic.

## Events
- `OnNewPacketConnectionStarted`: Triggered when a new connection starts.
- `OnNewPacketsConnectionLoad`: Lists all connections at once.
- `OnNewPacketConnectionEnded`: Triggered when a connection ends.

## Contribution
Contributions are welcome! Create an Issue or Pull Request with your ideas.

[![Buy Me a Coffee](https://img.shields.io/badge/Buy%20Me%20a%20Coffee-FFDD00?style=for-the-badge&logo=buy-me-a-coffee&logoColor=black)](https://buymeacoffee.com/du122oo)

[![PayPal](https://img.shields.io/badge/PayPal-00457C?style=for-the-badge&logo=paypal&logoColor=white)](https://paypal.me/Khanhtran283)
