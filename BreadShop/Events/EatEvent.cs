using BreadShop4Bili.BreadShop.Data;
using BreadShop4Bili.BreadShop.Models;
using BreadShop4Bili.BreadShop.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadShop4Bili.BreadShop.Events
{
    public class EatEvent : EventBase
    {
        private static Random random = new Random();

        [ShopEvent(3, 100)]
        public static string DimondRefresh(Profile profile)
        {
            int delta = random.Next(profile.BreadCount > 0 ? 1 : 0, (int)Math.Min((int)ShopSettings.EatMax + 1, profile.BreadCount + 1));
            DateTime now = DateTime.UtcNow;
            profile.EatTime = now.UtcTimeStamp();
            profile.BreadCount -= delta;
            profile.EatCount += delta + ShopSettings.LevelSize;
            return $"{{0}}吃了{delta}个{ShopSettings.Thing}, 吃到一个钻石{ShopSettings.Thing}, 等级+1, 当前等级Lv.{profile.Level}";
        }

        [ShopEvent(4, 300)]
        public static string GoldenRefresh(Profile profile)
        {
            int delta = random.Next(profile.BreadCount > 0 ? 1 : 0, (int)Math.Min((int)ShopSettings.EatMax + 1, profile.BreadCount + 1));
            profile.BreadCount -= delta;
            profile.EatCount += delta;
            profile.BuyTime = 0;
            profile.EatTime = 0;
            profile.GiveTime = 0;
            profile.RobTime = 0;
            profile.ToastTime = 0;
            return $"{{0}}吃了{delta}个{ShopSettings.Thing}, 吃到一个黄金{ShopSettings.Thing}, 刷新所有操作";
        }

        [ShopEvent(5, 500)]
        public static string Refresh(Profile profile)
        {
            int delta = random.Next(profile.BreadCount > 0 ? 1 : 0, (int)Math.Min((int)ShopSettings.EatMax + 1, profile.BreadCount + 1));
            profile.BreadCount -= delta;
            profile.EatCount += delta;
            return $"{{0}}吃了{delta}个{ShopSettings.Thing}, 吃爽了, 还能继续吃";
        }

        [ShopEvent(5, 500)]
        public static string Poisoning(Profile profile)
        {
            int delta = random.Next(profile.BreadCount > 0 ? 1 : 0, (int)Math.Min((int)ShopSettings.EatMax + 1, profile.BreadCount + 1));
            DateTime now = DateTime.UtcNow;
            profile.EatTime = now.AddMinutes(10).UtcTimeStamp();
            profile.BreadCount -= delta;
            return $"{{0}}吃了{delta}个{ShopSettings.Thing}, 里面有坏{ShopSettings.Thing}, 食物中毒了, 十分钟不能吃{ShopSettings.Thing}";
        }

        [ShopEvent(6, 10000)]
        public static string Normal(Profile profile)
        {
            int delta = random.Next(profile.BreadCount > 0 ? 1 : 0, (int)Math.Min((int)ShopSettings.EatMax + 1, profile.BreadCount + 1));
            DateTime now = DateTime.UtcNow;
            profile.EatTime = now.UtcTimeStamp();
            profile.BreadCount -= delta;
            profile.EatCount += delta;
            return $"{{0}}吃了{delta}个{ShopSettings.Thing}, 当前等级Lv.{profile.Level}";
        }
    }
}
