using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarkets.Model.JSON
{


    public class EventsLiveRoot
    {
        public Dictionary<string,contract> contracts_live { get; set; }
        public Dictionary<string, Event_Live> events_live { get; set; }
        public string[] events_live_ids { get; set; }
        public Dictionary<string, MarketLive> market { get; set; }
        public Dictionary<string, Quote> quotes { get; set; }
    }



    public class contract
    {
        public string id { get; set; }
        public object outcome { get; set; }
        public object outcome_timestamp { get; set; }
    }

    public class Event_Live
    {
        public string[] contract_live_ids { get; set; }
        public string id { get; set; }
        public bool managed { get; set; }
        public object scores { get; set; }
        public bool scouted { get; set; }
        public string state { get; set; }
    }



    public class MarketLive
    {
        public int number_of_winners { get; set; }
        public Reductions reductions { get; set; }
        public string state { get; set; }
    }

    public class Reductions
    {
        public List<object> reductions { get; set; }

    }


 

    public class Quote
    {
        public Bid[] bids { get; set; }
        public Offer[] offers { get; set; }
    }

    public class Bid
    {
        public int price { get; set; }
        public long quantity { get; set; }
    }

    public class Offer
    {
        public int price { get; set; }
        public long quantity { get; set; }
    }





}
