namespace Network_Packet_Analyzer
{
    partial class PingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PingForm));
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.btnPing = new System.Windows.Forms.Button();
            this.rtbResults = new System.Windows.Forms.RichTextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIPAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIPAddress.Location = new System.Drawing.Point(17, 17);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(325, 20);
            this.txtIPAddress.TabIndex = 0;
            // 
            // btnPing
            // 
            this.btnPing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPing.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnPing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPing.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.btnPing.ForeColor = System.Drawing.Color.White;
            this.btnPing.Location = new System.Drawing.Point(17, 52);
            this.btnPing.Name = "btnPing";
            this.btnPing.Size = new System.Drawing.Size(324, 30);
            this.btnPing.TabIndex = 1;
            this.btnPing.Text = "Ping";
            this.btnPing.UseVisualStyleBackColor = false;
            this.btnPing.Click += new System.EventHandler(this.btnPing_Click);
            // 
            // rtbResults
            // 
            this.rtbResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbResults.Location = new System.Drawing.Point(17, 91);
            this.rtbResults.Name = "rtbResults";
            this.rtbResults.ReadOnly = true;
            this.rtbResults.Size = new System.Drawing.Size(325, 109);
            this.rtbResults.TabIndex = 2;
            this.rtbResults.Text = "";
            this.rtbResults.WordWrap = false;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.ForeColor = System.Drawing.Color.LightGreen;
            this.progressBar.Location = new System.Drawing.Point(17, 203);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(324, 10);
            this.progressBar.TabIndex = 3;
            // 
            // PingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 229);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.rtbResults);
            this.Controls.Add(this.btnPing);
            this.Controls.Add(this.txtIPAddress);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ping Sniffer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #region Windows Form Designer generated code

        private System.Windows.Forms.TextBox txtIPAddress;
        private System.Windows.Forms.Button btnPing;
        private System.Windows.Forms.RichTextBox rtbResults;
        private System.Windows.Forms.ProgressBar progressBar;

        #endregion
    }
}
