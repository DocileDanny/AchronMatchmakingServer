namespace AchronMatchmaker
{
    partial class achronForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(achronForm));
            this.btnHost = new System.Windows.Forms.Button();
            this.upnpEnabled = new System.Windows.Forms.CheckBox();
            this.btnJoinServer = new System.Windows.Forms.Button();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.lblIP = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtJoinPort = new System.Windows.Forms.TextBox();
            this.lblPort2 = new System.Windows.Forms.Label();
            this.txtHostPort = new System.Windows.Forms.TextBox();
            this.serverBox = new System.Windows.Forms.GroupBox();
            this.btnDetect = new System.Windows.Forms.Button();
            this.lblExternalIP = new System.Windows.Forms.Label();
            this.txtExternalIP = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.Label();
            this.txtStatusB = new System.Windows.Forms.Label();
            this.txtServerStatus = new System.Windows.Forms.Label();
            this.txtClientStatus = new System.Windows.Forms.Label();
            this.outputTimer = new System.Windows.Forms.Timer(this.components);
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.btnDebug = new System.Windows.Forms.Button();
            this.ofdClient = new System.Windows.Forms.OpenFileDialog();
            this.btnPatch = new System.Windows.Forms.Button();
            this.lblPatch = new System.Windows.Forms.Label();
            this.serverBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnHost
            // 
            this.btnHost.BackColor = System.Drawing.SystemColors.Desktop;
            this.btnHost.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHost.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnHost.Location = new System.Drawing.Point(6, 151);
            this.btnHost.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnHost.Name = "btnHost";
            this.btnHost.Size = new System.Drawing.Size(146, 39);
            this.btnHost.TabIndex = 0;
            this.btnHost.Text = "Start Server";
            this.btnHost.UseVisualStyleBackColor = false;
            this.btnHost.Click += new System.EventHandler(this.btnHost_Click);
            // 
            // upnpEnabled
            // 
            this.upnpEnabled.AutoSize = true;
            this.upnpEnabled.Checked = true;
            this.upnpEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.upnpEnabled.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.upnpEnabled.FlatAppearance.CheckedBackColor = System.Drawing.Color.Black;
            this.upnpEnabled.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.upnpEnabled.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.upnpEnabled.ForeColor = System.Drawing.SystemColors.Highlight;
            this.upnpEnabled.Location = new System.Drawing.Point(6, 16);
            this.upnpEnabled.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.upnpEnabled.Name = "upnpEnabled";
            this.upnpEnabled.Size = new System.Drawing.Size(109, 21);
            this.upnpEnabled.TabIndex = 1;
            this.upnpEnabled.Text = "enable upnp";
            this.upnpEnabled.UseVisualStyleBackColor = true;
            // 
            // btnJoinServer
            // 
            this.btnJoinServer.BackColor = System.Drawing.SystemColors.Desktop;
            this.btnJoinServer.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnJoinServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJoinServer.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnJoinServer.Location = new System.Drawing.Point(6, 122);
            this.btnJoinServer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnJoinServer.Name = "btnJoinServer";
            this.btnJoinServer.Size = new System.Drawing.Size(146, 39);
            this.btnJoinServer.TabIndex = 2;
            this.btnJoinServer.Text = "Join Server";
            this.btnJoinServer.UseVisualStyleBackColor = false;
            this.btnJoinServer.Click += new System.EventHandler(this.btnJoinServer_Click);
            // 
            // txtIP
            // 
            this.txtIP.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIP.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtIP.Location = new System.Drawing.Point(6, 36);
            this.txtIP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(146, 22);
            this.txtIP.TabIndex = 3;
            this.txtIP.Text = "127.0.0.1";
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblIP.Location = new System.Drawing.Point(6, 17);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(109, 17);
            this.lblIP.TabIndex = 4;
            this.lblIP.Text = "Host IP Address";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblPort.Location = new System.Drawing.Point(6, 67);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(67, 17);
            this.lblPort.TabIndex = 6;
            this.lblPort.Text = "Host Port";
            // 
            // txtJoinPort
            // 
            this.txtJoinPort.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtJoinPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJoinPort.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtJoinPort.Location = new System.Drawing.Point(6, 86);
            this.txtJoinPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtJoinPort.Name = "txtJoinPort";
            this.txtJoinPort.Size = new System.Drawing.Size(146, 22);
            this.txtJoinPort.TabIndex = 5;
            this.txtJoinPort.Text = "42305";
            // 
            // lblPort2
            // 
            this.lblPort2.AutoSize = true;
            this.lblPort2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblPort2.Location = new System.Drawing.Point(3, 91);
            this.lblPort2.Name = "lblPort2";
            this.lblPort2.Size = new System.Drawing.Size(34, 17);
            this.lblPort2.TabIndex = 8;
            this.lblPort2.Text = "Port";
            // 
            // txtHostPort
            // 
            this.txtHostPort.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtHostPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHostPort.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtHostPort.Location = new System.Drawing.Point(6, 115);
            this.txtHostPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtHostPort.Name = "txtHostPort";
            this.txtHostPort.Size = new System.Drawing.Size(146, 22);
            this.txtHostPort.TabIndex = 7;
            this.txtHostPort.Text = "42305";
            // 
            // serverBox
            // 
            this.serverBox.Controls.Add(this.btnDetect);
            this.serverBox.Controls.Add(this.lblExternalIP);
            this.serverBox.Controls.Add(this.txtExternalIP);
            this.serverBox.Controls.Add(this.upnpEnabled);
            this.serverBox.Controls.Add(this.lblPort2);
            this.serverBox.Controls.Add(this.btnHost);
            this.serverBox.Controls.Add(this.txtHostPort);
            this.serverBox.Location = new System.Drawing.Point(12, 26);
            this.serverBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.serverBox.Name = "serverBox";
            this.serverBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.serverBox.Size = new System.Drawing.Size(158, 198);
            this.serverBox.TabIndex = 9;
            this.serverBox.TabStop = false;
            // 
            // btnDetect
            // 
            this.btnDetect.BackColor = System.Drawing.SystemColors.Desktop;
            this.btnDetect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetect.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnDetect.Location = new System.Drawing.Point(6, 36);
            this.btnDetect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDetect.Name = "btnDetect";
            this.btnDetect.Size = new System.Drawing.Size(72, 27);
            this.btnDetect.TabIndex = 11;
            this.btnDetect.Text = "detect";
            this.btnDetect.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDetect.UseVisualStyleBackColor = false;
            this.btnDetect.Click += new System.EventHandler(this.btnDetect_Click);
            // 
            // lblExternalIP
            // 
            this.lblExternalIP.AutoSize = true;
            this.lblExternalIP.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblExternalIP.Location = new System.Drawing.Point(77, 41);
            this.lblExternalIP.Name = "lblExternalIP";
            this.lblExternalIP.Size = new System.Drawing.Size(75, 17);
            this.lblExternalIP.TabIndex = 10;
            this.lblExternalIP.Text = "External IP";
            // 
            // txtExternalIP
            // 
            this.txtExternalIP.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtExternalIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExternalIP.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtExternalIP.Location = new System.Drawing.Point(6, 67);
            this.txtExternalIP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtExternalIP.Name = "txtExternalIP";
            this.txtExternalIP.ReadOnly = true;
            this.txtExternalIP.Size = new System.Drawing.Size(146, 22);
            this.txtExternalIP.TabIndex = 9;
            this.txtExternalIP.Text = "127.0.0.1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtIP);
            this.groupBox1.Controls.Add(this.btnJoinServer);
            this.groupBox1.Controls.Add(this.lblPort);
            this.groupBox1.Controls.Add(this.lblIP);
            this.groupBox1.Controls.Add(this.txtJoinPort);
            this.groupBox1.Location = new System.Drawing.Point(670, 26);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(158, 170);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "Host";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(667, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Join";
            // 
            // txtStatus
            // 
            this.txtStatus.AutoSize = true;
            this.txtStatus.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtStatus.Location = new System.Drawing.Point(9, 226);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(52, 17);
            this.txtStatus.TabIndex = 13;
            this.txtStatus.Text = "Status:";
            // 
            // txtStatusB
            // 
            this.txtStatusB.AutoSize = true;
            this.txtStatusB.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtStatusB.Location = new System.Drawing.Point(667, 199);
            this.txtStatusB.Name = "txtStatusB";
            this.txtStatusB.Size = new System.Drawing.Size(52, 17);
            this.txtStatusB.TabIndex = 14;
            this.txtStatusB.Text = "Status:";
            // 
            // txtServerStatus
            // 
            this.txtServerStatus.AutoSize = true;
            this.txtServerStatus.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtServerStatus.Location = new System.Drawing.Point(9, 243);
            this.txtServerStatus.Name = "txtServerStatus";
            this.txtServerStatus.Size = new System.Drawing.Size(49, 17);
            this.txtServerStatus.TabIndex = 15;
            this.txtServerStatus.Text = "Offline";
            // 
            // txtClientStatus
            // 
            this.txtClientStatus.AutoSize = true;
            this.txtClientStatus.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtClientStatus.Location = new System.Drawing.Point(667, 216);
            this.txtClientStatus.Name = "txtClientStatus";
            this.txtClientStatus.Size = new System.Drawing.Size(94, 17);
            this.txtClientStatus.TabIndex = 16;
            this.txtClientStatus.Text = "Disconnected";
            // 
            // outputTimer
            // 
            this.outputTimer.Enabled = true;
            this.outputTimer.Tick += new System.EventHandler(this.outputTimer_Tick);
            // 
            // txtConsole
            // 
            this.txtConsole.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtConsole.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConsole.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtConsole.Location = new System.Drawing.Point(179, 9);
            this.txtConsole.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.Size = new System.Drawing.Size(482, 295);
            this.txtConsole.TabIndex = 12;
            this.txtConsole.Visible = false;
            // 
            // btnDebug
            // 
            this.btnDebug.BackColor = System.Drawing.SystemColors.Desktop;
            this.btnDebug.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDebug.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnDebug.Location = new System.Drawing.Point(12, 277);
            this.btnDebug.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(146, 27);
            this.btnDebug.TabIndex = 12;
            this.btnDebug.Text = "Show/Hide Debug";
            this.btnDebug.UseVisualStyleBackColor = false;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // ofdClient
            // 
            this.ofdClient.FileName = "selectFile";
            // 
            // btnPatch
            // 
            this.btnPatch.BackColor = System.Drawing.SystemColors.Desktop;
            this.btnPatch.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnPatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPatch.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnPatch.Location = new System.Drawing.Point(670, 265);
            this.btnPatch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPatch.Name = "btnPatch";
            this.btnPatch.Size = new System.Drawing.Size(158, 39);
            this.btnPatch.TabIndex = 7;
            this.btnPatch.Text = "Patch Client";
            this.btnPatch.UseVisualStyleBackColor = false;
            this.btnPatch.Click += new System.EventHandler(this.btnPatch_Click);
            // 
            // lblPatch
            // 
            this.lblPatch.AutoSize = true;
            this.lblPatch.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblPatch.Location = new System.Drawing.Point(667, 246);
            this.lblPatch.Name = "lblPatch";
            this.lblPatch.Size = new System.Drawing.Size(0, 17);
            this.lblPatch.TabIndex = 17;
            // 
            // achronForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImage = global::AchronMatchmaker.Properties.Resources.achron;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(840, 315);
            this.Controls.Add(this.lblPatch);
            this.Controls.Add(this.btnPatch);
            this.Controls.Add(this.btnDebug);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.txtClientStatus);
            this.Controls.Add(this.txtServerStatus);
            this.Controls.Add(this.txtStatusB);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.serverBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "achronForm";
            this.Text = "Achron Matchmaker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.achronForm_FormClosing);
            this.Load += new System.EventHandler(this.achronForm_Load);
            this.serverBox.ResumeLayout(false);
            this.serverBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnHost;
        private System.Windows.Forms.CheckBox upnpEnabled;
        private System.Windows.Forms.Button btnJoinServer;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtJoinPort;
        private System.Windows.Forms.Label lblPort2;
        private System.Windows.Forms.TextBox txtHostPort;
        private System.Windows.Forms.GroupBox serverBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblExternalIP;
        private System.Windows.Forms.TextBox txtExternalIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDetect;
        private System.Windows.Forms.Label txtStatus;
        private System.Windows.Forms.Label txtStatusB;
        private System.Windows.Forms.Label txtServerStatus;
        private System.Windows.Forms.Label txtClientStatus;
        private System.Windows.Forms.Timer outputTimer;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.Button btnDebug;
        private System.Windows.Forms.OpenFileDialog ofdClient;
        private System.Windows.Forms.Button btnPatch;
        private System.Windows.Forms.Label lblPatch;
    }
}