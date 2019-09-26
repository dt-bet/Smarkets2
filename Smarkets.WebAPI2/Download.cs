using Newtonsoft.Json.Linq;
using Smarkets.Model.XML;
using Smarkets.Model.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Smarkets.WebAPI
{
    public static class Client
    {

        public static ClosePrices[] DownloadClosingPrices(DateTime begin, DateTime end, int results, params string[] contractids)
        {
            return Smarkets.JsonWebHelper.Download<ClosePricesRoot>(AddressBuilder.getPricesByDateRange(begin, end, results = 20, contractids)).result;
        }


        public static Last_Executed_Prices[] DownloadLastExecutedPrices(params string[] contractIds)
        {
            return Smarkets.JsonWebHelper.Download<LastPricesRoot>(AddressBuilder.getLastExecutedPrices(contractIds)).last_executed_prices;
        }

        public static Contract[] DownloadContracts(params string[] marketIds)
        {
            var x = Smarkets.JsonWebHelper.Download<Market>(AddressBuilder.getContracts(marketIds)).Contracts;
            return x;
        }

        public static Market[] DownloadMarkets(params string[] eventIds)
        {
            var x= Smarkets.JsonWebHelper.Download<Model.XML.Event>(AddressBuilder.getMarkets(eventIds)).Markets;
            return x;
        }

        public static Competitor[] DownloadCompetitors(params string[] eventIds)
        {
  
            var x= Smarkets.JsonWebHelper.Download<Competitors>(AddressBuilder.getCompetitors(eventIds)).competitors;
            return x;

        }


        public static Model.JSON.Event[] DownloadPopularEvents(string sport)
        {
            var x = Smarkets.JsonWebHelper.Download<PopularEventsRootobject>(AddressBuilder.getPopularEvents(sport)).popular_event_ids;
            return Smarkets.JsonWebHelper.Download<Events>(AddressBuilder.getEvents(x)).events.ToArray();
        }




        public static Dictionary<string, Quote> DownloadQuotes(params string[] marketIds)
        {
           var x= Smarkets.JsonWebHelper.Download<Dictionary<string, Quote>>(AddressBuilder.getQuotes(marketIds));
            return x;
        }

        public static Event_States[] DownloadStates(params string[] eventIds)
        
           => Smarkets.JsonWebHelper.Download<States>(AddressBuilder.getStates(eventIds)).event_states;


        public static async Task<Event_States[]> DownloadStatesAsync(params string[] eventIds)
            =>(await DownloadHelper.DownloadAsync<States>(AddressBuilder.getStates(eventIds))).event_states;




    }



}

