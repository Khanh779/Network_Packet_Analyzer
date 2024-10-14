using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Network_Packet_Analyzer
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            labelVersion.Text = "Version: " + ProductVersion;
        }

        // Event handler for button click
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
