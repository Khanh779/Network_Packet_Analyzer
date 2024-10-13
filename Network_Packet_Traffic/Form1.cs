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

        private void UpdateListView(object sender, PacketConnectionInfo[] packets)
        {
            connectionsMonitor.ProtocolFilter = ProtocolFilter.NoFilter;

            if (iPToolStripMenuItem.Checked)
                connectionsMonitor.ProtocolFilter |= ProtocolFilter.IPNET;
            if (tCPToolStripMenuItem.Checked)
                connectionsMonitor.ProtocolFilter |= ProtocolFilter.TCP;
            if (uDPToolStripMenuItem.Checked)
                connectionsMonitor.ProtocolFilter |= ProtocolFilter.UDP;
            if (aRPToolStripMenuItem.Checked)
                connectionsMonitor.ProtocolFilter |= ProtocolFilter.ARP;
            if (iCMPToolStripMenuItem.Checked)
                connectionsMonitor.ProtocolFilter |= ProtocolFilter.ICMP;
            if (otherUnknownToolStripMenuItem.Checked)
                connectionsMonitor.ProtocolFilter |= ProtocolFilter.UNKNOWN;


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
                if (IsPacketFilterMonitor(packetConnectionInfos[i]))
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

        private void OnPacketConnectionStarted(object sender, PacketConnectionInfo packet)
        {
            if (isLoadedConnections)
            {
                var liIt = CreateListViewItem(packet);
                listViewConnections.Invoke((Action)
                    delegate
                    {
                        if (IsPacketMatchFilter(packet) && !listViewConnections.Items.Contains(liIt))
                            listViewConnections.Items.Add(liIt);
                    });
            }
        }

        private void OnPacketConnectionEnded(object sender, PacketConnectionInfo packet)
        {
            if (isLoadedConnections)
            {
                var liIt = CreateListViewItem(packet);
                listViewConnections.Invoke((Action)
                    delegate
                    {
                        if (IsPacketMatchFilter(packet) && listViewConnections.Items.Contains(liIt))
                            listViewConnections.Items.Remove(liIt);
                    });
            }
        }

        bool IsPacketMatchFilter(PacketConnectionInfo packet)
        {
            if (connectionsMonitor.ProtocolFilter == ProtocolFilter.NoFilter) return true;

            if (connectionsMonitor.ProtocolFilter.HasFlag(ProtocolFilter.IPNET) && packet.Protocol == ProtocolType.IPNET) return true;
            if (connectionsMonitor.ProtocolFilter.HasFlag(ProtocolFilter.TCP) && packet.Protocol == ProtocolType.TCP) return true;
            if (connectionsMonitor.ProtocolFilter.HasFlag(ProtocolFilter.UDP) && packet.Protocol == ProtocolType.UDP) return true;
            if (connectionsMonitor.ProtocolFilter.HasFlag(ProtocolFilter.ARP) && packet.Protocol == ProtocolType.ARP) return true;
            if (connectionsMonitor.ProtocolFilter.HasFlag(ProtocolFilter.ICMP) && packet.Protocol == ProtocolType.ICMP) return true;
            if (connectionsMonitor.ProtocolFilter.HasFlag(ProtocolFilter.UNKNOWN) && packet.Protocol == ProtocolType.UNKNOWN) return true;

            return false;
        }

        bool IsPacketFilterMonitor(PacketConnectionInfo packet)
        {
            return
                packet.Protocol == ProtocolType.IPNET && iPToolStripMenuItem.Checked ||
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



    }
}
