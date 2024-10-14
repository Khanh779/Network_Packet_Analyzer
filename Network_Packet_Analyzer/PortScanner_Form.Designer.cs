namespace Network_Packet_Analyzer
{
    partial class PortScanner_Form
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.txtStartPort = new System.Windows.Forms.TextBox();
            this.txtEndPort = new System.Windows.Forms.TextBox();
            this.btnScan = new System.Windows.Forms.Button();
            this.listResults = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(17, 26);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(318, 20);
            this.txtIPAddress.TabIndex = 0;
            // 
            // txtStartPort
            // 
            this.txtStartPort.Location = new System.Drawing.Point(17, 69);
            this.txtStartPort.Name = "txtStartPort";
            this.txtStartPort.Size = new System.Drawing.Size(146, 20);
            this.txtStartPort.TabIndex = 1;
            this.txtStartPort.Text = "0";
            // 
            // txtEndPort
            // 
            this.txtEndPort.Location = new System.Drawing.Point(189, 69);
            this.txtEndPort.Name = "txtEndPort";
            this.txtEndPort.Size = new System.Drawing.Size(146, 20);
            this.txtEndPort.TabIndex = 2;
            this.txtEndPort.Text = "255";
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(17, 98);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(317, 26);
            this.btnScan.TabIndex = 3;
            this.btnScan.Text = "Scan Ports";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // listResults
            // 
            this.listResults.FormattingEnabled = true;
            this.listResults.IntegralHeight = false;
            this.listResults.Items.AddRange(new object[] {
            "sd",
            "sd",
            "df",
            "ssaf",
            "s"});
            this.listResults.Location = new System.Drawing.Point(17, 132);
            this.listResults.Name = "listResults";
            this.listResults.Size = new System.Drawing.Size(318, 90);
            this.listResults.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "IP Address:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Port Range: Start";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(189, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "to";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 231);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(360, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 8;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Ready";
            // 
            // PortScanner_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 253);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listResults);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.txtEndPort);
            this.Controls.Add(this.txtStartPort);
            this.Controls.Add(this.txtIPAddress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PortScanner_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Port Scanner";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIPAddress;
        private System.Windows.Forms.TextBox txtStartPort;
        private System.Windows.Forms.TextBox txtEndPort;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.ListBox listResults;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
    }
}
