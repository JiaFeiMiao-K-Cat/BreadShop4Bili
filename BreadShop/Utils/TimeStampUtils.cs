using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadShop4Bili.BreadShop.Utils
{
    public static class TimeStampUtils
    {
        public static readonly DateTime TimeStampStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static long UtcTimeStamp(this DateTime time)
        {
            return (long)time.ToUniversalTime()
                .Subtract(TimeStampStart)
                .TotalMilliseconds;
        }
        public static DateTime DateTimeUtc(this long timeStamp)
        {
            return TimeStampStart.AddMilliseconds(timeStamp);
        }
    }
}
