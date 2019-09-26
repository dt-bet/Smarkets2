using MoreLinq;
using Scrape.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using UtilityModel;

namespace Smarkets.BLL

{


    public class ScrapeEventsService
    {

        public ScrapeMatch[] Get(Day dateTime)
        {
            IEnumerable<ScrapeMatch> scrapeMatches = 
               Smarkets.Map.ScrapeMap.MapToEntity(EventsService.Get(dateTime));
      
            return scrapeMatches.ToArray();

        }

        public ScrapeMatch[] Get2(Day dateTime)
        {
            IEnumerable<ScrapeMatch> scrapeMatches =EventsService.Get3(dateTime);

            return scrapeMatches.ToArray();

        }
    }



}
