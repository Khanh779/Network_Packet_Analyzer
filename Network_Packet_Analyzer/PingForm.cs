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
            Excut1();
        }


        void Excut1()
        {
            string host = txtIPAddress.Text; // Lấy địa chỉ IP từ TextBox
            Ping ping = new Ping();
            int successCount = 0;
            int totalTime = 0;
            int minTime = int.MaxValue;
            int maxTime = int.MinValue;

            rtbResults.Clear(); // Xóa kết quả trước khi thực hiện ping
            for (int i = 0; i < 4; i++) // Gửi 4 ping
            {
                try
                {
                    PingReply reply = ping.Send(host);
                    if (reply.Status == IPStatus.Success)
                    {
                        rtbResults.AppendText($"Reply from {reply.Address}: time={reply.RoundtripTime}ms\n");
                        successCount++;
                        totalTime += (int)reply.RoundtripTime;

                        // Cập nhật min/max
                        if (reply.RoundtripTime < minTime) minTime = (int)reply.RoundtripTime;
                        if (reply.RoundtripTime > maxTime) maxTime = (int)reply.RoundtripTime;
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
            }

            // Hiện thống kê
            rtbResults.AppendText($"\nPing statistics:\n");
            rtbResults.AppendText($"Packets: Sent = 4, Received = {successCount}, Lost = {4 - successCount} ({(4 - successCount) * 100 / 4}% loss),\n");
            if (successCount > 0)
            {
                rtbResults.AppendText($"Approximate round trip times in milli-seconds:\n");
                rtbResults.AppendText($"Minimum = {minTime}ms, Maximum = {maxTime}ms, Average = {totalTime / successCount}ms\n");
            }
        }


        void Excut2()
        {
            // Get IP address or hostname
            string address = txtIPAddress.Text.Trim();
            if (string.IsNullOrEmpty(address) || address == "Enter IP Address or Hostname")
            {
                MessageBox.Show("Please enter a valid IP address or hostname.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Show progress
            progressBar.Style = ProgressBarStyle.Marquee;
            rtbResults.Clear();

            try
            {
                using (Ping ping = new Ping())
                {
                    // Execute ping
                    PingReply reply = ping.Send(address, 1000); // 1000ms timeout

                    // Show results
                    if (reply.Status == IPStatus.Success)
                    {
                        rtbResults.AppendText($"Ping to {reply.Address} successful: \n");
                        rtbResults.AppendText($"Time: {reply.RoundtripTime} ms\n");
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Reset progress
                progressBar.Style = ProgressBarStyle.Blocks;
            }
        }
    }
}
