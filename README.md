# Network_Packet_Traffic
A network traffic monitoring application that captures and displays packet connections.

## Features
- Analyze and display detailed network packet information.
- Support for TCP, UDP, and ARP protocols.
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
