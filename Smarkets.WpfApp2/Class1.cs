﻿
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
            xx = Smarkets.DAL.Sqlite.ScoreMatch.MatchDefault().Take(100);
        }

        public static IEnumerable<Betfair.Model.Football.ThreeWayOdd> GetOdds() => xx
            .Select(Maps.Map)
            .Where(a => a.HasValue)
            .Select(a=>a.ValueOr(default(Betfair.Model.Football.ThreeWayOdd)));




        public static IEnumerable<Betfair.Model.Football.ThreeWayResult> GetResults() => xx.Select(Maps.MapToResult);

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
                if(string.IsNullOrEmpty(xx.ValueOr(string.Empty)))
                {
                    return default;
                }
            }
            catch (Exception ex)
            {
                return Option.None<Betfair.Model.Football.ThreeWayOdd>();
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
            return Option.Some(new Betfair.Model.Football.ThreeWayOdd
            {
                EventDate = new DateTime(match.Start),
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
                OddsDate = new DateTime(odds.time),
            });


        }


        public static Betfair.Model.Football.ThreeWayResult MapToResult(Smarkets.Entity.Match match)
        {
            return new Betfair.Model.Football.ThreeWayResult
            {
                Player1Status = match.HomeScore > match.AwayScore ? Betfair.Model.EndResult.Winner : Betfair.Model.EndResult.Loser,
                Player3Status = match.HomeScore == match.AwayScore ? Betfair.Model.EndResult.Winner : Betfair.Model.EndResult.Loser,
                Player2Status = match.HomeScore < match.AwayScore ? Betfair.Model.EndResult.Winner : Betfair.Model.EndResult.Loser,
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
