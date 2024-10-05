using Network_Packet_Traffic.Connections;
using Network_Packet_Traffic.Connections.Enums;
using Network_Packet_Traffic.Connections.Structs;
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
            comboBox1.Items.Add("ALL");
            comboBox1.Items.AddRange(Enum.GetNames(typeof(StateType)));

            comboBox1.SelectedIndex = 0;

         

            InitializeConnectionsMonitor();
        }

        private ConnectionsMonitor connectionsMonitor;
        bool isLoadedConnections = false;

        private void InitializeConnectionsMonitor()
        {
            connectionsMonitor = new ConnectionsMonitor(true);
            connectionsMonitor.NewPacketsConnectionLoad += UpdateListView;
            connectionsMonitor.NewPacketConnectionStarted += OnPacketConnectionStarted;
            connectionsMonitor.NewPacketConnectionEnded += OnPacketConnectionEnded;
            connectionsMonitor.StartListening();
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
                if (comboBox1.SelectedIndex == 0 || packet.State == (StateType)comboBox1.Items[comboBox1.SelectedIndex])
                {
                    var existingItem = listView1.Items.Cast<ListViewItem>().FirstOrDefault(i => i.Text == packet.LocalAddress && i.SubItems[1].Text == packet.LocalPort.ToString());

                    if (existingItem != null)
                    {
                        // Nếu mục đã tồn tại, cập nhật thông tin
                        existingItem.SubItems[2].Text = packet.RemoteAddress;
                        existingItem.SubItems[3].Text = packet.RemotePort.ToString();
                        existingItem.SubItems[4].Text = packet.Protocol.ToString();
                        existingItem.SubItems[5].Text = packet.State.ToString();
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
                        item.SubItems.Add(packet.State.ToString());
                        listView1.Items.Add(item);
                    }
                }
            }
            isLoadedConnections = true;
        }

        private void OnPacketConnectionStarted(object sender, PacketConnectionInfo packet)
        {
            // Xử lý cho gói mới bắt đầu kết nối
            // MessageBox.Show($"New connection started: {packet.LocalAddress}:{packet.LocalPort} -> {packet.RemoteAddress}:{packet.RemotePort}");

            // Thêm mục vào ListView
            if (listView1.InvokeRequired && isLoadedConnections)
            {
                listView1.Invoke(new Action(() =>
                {
                    if (comboBox1.SelectedIndex == 0 || packet.State == (StateType)comboBox1.Items[comboBox1.SelectedIndex])
                    {
                        listView1.Items.Add(new ListViewItem(new string[]
                        {
                            packet.ProcessId.ToString(),
                            packet.LocalAddress,
                            packet.LocalPort.ToString(),
                            packet.RemoteAddress,
                            packet.RemotePort.ToString(),
                            packet.Protocol.ToString(),
                            packet.State .ToString()
                        }));

                    }
                }));
                toolStripStatusLabel1.Text = "Number of connections: " + listView1.Items.Count.ToString();
            }

        }

        private void OnPacketConnectionEnded(object sender, PacketConnectionInfo packet)
        {
            // Xử lý cho gói kết thúc kết nối
            // MessageBox.Show($"Connection ended: {packet.LocalAddress}:{packet.LocalPort} -> {packet.RemoteAddress}:{packet.RemotePort}");

            // Xoá mục trong ListView
            if (listView1.InvokeRequired && isLoadedConnections)
            {
                listView1.Invoke(new Action(() =>
                {
                    listView1.Items.Remove(listView1.Items.Cast<ListViewItem>().FirstOrDefault(i => i.Text == packet.ProcessId.ToString() && i.SubItems[1].Text == packet.LocalAddress && i.SubItems[2].Text == packet.LocalPort.ToString()
                        && i.SubItems[3].Text == packet.RemoteAddress && i.SubItems[4].Text == packet.RemotePort.ToString() && i.SubItems[5].Text == packet.Protocol.ToString() && i.SubItems[6].Text == packet.State.ToString()
                    ));
                }));
                toolStripStatusLabel1.Text = "Number of connections: " + listView1.Items.Count.ToString();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
