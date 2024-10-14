using Network_Packet_Traffic.Connections;
using Network_Packet_Traffic.Connections.Enums;
using Network_Packet_Traffic.Connections.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Network_Packet_Traffic
{
    public partial class Form1 : Form
    {
        private ConnectionsMonitor connectionsMonitor;
        private bool isLoadedConnections = false;
        private List<ListViewItem> originalItems = new List<ListViewItem>();

        public Form1()
        {
            InitializeComponent();
            AddColumnsToListView();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeConnectionsMonitor();
        }

        private void AddColumnsToListView()
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
            if (isLoadedConnections)
            {
                listViewConnections.Invoke((Action)(() =>
                {
                    var liIt = CreateListViewItem(packet);
                    if (IsPacketFilterMonitor(packet) && !listViewConnections.Items.Contains(liIt))
                    {
                        listViewConnections.Items.Add(liIt);
                    }
                }));
            }
            UpdateStatusLabel();
        }

        private void OnPacketConnectionEnded(object sender, PacketConnectionInfo packet)
        {
            if (isLoadedConnections)
            {
                listViewConnections.Invoke((Action)(() =>
                {
                    var liIt = CreateListViewItem(packet);
                    if (!IsPacketFilterMonitor(packet) || listViewConnections.Items.Contains(liIt))
                    {
                        listViewConnections.Items.Remove(liIt);
                    }
                }));
            }
            UpdateStatusLabel();
        }

        private void UpdateListView(object sender, PacketConnectionInfo[] packets)
        {
            //UpdateStatusLabel();
            //if (isLoadedConnections == false)
            //{
            //    var listViewItems = ConvertFromPackArrayToListView(packets);
            //    listViewConnections.Invoke((Action)(() =>
            //    {
            //        listViewConnections.Items.Clear();
            //        listViewConnections.Items.AddRange(listViewItems);
            //        originalItems = listViewItems.ToList(); // Cập nhật danh sách item gốc
            //    }));
               isLoadedConnections = true;
            //}
            UpdateStatusLabel();
        }

        private ListViewItem[] ConvertFromPackArrayToListView(PacketConnectionInfo[] packetConnectionInfos)
        {
            return packetConnectionInfos.Select(CreateListViewItem).ToArray();
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
            string statusMessage = $"Total Connections: {listViewConnections.Items.Count}{(connectionsMonitor.IsRunning ? "" : " - Stopped")}";
            statusStrip.Invoke((Action)(() => toolStripStatusLabel.Text = statusMessage));
        }


        private bool IsPacketFilterMonitor(PacketConnectionInfo packet)
        {
            return (packet.Protocol == ProtocolType.IPNET && iPToolStripMenuItem1.Checked) ||
                   (packet.Protocol == ProtocolType.TCP && tCPToolStripMenuItem.Checked) ||
                   (packet.Protocol == ProtocolType.UDP && uDPToolStripMenuItem.Checked) ||
                   (packet.Protocol == ProtocolType.ARP && aRPToolStripMenuItem.Checked) ||
                   (packet.Protocol == ProtocolType.ICMP && iCMPToolStripMenuItem.Checked);
        }

        private void tbt_Filter_TextChanged(object sender, EventArgs e)
        {
            string filterText = tbt_Filter.Text.ToLower();
            var filteredItems = originalItems
                .Where(item => item.SubItems.Cast<ListViewItem.ListViewSubItem>()
                .Any(subItem => subItem.Text.ToLower().Contains(filterText)))
                .Select(item => item.Clone() as ListViewItem)
                .ToArray();

            listViewConnections.Items.Clear();
            listViewConnections.Items.AddRange(filteredItems);
            UpdateStatusLabel();
        }

        private void UpdateFilter()
        {
            connectionsMonitor.ProtocolFilter = protocolFilters;
        }

        private ProtocolType protocolFilters
        {
            get
            {
                ProtocolType protocolType = ProtocolType.UNKNOWN;
                if (iPToolStripMenuItem1.Checked) protocolType |= ProtocolType.IPNET;
                if (tCPToolStripMenuItem.Checked) protocolType |= ProtocolType.TCP;
                if (uDPToolStripMenuItem.Checked) protocolType |= ProtocolType.UDP;
                if (iCMPToolStripMenuItem.Checked) protocolType |= ProtocolType.ICMP;
                if (dHPCToolStripMenuItem.Checked) protocolType |= ProtocolType.DHCP;
                if (dNSToolStripMenuItem.Checked) protocolType |= ProtocolType.DNS;

                return protocolType;
            }
        }

        private void iPToolStripMenuItem1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFilter();
            isLoadedConnections = false;

            if (connectionsMonitor != null)
            {
                if (iPToolStripMenuItem.Checked)
                {
                    connectionsMonitor.StartListening();
                }
                else
                {
                    connectionsMonitor.StopListening();
                }

                UpdateStatusLabel();
                MessageBox.Show("IP Filter is " + (iPToolStripMenuItem.Checked ? "Enabled" : "Disabled"));
            }
        }

        private void OtherMenuItems_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFilter();
            listViewConnections.Items.Clear();
            isLoadedConnections = false;
            if (connectionsMonitor != null)
            {
                connectionsMonitor.RestartListening();
                UpdateStatusLabel();
            }
        }
    }
}
