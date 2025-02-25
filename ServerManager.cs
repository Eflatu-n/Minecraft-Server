using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Minecraft_Server
{
    public class ServerManager
    {
        private TextBox txtConsole;
        private Button btnKillServer;
        private Label lblPlayerCount;
        public bool IsServerRunning { get; private set; } = false;
        public static int GetPlayerCount()
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "java";
                process.StartInfo.Arguments = "-jar server.jar";
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();

                // "list" komutunu sunucuya gönder
                process.StandardInput.WriteLine("list");
                process.StandardInput.Flush();
                process.StandardInput.Close();

                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                string pattern = @"There are (\d+)/\d+ players online";
                Match match = Regex.Match(output, pattern);

                if (match.Success && int.TryParse(match.Groups[1].Value, out int playerCount))
                {
                    return playerCount; 
                }
                else
                {
                    return 0; 
                }
            }
            catch
            {
                return -1; 
            }
        }

        public static void ExtractJar()
        {
            string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.jar");

            if (!File.Exists(outputPath)) // Eğer zaten varsa tekrar çıkartma
            {
                using (Stream stream = Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("Minecraft_Server.Resources.server.jar"))
                {
                    if (stream == null)
                    {
                        MessageBox.Show("server.jar kaynağı bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        stream.CopyTo(fileStream);
                    }
                }
            }
        }
        public static void EnsureEula()
        {
            string eulaPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "eula.txt");

            try
            {
                File.WriteAllText(eulaPath, "eula=true");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"EULA dosyası oluşturulurken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}