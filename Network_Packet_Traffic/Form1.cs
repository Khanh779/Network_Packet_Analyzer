using Network_Packet_Traffic.Connections;
using Network_Packet_Traffic.Connections.Struct;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Network_Packet_Traffic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeConnectionsMonitor();
        }

        private ConnectionsMonitor connectionsMonitor;

        private void InitializeConnectionsMonitor()
        {
            connectionsMonitor = new ConnectionsMonitor(true);
            connectionsMonitor.OnNewPacketsConnectionLoad += UpdateListView;
            connectionsMonitor.OnNewPacketConnectionStarted += OnPacketConnectionStarted;
            connectionsMonitor.OnNewPacketConnectionEnded += OnPacketConnectionEnded;
            connectionsMonitor.StartListen();
        }

        private void UpdateListView(object sender, PacketConnectionInfo[] packets)
        {
            // Cập nhật ListView với thông tin gói
            if (listView1.InvokeRequired)
            {
                listView1.Invoke(new Action(() => UpdateListView(sender, packets)));
                return;
            }

            // Cập nhật hoặc thêm mới các mục trong ListView
            foreach (var packet in packets)
            {
                var existingItem = listView1.Items.Cast<ListViewItem>().FirstOrDefault(i => i.Text == packet.LocalAddress && i.SubItems[1].Text == packet.LocalPort.ToString());

                if (existingItem != null)
                {
                    // Nếu mục đã tồn tại, cập nhật thông tin
                    existingItem.SubItems[2].Text = packet.RemoteAddress;
                    existingItem.SubItems[3].Text = packet.RemotePort.ToString();
                    existingItem.SubItems[4].Text = packet.Protocol.ToString();
                    existingItem.SubItems[5].Text = packet.State;
                }
                else
                {
                    // Nếu không tồn tại, thêm mục mới
                    var item = new ListViewItem(packet.ProcessId.ToString());
                    item.SubItems.Add(packet.LocalAddress.ToString());
                    item.SubItems.Add(packet.LocalPort.ToString());
                    item.SubItems.Add(packet.RemoteAddress);
                    item.SubItems.Add(packet.RemotePort.ToString());
                    item.SubItems.Add(packet.Protocol.ToString());
                    item.SubItems.Add(packet.State);
                    listView1.Items.Add(item);
                }
            }
        }

        private void OnPacketConnectionStarted(object sender, PacketConnectionInfo packet)
        {
            // Xử lý cho gói mới bắt đầu kết nối
            // MessageBox.Show($"New connection started: {packet.LocalAddress}:{packet.LocalPort} -> {packet.RemoteAddress}:{packet.RemotePort}");
        }

        private void OnPacketConnectionEnded(object sender, PacketConnectionInfo packet)
        {
            // Xử lý cho gói kết thúc kết nối
            // MessageBox.Show($"Connection ended: {packet.LocalAddress}:{packet.LocalPort} -> {packet.RemoteAddress}:{packet.RemotePort}");
        }
    }
}
