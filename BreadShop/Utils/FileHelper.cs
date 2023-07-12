using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BreadShop4Bili.BreadShop.Utils
{
    internal class FileHelper
    {
        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        internal static readonly string DataDirectoryPath = Path.Combine(AssemblyDirectory, "面包店");

        internal static readonly string ReferenceDirectoryPath = Path.Combine(DataDirectoryPath, "reference");

        internal static readonly string DatabaseDirectoryPath = Path.Combine(DataDirectoryPath, "data.db");

        internal static readonly string ConfigFilePath = Path.Combine(DataDirectoryPath, "config.json");

        internal static readonly string MessageOutputFilePath = Path.Combine(DataDirectoryPath, "消息.txt");

        internal static readonly string RankOutputFilePath = Path.Combine(DataDirectoryPath, "榜单.txt");

        internal static void LoadSettings()
        {
            if (!File.Exists(ConfigFilePath))
            {
                WriteSettings();
            }
            else
            {
                JsonConvert.DeserializeObject<ShopSettings>(File.ReadAllText(ConfigFilePath));
            }
        }
        internal static void WriteSettings()
        {
            File.WriteAllText(ConfigFilePath, JsonConvert.SerializeObject(new ShopSettings(), Formatting.Indented));
        }
        internal static void WriteMessage(string message)
        {
            File.WriteAllText(MessageOutputFilePath, message);
        }

        internal static void WriteRank(string rank)
        {
            File.WriteAllText(RankOutputFilePath, rank);
        }
    }
}