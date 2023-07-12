using BreadShop4Bili.BreadShop.Models;
using BreadShop4Bili.BreadShop.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadShop4Bili.BreadShop.Events
{
    public class GiveEvent : EventBase
    {

        private static Random random = new Random();

        [ShopEvent(4, 300)]
        public static string WinWin(Profile sender, Profile receiver)
        {
            int delta = random.Next(sender.BreadCount > 0 ? 1 : 0, (int)Math.Min((int)ShopSettings.GiveMax + 1, sender.BreadCount + 1));
            DateTime now = DateTime.UtcNow;
            sender.GiveTime = now.UtcTimeStamp();
            sender.BreadCount += delta;
            sender.GiveCount += delta;
            receiver.BreadCount += delta;
            return $"{{0}}真是个大善人, 我给你们各送了{delta}个{ShopSettings.Thing}, {{1}}现在有{receiver.BreadCount}个{ShopSettings.Thing}";
        }

        [ShopEvent(5, 500)]
        public static string Free(Profile sender, Profile receiver)
        {
            int delta = random.Next(sender.BreadCount > 0 ? 1 : 0, (int)Math.Min((int)ShopSettings.GiveMax + 1, sender.BreadCount + 1));
            DateTime now = DateTime.UtcNow;
            sender.GiveTime = now.UtcTimeStamp();
            sender.GiveCount += delta;
            receiver.BreadCount += delta;
            return $"{{0}}真是个大善人, 我帮你送了{delta}个{ShopSettings.Thing}给{{1}}, {{1}}现在有{receiver.BreadCount}个{ShopSettings.Thing}";
        }

        [ShopEvent(6, 10000)]
        public static string Normal(Profile sender, Profile receiver)
        {
            int delta = random.Next(sender.BreadCount > 0 ? 1 : 0, (int)Math.Min((int)ShopSettings.GiveMax + 1, sender.BreadCount + 1));
            DateTime now = DateTime.UtcNow;
            sender.GiveTime = now.UtcTimeStamp();
            sender.BreadCount -= delta;
            sender.GiveCount += delta;
            receiver.BreadCount += delta;
            return $"{{0}}送了{delta}个{ShopSettings.Thing}给{{1}}, {{1}}现在有{receiver.BreadCount}个{ShopSettings.Thing}";
        }
    }
}
