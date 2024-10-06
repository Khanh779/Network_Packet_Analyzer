using Network_Packet_Traffic.Connections;
using Network_Packet_Traffic.Connections.Enums;
using Network_Packet_Traffic.Connections.Structs;
using System;
using System.Collections.Generic;
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
            comboBox1.Items.Add("ALL");
            comboBox1.Items.AddRange(Enum.GetNames(typeof(StateType)));
            comboBox1.SelectedIndex = 0;

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


        List<ListViewItem> newItems;

        private void UpdateListView(object sender, PacketConnectionInfo[] packets)
        {
            if (listView1.InvokeRequired)
            {
                listView1.Invoke(new Action(() => UpdateListView(sender, packets)));
                return;
            }

            // Temporary list to hold new items for batch addition
            newItems = new List<ListViewItem>();
            int selectedIndex = comboBox1.SelectedIndex;

            foreach (var packet in packets)
            {
                if (selectedIndex == 0 || packet.State == (StateType)comboBox1.Items[selectedIndex])
                {
                    var existingItem = listView1.Items
                        .Cast<ListViewItem>()
                        .FirstOrDefault(i => i.SubItems[1].Text == packet.LocalAddress && i.SubItems[2].Text == packet.LocalPort.ToString());

                    if (existingItem != null)
                    {
                        // Update existing item
                        existingItem.SubItems[3].Text = packet.RemoteAddress;
                        existingItem.SubItems[4].Text = packet.RemotePort.ToString();
                        existingItem.SubItems[5].Text = packet.Protocol.ToString();
                        existingItem.SubItems[6].Text = packet.State.ToString();
                    }
                    else
                    {
                        // Create new item
                        var newItem = new ListViewItem(new string[]
                        {
                            packet.ProcessId.ToString(),
                            packet.LocalAddress,
                            packet.LocalPort.ToString(),
                            packet.RemoteAddress,
                            packet.RemotePort.ToString(),
                            packet.Protocol.ToString(),
                            packet.State.ToString(),
                            NetHelper.GetProcessName(packet.ProcessId)
                        });
                        newItems.Add(newItem);
                    }
                }
            }

            AddToList();
            isLoadedConnections = true;
        }

        void AddToList()
        {
            // Add all new items to the ListView at once
            if (newItems.Count > 0)
            {
                listView1.Items.AddRange(newItems.ToArray());
            }
            toolStripStatusLabel1.Text = $"Number of connections: {listView1.Items.Count}";
        }

        private void OnPacketConnectionStarted(object sender, PacketConnectionInfo packet)
        {
            if (listView1.InvokeRequired && isLoadedConnections)
            {
                if (comboBox1.SelectedIndex == 0 || packet.State == (StateType)comboBox1.Items[comboBox1.SelectedIndex])
                {
                    var newItem = new ListViewItem(new string[]
                    {
                            packet.ProcessId.ToString(),
                            packet.LocalAddress,
                            packet.LocalPort.ToString(),
                            packet.RemoteAddress,
                            packet.RemotePort.ToString(),
                            packet.Protocol.ToString(),
                            packet.State.ToString(),
                            NetHelper.GetProcessName(packet.ProcessId)
                    });

                    newItems.Add(newItem);

                    AddToList();
                }
            }
        }

        private void OnPacketConnectionEnded(object sender, PacketConnectionInfo packet)
        {
            if (listView1.InvokeRequired && isLoadedConnections)
            {
                listView1.Invoke(new Action(() =>
                {
                    var itemToRemove = newItems.Cast<ListViewItem>().FirstOrDefault(i =>
                        i.Text == packet.ProcessId.ToString() &&
                        i.SubItems[1].Text == packet.LocalAddress &&
                        i.SubItems[2].Text == packet.LocalPort.ToString() &&
                        i.SubItems[3].Text == packet.RemoteAddress &&
                        i.SubItems[4].Text == packet.RemotePort.ToString() &&
                        i.SubItems[5].Text == packet.Protocol.ToString() &&
                        i.SubItems[6].Text == packet.State.ToString()
                    );

                    if (itemToRemove != null)
                    {
                        newItems.Remove(itemToRemove);
                    }
                }));
                AddToList();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle selection change if needed
        }
    }
}
