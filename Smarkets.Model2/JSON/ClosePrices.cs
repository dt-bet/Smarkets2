using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarkets.Model.JSON
{



    public class ClosePricesRoot
    {
        public ClosePrices[] result { get; set; }
    }

    public class ClosePrices
    {
        public Close_Price[] close_prices { get; set; }
        public string contract_id { get; set; }
    }

    public class Close_Price
    {
        public string close_price { get; set; }
        public DateTime timestamp { get; set; }
    }

}



