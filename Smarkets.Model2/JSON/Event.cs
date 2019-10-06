using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarkets.Model.JSON
{
    public class Events
    {
        public Event[] events { get; set; }

        public Pagination pagination { get; set; }
    }

    public class Pagination
    {
        public string next_page { get; set; }
    }

    public class Event
    {
        public bool bettable { get; set; }
        public DateTime created { get; set; }
        public object description { get; set; }
        public object end_date { get; set; }
        public string full_slug { get; set; }
        public string id { get; set; }
        public DateTime modified { get; set; }
        public string name { get; set; }
        public string parent_id { get; set; }
        public string short_name { get; set; }
        public string slug { get; set; }
        public object special_rules { get; set; }
        public string start_date { get; set; }
        public DateTime start_datetime { get; set; }
        public string state { get; set; }
        public string type { get; set; }
    }

}


