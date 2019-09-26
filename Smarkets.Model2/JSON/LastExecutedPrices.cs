using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Smarkets.Model.JSON
{

    public class LastPricesRoot
    {
        public Last_Executed_Prices[] last_executed_prices { get; set; }
    }

    public class Last_Executed_Prices
    {
        public string contract_id { get; set; }
        public string last_executed_price { get; set; }
        public DateTime? timestamp { get; set; }
    }

}
