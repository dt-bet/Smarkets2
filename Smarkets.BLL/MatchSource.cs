
using Scrape.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityModel;

namespace Smarkets.BLL
{
    [Description("Smarkets")]
    public class MatchSource : Scrape.Entity.IMatchSource
    {
        public MatchSource()
        {
        }

        public string Key => string.Empty;

        public Task<ScrapeMatchCollection> GetTaskItems(Day date)
        {
            Exception ex = null;
            IEnumerable<ScrapeMatch> scrapeMatches = new ScrapeMatch[] { };
            try
            {
                scrapeMatches = Smarkets.Map.ScrapeMap.MapToEntity(EventsService.Get(date));
            }
            catch (Exception ex2)
            {
                ex = ex2;
            }
            return Task.Run(() => new ScrapeMatchCollection { Exception = ex, ScrapeMatches = scrapeMatches.ToArray() });
        }

        public Task<ScrapeMatchCollection> GetTaskItems()
        {
            Exception ex = null;
            IEnumerable<ScrapeMatch> scrapeMatches = new ScrapeMatch[] { };
            try
            {
                scrapeMatches = DAL.Sqlite.ScoreMatch.MatchDefault().Select(_ => Smarkets.Map.SmarketsMap2.MapToEntity(_));
            }
            catch (Exception ex2)
            {
                ex = ex2;
            }
            return Task.Run(() => new ScrapeMatchCollection { Exception = ex, ScrapeMatches = scrapeMatches.ToArray() });
        }

  

  
    }

}

