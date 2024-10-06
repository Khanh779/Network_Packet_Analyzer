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
        private ConnectionsMonitor connectionsMonitor;
        private bool isLoadedConnections = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxStateFilter.Items.Add("ALL");
            comboBoxStateFilter.Items.AddRange(Enum.GetNames(typeof(StateType)));
            comboBoxStateFilter.SelectedIndex = 0;

            InitializeConnectionsMonitor();
        }

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
            if (listViewConnections.InvokeRequired)
            {
                listViewConnections.Invoke(new Action(() => UpdateListView(sender, packets)));
                return;
            }

            // Add packets directly to the ListView
            foreach (var packet in packets)
            {
                if (comboBoxStateFilter.Items[comboBoxStateFilter.SelectedIndex].ToString() == "ALL" ||
                    comboBoxStateFilter.Items[comboBoxStateFilter.SelectedIndex].ToString() == packet.State.ToString())
                {
                    var newItem = new ListViewItem(new string[]
                    {
                        packet.ProcessId.ToString(),
                        packet.LocalAddress.ToString(),
                        packet.LocalPort.ToString(),
                        packet.RemoteAddress.ToString(),
                        packet.RemotePort.ToString(),
                        packet.Protocol.ToString(),
                        packet.State.ToString(),
                        NetHelper.GetProcessName((int)packet.ProcessId)
                    });

                    if (!listViewConnections.Items.Contains(newItem))
                    {
                        listViewConnections.Invoke((Action)(() =>
                        {
                            listViewConnections.Items.Add(newItem);
                        }));
                    }
                }
            }

            isLoadedConnections = true;
            UpdateStatusLabel(); // Cập nhật label trạng thái

        }


        private void UpdateStatusLabel()
        {
            // Cập nhật status label trong luồng chính
            if (statusStrip.InvokeRequired)
            {
                toolStripStatusLabel.Text = $"Total Connections: {listViewConnections.Items.Count}";
            }

        }

        private void OnPacketConnectionStarted(object sender, PacketConnectionInfo packet)
        {
            if (isLoadedConnections)
            {
                var newItem = new ListViewItem(new string[]
                {
                    packet.ProcessId.ToString(),
                    packet.LocalAddress.ToString(),
                    packet.LocalPort.ToString(),
                    packet.RemoteAddress.ToString(),
                    packet.RemotePort.ToString(),
                    packet.Protocol.ToString(),
                    packet.State.ToString(),
                    NetHelper.GetProcessName((int) packet.ProcessId)
                });

                if (comboBoxStateFilter.Items[comboBoxStateFilter.SelectedIndex].ToString() == "ALL" ||
                    comboBoxStateFilter.Items[comboBoxStateFilter.SelectedIndex].ToString() == packet.State.ToString())
                {
                    if (!listViewConnections.Items.Contains(newItem))
                    {
                        listViewConnections.Invoke((Action)(() =>
                        {
                            listViewConnections.Items.Add(newItem);

                        }));
                    }
                }
                UpdateStatusLabel(); // Cập nhật label trạng thái
            }
        }

        private void OnPacketConnectionEnded(object sender, PacketConnectionInfo packet)
        {
            if (isLoadedConnections)
            {
                listViewConnections.Invoke((Action)(() =>
                {
                    var itemToRemove = listViewConnections.Items.Cast<ListViewItem>().FirstOrDefault(i =>
                        i.Text == packet.ProcessId.ToString() &&
                        i.SubItems[1].Text == packet.LocalAddress.ToString() &&
                        i.SubItems[2].Text == packet.LocalPort.ToString() &&
                        i.SubItems[3].Text == packet.RemoteAddress.ToString() &&
                        i.SubItems[4].Text == packet.RemotePort.ToString() &&
                        i.SubItems[5].Text == packet.Protocol.ToString() &&
                        i.SubItems[6].Text == packet.State.ToString() &&
                        i.SubItems[7].Text == NetHelper.GetProcessName((int)packet.ProcessId));

                    if (itemToRemove != null)
                    {
                        listViewConnections.Items.Remove(itemToRemove);
                    }
                    UpdateStatusLabel(); // Cập nhật label trạng thái
                }));
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listViewConnections.Items.Clear();
            UpdateListView(this, connectionsMonitor.GetPacketConnections().ToArray());
        }
    }
}
