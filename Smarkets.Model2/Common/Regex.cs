
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Smarkets.Model
{

    public static class RegexExtension
    {

        public static string RemoveYears(string inputText)
        {

            var regex = new System.Text.RegularExpressions.Regex(@"\d{4}-\d{4}");
            return regex.Replace(inputText, "").Trim();

        }
    }


}


