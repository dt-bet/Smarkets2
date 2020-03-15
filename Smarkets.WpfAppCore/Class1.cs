
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Optional;
using Optional.Collections;
using Scrape.Entity;
using Smarkets.Entity;

namespace Smarkets.WpfApp2
{
    class Class1
    {
        private static IEnumerable<Match> xx;

        static Class1()
        {
            xx = Smarkets.DAL.Sqlite.ScoreMatch.MatchDefault();
        }

        public static IEnumerable<Option<Betfair.Model.Football.ThreeWayOdd>> GetOdds() => xx.Select(Maps.Map);
  

        public static IEnumerable<Betfair.Model.Football.ThreeWayResult> GetResults() => xx.Select(Maps.MapToResult);


        public static IEnumerable<(Betfair.Model.Football.ThreeWayResult, Option<Betfair.Model.Football.ThreeWayOdd>)> SelectAll()
        {
            return xx.Select(x => (Maps.MapToResult(x), Maps.Map(x)));
        }
    }


    class Maps
    {
        static LeaguesRepository lrepo;

        static Maps()
        {
            lrepo = new LeaguesRepository();
        }

        public static Option<Betfair.Model.Football.ThreeWayOdd> Map(Smarkets.Entity.Match match)
        {
            Option<string> xx = default;
            try
            {
                xx = lrepo.Get("Smarkets", match.League, "BetfairName");
            }
            catch (Exception ex)
            {
                return Option.None<Betfair.Model.Football.ThreeWayOdd>();
            }

            //Console.WriteLine("found match");
         
            return GetOdds(match).LastOrNone().FlatMap(o=> xx.Map(x=>

            new Betfair.Model.Football.ThreeWayOdd
            {
                EventDate = new DateTime(match.Start),
                Competition = x,
                CompetitionId = string.Empty,
                MarketId = match.EventId.ToString(),
                Player1Odd = (int)o.home,
                Player2Odd = (int)o.away,
                Player3Odd = (int)o.draw,
                Player1Id = 1,
                Player2Id = 2,
                Player1Name = string.Empty,
                Player2Name = string.Empty,
                OddsDate = new DateTime(o.time),
            }));


        }


        public static Betfair.Model.Football.ThreeWayResult MapToResult(Smarkets.Entity.Match match)
        {
            return new Betfair.Model.Football.ThreeWayResult
            {
                MarketId = match.EventId.ToString(),
                Player1Status = match.HomeScore > match.AwayScore ? Betfair.Model.EndResult.Winner : Betfair.Model.EndResult.Loser,
                Player3Status = match.HomeScore == match.AwayScore ? Betfair.Model.EndResult.Winner : Betfair.Model.EndResult.Loser,
                Player2Status = match.HomeScore < match.AwayScore ? Betfair.Model.EndResult.Winner : Betfair.Model.EndResult.Loser,
                
            };
        }

        public static IEnumerable<(long time, long home, long draw, long away)> GetOdds(Smarkets.Entity.Match src)
        {
            var market = src.Markets.SingleOrDefault(_ => _.Key == Betting.Enum.MarketType.FullTimeResult);
            var xx = market?.IndexedContracts.Count().Equals(3) ?? false ?
                  market.IndexedContracts.ToDictionary(c => c.Key, c => c.Value.MaxOffers) :
                  default;

            return xx != null ?
             from home in xx[Betting.Enum.ContractType.Home]
             join draw in xx[Betting.Enum.ContractType.Draw] on home.Time equals draw.Time
             join away in xx[Betting.Enum.ContractType.Away] on home.Time equals away.Time
             select (home.Time, home.Value, draw.Value, away.Value) :
             new List<(long time, long home, long draw, long away)>();
        }
    }
}
