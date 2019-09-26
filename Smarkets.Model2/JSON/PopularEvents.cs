using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarkets.Model.JSON
{

    public class PopularEventsRootobject
    {
        public string[] popular_event_ids { get; set; }
    }



    public class PopularEvents
    {
        public PopularEvent[] results { get; set; }
    }

    public class PopularEvent
    {
        public string expires { get; set; }
        public string id { get; set; }
        public bool manually_added { get; set; }
        public string name { get; set; }
        public string parent_name { get; set; }
        public string url { get; set; }
    }


}
