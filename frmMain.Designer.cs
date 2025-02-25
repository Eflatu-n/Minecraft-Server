namespace Minecraft_Server
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnStartServer = new Button();
            btnKillServer = new Button();
            lstConsole = new ListBox();
            lblServerIP = new Label();
            lblPublicIP = new Label();
            btnRestart = new Button();
            txtConsole = new TextBox();
            label1 = new Label();
            label2 = new Label();
            btnSettings = new Button();
            lblPlayerCount = new Label();
            cmbRam = new ComboBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // btnStartServer
            // 
            btnStartServer.Location = new Point(12, 12);
            btnStartServer.Name = "btnStartServer";
            btnStartServer.Size = new Size(75, 67);
            btnStartServer.TabIndex = 0;
            btnStartServer.Text = "Sunucuyu Başlat";
            btnStartServer.UseVisualStyleBackColor = true;
            btnStartServer.Click += btnStartServer_Click;
            // 
            // btnKillServer
            // 
            btnKillServer.Location = new Point(93, 12);
            btnKillServer.Name = "btnKillServer";
            btnKillServer.Size = new Size(75, 67);
            btnKillServer.TabIndex = 1;
            btnKillServer.Text = "Sunucuyu Devre Dışı Bırak";
            btnKillServer.UseVisualStyleBackColor = true;
            btnKillServer.Click += btnKillServer_Click;
            // 
            // lstConsole
            // 
            lstConsole.Dock = DockStyle.Right;
            lstConsole.FormattingEnabled = true;
            lstConsole.ItemHeight = 15;
            lstConsole.Location = new Point(322, 0);
            lstConsole.Name = "lstConsole";
            lstConsole.Size = new Size(768, 538);
            lstConsole.TabIndex = 2;
            // 
            // lblServerIP
            // 
            lblServerIP.AutoSize = true;
            lblServerIP.Location = new Point(25, 156);
            lblServerIP.Name = "lblServerIP";
            lblServerIP.Size = new Size(45, 15);
            lblServerIP.TabIndex = 3;
            lblServerIP.Text = "Yerel IP";
            // 
            // lblPublicIP
            // 
            lblPublicIP.AutoSize = true;
            lblPublicIP.Location = new Point(25, 202);
            lblPublicIP.Name = "lblPublicIP";
            lblPublicIP.Size = new Size(51, 15);
            lblPublicIP.TabIndex = 4;
            lblPublicIP.Text = "Harici IP";
            // 
            // btnRestart
            // 
            btnRestart.Enabled = false;
            btnRestart.Location = new Point(174, 12);
            btnRestart.Name = "btnRestart";
            btnRestart.Size = new Size(75, 67);
            btnRestart.TabIndex = 5;
            btnRestart.Text = "Yeniden Başlat";
            btnRestart.UseVisualStyleBackColor = true;
            btnRestart.Visible = false;
            btnRestart.Click += btnRestart_Click;
            // 
            // txtConsole
            // 
            txtConsole.Location = new Point(12, 503);
            txtConsole.Name = "txtConsole";
            txtConsole.Size = new Size(304, 23);
            txtConsole.TabIndex = 6;
            txtConsole.KeyDown += txtConsole_KeyDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 485);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 7;
            label1.Text = "Konsol";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 444);
            label2.Name = "label2";
            label2.Size = new Size(279, 15);
            label2.TabIndex = 9;
            label2.Text = "Modem Arayüzünde UPnP Açık Olmak Zorundadır !";
            // 
            // btnSettings
            // 
            btnSettings.Location = new Point(174, 12);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(75, 67);
            btnSettings.TabIndex = 10;
            btnSettings.Text = "Ayarlar";
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // lblPlayerCount
            // 
            lblPlayerCount.AutoSize = true;
            lblPlayerCount.Location = new Point(25, 109);
            lblPlayerCount.Name = "lblPlayerCount";
            lblPlayerCount.Size = new Size(80, 15);
            lblPlayerCount.TabIndex = 11;
            lblPlayerCount.Text = "Aktif Oyuncu:";
            // 
            // cmbRam
            // 
            cmbRam.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRam.FormattingEnabled = true;
            cmbRam.Location = new Point(25, 257);
            cmbRam.Name = "cmbRam";
            cmbRam.Size = new Size(121, 23);
            cmbRam.TabIndex = 12;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(25, 239);
            label3.Name = "label3";
            label3.Size = new Size(71, 15);
            label3.TabIndex = 13;
            label3.Text = "Ram Miktarı";
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1090, 538);
            Controls.Add(label3);
            Controls.Add(cmbRam);
            Controls.Add(lblPlayerCount);
            Controls.Add(btnSettings);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtConsole);
            Controls.Add(btnRestart);
            Controls.Add(lblPublicIP);
            Controls.Add(lblServerIP);
            Controls.Add(lstConsole);
            Controls.Add(btnKillServer);
            Controls.Add(btnStartServer);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmMain";
            Text = "Sunucu Paneli";
            FormClosing += frmMain_FormClosing;
            Load += frmMain_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnStartServer;
        private Button btnKillServer;
        private ListBox lstConsole;
        private Label lblServerIP;
        private Label lblPublicIP;
        private Button btnRestart;
        private TextBox txtConsole;
        private Label label1;
        private Label label2;
        private Button btnSettings;
        private Label lblPlayerCount;
        private ComboBox cmbRam;
        private Label label3;
    }
}
