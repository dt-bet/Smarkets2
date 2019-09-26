using MoreLinq;
using Smarkets.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smarkets.DAL.Sqlite
{
    public class ScoreMatch
    {
        private static class Helper
        {
            public static string GetPath(string x) => (System.IO.File.Exists(x) ? "" : "..//") + x;
        }
        private static readonly string teamsDatabase = Helper.GetPath("..//..//..///Smarkets.SqliteScoreApp//bin//debug//netcoreapp2.0//Teams.sqlite");

        private static readonly string data = "..//..//..//Smarkets.EF6App//Data";



        public static IEnumerable<Match> MatchDefault()
        {

            using (var conn2 = new SQLite.SQLiteConnection(teamsDatabase))
            {
                var conn = MatchRepository.SelectAll(data).Batch(100);
                var results = conn2.Table<Entity.Temp.Result>().ToList().ToArray();
                foreach (var batch in conn)
                    foreach (var ty in from result in results
                                       where result.HasResult
                                       join match in batch on result.Id equals match.EventId
                                       select new { match, result })
                    {
                  
                        ty.match.HomeScore = ty.result.Team1FullTimeScore;
                        ty.match.AwayScore = ty.result.Team2FullTimeScore;
                        yield return ty.match;
                    }
            }
        }

        public static IEnumerable<Match> MatchByName()
        {

            using (var conn2 = new SQLite.SQLiteConnection(teamsDatabase))
            {
                IEnumerable<Match> matches = MatchRepository.SelectAll(data);

                foreach (var joined in from match in matches
                                       join result in Temp.Repo.GetResult()
                                       on match.EventId equals result.result.Id
                                       where result.result.HasResult
                                       where result.team1.Name == match.GetHomeTeam()
                                       where result.team2.Name == match.GetAwayTeam()
                                       select (match, result.result))
                {
                    joined.match.HomeScore = joined.result.Team1FullTimeScore;
                    joined.match.AwayScore = joined.result.Team2FullTimeScore;
                    yield return joined.match;
                }
            }


        }
    }
}
