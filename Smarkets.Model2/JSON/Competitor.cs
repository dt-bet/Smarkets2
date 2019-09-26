using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarkets.Model.JSON
{


    public class Competitors
    {
        public Competitor[] competitors { get; set; }
    }

    public class Competitor
    {
        public string event_id { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public object short_code { get; set; }
        public string short_name { get; set; }
        public string slug { get; set; }
        public string type { get; set; }
    }
}