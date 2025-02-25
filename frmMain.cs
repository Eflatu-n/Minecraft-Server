using System.Diagnostics;
using System.Windows.Forms;
using System.Net.Http;
using System.Net;
using Open.Nat;
using System.IO;
using Timer = System.Windows.Forms.Timer;


namespace Minecraft_Server
{
    public partial class frmMain : Form
    {
        public Process serverProcess;
        private FormHelper formHelper;
        private ServerManager serverManager;
        private static frmMain _instance;
        private JavaVersionChecker javaVersionChecker;  
        public static frmMain Instance
        {
            get { return _instance; }
        }
        public frmMain()
        {
            InitializeComponent();
            formHelper = new FormHelper();
            formHelper.Initialize(this);
            txtConsole.KeyDown += txtConsole_KeyDown;
            txtConsole.Enabled = false;
            _instance = this;
        }
        internal void frmMain_Load(object sender, EventArgs e)
        {
            javaVersionChecker = new JavaVersionChecker();
            javaVersionChecker.CheckJavaVersion();
            ServerManager.ExtractJar();
            btnKillServer.Enabled = false;
            btnRestart.Enabled = false;
            // playerCountTimer = new System.Windows.Forms.Timer();
            // playerCountTimer.Interval = 5000; // Her 5 saniyede bir güncelle
            // playerCountTimer.Tick += PlayerCountTimer_Tick;
            // playerCountTimer.Start();
            cmbRam.Items.AddRange(new string[] { "1G", "2G", "4G", "8G", "16G" });
            cmbRam.SelectedIndex = 1;
        }
        //private System.Windows.Forms.Timer playerCountTimer;
        

        private bool isStopping = false;

        private async void btnStartServer_Click(object sender, EventArgs e)
        {
            await StartServer();
        }
        private void UpdateSettingsControls(bool enabled)
        {
            frmSettings.ControlsEnabledState = enabled; 

            if (frmSettings.Instance != null)
            {
                frmSettings.Instance.SetControlsEnabled(enabled);
            }
        }
        private void AppendToListBox(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AppendToListBox), text);
            }
            else
            {
                lstConsole.Items.Add(text);
                lstConsole.TopIndex = lstConsole.Items.Count - 1; 
            }
        }
        private async void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isStopping) return; 

            if (serverProcess != null && !serverProcess.HasExited)
            {
                try
                {
                    e.Cancel = true; 
                    await Task.Run(() => StopServer());
                }
                catch (InvalidOperationException)
                {
                    serverProcess = null;
                }
                finally
                {
                    Invoke((Action)(() =>
                    {
                        Environment.Exit(0);
                    }));
                }
            }
            else
            {
                Environment.Exit(0);
            }
        }
        private async Task StopServer()
        {
            UpdateSettingsControls(true);
            if (isStopping || serverProcess == null || serverProcess.HasExited)
            {
                return; 
            }

            isStopping = true; 

            try
            {
                txtConsole.Invoke((Action)(() => txtConsole.Enabled = false));
                cmbRam.Invoke((Action)(() => cmbRam.Enabled = true));
                serverProcess.StandardInput.WriteLine("stop");
                serverProcess.StandardInput.Flush();
                serverProcess.StandardInput.Close();
                serverProcess.WaitForExit(9999999);
                if (!serverProcess.HasExited)
                {
                    Process.Start("taskkill", $"/PID {serverProcess.Id} /F");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sunucu kapatma sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                if (serverProcess != null && !serverProcess.HasExited)
                {
                    KillAllJavaProcesses();
                }

                if (serverProcess != null)
                {
                    serverProcess.Dispose();
                    serverProcess = null;
                }
                isStopping = false; 
            }
        }
        private void  KillAllJavaProcesses()
        {
            try
            {
                var javaProcesses = Process.GetProcessesByName("java");
                foreach (var process in javaProcesses)
                {
                    process.Kill();
                }
                MessageBox.Show("Tüm Java süreçleri kapatıldı.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Java süreçleri kapatılamadı: " + ex.Message);
            }
        }
        private string GetLocalIPAddress()
        {
            string localIP = "Bilinmiyor";
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }
        private void ServerProcess_Exited(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                btnStartServer.Enabled = true;
                btnKillServer.Enabled = false;
                txtConsole.Enabled = false;
                cmbRam.Enabled = true;
                frmSettings.SetControlsState(true);
            });
        }
        private async Task StartServer()
        {
            UpdateSettingsControls(false);
            frmSettings.SetControlsState(false);
            ServerManager.EnsureEula();
            await OpenPortAsync(25565);
            if (serverProcess == null || serverProcess.HasExited)
            {
                txtConsole.Enabled = true;
                btnStartServer.Enabled = false;
                btnKillServer.Enabled = true;
                btnRestart.Enabled = true;
                cmbRam.Enabled = false;


                string selectedRam = cmbRam.SelectedItem?.ToString() ?? "1G";

                serverProcess = new Process();
                serverProcess.StartInfo.FileName = "java";
                serverProcess.StartInfo.Arguments = $"-Xmx{selectedRam} -Xms{selectedRam} -jar server.jar nogui";  
                serverProcess.StartInfo.UseShellExecute = false;
                serverProcess.StartInfo.RedirectStandardInput = true;
                serverProcess.StartInfo.RedirectStandardOutput = true;
                serverProcess.StartInfo.RedirectStandardError = true;
                serverProcess.StartInfo.CreateNoWindow = true;
                serverProcess.EnableRaisingEvents = true;
                serverProcess.Exited += ServerProcess_Exited;
                serverProcess.EnableRaisingEvents = true;
                lblServerIP.Text = "Yerel IP: " + GetLocalIPAddress();

                serverProcess.OutputDataReceived += (s, ev) =>
                {
                    if (ev.Data != null)
                        AppendToListBox(ev.Data);
                };

                serverProcess.ErrorDataReceived += (s, ev) =>
                {
                    if (ev.Data != null)
                        AppendToListBox("ERR: " + ev.Data);
                };
                serverProcess.Start();
                serverProcess.BeginOutputReadLine();
                serverProcess.BeginErrorReadLine();
            }
        }
        private void btnKillServer_Click(object sender, EventArgs e)
        {
            btnSettings.Enabled = true;
            cmbRam.Enabled = true;

            if (serverProcess != null && !serverProcess.HasExited)
            {
                btnKillServer.Enabled = false;
                serverProcess.StandardInput.WriteLine("stop");  
                serverProcess.StandardInput.Flush();
                serverProcess.WaitForExit(5000); 
                UpdateSettingsControls(true);
                if (!serverProcess.HasExited)
                {
                    serverProcess.Kill();
                    
                }

            }
        }
        private void PlayerCountTimer_Tick(object sender, EventArgs e)
        {
            UpdatePlayerCountLabel();
        }
        private void UpdatePlayerCountLabel()
        {
            try
            {
                int playerCount = ServerManager.GetPlayerCount(); 
                lblPlayerCount.Text = "Oyuncu Sayısı: " + playerCount;
            }
            catch (Exception ex)
            {
                lblPlayerCount.Text = "Oyuncu Sayısı: Hata!";
                Console.WriteLine("Hata: " + ex.Message);
            }
        }
        private async void btnRestart_Click(object sender, EventArgs e)
        {
            serverProcess.StandardInput.WriteLine("restart");
        }
        private void txtConsole_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (serverProcess != null && !serverProcess.HasExited)
                {
                    string command = txtConsole.Text.Trim();

                    if (!string.IsNullOrEmpty(command))
                    {
                        serverProcess.StandardInput.WriteLine(command);
                        txtConsole.Clear();
                    }

                }

                else
                {
                    MessageBox.Show("Sunucu Çalışmıyor!");
                }

            }
        }
        private async Task OpenPortAsync(int port)
        {
            MessageBox.Show($"Port {port} açılıyor...");
            var discoverer = new NatDiscoverer();
            var cts = new System.Threading.CancellationTokenSource(5000);

            try
            {
                var device = await discoverer.DiscoverDeviceAsync(PortMapper.Upnp, cts);
                string externalIp = (await device.GetExternalIPAsync()).ToString();

                await device.CreatePortMapAsync(new Mapping(Protocol.Tcp, port, port, "Minecraft Server"));

                lblPublicIP.Text = $"Bağlantı IP: {externalIp}:{port}";

                lblPublicIP.Text = $"Bağlantı IP: {externalIp}:{port}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Port Forwarding err{ex.Message}");
            }
        }
        private void btnSettings_Click(object sender, EventArgs e)
        {
            string propertiesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.properties");
            string whitelistPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "whitelist.json");


            if (File.Exists(propertiesPath) && File.Exists(whitelistPath))
            {
                formHelper.OpenForm<frmSettings>();
            }
            else
            {
                MessageBox.Show("Ayarlar dosyaları bulunamadı!");
            }
        }
    }
}
