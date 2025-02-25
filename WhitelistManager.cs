using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server
{
    internal class WhitelistManager
    {
        public class Player
        {
            public string name { get; set; }
        }
        public static void LoadWhiteList()
        {
            string whitelistPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "whitelist.json");

            if (File.Exists(whitelistPath))
            {
                try
                {
                    string json = File.ReadAllText(whitelistPath);
                    List<Player> players = JsonConvert.DeserializeObject<List<Player>>(json);

                    frmSettings.Instance.lstWhitelist.Items.Clear();

                    foreach (var player in players)
                    {
                        frmSettings.Instance.lstWhitelist.Items.Add(player.name);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Whitelist yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("whitelist.json dosyası bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
