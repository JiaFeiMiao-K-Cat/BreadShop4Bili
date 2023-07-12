using BreadShop4Bili.BreadShop.Models;
using BreadShop4Bili.BreadShop.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadShop4Bili.BreadShop.Events
{
    public class BuyEvent : EventBase
    {

        private static Random random = new Random();

        [ShopEvent(3, 100)]
        public static string Diamond(Profile profile)
        {
            int delta = random.Next(1, (int)ShopSettings.BuyMax + 1);
            DateTime now = DateTime.UtcNow;
            profile.BuyTime = now.UtcTimeStamp();
            profile.BreadCount += delta + 3 * ShopSettings.LevelSize;
            return $"{{0}}买了{delta}个{ShopSettings.Thing}, 里面有一个钻石{ShopSettings.Thing}, 再送{3 * ShopSettings.LevelSize}个, 总共有{profile.BreadCount}个{ShopSettings.Thing}";
        }
        [ShopEvent(4, 300)]
        public static string Golden(Profile profile)
        {
            int delta = random.Next(1, (int)ShopSettings.BuyMax + 1);
            DateTime now = DateTime.UtcNow;
            profile.BuyTime = now.UtcTimeStamp();
            profile.BreadCount += delta + ShopSettings.LevelSize;
            return $"{{0}}买了{delta}个{ShopSettings.Thing}, 里面有一个黄金{ShopSettings.Thing}, 再送{ShopSettings.LevelSize}个, 总共有{profile.BreadCount}个{ShopSettings.Thing}";
        }
        [ShopEvent(5, 500)]
        public static string Tumble(Profile profile)
        {
            int delta = random.Next(1, (int)Math.Min(profile.BreadCount + 1, (int)ShopSettings.BuyMax + 1));
            DateTime now = DateTime.UtcNow;
            profile.BuyTime = now.UtcTimeStamp();
            profile.BreadCount -= delta;
            return $"{{0}}摔了一跤, 丢了{delta}个{ShopSettings.Thing}, 总共有{profile.BreadCount}个{ShopSettings.Thing}";
        }
        [ShopEvent(5, 500)]
        public static string TooLittle(Profile profile)
        {
            if (profile.BreadCount > 10)
            {
                return Normal(profile);
            }
            else
            {
                int delta = random.Next((int)ShopSettings.BuyMax + 1);
                DateTime now = DateTime.UtcNow;
                profile.BuyTime = now.UtcTimeStamp();
                profile.BreadCount += 2 * delta;
                return $"{{0}}的{ShopSettings.Thing}太少了, 给你2x{delta}个, 总共有{profile.BreadCount}个{ShopSettings.Thing}";
            }
        }
        [ShopEvent(5, 500)]
        public static string Bad(Profile profile)
        {
            DateTime now = DateTime.UtcNow;
            profile.BuyTime = now.UtcTimeStamp();
            return $"{{0}}买到一个坏{ShopSettings.Thing}, 不想买了";
        }
        [ShopEvent(5, 500)]
        public static string Queue(Profile profile)
        {
            DateTime now = DateTime.UtcNow;
            profile.BuyTime = now.AddMinutes(10).UtcTimeStamp();
            return $"{{0}}走错路去了苏联{ShopSettings.Thing}店, 再排十分钟队才可以买{ShopSettings.Thing}";
        }
        [ShopEvent(6, 10000)]
        public static string Normal(Profile profile)
        {
            int delta = random.Next((int)ShopSettings.BuyMax + 1);
            DateTime now = DateTime.UtcNow;
            profile.BuyTime = now.UtcTimeStamp();
            profile.BreadCount += delta;
            return $"{{0}}买了{delta}个{ShopSettings.Thing}, 总共有{profile.BreadCount}个{ShopSettings.Thing}";
        }
    }
}
