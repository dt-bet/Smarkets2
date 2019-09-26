using Smarkets.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarkets.WebAPI
{
    public static class AddressBuilder
    {
        public static readonly string urlmainV0 = "https://smarkets.com/v0";

        public static readonly string urlmainV3 = "https://api.smarkets.com/v3";


        private static readonly string popularEvents = "/events/popular/";

        private static readonly string popularEventsv3 = "/popular/event_ids/sport/";


        public static string GetPopularEvents() { return urlmainV0 + popularEvents; }

        //static string getContracts(params string[] contractids) { return urlmain + $"/id/{String.Join(",", contractids)}/live/"; }


        public static string getMarkets(params string[] ids) => getLimitedEvents("markets", ids);
        public static string getStates(params string[] ids) => getLimitedEvents("states", ids);
        public static string getCompetitors(params string[] ids) => getLimitedEvents("competitors", ids);
        public static string getEvents(params string[] ids) { return urlmainV3 + $"/events/{String.Join(",", ids)}"; }
        private static string getLimitedEvents(string endPoint,string[] ids) { return urlmainV3 + $"/events/{String.Join(",", ids)}/{endPoint}/"; }



        public static string getContracts(params string[] marketids) { return urlmainV3 + $"/markets/{String.Join(",", marketids)}/contracts/"; }
        public static string getLastExecutedPrices(params string[] contractids) { return urlmainV3 + $"/contracts/{String.Join(",", contractids)}/last_executed_prices/"; }
        //public static string getLiveEvents(params string[] eventids) { return urlmainV0 + $"/events/ids/{String.Join(",", eventids)}/live/"; }
        //public static string getPopularEvents() { return urlmainV0 + popularEvents; }
        public static string getQuotes(params string[] marketids) { return urlmainV3 + $"/markets/{String.Join(",", marketids)}/quotes/"; }

        public static string getPopularEvents(string sport) { return urlmainV3 + popularEventsv3+ sport; }
        //https://api.smarkets.com/v3/popular/event_ids/sport/horse-racing/

     

        //https://smarkets.com/v0/events/ids/911149,911154,911162,911147,911150,911143,911146,911145,911144,911176,911152,911177,911161,911173,911172,911171,911170,911169,911168,911167,911166,911165,911178,911174,911134,911420,911418,911417,911175,911180,911179,911151,911132,911156/live/
        //https://api.smarkets.com/v3/popular/event_ids/sport/football/
        //https://api.smarkets.com/v3/contracts/23604727,23604709,23604713/close_price_series/?data_points=100&timestamp_ge=2018-04-05T12%3A30%3A00.000Z&timestamp_lt=2018-04-07T13%3A44%3A41.000Z



        public static string getPricesByDateRange(DateTime begin, DateTime end, int results = 20, params string[] contractids)
        {
            var date1 = "&timestamp_ge=" + DateTimeHelper.FormatDateTimeToSmarkets(begin);
            var date2 = "&timestamp_lt=" + DateTimeHelper.FormatDateTimeToSmarkets(end);
            var datepoints = "?data_points=" + results;

            return urlmainV3 + $"/contracts/{String.Join(",", contractids)}/close_price_series/" + datepoints + date1 + date2;

        }
    }



}



