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
        private int tempState = 0;

        public Form1()
        {
            InitializeComponent();

            AddComlumnsToListView();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeConnectionsMonitor();
          
        }


        void AddComlumnsToListView()
        {
            listViewConnections.SuspendLayout();
            listViewConnections.Columns.Add("Local Address", 100);
            listViewConnections.Columns.Add("Local Port", 70);
            listViewConnections.Columns.Add("Remote Address", 100);
            listViewConnections.Columns.Add("Remote Port", 75);
            listViewConnections.Columns.Add("MAC Address", 100);
            listViewConnections.Columns.Add("Protocol", 60);
            listViewConnections.Columns.Add("State", 50);
            listViewConnections.Columns.Add("Process ID", 60);
            listViewConnections.Columns.Add("Process Name", 100);
            listViewConnections.ResumeLayout();
            listViewConnections.PerformLayout();
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
           
        }


        // Boolean variable to check if package should be added to ListView or not based on tempState
        // If tempState = 0 then add all packages to ListView. If tempState is different from 0 then only add packages with State = tempState - 1 filter packages based on StateType of package
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
                packet.MacAddress,
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
                   // toolStripStatusLabel.Text = $"Total Connections: {listViewConnections.Items.Count}";
                }));
            }

        }

        private void OnPacketConnectionStarted(object sender, PacketConnectionInfo packet)
        {
            
        }

        private void OnPacketConnectionEnded(object sender, PacketConnectionInfo packet)
        {
           
        }
    }
}
