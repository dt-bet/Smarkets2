
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Optional;
using Scrape.Entity;
using Smarkets.Entity;

namespace Smarkets.WpfApp2
{
    class Class1
    {
        private static IEnumerable<Match> xx;

        static Class1()
        {
            xx = Smarkets.DAL.Sqlite.ScoreMatch.MatchDefault().Take(10000);
        }

        public static IEnumerable<Betfair.Model.Football.Odd> GetOdds() => xx
            .Select(Maps.Map)
            .Where(a => a.HasValue)
            .Select(a=>a.ValueOr(default(Betfair.Model.Football.Odd)));

        public static IEnumerable<Betfair.Model.Football.Result> GetResults() => xx.Select(Maps.MapToResult);

    }


    class Maps
    {
        static LeaguesRepository lrepo;

        static Maps()
        {
            lrepo = new LeaguesRepository();
        }

        public static Option<Betfair.Model.Football.Odd> Map(Smarkets.Entity.Match match)
        {


            Option<string> xx = default;
            try
            {
                xx = lrepo.Get("Smarkets", match.League, "BetfairName");
                if(string.IsNullOrEmpty(xx.ValueOr(string.Empty)))
                {
                    return default;
                }
            }
            catch (Exception ex)
            {
                return Option.None<Betfair.Model.Football.Odd>();
            }

            var odds = GetOdds(match).LastOrDefault();
            if (odds == default)
            {
                return default;
            }

            xx.MatchSome(a =>
            {
                Console.WriteLine();
            });
            return Option.Some<Betfair.Model.Football.Odd>(new Betfair.Model.Football.Odd
            {
                EventDate = match.Start,
                Competition = xx.ValueOr(string.Empty),
                CompetitionId = string.Empty,
                MarketId = string.Empty,
                Player1Odd = (int)odds.home,
                Player2Odd = (int)odds.away,
                Player3Odd = (int)odds.draw,
                Player1Id = 1,
                Player2Id = 2,
                Player1Name = string.Empty,
                Player2Name = string.Empty,
                OddsDate = odds.time,
            });


        }


        public static Betfair.Model.Football.Result MapToResult(Smarkets.Entity.Match match)
        {
            return new Betfair.Model.Football.Result
            {
                Player1Status = match.HomeScore > match.AwayScore ? 1 : 2,
                Player3Status = match.HomeScore < match.AwayScore ? 1 : 2,
                Player2Status = match.HomeScore == match.AwayScore ? 1 : 2,
            };
        }

        public static IEnumerable<(long time, long home, long draw, long away)> GetOdds(Smarkets.Entity.Match src)
        {
            var market = src.Markets.SingleOrDefault(_ => _.Key == 24);
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
