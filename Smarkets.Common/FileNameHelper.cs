using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Smarkets.Common
{
    public class FileNameHelper
    {
        const string dateTimeFormat = "yyyy_MM_ddTHH_mm_ss";

        const string extension = ".xml";

        const string directoryDateTimeFormat = "yy_MM_dd";

        public static string GetFileName() => Constants.XMLDirectory + "smarkets" + DateTime.Now.ToString(dateTimeFormat) + extension;


        public static IEnumerable<(DateTime, string)> GetDateTimeFromFileName() => from _ in System.IO.Directory.GetFiles(Constants.XMLDirectory)
                                                                                   select (GetDateTimeFromFileName(_), _);



        public static DateTime GetDateTimeFromFileName(string fileName) =>
            DateTime.ParseExact(
            (System.IO.Path.GetFileNameWithoutExtension(fileName).Replace("smarkets", string.Empty)),
            dateTimeFormat,
            CultureInfo.InvariantCulture);

        public static DateTime GetDateTimeFromDirectory(string directoryName) => DateTime.ParseExact(directoryName, directoryDateTimeFormat, CultureInfo.InvariantCulture);



        public static string MakeValidFileName(string name)
        {
            string invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
            string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return System.Text.RegularExpressions.Regex.Replace(name, invalidRegStr, "_");
        }



        public static string MakeDateDirectoryName(DateTime date)
        {
            return date.ToString(directoryDateTimeFormat);
        }

    }
}
