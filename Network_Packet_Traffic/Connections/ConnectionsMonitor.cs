using Network_Packet_Traffic.Connections.ARP;
using Network_Packet_Traffic.Connections.Enums;
using Network_Packet_Traffic.Connections.IPNET;
using Network_Packet_Traffic.Connections.Struct;
using Network_Packet_Traffic.Connections.TCP;
using Network_Packet_Traffic.Connections.UDP;
using System.Linq;
using System.Threading;
using static Network_Packet_Traffic.Connections.NetHelper;
namespace Network_Packet_Traffic.Connections
{
    public delegate void OnNewPacketsConnectionLoad(object ob, PacketConnectionInfo[] packet); // Liệt kê tất cả các gói
    public delegate void OnNewPacketConnectionStarted(object ob, PacketConnectionInfo packet); // Liệt kê các gói mới đã bắt đầu kết nối
    public delegate void OnNewPacketConnectionEnded(object ob, PacketConnectionInfo packet); // Liệt kê các gói mới đã kết thúc kết nối

    public class ConnectionsMonitor
    {
        public event OnNewPacketConnectionStarted OnNewPacketConnectionStarted;
        public event OnNewPacketsConnectionLoad OnNewPacketsConnectionLoad;
        public event OnNewPacketConnectionEnded OnNewPacketConnectionEnded;
        PacketConnectionInfo[] _oldPackets = new PacketConnectionInfo[0];
        Thread task = null;

        public ConnectionsMonitor(bool autoReload = true)
        {
            task = new Thread(GetAllPacket);
            IsAutoReload = autoReload;
            task.IsBackground = true;
        }

        public bool IsAutoReload { get; set; } = true;

        public void StartListen()
        {
            // Gọi các phương thức để lấy thông tin và xử lý nó ở đây
            if (task != null && task.ThreadState != ThreadState.Running)
                task.Start();
        }

        public bool IsRunning => task.ThreadState == ThreadState.Running;

        public void StopListen()
        {
            // Đối với các tác vụ dừng lắng nghe, bạn có thể thực hiện các bước tương ứng ở đây
            if (task.ThreadState == ThreadState.Running)
                task.Abort();
        }

        #region Connection Table

        void GetAllPacket()
        {
            do
            {
                MIB_TCPTABLE_OWNER_PID tcpTable = TCP.TCP_Info.GetTcpTable();
                MIB_UDPTABLE_OWNER_PID udpTable = UDP.UDP_Info.GetUdpTable();
                MIB_IPNETTABLE mIB_IPNETTABLE = IPNET.IPNET_Info.GetIpNetable();
                var arpTable = ARP.ARP_Info.GetARPTable();


                PacketConnectionInfo[] packetConnectionInfos = new PacketConnectionInfo[tcpTable.dwNumEntries + udpTable.dwNumEntries + mIB_IPNETTABLE.dwNumEntries + arpTable.NumberOfEntries];
                for (int i = 0; i < tcpTable.dwNumEntries; i++)
                {
                    MIB_TCPROW_OWNER_PID tcpRow = tcpTable.table[i];
                    PacketConnectionInfo packet = new PacketConnectionInfo();
                    packet.LocalAddress = ConvertIpAddress((int)tcpRow.dwLocalAddr).ToString();
                    packet.LocalPort = tcpRow.dwLocalPort;
                    packet.RemoteAddress = ConvertIpAddress((int)tcpRow.dwRemoteAddr).ToString();
                    packet.RemotePort = tcpRow.dwRemotePort;
                    packet.ProcessId = tcpRow.dwOwningPid;
                    packet.Protocol = ProtocolType.TCP;
                    packet.State = (GetState((int)tcpRow.dwState)).ToString();
                    packetConnectionInfos[i] = packet;
                }


                for (int i = 0; i < udpTable.dwNumEntries; i++)
                {
                    MIB_UDPROW_OWNER_PID udpRow = udpTable.table[i];
                    PacketConnectionInfo packet = new PacketConnectionInfo();
                    packet.LocalAddress = ConvertIpAddress((int)udpRow.dwLocalAddr).ToString();
                    packet.LocalPort = udpRow.dwLocalPort;
                    packet.RemoteAddress = ConvertIpAddress((int)udpRow.dwRemoteAddr).ToString();
                    packet.RemotePort = udpRow.dwRemotePort;
                    packet.State = GetState(12).ToString();
                    packet.ProcessId = udpRow.dwOwningPid;
                    packet.Protocol = ProtocolType.UDP;
                    packetConnectionInfos[i + tcpTable.dwNumEntries] = packet;

                }

                for (int i = 0; i < mIB_IPNETTABLE.dwNumEntries; i++)
                {
                    MIB_IPNETROW arpRow = mIB_IPNETTABLE.table[i];
                    PacketConnectionInfo packet = new PacketConnectionInfo();
                    packet.LocalAddress = ConvertIpAddress(arpRow.dwAddr).ToString();
                    packet.LocalPort = 0;
                    packet.RemoteAddress = ConvertIpAddress(arpRow.dwPhysAddrLen).ToString();
                    packet.RemotePort = 0;
                    packet.State = GetState(12).ToString();
                    packet.ProcessId = 0;
                    packet.Protocol = ProtocolType.ARP;
                    packetConnectionInfos[i + tcpTable.dwNumEntries + udpTable.dwNumEntries] = packet;
                }

                for (int i = 0; i < arpTable.NumberOfEntries; i++)
                {
                    var d = arpTable.ARPConnections[i];
                    MIB_ARPROW arpRow = d;
                    PacketConnectionInfo packet = new PacketConnectionInfo();
                    packet.LocalAddress = arpRow.Address.ToString();
                    packet.LocalPort = 0;
                    packet.MacAddress = arpRow.PhysicalAddress.ToString();
                    packet.RemoteAddress = "";
                    packet.RemotePort = 0;
                    packet.State = GetState(-1).ToString();
                    packet.ProcessId = 0;
                    packet.Protocol = ProtocolType.ARP;
                    packetConnectionInfos[i + tcpTable.dwNumEntries + udpTable.dwNumEntries + mIB_IPNETTABLE.dwNumEntries] = packet;
                }

                OnNewPacketsConnectionLoad?.Invoke(this, packetConnectionInfos);

                // Kiểm tra nếu các gói đã lấy trước đó mà vẫn còn tồn tại trong _oldPackets thì bỏ qua, nếu không thì gọi sự kiện OnNewPacketConnectionStarted
                packetConnectionInfos.ToList().ForEach(x =>
                {
                    if (!_oldPackets.Contains(x))
                    {
                        OnNewPacketConnectionStarted?.Invoke(this, x);
                    }

                });

                // Kiểm tra gói cũ mà không có trong gói mới thì gọi sự kiện OnNewPacketConnectionEnded
                _oldPackets.ToList().ForEach(x =>
                {
                    if (!packetConnectionInfos.Contains(x))
                        OnNewPacketConnectionEnded?.Invoke(this, x);
                });

                Thread.Sleep(5000);
                _oldPackets = packetConnectionInfos;
            }
            while (IsAutoReload);

        }

        #endregion

    }
}
