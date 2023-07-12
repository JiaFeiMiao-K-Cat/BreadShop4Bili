using BreadShop4Bili.BreadShop.Data;
using BreadShop4Bili.BreadShop.Events;
using BreadShop4Bili.BreadShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadShop4Bili.BreadShop.Utils
{
    public static class Shop
    {
        private static Context context = new Context();

        public static Context GetContext()
        {
            context.Database.EnsureCreated();
            return context;
        }

        public static void Close()
        {
            context.SaveChanges();
        }
        public static async Task<string> Buy(long uid, string uname)
        {
            Context context = GetContext();
            Profile profile = context.Profiles.FirstOrDefault(e => e.Id == uid);
            if (profile == null)
            {
                profile = new Profile();
                profile.Id = uid;
                context.Profiles.Add(profile);
                await context.SaveChangesAsync();
            }
            profile.Name = uname;
            DateTime now = DateTime.UtcNow;
            if (profile.BuyTime.DateTimeUtc().AddSeconds(ShopSettings.BuyCD) >= now)
            {
                return $"{uname}太贪心了, 还没到冷却时间, 再等{TimeSpan.FromSeconds(ShopSettings.BuyCD - (DateTime.UtcNow - profile.BuyTime.DateTimeUtc()).TotalSeconds).ToString(@"mm\:ss")}";
            }
            else
            {
                string result = ShopEventHandler<BuyEvent, ConstantSize1>.Excute(profile);
                await context.SaveChangesAsync();
                return String.Format(result, uname);
            }
        }
        public static async Task<string> Eat(long uid, string uname)
        {
            Context context = GetContext();
            Profile profile = context.Profiles.FirstOrDefault(e => e.Id == uid);
            if (profile == null)
            {
                profile = new Profile();
                profile.Id = uid;
                context.Profiles.Add(profile);
                await context.SaveChangesAsync();
            }
            profile.Name = uname;
            DateTime now = DateTime.UtcNow;
            if (profile.EatTime.DateTimeUtc().AddSeconds(ShopSettings.EatCD) >= now)
            {
                return $"{uname}太贪心了, 还没到冷却时间, 再等{TimeSpan.FromSeconds(ShopSettings.EatCD - (DateTime.UtcNow - profile.EatTime.DateTimeUtc()).TotalSeconds).ToString(@"mm\:ss")}";
            }
            else
            {
                string result = ShopEventHandler<EatEvent, ConstantSize1>.Excute(profile);
                await context.SaveChangesAsync();
                return String.Format(result, uname);
            }
        }
        public static async Task<string> Toast(long uid, string uname)
        {
            Context context = GetContext();
            Profile profile = context.Profiles.FirstOrDefault(e => e.Id == uid);
            if (profile == null)
            {
                profile = new Profile();
                profile.Id = uid;
                context.Profiles.Add(profile);
                await context.SaveChangesAsync();
            }
            profile.Name = uname;
            DateTime now = DateTime.UtcNow;
            if (profile.ToastTime.DateTimeUtc().AddSeconds(ShopSettings.ToastCD) >= now)
            {
                return $"{uname}太贪心了, 还没到冷却时间, 再等{TimeSpan.FromSeconds(ShopSettings.ToastCD - (DateTime.UtcNow - profile.ToastTime.DateTimeUtc()).TotalSeconds).ToString(@"mm\:ss")}";
            }
            else
            {
                string result = ShopEventHandler<ToastEvent, ConstantSize1>.Excute(profile);
                await context.SaveChangesAsync();
                return String.Format(result, uname);
            }
        }
        public static async Task<string> Draw(long uid, string uname)
        {
            Context context = GetContext();
            Profile profile = context.Profiles.FirstOrDefault(e => e.Id == uid);
            if (profile == null)
            {
                profile = new Profile();
                profile.Id = uid;
                context.Profiles.Add(profile);
                await context.SaveChangesAsync();
            }
            profile.Name = uname;
            DateTime now = DateTime.UtcNow;

            if (profile.BreadCount < ShopSettings.DrawPrice)
            {
                return $"没钱还想抽奖? 攒够{ShopSettings.DrawPrice}个{ShopSettings.Thing}再来吧";
            }
            else
            {
                string result = ShopEventHandler<DrawEvent, ConstantSize1>.Excute(profile);
                await context.SaveChangesAsync();
                return String.Format(result, uname);
            }
        }
        public static async Task<string> Punch(long uid, string uname)
        {
            Context context = GetContext();
            Profile profile = context.Profiles.FirstOrDefault(e => e.Id == uid);
            if (profile == null)
            {
                profile = new Profile();
                profile.Id = uid;
                context.Profiles.Add(profile);
                await context.SaveChangesAsync();
            }
            profile.Name = uname;
            DateTime now = DateTime.UtcNow;
            if (profile.PunchTime.DateTimeUtc().ToLocalTime().DayOfYear >= now.DayOfYear)
            {
                return $"{uname}太贪心了, 今天已经签过到了";
            }
            else
            {
                string result = ShopEventHandler<PunchEvent, ConstantSize1>.Excute(profile);
                await context.SaveChangesAsync();
                return String.Format(result, uname);
            }
        }
        public static string Rank()
        {
            StringBuilder builder = new StringBuilder($"{ShopSettings.ThingEmoji}直播间{ShopSettings.Thing}排行top!{ShopSettings.ThingEmoji}\n");
            Context context = GetContext();
            List<Profile> profiles = context.Profiles
                .OrderByDescending(e => e.EatCount)
                .ThenByDescending(q => q.BreadCount)
                .Take(5)
                .ToList();
            int index = 1;
            foreach(Profile profile in profiles)
            {
                builder.AppendLine($"{index++}. {profile.Name}, Lv.{profile.Level}, 拥有{profile.BreadCount}个{ShopSettings.Thing}");
            }
            builder.AppendLine("大家继续努力!");
            return builder.ToString();
        }
    }
}
