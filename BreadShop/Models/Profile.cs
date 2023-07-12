using BreadShop4Bili.BreadShop.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadShop4Bili.BreadShop.Models
{
    public class Profile
    {
        public long Id { get; set; } = 0;
        public string Name { get; set; } = "empty";
        public long BuyTime { get; set; } = 0;
        public long BuyCount { get; set; } = 0;
        public long EatTime { get; set; } = 0;
        public long EatCount { get; set; } = 0;
        public long RobTime { get; set; } = 0;
        public long RobCount { get; set; } = 0;
        public long GiveTime { get; set; } = 0;
        public long GiveCount { get; set; } = 0;
        public long ToastTime { get; set; } = 0;
        public long ToastCount { get; set; } = 0;
        public long PunchTime { get; set; } = 0;
        public long BreadCount { get; set; } = 0;

        [NotMapped]
        public long Level { get { return EatCount / ShopSettings.LevelSize; } }
    }
}
