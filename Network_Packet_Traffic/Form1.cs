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


        private void OnPacketConnectionStarted(object sender, PacketConnectionInfo packet)
        {
            if (isLoadedConnections && listViewConnections.InvokeRequired)
            {
                var liIt = CreateListViewItem(packet);
                listViewConnections.Invoke((Action)
                    delegate
                    {
                        if (IsPacketFilterMonitor(packet) && !listViewConnections.Items.Contains(liIt))
                            listViewConnections.Items.Add(liIt);
                    });
            }
        }

        private void OnPacketConnectionEnded(object sender, PacketConnectionInfo packet)
        {
            if (isLoadedConnections && listViewConnections.InvokeRequired)
            {
                var liIt = CreateListViewItem(packet);
                listViewConnections.Invoke((Action)
                    delegate
                    {
                        if (!IsPacketFilterMonitor(packet) || (IsPacketFilterMonitor(packet) && listViewConnections.Items.Contains(liIt)))
                            listViewConnections.Items.Remove(liIt);
                    });
            }
        }





        private void UpdateListView(object sender, PacketConnectionInfo[] packets)
        {
            if (isLoadedConnections == false)
            {
                var listViewItems = ConvertFromPackArrayToListView(packets);
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
            string a = $"Total Connections" + (tbt_Filter.TextLength != 0 ? $" ({tbt_Filter.Text})" : "") + $": {listViewConnections.Items.Count}" + (!connectionsMonitor.IsRunning ? " - Stopped" : "");
            if (statusStrip.InvokeRequired)
            {
                statusStrip.Invoke((Action)(() =>
                {
                    toolStripStatusLabel.Text = a;
                }));
            }
            else
            {
                toolStripStatusLabel.Text = a;
            }
        }

        bool IsPacketFilterMonitor(PacketConnectionInfo packet)
        {
            return
                packet.Protocol == ProtocolType.IPNET && iPToolStripMenuItem1.Checked ||
                packet.Protocol == ProtocolType.TCP && tCPToolStripMenuItem.Checked ||
                packet.Protocol == ProtocolType.UDP && uDPToolStripMenuItem.Checked ||
                packet.Protocol == ProtocolType.ARP && aRPToolStripMenuItem.Checked ||
                packet.Protocol == ProtocolType.ICMP && iCMPToolStripMenuItem.Checked ||
                packet.Protocol == ProtocolType.UNKNOWN && otherUnknownToolStripMenuItem.Checked;
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
            filteredItems.Clear();
            UpdateStatusLabel();
        }

        void UpdateFilter()
        {
            connectionsMonitor.ProtocolFilter = protocolFilters;
        }

        ProtocolFilter protocolFilters
        {
            get
            {
                ProtocolFilter protocolFilter = ProtocolFilter.NoFilter;
                if (iPToolStripMenuItem1.Checked)
                    protocolFilter |= ProtocolFilter.IPNET;
                if (tCPToolStripMenuItem.Checked)
                    protocolFilter |= ProtocolFilter.TCP;
                if (uDPToolStripMenuItem.Checked)
                    protocolFilter |= ProtocolFilter.UDP;
                if (aRPToolStripMenuItem.Checked)
                    protocolFilter |= ProtocolFilter.ARP;
                if (iCMPToolStripMenuItem.Checked)
                    protocolFilter |= ProtocolFilter.ICMP;
                if (otherUnknownToolStripMenuItem.Checked)
                    protocolFilter |= ProtocolFilter.UNKNOWN;
                return protocolFilter;
            }
        }


        private void iPToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (connectionsMonitor != null)
            {
                UpdateFilter();
                if (iPToolStripMenuItem.Checked)
                    connectionsMonitor.StartListening();
                else
                    connectionsMonitor.StopListening();

                UpdateStatusLabel();
                MessageBox.Show("IP Filter is " + (iPToolStripMenuItem.Checked ? "Enabled" : "Disabled"));
            }
        }

        private void iPToolStripMenuItem1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFilter();
        }

        private void tCPToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFilter();
        }

        private void uDPToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFilter();
        }

        private void aRPToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFilter();
        }

        private void iCMPToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFilter();
        }

        private void otherUnknownToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFilter();
        }
    }
}
