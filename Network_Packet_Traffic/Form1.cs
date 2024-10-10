using Network_Packet_Traffic.Connections;
using Network_Packet_Traffic.Connections.Enums;
using Network_Packet_Traffic.Connections.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;

namespace Network_Packet_Traffic
{
    public partial class Form1 : Form
    {
        private ConnectionsMonitor connectionsMonitor;
        private bool isLoadedConnections = false;
        private int tempState = 0;
        private List<ListViewItem> originalItems = new List<ListViewItem>(); // Danh sách tạm để lưu các item gốc

        public Form1()
        {
            InitializeComponent();
            AddColumnsToListView();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeConnectionsMonitor();
        }

        void AddColumnsToListView()
        {
            var columns = new[]
            {
                new ColumnHeader { Text = "Local Address", Width = 100 },
                new ColumnHeader { Text = "Local Port", Width = 60 },
                new ColumnHeader { Text = "Remote Address", Width = 100 },
                new ColumnHeader { Text = "Remote Port", Width = 75 },
                new ColumnHeader { Text = "MAC Address", Width = 100 },
                new ColumnHeader { Text = "Protocol", Width = 60 },
                new ColumnHeader { Text = "State", Width = 70 },
                new ColumnHeader { Text = "Process ID", Width = 70 },
                new ColumnHeader { Text = "Process Name", Width = 100 }
            };

            listViewConnections.Columns.AddRange(columns);
            listViewConnections.Scrollable = true;
            listViewConnections.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
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
            if (connectionsMonitor != null)
            {
                UpdateFilter();
            }

            var listViewItems = ConvertFromPackArrayToListView(packets);

            if (isLoadedConnections == false)
            {
                if (listViewConnections.InvokeRequired)
                {
                    listViewConnections.Invoke((Action)(() =>
                    {
                        listViewConnections.Items.Clear();
                        listViewConnections.Items.AddRange(listViewItems);
                        originalItems.Clear();
                        originalItems.AddRange(listViewItems); // Lưu các item gốc
                    }));
                }
            }
            isLoadedConnections = true;
            UpdateStatusLabel();
        }

        ListViewItem[] ConvertFromPackArrayToListView(PacketConnectionInfo[] packetConnectionInfos)
        {
            HashSet<ListViewItem> listViewItems = new HashSet<ListViewItem>();

            for (int i = 0; i < packetConnectionInfos.Length; i++)
            {
                var a = CreateListViewItem(packetConnectionInfos[i]);
                listViewItems.Add(a);
            }
            return listViewItems.ToArray();
        }

        bool CheckPacketState(PacketConnectionInfo packet)
        {
            return tempState <= 0 || packet.State == (StateType)(tempState - 1);
        }

        private ListViewItem CreateListViewItem(PacketConnectionInfo packet)
        {
            return new ListViewItem(new string[]
            {
                packet.LocalAddress.ToString(),
                packet.LocalPort.ToString(),
                packet.RemoteAddress.ToString(),
                packet.RemotePort.ToString(),
                packet.MacAddress,
                packet.Protocol.ToString(),
                packet.State.ToString(),
                packet.ProcessId.ToString(),
                NetHelper.GetProcessName((int)packet.ProcessId)
            });
        }

        private void UpdateStatusLabel()
        {
            if (statusStrip.InvokeRequired)
            {
                statusStrip.Invoke((Action)(() =>
                {
                    toolStripStatusLabel.Text = $"Total Connections" + (tbt_Filter.TextLength != 0 ? $" ({tbt_Filter.Text})" : "") + $": {listViewConnections.Items.Count}" + (!connectionsMonitor.IsRunning ? " - Stopped" : "");
                }));
            }
            else
            {
                toolStripStatusLabel.Text = $"Total Connections" + (tbt_Filter.TextLength != 0 ? $" ({tbt_Filter.Text})" : "") + $": {listViewConnections.Items.Count}" + (!connectionsMonitor.IsRunning ? " - Stopped" : "");
            }
        }

        private void OnPacketConnectionStarted(object sender, PacketConnectionInfo packet)
        {
            if (listViewConnections.InvokeRequired && isLoadedConnections)
            {
                var liIt = CreateListViewItem(packet);
                listViewConnections.Invoke((Action)
                    delegate
                    {
                        if (CheckPacketState(packet) && !listViewConnections.Items.Contains(liIt))
                            listViewConnections.Items.Add(liIt);
                    });
            }
        }

        private void OnPacketConnectionEnded(object sender, PacketConnectionInfo packet)
        {
            if (listViewConnections.InvokeRequired && isLoadedConnections)
            {
                var liIt = CreateListViewItem(packet);
                listViewConnections.Invoke((Action)
                    delegate
                    {
                        if (CheckPacketState(packet) && listViewConnections.Items.Contains(liIt))
                            listViewConnections.Items.Remove(liIt);
                    });
            }
        }

        private void tbt_Filter_TextChanged(object sender, EventArgs e)
        {
            string filterText = tbt_Filter.Text.ToLower();
            List<ListViewItem> filteredItems = new List<ListViewItem>();
            foreach (var item in originalItems)
            {
                bool matches = item.SubItems.Cast<ListViewItem.ListViewSubItem>().Any(subItem => subItem.Text.ToLower().Contains(filterText));
                if (matches) filteredItems.Add(item.Clone() as ListViewItem);
            }

            listViewConnections.Items.Clear();
            listViewConnections.Items.AddRange(filteredItems.ToArray());
            UpdateStatusLabel();
        }

        private void iPToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (connectionsMonitor != null)
            {
                if (iPToolStripMenuItem.Checked)
                    connectionsMonitor.StartListening();
                else
                    connectionsMonitor.StopListening();

                UpdateStatusLabel();
                MessageBox.Show("IP Filter is " + (iPToolStripMenuItem.Checked ? "Enabled" : "Disabled"));
            }
        }

        void UpdateFilter()
        {
            if (tCPToolStripMenuItem.Checked)
                tempState = 1;
            else if (uDPToolStripMenuItem.Checked)
                tempState = 2;
            else if (aRPToolStripMenuItem.Checked)
                tempState = 3;
            else if (iCMPToolStripMenuItem.Checked)
                tempState = 4;
            else if (otherUnknownToolStripMenuItem.Checked)
                tempState = -1;
            else
                tempState = 0;

            connectionsMonitor.ProtocolFilter = (ProtocolFilter)tempState;

        }


    }
}
