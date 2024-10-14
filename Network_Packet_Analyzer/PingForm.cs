using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Network_Packet_Analyzer
{
    public partial class PingForm : Form
    {


        const int WM_SETTEXT = 0x000C;
        const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);

        public PingForm()
        {
            InitializeComponent();

            SendMessage(txtIPAddress.Handle, EM_SETCUEBANNER, 0, placeholder);
        }

        string placeholder = "Enter IP Address or Hostname";

        private void btnPing_Click(object sender, EventArgs e)
        {
            ExecutePing();
        }


        void ExecutePing()
        {
            string address = txtIPAddress.Text.Trim();
            if (string.IsNullOrEmpty(address) || address == "Enter IP Address or Hostname")
            {
                MessageBox.Show("Please enter a valid IP address or hostname.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            progressBar.Style = ProgressBarStyle.Marquee;
            rtbResults.Clear();

            Ping ping = new Ping();
            int successCount = 0;
            int totalTime = 0;
            int minTime = int.MaxValue;
            int maxTime = int.MinValue;

            try
            {
                PingReply reply = ping.Send(address, 1000); // timeout 1000ms

                if (reply.Status == IPStatus.Success)
                {
                    // Show results from ping
                    rtbResults.AppendText($"Reply from {reply.Address}: time={reply.RoundtripTime}ms\n");
                    successCount++;
                    totalTime += (int)reply.RoundtripTime;

                    // Refresh min and max time
                    if (reply.RoundtripTime < minTime) minTime = (int)reply.RoundtripTime;
                    if (reply.RoundtripTime > maxTime) maxTime = (int)reply.RoundtripTime;

                    // Show more details
                    rtbResults.AppendText($"IPv4 Address: {reply.Address.MapToIPv4()}\n");
                    rtbResults.AppendText($"IPv6 Address: {reply.Address.MapToIPv6()}\n");
                    rtbResults.AppendText($"Address Family: {reply.Address.AddressFamily}\n");

                    if (reply.Options != null)
                    {
                        rtbResults.AppendText($"TTL: {reply.Options.Ttl}\n");
                    }
                    else
                    {
                        rtbResults.AppendText("TTL: N/A\n");
                    }

                    rtbResults.AppendText($"Buffer Size: {reply.Buffer.Length} bytes\n");
                }
                else
                {
                    rtbResults.AppendText($"Ping failed: {reply.Status}\n");
                }
            }
            catch (Exception ex)
            {
                rtbResults.AppendText($"Error: {ex.Message}\n");
            }

            //Show summary
            rtbResults.AppendText($"\nPing statistics:\n");
            rtbResults.AppendText($"Packets: Sent = 4, Received = {successCount}, Lost = {4 - successCount} ({(4 - successCount) * 100 / 4}% loss)\n");
            if (successCount > 0)
            {
                rtbResults.AppendText($"Approximate round trip times in milli-seconds:\n");
                rtbResults.AppendText($"Minimum = {minTime}ms, Maximum = {maxTime}ms, Average = {totalTime / successCount}ms\n");
            }

            progressBar.Style = ProgressBarStyle.Blocks;
        }


        private void PingForm_Load(object sender, EventArgs e)
        {

        }
    }
}
