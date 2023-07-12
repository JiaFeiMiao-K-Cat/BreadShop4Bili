using BilibiliDM_PluginFramework;
using BreadShop4Bili.BreadShop.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace BreadShop4Bili
{
    public class PluginMain : DMPlugin
    {
        private DanmuHandler _danmuHandler = new DanmuHandler();
        private MainWindow window;
        public PluginMain()
        {
            PluginName = "直播间面包店";
            PluginAuth = "钾肥喵";
            PluginCont = "xuzhengzheng@outlook.com";
            PluginDesc = "买面包喵~吃面包喵~";
            PluginVer = "0.0.1.0";
            ReceivedDanmaku += OnReceivedDanmaku; 

            if (!Directory.Exists(FileHelper.DataDirectoryPath))
            {
                Directory.CreateDirectory(FileHelper.DataDirectoryPath);
            }
            FileHelper.LoadSettings();
            window = new MainWindow();
        }
        private void OnReceivedDanmaku(object sender, ReceivedDanmakuArgs e)
        {
            if (e.Danmaku.MsgType == MsgTypeEnum.Comment)
            {
                _danmuHandler.ProcessDanmu(e.Danmaku);
            }
        }
        public override void Admin()
        {
            window.Show();
            window.Activate();
        }

        public override void Inited()
        {
            base.Inited();
        }

        public override void DeInit()
        {
            _danmuHandler.Colse();
            base.DeInit();
        }

        public override void Start()
        {
            _danmuHandler.InitOrCreateDatabase();
            base.Start();
        }

        public override void Stop()
        {
            _danmuHandler.Colse();
            base.Stop();
        }
    }
}
