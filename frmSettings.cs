using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Server
{
    public partial class frmSettings : Form
    {
        public static bool ControlsEnabledState { get; set; } = true;
        private FormHelper formHelper;
        private ServerManager serverManager;
        private WhitelistManager WhitelistManager;

        private static frmSettings _instance;
        public static frmSettings Instance
        {
            get { return _instance; }
        }
        public frmSettings()
        {
            InitializeComponent();
            _instance = this;
            SetControlsEnabled(ControlsEnabledState);
        }
        public static void SetControlsState(bool enabled)
        {
            if (Application.OpenForms["frmSettings"] is frmSettings settings)
            {
                settings.SetControlsEnabled(enabled);
            }
        }
        public void SetControlsEnabled(bool enabled)
        {
            ControlsEnabledState = enabled;

            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => ApplyControlsState(enabled)));
                return;
            }

            ApplyControlsState(enabled);

        }
        private void ApplyControlsState(bool enabled)
        {
            btnResetOverworld.Enabled = enabled;
            btnResetNether.Enabled = enabled;
            btnResetTheEnd.Enabled = enabled;
            txtMotd.Enabled = enabled;
            txtPlayerCount.Enabled = enabled;
            cmbOnlineMode.Enabled = enabled;
            btnResetServer.Enabled = enabled;
            cmbPVP.Enabled = enabled;
            cmbWhitelist.Enabled = enabled;
            cmbHardcore.Enabled = enabled;
            cmbChunks.Enabled = enabled;
            btnSaveResourcePack.Enabled = enabled;
        }

        private bool isWhitelistTrue { get; set; } = false;
        public void btnResetOverworld_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "world");

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true); // Tüm alt klasörleri ve dosyaları sil
                MessageBox.Show("Overworld başarıyla sıfırlandı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Overworld klasörü bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnResetNether_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "world_nether");

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                MessageBox.Show("Nether başarıyla sıfırlandı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Nether klasörü bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnResetTheEnd_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "world_the_end");

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                MessageBox.Show("The End başarıyla sıfırlandı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("The End klasörü bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbDifficulty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            string propertiesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.properties");

            if (File.Exists(propertiesPath))
            {
                string[] lines = File.ReadAllLines(propertiesPath);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("difficulty="))
                    {
                        lines[i] = "difficulty=" + cmbDifficulty.SelectedItem.ToString();
                        break;
                    }
                }

                // Güncellenmiş içeriği tekrar dosyaya yaz
                File.WriteAllLines(propertiesPath, lines);
                MessageBox.Show("Zorluk seviyesi başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("server.properties dosyası bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool isLoading = true;

        private void cmbPVP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            string propertiesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.properties");

            if (File.Exists(propertiesPath))
            {
                string[] lines = File.ReadAllLines(propertiesPath);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("pvp="))
                    {
                        lines[i] = "pvp=" + cmbPVP.SelectedItem.ToString();
                        break;
                    }
                }

                File.WriteAllLines(propertiesPath, lines);
                MessageBox.Show("Pvp tercihi başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("server.properties dosyası bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void AddPlayerToWhitelist()
        {
            Process server = frmMain.Instance.serverProcess;
            if (server != null)
            {
                string playerName = txtAddPlayerToWhitelist.Text.Trim();
                server.StandardInput.WriteLine($"whitelist add {playerName}");
                server.StandardInput.Flush();
                if (string.IsNullOrEmpty(playerName))
                {
                    MessageBox.Show("Lütfen bir oyuncu adı girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (server != null && !server.HasExited)
                {
                    try
                    {
                        server.StandardInput.WriteLine($"whitelist add {playerName}");
                        server.StandardInput.Flush();
                        MessageBox.Show($"{playerName} başarıyla whitelist'e eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAddPlayerToWhitelist.Clear();
                        lstWhitelist.Items.Add(playerName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Oyuncu eklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Sunucu çalışmıyor!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Whitelist()
        {
            string propertiesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.properties");
            if (File.Exists(propertiesPath))
            {
                string[] lines = File.ReadAllLines(propertiesPath);
                foreach (string line in lines)
                {
                    if (line.StartsWith("white-list="))
                    {
                        string currentWhitelist = line.Split('=')[1];
                        if (cmbWhitelist.Items.Contains(currentWhitelist))
                        {
                            cmbWhitelist.SelectedItem = currentWhitelist;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("server.properties dosyası bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            isLoading = false;
        }
        private void Difficulty()
        {
            // Mevcut zorluğu oku ve ComboBox'a ata
            string propertiesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.properties");

            if (File.Exists(propertiesPath))
            {
                string[] lines = File.ReadAllLines(propertiesPath);
                foreach (string line in lines)
                {
                    if (line.StartsWith("difficulty="))
                    {
                        string currentDifficulty = line.Split('=')[1]; // "difficulty=hard" → "hard"
                        if (cmbDifficulty.Items.Contains(currentDifficulty))
                        {
                            cmbDifficulty.SelectedItem = currentDifficulty;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("server.properties dosyası bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            isLoading = false;
        }
        private void Motd()
        {
            string propertiesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.properties");
            if (File.Exists(propertiesPath))
            {
                string[] lines = File.ReadAllLines(propertiesPath);
                foreach (string line in lines)
                {
                    if (line.StartsWith("motd="))
                    {
                        string currentMotd = line.Split('=')[1];
                        txtMotd.Text = currentMotd;
                    }
                }
            }
            else
            {
                MessageBox.Show("server.properties dosyası bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            isLoading = false;
        }
        private void OnlineMode()
        {
            string propertiesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.properties");
            if (File.Exists(propertiesPath))
            {
                string[] lines = File.ReadAllLines(propertiesPath);
                foreach (string line in lines)
                {
                    if (line.StartsWith("online-mode="))
                    {
                        string currentOnlineMode = line.Split('=')[1];
                        if (cmbOnlineMode.Items.Contains(currentOnlineMode))
                        {
                            cmbOnlineMode.SelectedItem = currentOnlineMode;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("server.properties dosyası bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            isLoading = false;
        }
        private void PlayerCount()
        {
            string propertiesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.properties");
            if (File.Exists(propertiesPath))
            {
                string[] lines = File.ReadAllLines(propertiesPath);
                foreach (string line in lines)
                {
                    if (line.StartsWith("max-players="))
                    {
                        string currentMaxPlayers = line.Split('=')[1];
                        txtPlayerCount.Text = currentMaxPlayers;
                    }
                }
            }
            else
            {
                MessageBox.Show("server.properties dosyası bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            isLoading = false;
        }
        private void Chunks()
        {
            string propertiesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.properties");
            if (File.Exists(propertiesPath))
            {
                string[] lines = File.ReadAllLines(propertiesPath);
                foreach (string line in lines)
                {
                    if (line.StartsWith("view - distance="))
                    {
                        string currentChunks = line.Split('=')[1];
                        if (cmbChunks.Items.Contains(currentChunks))
                        {
                            cmbChunks.SelectedItem = currentChunks;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("server.properties dosyası bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            isLoading = false;

        }
        private void PVP()
        {
            string propertiesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.properties");
            if (File.Exists(propertiesPath))
            {
                string[] lines = File.ReadAllLines(propertiesPath);
                foreach (string line in lines)
                {
                    if (line.StartsWith("pvp="))
                    {
                        string currentPVP = line.Split('=')[1];
                        if (cmbPVP.Items.Contains(currentPVP))
                        {
                            cmbPVP.SelectedItem = currentPVP;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("server.properties dosyası bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            isLoading = false;
        }
        private void LoadViewDistance()
        {
            string propertiesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.properties");

            if (File.Exists(propertiesPath))
            {
                string[] lines = File.ReadAllLines(propertiesPath);
                foreach (string line in lines)
                {
                    if (line.StartsWith("view-distance="))
                    {
                        string currentValue = line.Split('=')[1].Trim();
                        int index = cmbChunks.Items.IndexOf(currentValue);
                        if (index != -1)
                        {
                            cmbChunks.SelectedIndex = index;
                        }
                        break;
                    }
                }
            }
        }
        private void Hardcore()
        {
            string propertiesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.properties");
            if (File.Exists(propertiesPath))
            {
                string[] lines = File.ReadAllLines(propertiesPath);
                foreach (string line in lines)
                {
                    if (line.StartsWith("hardcore="))
                    {
                        string currentPVP = line.Split('=')[1];
                        if (cmbHardcore.Items.Contains(currentPVP))
                        {
                            cmbHardcore.SelectedItem = currentPVP;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("server.properties dosyası bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            isLoading = false;
        }

        private void LoadResourcePack()
        {
            string propertiesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.properties");

            if (File.Exists(propertiesPath))
            {
                string[] lines = File.ReadAllLines(propertiesPath);
                foreach (string line in lines)
                {
                    if (line.StartsWith("resource-pack="))
                    {
                        txtResourcePack.Text = line.Split('=')[1].Trim(); // "resource-pack=http://..." → "http://..."
                        break;
                    }
                }
            }
        }


        private void frmSettings_Load(object sender, EventArgs e)
        {
            WhitelistManager.LoadWhiteList();
            txtAddPlayerToWhitelist.CharacterCasing = CharacterCasing.Normal;
            isLoading = true;
            #region combobox items
            cmbDifficulty.Items.Add("peaceful");
            cmbDifficulty.Items.Add("easy");
            cmbDifficulty.Items.Add("normal");
            cmbDifficulty.Items.Add("hard");
            cmbPVP.Items.Add("true");
            cmbPVP.Items.Add("false");
            cmbOnlineMode.Items.Add("true");
            cmbOnlineMode.Items.Add("false");
            cmbWhitelist.Items.Add("true");
            cmbWhitelist.Items.Add("false");
            cmbHardcore.Items.Add("true");
            cmbHardcore.Items.Add("false");
            #endregion 

            #region Chunk Items
            for (int i = 4; i <= 32; i++)
            {
                cmbChunks.Items.Add(i.ToString());
            }
            LoadViewDistance();
            #endregion
            #region Events
            cmbPVP.SelectedIndexChanged -= cmbPVP_SelectedIndexChanged;
            cmbWhitelist.SelectedIndexChanged -= cmbWhitelist_SelectedIndexChanged;
            cmbOnlineMode.SelectedIndexChanged -= cmbOnlineMode_SelectedIndexChanged;
            cmbDifficulty.SelectedIndexChanged -= cmbDifficulty_SelectedIndexChanged;
            cmbChunks.SelectedIndexChanged -= cmbChunks_SelectedIndexChanged;
            cmbHardcore.SelectedIndexChanged -= cmbHardcore_SelectedIndexChanged;
            Difficulty();
            LoadResourcePack();
            PVP();
            PlayerCount();
            Motd();
            OnlineMode();
            Whitelist();
            Chunks();
            Hardcore();
            cmbPVP.SelectedIndexChanged += cmbPVP_SelectedIndexChanged;
            cmbHardcore.SelectedIndexChanged += cmbHardcore_SelectedIndexChanged;
            cmbChunks.SelectedIndexChanged += cmbChunks_SelectedIndexChanged;
            cmbWhitelist.SelectedIndexChanged += cmbWhitelist_SelectedIndexChanged;
            cmbOnlineMode.SelectedIndexChanged += cmbOnlineMode_SelectedIndexChanged;
            cmbDifficulty.SelectedIndexChanged += cmbDifficulty_SelectedIndexChanged;
            #endregion 
            isLoading = false;
        }
        private void txtPlayerCount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                string propertiesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.properties");

                if (File.Exists(propertiesPath))
                {
                    string[] lines = File.ReadAllLines(propertiesPath);

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].StartsWith("max-players="))
                        {
                            lines[i] = "max-players=" + txtPlayerCount.Text;
                            break;
                        }
                    }

                    File.WriteAllLines(propertiesPath, lines);

                    MessageBox.Show("Oyuncu sayısı başarıyla güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("server.properties dosyası bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void txtMotd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                string propertiesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.properties");

                if (File.Exists(propertiesPath))
                {
                    string[] lines = File.ReadAllLines(propertiesPath);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].StartsWith("motd="))
                        {
                            lines[i] = "motd=" + txtMotd.Text;
                            break;
                        }
                    }
                    File.WriteAllLines(propertiesPath, lines);
                    MessageBox.Show("MOTD başarıyla güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("server.properties dosyası bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void cmbOnlineMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            string propertiesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.properties");

            if (File.Exists(propertiesPath))
            {
                string[] lines = File.ReadAllLines(propertiesPath);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("online-mode="))
                    {
                        lines[i] = "online-mode=" + cmbOnlineMode.SelectedItem.ToString();
                        break;
                    }
                }

                File.WriteAllLines(propertiesPath, lines);
                MessageBox.Show("Online mod tercihi başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("server.properties dosyası bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnResetServer_Click(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
            string jarFile = Path.Combine(path, "server.jar");
            string dllFile = Path.Combine(path, "Minecraft Server.dll");
            string exeFile = Path.Combine(path, "Minecraft Server.exe");
            string newtondll = Path.Combine(path, "Newtonsoft.Json.dll");
            string opennatDll = Path.Combine(path, "Open.Nat.dll");
            try
            {
                if (Directory.Exists(path))
                {
                    foreach (string file in Directory.GetFiles(path))
                    {
                        if (file != jarFile && file != dllFile && file != exeFile && file != newtondll && file != opennatDll) // Korumak istediğimiz dosyalar
                        {
                            File.SetAttributes(file, FileAttributes.Normal); // Salt okunurluğu kaldır
                            File.Delete(file);
                        }
                    }
                    foreach (string dir in Directory.GetDirectories(path))
                    {
                        Directory.Delete(dir, true);
                    }
                    MessageBox.Show("Sunucu başarıyla sıfırlandı .", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmSettings.ActiveForm.Close();
                }
                else
                {
                    MessageBox.Show("Sunucu klasörü bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Silme işlemi sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbWhitelist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            if (isWhitelistTrue) return;
            string propertiesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.properties");

            if (File.Exists(propertiesPath))
            {
                string[] lines = File.ReadAllLines(propertiesPath);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("white-list="))
                    {
                        lines[i] = "white-list=" + cmbWhitelist.SelectedItem.ToString();
                        break;
                    }
                }

                File.WriteAllLines(propertiesPath, lines);
                MessageBox.Show("Whitelist tercihi başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("server.properties dosyası bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtAddPlayerToWhitelist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                frmMain main = frmMain.Instance;
                AddPlayerToWhitelist();
            }
        }

        private void txtAddPlayerToWhitelist_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDeletePlayer_Click(object sender, EventArgs e)
        {
            if (lstWhitelist.SelectedItem == null)
            {
                MessageBox.Show("Lütfen silmek için bir oyuncu seçin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string playerName = lstWhitelist.SelectedItem.ToString();
            Process server = frmMain.Instance.serverProcess;

            if (server != null && !server.HasExited)
            {
                try
                {
                    server.StandardInput.WriteLine($"whitelist remove {playerName}");
                    server.StandardInput.Flush();

                    MessageBox.Show($"{playerName} başarıyla whitelist'ten silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lstWhitelist.Items.Remove(playerName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Oyuncu silinirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Sunucu çalışmıyor!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstWhitelist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbChunks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            string propertiesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.properties");

            if (File.Exists(propertiesPath))
            {
                string[] lines = File.ReadAllLines(propertiesPath);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("view-distance="))
                    {
                        lines[i] = "view-distance=" + cmbChunks.SelectedItem.ToString();
                        break;
                    }
                }

                File.WriteAllLines(propertiesPath, lines);
                MessageBox.Show("Görüş mesafesi tercihi başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("server.properties dosyası bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbHardcore_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            string propertiesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.properties");

            if (File.Exists(propertiesPath))
            {
                string[] lines = File.ReadAllLines(propertiesPath);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("hardcore="))
                    {
                        lines[i] = "hardcore=" + cmbHardcore.SelectedItem.ToString();
                        break;
                    }
                }

                File.WriteAllLines(propertiesPath, lines);
                MessageBox.Show("Hardcore tercihi başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("server.properties dosyası bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnSaveResourcePack_Click(object sender, EventArgs e)
        {
            string propertiesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.properties");

            if (!File.Exists(propertiesPath))
            {
                MessageBox.Show("server.properties dosyası bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string resourcePackURL = txtResourcePack.Text.Trim();



            string[] lines = File.ReadAllLines(propertiesPath);
            bool found = false;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("resource-pack="))
                {
                    lines[i] = "resource-pack=" + resourcePackURL;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                // Eğer satır yoksa en sona ekle
                File.AppendAllText(propertiesPath, Environment.NewLine + "resource-pack=" + resourcePackURL);
            }
            else
            {
                File.WriteAllLines(propertiesPath, lines);
            }

            MessageBox.Show("Kaynak paketi başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}