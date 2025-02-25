using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Server
{
    public class JavaVersionChecker 
    {
        public void CheckJavaVersion()
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "java";
                process.StartInfo.Arguments = "-version";
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                string output = process.StandardError.ReadToEnd();
                process.WaitForExit();

                Match match = Regex.Match(output, "\"(\\d+)\\.(\\d+)\\.(\\d+)\"");
                if (match.Success)
                {
                    int majorVersion = int.Parse(match.Groups[1].Value);

                    if (majorVersion < 21)
                    {
                        MessageBox.Show("Java 21 veya üzeri sürüm yüklü değil! Lütfen Java'yı güncelleyin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Java sürümü algılanamadı! Lütfen Java'yı yükleyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Java sürümü kontrol edilirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
