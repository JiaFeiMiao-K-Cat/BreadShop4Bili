using BreadShop4Bili.BreadShop.Models;
using BreadShop4Bili.BreadShop.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadShop4Bili.BreadShop.Events
{
    public class ToastEvent : EventBase
    {

        private static Random random = new Random();

        [ShopEvent(5, 1000)]
        public static string Follish(Profile profile)
        {
            int delta = random.Next((int)ShopSettings.ToastMax + 1);
            DateTime now = DateTime.UtcNow;
            profile.ToastTime = (now.AddMinutes(10)).UtcTimeStamp();
            return $"{{0}}烤糊了{delta}个{ShopSettings.Thing}, 真是个小笨蛋, 十分钟不能烤{ShopSettings.Thing}";
        }

        [ShopEvent(6, 10000)]
        public static string Normal(Profile profile)
        {
            int delta = random.Next((int)ShopSettings.ToastMax + 1);
            DateTime now = DateTime.UtcNow;
            profile.ToastTime = now.UtcTimeStamp();
            profile.BreadCount += delta;
            return $"{{0}}烤了{delta}个{ShopSettings.Thing}, 总共有{profile.BreadCount}个{ShopSettings.Thing}";
        }
    }
}
