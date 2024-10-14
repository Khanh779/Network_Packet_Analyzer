using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Network_Packet_Analyzer
{
    public partial class PortScanner_Form : Form
    {

        const int WM_SETTEXT = 0x000C;
        const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);

        public PortScanner_Form()
        {
            InitializeComponent();
            SendMessage(txtIPAddress.Handle, EM_SETCUEBANNER, 0, placeholder);
        }

        string placeholder = "Enter IP Address (Example: 192.168.1.1)";

        private void btnScan_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
                btnScan.Text = "Scan Ports";
            }
            if (!backgroundWorker1.IsBusy)
            {
                listResults.Items.Clear();
                backgroundWorker1.RunWorkerAsync();
                btnScan.Text = "Stop";
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string ip = txtIPAddress.Text;
            int startPort = int.Parse(txtStartPort.Text);
            int endPort = int.Parse(txtEndPort.Text);

            for (int port = startPort; port <= endPort; port++)
            {
                bool isTcpPortOpen = false;
                bool isUdpPortOpen = false;

                toolStripStatusLabel.Text = $"Status: Scanning (Port: {port}) ...";

                using (var tcpClient = new TcpClient())
                {
                    try
                    {
                        tcpClient.Connect(ip, port);
                        isTcpPortOpen = true;
                    }
                    catch { }
                }

                using (var udpClient = new UdpClient())
                {
                    try
                    {
                        udpClient.Connect(ip, port);
                        isUdpPortOpen = true; 
                    }
                    catch { }
                }

                listResults.BeginInvoke((Action)delegate
                {
                    try
                    {
                        string resultMessage = $"Port {port}: {(isTcpPortOpen || isUdpPortOpen ? "OPEN" : "CLOSE")} (";

                        if (isTcpPortOpen) resultMessage += "TCP ";
                        if (isUdpPortOpen) resultMessage += "UDP";
                        resultMessage += isTcpPortOpen || isUdpPortOpen ? ")" : "";

                        listResults.Items.Add(resultMessage);
                        listResults.Items.Add("_______________________");
                    }
                    catch { }
                });
            }
        }


        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripStatusLabel.Text = "Status: Complete!";
            btnScan.Text = "Scan Ports";
        }


    }
}