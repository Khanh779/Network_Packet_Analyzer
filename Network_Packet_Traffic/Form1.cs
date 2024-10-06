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
        private int tempState = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeComboBox();
            InitializeConnectionsMonitor();
        }

        private void InitializeComboBox()
        {
            comboBoxStateFilter.Items.Add("ALL");
            comboBoxStateFilter.Items.AddRange(Enum.GetNames(typeof(StateType)));
            comboBoxStateFilter.SelectedIndex = 0;
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

            foreach (var packet in packets)
            {
                if (CheckPacketState(packet))
                {
                    var newItem = CreateListViewItem(packet);

                    if (!listViewConnections.Items.Contains(newItem))
                    {
                        listViewConnections.Items.Add(newItem);
                    }
                }
            }

            isLoadedConnections = true;
            UpdateStatusLabel();
        }

        // Biến bool để kiểm tra gói có nên thêm vào ListView hay không dựa trên tempState
        // Nếu tempState = 0 thì thêm tất cả các gói vào ListView. Còn nếu tempState khác 0 thì chỉ thêm gói có State = tempState - 1 lọc gói dựa trên StateType của gói
        bool CheckPacketState(PacketConnectionInfo packet)
        {
            if (tempState <= 0)
            {
                return true;
            }
            else
            {
                return packet.State == (StateType)(tempState - 1);
            }
        }

        private ListViewItem CreateListViewItem(PacketConnectionInfo packet)
        {
            return new ListViewItem(new string[]
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
        }


        private void UpdateStatusLabel()
        {
            if (statusStrip.InvokeRequired)
            {
                statusStrip.Invoke((Action)(() =>
                {
                    toolStripStatusLabel.Text = $"Total Connections: {listViewConnections.Items.Count}";
                }));
            }

        }

        private void OnPacketConnectionStarted(object sender, PacketConnectionInfo packet)
        {
            if (isLoadedConnections)
            {
                var newItem = CreateListViewItem(packet);
                if (CheckPacketState(packet))
                {
                    if (!listViewConnections.Items.Contains(newItem))
                    {
                        listViewConnections.Invoke((Action)(() =>
                        {
                            listViewConnections.Items.Add(newItem);
                        }));
                    }
                }

                UpdateStatusLabel();
            }
        }

        private void OnPacketConnectionEnded(object sender, PacketConnectionInfo packet)
        {
            if (isLoadedConnections)
            {
                listViewConnections.Invoke((Action)(() =>
                {
                    RemovePacketFromListView(packet);
                }));
            }
        }

        private void RemovePacketFromListView(PacketConnectionInfo packet)
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
                listViewConnections.Invoke((Action)(() =>
                {
                    listViewConnections.Items.Remove(itemToRemove);
                }));
            }
            UpdateStatusLabel();
        }

        private void comboBoxStateFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (connectionsMonitor != null && listViewConnections.Items.Count > 0)
            {
                listViewConnections.Items.Clear();
                tempState = comboBoxStateFilter.SelectedIndex;
                UpdateListView(this, connectionsMonitor.GetPacketConnections().ToArray());
            }
        }
    }
}
