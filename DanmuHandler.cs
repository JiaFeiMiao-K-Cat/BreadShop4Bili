using BilibiliDM_PluginFramework;
using BreadShop4Bili.BreadShop.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BreadShop4Bili
{
    internal class DanmuHandler : INotifyPropertyChanged
    {
        bool connected = false;

        int roomId = 0;
        public void Connected(int roomId)
        {
            this.roomId = roomId;
            connected = true;
        }

        public void Disconnected()
        {
            connected = false;
            roomId = 0;
        }

        public void InitOrCreateDatabase()
        {
            Shop.GetContext().Database.EnsureCreated();
        }
        public void Colse()
        {
            Shop.Close();
        }
        internal async void ProcessDanmu(DanmakuModel danmakuModel)
        {
            if (string.IsNullOrWhiteSpace(danmakuModel.CommentText))
            {
                return;
            }

            string command = danmakuModel.CommentText;

            string message;
            string rank;

            if (command == $"买{ShopSettings.Thing}")
            {
                message = await Shop.Buy(danmakuModel.UserID_long, danmakuModel.UserName);
            }
            else if (command == $"吃{ShopSettings.Thing}")
            {
                message = await Shop.Eat(danmakuModel.UserID_long, danmakuModel.UserName);
            }
            else if (command == $"烤{ShopSettings.Thing}")
            {
                message = await Shop.Toast(danmakuModel.UserID_long, danmakuModel.UserName);
            }
            else if (command == $"{ShopSettings.Thing}签到")
            {
                message = await Shop.Punch(danmakuModel.UserID_long, danmakuModel.UserName);
            }
            else if (command == $"{ShopSettings.Thing}抽奖")
            {
                message = await Shop.Draw(danmakuModel.UserID_long, danmakuModel.UserName);
            }
            else
            {
                return;
            }

            rank = Shop.Rank();
            FileHelper.WriteMessage(message);
            FileHelper.WriteRank(rank);

            return;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }
    }
}
