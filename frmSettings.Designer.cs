namespace Minecraft_Server
{
    partial class frmSettings
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
            btnResetOverworld = new Button();
            btnResetNether = new Button();
            btnResetTheEnd = new Button();
            cmbDifficulty = new ComboBox();
            label1 = new Label();
            cmbPVP = new ComboBox();
            label2 = new Label();
            txtPlayerCount = new TextBox();
            label3 = new Label();
            txtMotd = new TextBox();
            label4 = new Label();
            cmbOnlineMode = new ComboBox();
            label5 = new Label();
            btnResetServer = new Button();
            cmbWhitelist = new ComboBox();
            lblWhitelist = new Label();
            lstWhitelist = new ListBox();
            label6 = new Label();
            txtAddPlayerToWhitelist = new TextBox();
            label7 = new Label();
            btnDeletePlayer = new Button();
            cmbChunks = new ComboBox();
            lblChunks = new Label();
            cmbHardcore = new ComboBox();
            label8 = new Label();
            txtResourcePack = new TextBox();
            label9 = new Label();
            btnSaveResourcePack = new Button();
            SuspendLayout();
            // 
            // btnResetOverworld
            // 
            btnResetOverworld.Location = new Point(12, 12);
            btnResetOverworld.Name = "btnResetOverworld";
            btnResetOverworld.Size = new Size(78, 72);
            btnResetOverworld.TabIndex = 0;
            btnResetOverworld.Text = "Dünyayı Sıfırla";
            btnResetOverworld.UseVisualStyleBackColor = true;
            btnResetOverworld.Click += btnResetOverworld_Click;
            // 
            // btnResetNether
            // 
            btnResetNether.Location = new Point(110, 12);
            btnResetNether.Name = "btnResetNether";
            btnResetNether.Size = new Size(78, 72);
            btnResetNether.TabIndex = 1;
            btnResetNether.Text = "Netheri Sıfırla";
            btnResetNether.UseVisualStyleBackColor = true;
            btnResetNether.Click += btnResetNether_Click;
            // 
            // btnResetTheEnd
            // 
            btnResetTheEnd.Location = new Point(211, 12);
            btnResetTheEnd.Name = "btnResetTheEnd";
            btnResetTheEnd.Size = new Size(78, 72);
            btnResetTheEnd.TabIndex = 2;
            btnResetTheEnd.Text = "End'i Sıfırla";
            btnResetTheEnd.UseVisualStyleBackColor = true;
            btnResetTheEnd.Click += btnResetTheEnd_Click;
            // 
            // cmbDifficulty
            // 
            cmbDifficulty.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDifficulty.FormattingEnabled = true;
            cmbDifficulty.Location = new Point(12, 124);
            cmbDifficulty.Name = "cmbDifficulty";
            cmbDifficulty.Size = new Size(119, 23);
            cmbDifficulty.TabIndex = 3;
            cmbDifficulty.SelectedIndexChanged += cmbDifficulty_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 106);
            label1.Name = "label1";
            label1.Size = new Size(41, 15);
            label1.TabIndex = 4;
            label1.Text = "Zorluk";
            // 
            // cmbPVP
            // 
            cmbPVP.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPVP.FormattingEnabled = true;
            cmbPVP.Location = new Point(12, 196);
            cmbPVP.Name = "cmbPVP";
            cmbPVP.Size = new Size(119, 23);
            cmbPVP.TabIndex = 5;
            cmbPVP.SelectedIndexChanged += cmbPVP_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 178);
            label2.Name = "label2";
            label2.Size = new Size(28, 15);
            label2.TabIndex = 6;
            label2.Text = "PVP";
            // 
            // txtPlayerCount
            // 
            txtPlayerCount.Location = new Point(12, 263);
            txtPlayerCount.Name = "txtPlayerCount";
            txtPlayerCount.Size = new Size(119, 23);
            txtPlayerCount.TabIndex = 7;
            txtPlayerCount.KeyDown += txtPlayerCount_KeyDown;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 245);
            label3.Name = "label3";
            label3.Size = new Size(144, 15);
            label3.TabIndex = 8;
            label3.Text = "Maksimum Oyuncu Sayısı";
            // 
            // txtMotd
            // 
            txtMotd.Location = new Point(12, 328);
            txtMotd.Name = "txtMotd";
            txtMotd.Size = new Size(119, 23);
            txtMotd.TabIndex = 9;
            txtMotd.KeyDown += txtMotd_KeyDown;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 310);
            label4.Name = "label4";
            label4.Size = new Size(107, 15);
            label4.TabIndex = 10;
            label4.Text = "Sunucu Açıklaması";
            // 
            // cmbOnlineMode
            // 
            cmbOnlineMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOnlineMode.FormattingEnabled = true;
            cmbOnlineMode.Location = new Point(12, 396);
            cmbOnlineMode.Name = "cmbOnlineMode";
            cmbOnlineMode.Size = new Size(121, 23);
            cmbOnlineMode.TabIndex = 11;
            cmbOnlineMode.SelectedIndexChanged += cmbOnlineMode_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 378);
            label5.Name = "label5";
            label5.Size = new Size(70, 15);
            label5.TabIndex = 12;
            label5.Text = "Online Mod";
            // 
            // btnResetServer
            // 
            btnResetServer.Location = new Point(307, 12);
            btnResetServer.Name = "btnResetServer";
            btnResetServer.Size = new Size(78, 72);
            btnResetServer.TabIndex = 13;
            btnResetServer.Text = "Sunucuyu Sıfırla";
            btnResetServer.UseVisualStyleBackColor = true;
            btnResetServer.Click += btnResetServer_Click;
            // 
            // cmbWhitelist
            // 
            cmbWhitelist.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbWhitelist.FormattingEnabled = true;
            cmbWhitelist.Location = new Point(180, 124);
            cmbWhitelist.Name = "cmbWhitelist";
            cmbWhitelist.Size = new Size(121, 23);
            cmbWhitelist.TabIndex = 14;
            cmbWhitelist.SelectedIndexChanged += cmbWhitelist_SelectedIndexChanged;
            // 
            // lblWhitelist
            // 
            lblWhitelist.AutoSize = true;
            lblWhitelist.Location = new Point(180, 106);
            lblWhitelist.Name = "lblWhitelist";
            lblWhitelist.Size = new Size(53, 15);
            lblWhitelist.TabIndex = 15;
            lblWhitelist.Text = "Whitelist";
            // 
            // lstWhitelist
            // 
            lstWhitelist.FormattingEnabled = true;
            lstWhitelist.ItemHeight = 15;
            lstWhitelist.Location = new Point(180, 196);
            lstWhitelist.Name = "lstWhitelist";
            lstWhitelist.Size = new Size(190, 169);
            lstWhitelist.TabIndex = 16;
            lstWhitelist.SelectedIndexChanged += lstWhitelist_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(180, 178);
            label6.Name = "label6";
            label6.Size = new Size(130, 15);
            label6.TabIndex = 17;
            label6.Text = "Whitelistteki Oyuncular";
            // 
            // txtAddPlayerToWhitelist
            // 
            txtAddPlayerToWhitelist.Location = new Point(180, 396);
            txtAddPlayerToWhitelist.Name = "txtAddPlayerToWhitelist";
            txtAddPlayerToWhitelist.Size = new Size(130, 23);
            txtAddPlayerToWhitelist.TabIndex = 18;
            txtAddPlayerToWhitelist.TextChanged += txtAddPlayerToWhitelist_TextChanged;
            txtAddPlayerToWhitelist.KeyDown += txtAddPlayerToWhitelist_KeyDown;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(180, 378);
            label7.Name = "label7";
            label7.Size = new Size(128, 15);
            label7.TabIndex = 19;
            label7.Text = "Whiteliste Oyuncu Ekle";
            // 
            // btnDeletePlayer
            // 
            btnDeletePlayer.Location = new Point(179, 461);
            btnDeletePlayer.Name = "btnDeletePlayer";
            btnDeletePlayer.Size = new Size(131, 23);
            btnDeletePlayer.TabIndex = 20;
            btnDeletePlayer.Text = "Seçili Oyuncuyu Sil";
            btnDeletePlayer.UseVisualStyleBackColor = true;
            btnDeletePlayer.Click += btnDeletePlayer_Click;
            // 
            // cmbChunks
            // 
            cmbChunks.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbChunks.FormattingEnabled = true;
            cmbChunks.Location = new Point(12, 461);
            cmbChunks.Name = "cmbChunks";
            cmbChunks.Size = new Size(121, 23);
            cmbChunks.TabIndex = 21;
            cmbChunks.SelectedIndexChanged += cmbChunks_SelectedIndexChanged;
            // 
            // lblChunks
            // 
            lblChunks.AutoSize = true;
            lblChunks.Location = new Point(15, 442);
            lblChunks.Name = "lblChunks";
            lblChunks.Size = new Size(87, 15);
            lblChunks.TabIndex = 22;
            lblChunks.Text = "Görüş Mesafesi";
            // 
            // cmbHardcore
            // 
            cmbHardcore.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbHardcore.FormattingEnabled = true;
            cmbHardcore.Location = new Point(10, 527);
            cmbHardcore.Name = "cmbHardcore";
            cmbHardcore.Size = new Size(123, 23);
            cmbHardcore.TabIndex = 23;
            cmbHardcore.SelectedIndexChanged += cmbHardcore_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(10, 509);
            label8.Name = "label8";
            label8.Size = new Size(56, 15);
            label8.TabIndex = 24;
            label8.Text = "Hardcore";
            // 
            // txtResourcePack
            // 
            txtResourcePack.Location = new Point(180, 527);
            txtResourcePack.Name = "txtResourcePack";
            txtResourcePack.Size = new Size(130, 23);
            txtResourcePack.TabIndex = 25;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(179, 509);
            label9.Name = "label9";
            label9.Size = new Size(220, 15);
            label9.TabIndex = 26;
            label9.Text = "Kaynak Paketi(URL Formatında Girilmeli)";
            // 
            // btnSaveResourcePack
            // 
            btnSaveResourcePack.Location = new Point(310, 527);
            btnSaveResourcePack.Name = "btnSaveResourcePack";
            btnSaveResourcePack.Size = new Size(60, 23);
            btnSaveResourcePack.TabIndex = 27;
            btnSaveResourcePack.Text = "Kaydet";
            btnSaveResourcePack.UseVisualStyleBackColor = true;
            btnSaveResourcePack.Click += btnSaveResourcePack_Click;
            // 
            // frmSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(402, 600);
            Controls.Add(btnSaveResourcePack);
            Controls.Add(label9);
            Controls.Add(txtResourcePack);
            Controls.Add(label8);
            Controls.Add(cmbHardcore);
            Controls.Add(lblChunks);
            Controls.Add(cmbChunks);
            Controls.Add(btnDeletePlayer);
            Controls.Add(label7);
            Controls.Add(txtAddPlayerToWhitelist);
            Controls.Add(label6);
            Controls.Add(lstWhitelist);
            Controls.Add(lblWhitelist);
            Controls.Add(cmbWhitelist);
            Controls.Add(btnResetServer);
            Controls.Add(label5);
            Controls.Add(cmbOnlineMode);
            Controls.Add(label4);
            Controls.Add(txtMotd);
            Controls.Add(label3);
            Controls.Add(txtPlayerCount);
            Controls.Add(label2);
            Controls.Add(cmbPVP);
            Controls.Add(label1);
            Controls.Add(cmbDifficulty);
            Controls.Add(btnResetTheEnd);
            Controls.Add(btnResetNether);
            Controls.Add(btnResetOverworld);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmSettings";
            Text = "Sunucu Ayarları";
            Load += frmSettings_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        public Button btnResetNether;
        public Button btnResetTheEnd;
        private ComboBox cmbDifficulty;
        private Label label1;
        private ComboBox cmbPVP;
        private Label label2;
        private TextBox txtPlayerCount;
        private Label label3;
        private TextBox txtMotd;
        private Label label4;
        private ComboBox cmbOnlineMode;
        private Label label5;
        private Button btnResetServer;
        private ComboBox cmbWhitelist;
        private Label lblWhitelist;
        private Label label6;
        private Label label7;
        public Button btnResetOverworld;
        public TextBox txtAddPlayerToWhitelist;
        private Button btnDeletePlayer;
        private ComboBox cmbChunks;
        private Label lblChunks;
        private ComboBox cmbHardcore;
        private Label label8;
        private TextBox txtResourcePack;
        private Label label9;
        private Button btnSaveResourcePack;
        public ListBox lstWhitelist;
    }
}