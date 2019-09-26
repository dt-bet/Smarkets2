using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarkets.Common
{
    public class DateTimeHelper
    {

        public static string FormatDateTimeToSmarkets(DateTime dt)
        {

            TimeSpan nowt = new TimeSpan(dt.Hour, dt.Minute, dt.Second);

            return dt.Date.ToString("yyyy-MM-ddT") + System.Web.HttpUtility.UrlEncode(nowt.ToString()) + ".000Z";
        }
    }
}
