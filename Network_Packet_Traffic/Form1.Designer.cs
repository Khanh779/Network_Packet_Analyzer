using System.Windows.Forms;

namespace Network_Packet_Traffic
{
    partial class Form1 : Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolKitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portScannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pingSnifferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dHPCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dNSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iPToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tCPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uDPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aRPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iCMPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tab_Home = new System.Windows.Forms.TabPage();
            this.tab_ListMonitor = new System.Windows.Forms.TabPage();
            this.tbt_Filter = new System.Windows.Forms.TextBox();
            this.LB_Filter = new System.Windows.Forms.Label();
            this.listViewConnections = new System.Windows.Forms.ListView();
            this.lblTcpDescription = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tab_ListMonitor.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolKitToolStripMenuItem,
            this.filterMonitorToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(640, 24);
            this.menuStrip.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // toolKitToolStripMenuItem
            // 
            this.toolKitToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.portScannerToolStripMenuItem,
            this.pingSnifferToolStripMenuItem,
            this.dHPCToolStripMenuItem,
            this.dNSToolStripMenuItem});
            this.toolKitToolStripMenuItem.Name = "toolKitToolStripMenuItem";
            this.toolKitToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.toolKitToolStripMenuItem.Text = "Tool-Kit";
            // 
            // portScannerToolStripMenuItem
            // 
            this.portScannerToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.portScannerToolStripMenuItem.Name = "portScannerToolStripMenuItem";
            this.portScannerToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.portScannerToolStripMenuItem.Text = "Port Scanner";
            // 
            // pingSnifferToolStripMenuItem
            // 
            this.pingSnifferToolStripMenuItem.Name = "pingSnifferToolStripMenuItem";
            this.pingSnifferToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.pingSnifferToolStripMenuItem.Text = "Ping Sniffer";
            // 
            // dHPCToolStripMenuItem
            // 
            this.dHPCToolStripMenuItem.Name = "dHPCToolStripMenuItem";
            this.dHPCToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.dHPCToolStripMenuItem.Text = "DHPC";
            // 
            // dNSToolStripMenuItem
            // 
            this.dNSToolStripMenuItem.Name = "dNSToolStripMenuItem";
            this.dNSToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.dNSToolStripMenuItem.Text = "DNS";
            // 
            // filterMonitorToolStripMenuItem
            // 
            this.filterMonitorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iPToolStripMenuItem,
            this.otherToolStripMenuItem});
            this.filterMonitorToolStripMenuItem.Name = "filterMonitorToolStripMenuItem";
            this.filterMonitorToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.filterMonitorToolStripMenuItem.Text = "Monitor";
            // 
            // iPToolStripMenuItem
            // 
            this.iPToolStripMenuItem.Checked = true;
            this.iPToolStripMenuItem.CheckOnClick = true;
            this.iPToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.iPToolStripMenuItem.Name = "iPToolStripMenuItem";
            this.iPToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.iPToolStripMenuItem.Text = "On/ Off";
            this.iPToolStripMenuItem.CheckedChanged += new System.EventHandler(this.iPToolStripMenuItem_CheckedChanged);
            // 
            // otherToolStripMenuItem
            // 
            this.otherToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iPToolStripMenuItem1,
            this.tCPToolStripMenuItem,
            this.uDPToolStripMenuItem,
            this.aRPToolStripMenuItem,
            this.iCMPToolStripMenuItem});
            this.otherToolStripMenuItem.Name = "otherToolStripMenuItem";
            this.otherToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.otherToolStripMenuItem.Text = "Filter";
            // 
            // iPToolStripMenuItem1
            // 
            this.iPToolStripMenuItem1.Checked = true;
            this.iPToolStripMenuItem1.CheckOnClick = true;
            this.iPToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.iPToolStripMenuItem1.Name = "iPToolStripMenuItem1";
            this.iPToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.iPToolStripMenuItem1.Text = "IP";
            this.iPToolStripMenuItem1.CheckedChanged += new System.EventHandler(this.iPToolStripMenuItem1_CheckedChanged);
            // 
            // tCPToolStripMenuItem
            // 
            this.tCPToolStripMenuItem.Checked = true;
            this.tCPToolStripMenuItem.CheckOnClick = true;
            this.tCPToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tCPToolStripMenuItem.Name = "tCPToolStripMenuItem";
            this.tCPToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.tCPToolStripMenuItem.Text = "TCP";
            this.tCPToolStripMenuItem.CheckedChanged += new System.EventHandler(this.tCPToolStripMenuItem_CheckedChanged);
            // 
            // uDPToolStripMenuItem
            // 
            this.uDPToolStripMenuItem.Checked = true;
            this.uDPToolStripMenuItem.CheckOnClick = true;
            this.uDPToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.uDPToolStripMenuItem.Name = "uDPToolStripMenuItem";
            this.uDPToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.uDPToolStripMenuItem.Text = "UDP";
            this.uDPToolStripMenuItem.CheckedChanged += new System.EventHandler(this.uDPToolStripMenuItem_CheckedChanged);
            // 
            // aRPToolStripMenuItem
            // 
            this.aRPToolStripMenuItem.Checked = true;
            this.aRPToolStripMenuItem.CheckOnClick = true;
            this.aRPToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.aRPToolStripMenuItem.Name = "aRPToolStripMenuItem";
            this.aRPToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aRPToolStripMenuItem.Text = "ARP";
            this.aRPToolStripMenuItem.CheckedChanged += new System.EventHandler(this.aRPToolStripMenuItem_CheckedChanged);
            // 
            // iCMPToolStripMenuItem
            // 
            this.iCMPToolStripMenuItem.Checked = true;
            this.iCMPToolStripMenuItem.CheckOnClick = true;
            this.iCMPToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.iCMPToolStripMenuItem.Name = "iCMPToolStripMenuItem";
            this.iCMPToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.iCMPToolStripMenuItem.Text = "ICMP";
            this.iCMPToolStripMenuItem.CheckedChanged += new System.EventHandler(this.iCMPToolStripMenuItem_CheckedChanged);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tab_Home);
            this.tabControl.Controls.Add(this.tab_ListMonitor);
            this.tabControl.ItemSize = new System.Drawing.Size(58, 18);
            this.tabControl.Location = new System.Drawing.Point(5, 27);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(631, 337);
            this.tabControl.TabIndex = 1;
            // 
            // tab_Home
            // 
            this.tab_Home.BackColor = System.Drawing.Color.White;
            this.tab_Home.Location = new System.Drawing.Point(4, 22);
            this.tab_Home.Name = "tab_Home";
            this.tab_Home.Size = new System.Drawing.Size(623, 311);
            this.tab_Home.TabIndex = 0;
            this.tab_Home.Text = "Home";
            // 
            // tab_ListMonitor
            // 
            this.tab_ListMonitor.BackColor = System.Drawing.Color.White;
            this.tab_ListMonitor.Controls.Add(this.tbt_Filter);
            this.tab_ListMonitor.Controls.Add(this.LB_Filter);
            this.tab_ListMonitor.Controls.Add(this.listViewConnections);
            this.tab_ListMonitor.Controls.Add(this.lblTcpDescription);
            this.tab_ListMonitor.Location = new System.Drawing.Point(4, 22);
            this.tab_ListMonitor.Name = "tab_ListMonitor";
            this.tab_ListMonitor.Size = new System.Drawing.Size(623, 311);
            this.tab_ListMonitor.TabIndex = 1;
            this.tab_ListMonitor.Text = "Packet Monitor";
            // 
            // tbt_Filter
            // 
            this.tbt_Filter.Location = new System.Drawing.Point(51, 37);
            this.tbt_Filter.Name = "tbt_Filter";
            this.tbt_Filter.Size = new System.Drawing.Size(151, 20);
            this.tbt_Filter.TabIndex = 4;
            this.tbt_Filter.TextChanged += new System.EventHandler(this.tbt_Filter_TextChanged);
            // 
            // LB_Filter
            // 
            this.LB_Filter.AutoEllipsis = true;
            this.LB_Filter.Location = new System.Drawing.Point(5, 37);
            this.LB_Filter.Name = "LB_Filter";
            this.LB_Filter.Size = new System.Drawing.Size(52, 18);
            this.LB_Filter.TabIndex = 3;
            this.LB_Filter.Text = "Filer by:";
            this.LB_Filter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listViewConnections
            // 
            this.listViewConnections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewConnections.FullRowSelect = true;
            this.listViewConnections.GridLines = true;
            this.listViewConnections.HideSelection = false;
            this.listViewConnections.Location = new System.Drawing.Point(3, 65);
            this.listViewConnections.Name = "listViewConnections";
            this.listViewConnections.Size = new System.Drawing.Size(617, 245);
            this.listViewConnections.TabIndex = 2;
            this.listViewConnections.UseCompatibleStateImageBehavior = false;
            this.listViewConnections.View = System.Windows.Forms.View.Details;
            // 
            // lblTcpDescription
            // 
            this.lblTcpDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTcpDescription.AutoEllipsis = true;
            this.lblTcpDescription.Location = new System.Drawing.Point(6, 10);
            this.lblTcpDescription.Name = "lblTcpDescription";
            this.lblTcpDescription.Size = new System.Drawing.Size(608, 24);
            this.lblTcpDescription.TabIndex = 0;
            this.lblTcpDescription.Text = "Monitor and manage network packet connections based on the selected protocol.";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 367);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(640, 22);
            this.statusStrip.TabIndex = 2;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Ready";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(640, 389);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.statusStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Network Packet Analyzer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tab_ListMonitor.ResumeLayout(false);
            this.tab_ListMonitor.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private MenuStrip menuStrip;
        private TabControl tabControl;
        private TabPage tab_Home;
        private TabPage tab_ListMonitor;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripStatusLabel;
        private Label lblTcpDescription;
        private ListView listViewConnections;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem toolKitToolStripMenuItem;
        private ToolStripMenuItem portScannerToolStripMenuItem;
        private ToolStripMenuItem pingSnifferToolStripMenuItem;
        private ToolStripMenuItem dHPCToolStripMenuItem;
        private ToolStripMenuItem dNSToolStripMenuItem;
        private ToolStripMenuItem filterMonitorToolStripMenuItem;
        private ToolStripMenuItem iPToolStripMenuItem;
        private ToolStripMenuItem otherToolStripMenuItem;
        private ToolStripMenuItem iPToolStripMenuItem1;
        private ToolStripMenuItem tCPToolStripMenuItem;
        private ToolStripMenuItem uDPToolStripMenuItem;
        private ToolStripMenuItem aRPToolStripMenuItem;
        private ToolStripMenuItem iCMPToolStripMenuItem;
        private Label LB_Filter;
        private TextBox tbt_Filter;
    }
}
