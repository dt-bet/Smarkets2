using System;
using System.Collections.Generic;
using System.Text;

namespace Smarkets.Model.JSON
{
    public class ExecutionPriceRoot
    {
        public ExecutionPrices[] result { get; set; }
    }

    public class ExecutionPrices
    {
        public string contract_id { get; set; }

        public ExecutionPrice[] values { get; set; }
    }

    public class ExecutionPrice
    {
        public int? close_price { get; set; }
        public int? high_price { get; set; }
        public int? low_price { get; set; }
        public int? open_price { get; set; }
        public long? quantity { get; set; }
        public DateTime timestamp { get; set; }
    }
}
