using BreadShop4Bili.BreadShop.Models;
using BreadShop4Bili.BreadShop.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadShop4Bili.BreadShop.Events
{
    public class PunchEvent : EventBase
    {

        private static Random random = new Random();

        [ShopEvent(4, 500)]
        public static string Strikebreaker(Profile profile)
        {
            int delta = random.Next((int)ShopSettings.PunchMax + 1) / 10;
            DateTime now = DateTime.UtcNow;
            profile.PunchTime = now.UtcTimeStamp();
            profile.BreadCount -= delta;
            return $"{{0}}被当成工贼, 被打了, 丢了{delta}个{ShopSettings.Thing}, 还剩{profile.BreadCount}个{ShopSettings.Thing}";
        }

        [ShopEvent(4, 500)]
        public static string Diamond(Profile profile)
        {
            int delta = random.Next((int)ShopSettings.PunchMax + 1);
            DateTime now = DateTime.UtcNow;
            profile.PunchTime = now.UtcTimeStamp();
            profile.BreadCount += delta * 3;
            return $"{{0}}{{0}}完成了黄金签到, 获得了3x{delta}个{ShopSettings.Thing}, 总共有{profile.BreadCount}个{ShopSettings.Thing}";
        }

        [ShopEvent(5, 1000)]
        public static string Golden(Profile profile)
        {
            int delta = random.Next((int)ShopSettings.PunchMax + 1);
            DateTime now = DateTime.UtcNow;
            profile.PunchTime = now.UtcTimeStamp();
            profile.BreadCount += delta * 2;
            return $"{{0}}完成了黄金签到, 获得了2x{delta}个{ShopSettings.Thing}, 总共有{profile.BreadCount}个{ShopSettings.Thing}";
        }

        [ShopEvent(6, 10000)]
        public static string Normal(Profile profile)
        {
            int delta = random.Next((int)ShopSettings.PunchMax + 1);
            DateTime now = DateTime.UtcNow;
            profile.PunchTime = now.UtcTimeStamp();
            profile.BreadCount += delta;
            return $"{{0}}成功签到, 获得了{delta}个{ShopSettings.Thing}, 总共有{profile.BreadCount}个{ShopSettings.Thing}";
        }
    }
}
