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
            this.listViewConnections = new System.Windows.Forms.ListView();
            this.columnHeaderID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLocalAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLocalPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRemoteAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRemotePort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CH_MAC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderProtocol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderProcName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelFilter = new System.Windows.Forms.Label();
            this.comboBoxStateFilter = new System.Windows.Forms.ComboBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewConnections
            // 
            this.listViewConnections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewConnections.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderID,
            this.columnHeaderLocalAddress,
            this.columnHeaderLocalPort,
            this.columnHeaderRemoteAddress,
            this.columnHeaderRemotePort,
            this.CH_MAC,
            this.columnHeaderProtocol,
            this.columnHeaderState,
            this.columnHeaderProcName});
            this.listViewConnections.FullRowSelect = true;
            this.listViewConnections.GridLines = true;
            this.listViewConnections.HideSelection = false;
            this.listViewConnections.Location = new System.Drawing.Point(12, 47);
            this.listViewConnections.Name = "listViewConnections";
            this.listViewConnections.Size = new System.Drawing.Size(641, 326);
            this.listViewConnections.TabIndex = 0;
            this.listViewConnections.UseCompatibleStateImageBehavior = false;
            this.listViewConnections.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderID
            // 
            this.columnHeaderID.Text = "ID";
            this.columnHeaderID.Width = 55;
            // 
            // columnHeaderLocalAddress
            // 
            this.columnHeaderLocalAddress.Text = "Local Address";
            this.columnHeaderLocalAddress.Width = 110;
            // 
            // columnHeaderLocalPort
            // 
            this.columnHeaderLocalPort.Text = "Local Port";
            this.columnHeaderLocalPort.Width = 62;
            // 
            // columnHeaderRemoteAddress
            // 
            this.columnHeaderRemoteAddress.Text = "Remote Address";
            this.columnHeaderRemoteAddress.Width = 110;
            // 
            // columnHeaderRemotePort
            // 
            this.columnHeaderRemotePort.Text = "Remote Port";
            this.columnHeaderRemotePort.Width = 75;
            // 
            // CH_MAC
            // 
            this.CH_MAC.Text = "MAC Address";
            this.CH_MAC.Width = 120;
            // 
            // columnHeaderProtocol
            // 
            this.columnHeaderProtocol.Text = "Protocol";
            // 
            // columnHeaderState
            // 
            this.columnHeaderState.Text = "State";
            this.columnHeaderState.Width = 50;
            // 
            // columnHeaderProcName
            // 
            this.columnHeaderProcName.Text = "Process Name";
            // 
            // labelFilter
            // 
            this.labelFilter.AutoSize = true;
            this.labelFilter.Location = new System.Drawing.Point(12, 18);
            this.labelFilter.Name = "labelFilter";
            this.labelFilter.Size = new System.Drawing.Size(74, 13);
            this.labelFilter.TabIndex = 1;
            this.labelFilter.Text = "Filter by State:";
            // 
            // comboBoxStateFilter
            // 
            this.comboBoxStateFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStateFilter.FormattingEnabled = true;
            this.comboBoxStateFilter.Location = new System.Drawing.Point(92, 15);
            this.comboBoxStateFilter.Name = "comboBoxStateFilter";
            this.comboBoxStateFilter.Size = new System.Drawing.Size(150, 21);
            this.comboBoxStateFilter.TabIndex = 2;
            this.comboBoxStateFilter.SelectedIndexChanged += new System.EventHandler(this.comboBoxStateFilter_SelectedIndexChanged);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 376);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(665, 22);
            this.statusStrip.TabIndex = 3;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(145, 17);
            this.toolStripStatusLabel.Text = "Number of connections: 0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 398);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.comboBoxStateFilter);
            this.Controls.Add(this.labelFilter);
            this.Controls.Add(this.listViewConnections);
            this.Name = "Form1";
            this.Text = "Network Traffic Monitor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private ListView listViewConnections;
        private ColumnHeader columnHeaderID;
        private ColumnHeader columnHeaderLocalAddress;
        private ColumnHeader columnHeaderLocalPort;
        private ColumnHeader columnHeaderRemoteAddress;
        private ColumnHeader columnHeaderRemotePort;
        private ColumnHeader columnHeaderProtocol;
        private ColumnHeader columnHeaderState;
        private ColumnHeader columnHeaderProcName;
        private Label labelFilter;
        private ComboBox comboBoxStateFilter;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripStatusLabel;
        private ColumnHeader CH_MAC;
    }
}
