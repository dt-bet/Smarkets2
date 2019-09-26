using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarkets
{
    public class Constants
    {
        public const string dateTimeFormat = "yyyy_MM_ddTHH_mm_ss";

        public const string drive = "D";

        public static readonly string XMLDirectory = new System.IO.DriveInfo(drive).RootDirectory +"/SMarketXML";


     
    }
}
