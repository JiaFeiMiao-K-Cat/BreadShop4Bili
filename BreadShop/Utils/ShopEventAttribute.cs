using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadShop4Bili.BreadShop.Utils
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ShopEventAttribute : Attribute
    {
        private readonly int _priority;

        private readonly int _probability;

        /// <param name="priority">优先级(0~6), 0为最高优先级, 6为默认事件(概率应填写为100%)</param>
        /// <param name="probability">概率(0~10000), 单位为万分之一</param>
        public ShopEventAttribute(int priority, int probability)
        {
            _priority = priority;
            _probability = probability;
        }

        public int Priority => _priority;
        public int Probability => _probability;
    }
}
