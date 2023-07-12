using BreadShop4Bili.BreadShop.Models;
using BreadShop4Bili.BreadShop.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadShop4Bili.BreadShop.Events
{
    public class DrawEvent : EventBase
    {

        private static Random random = new Random();

        [ShopEvent(6, 10000)]
        public static string Normal(Profile profile)
        {
            return $"别急, 还没写好";
        }
    }
}
