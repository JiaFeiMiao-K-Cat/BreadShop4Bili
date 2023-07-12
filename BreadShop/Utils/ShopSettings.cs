using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadShop4Bili.BreadShop.Utils
{
    public class ShopSettings
    {
        [JsonProperty("BuyMax")]
        public static long BuyMax { get; set; } = 100;
        [JsonProperty("BuyCD")]
        public static long BuyCD { get; set; } = 60;
        [JsonProperty("EatMax")]
        public static long EatMax { get; set; } = 100;
        [JsonProperty("EatCD")]
        public static long EatCD { get; set; } = 60;
        [JsonProperty("GiveMax")]
        public static long GiveMax { get; set; } = 100;
        [JsonProperty("GiveCD")]
        public static long GiveCD { get; set; } = 60;
        [JsonProperty("RobMax")]
        public static long RobMax { get; set; } = 100;
        [JsonProperty("RobCD")]
        public static long RobCD { get; set; } = 60;
        [JsonProperty("ToastMax")]
        public static long ToastMax { get; set; } = 100;
        [JsonProperty("ToastCD")]
        public static long ToastCD { get; set; } = 60;
        [JsonProperty("PunchMax")]
        public static long PunchMax { get; set; } = 100;
        [JsonProperty("LevelSize")]
        public static long LevelSize { get; set; } = 100;
        [JsonProperty("DrawPrice")]
        public static long DrawPrice { get; set; } = 50;
        [JsonProperty("Thing")]
        public static string Thing { get; set; } = "面包";
        [JsonProperty("ThingEmoji")]
        public static string ThingEmoji { get; set; } = "🍞";
    }
}
