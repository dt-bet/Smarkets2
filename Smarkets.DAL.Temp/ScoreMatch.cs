using Smarkets.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smarkets.DAL.Sqlite
{
    public class ScoreMatch
    {
        public static IEnumerable<Match> Match()
        {

            using (var conn2 = new SQLite.SQLiteConnection("..//..//..///Smarkets.SqliteScoreApp//bin//debug//netcoreapp2.0//Teams.sqlite"))
            {
                var conn = System.IO.Directory.EnumerateFiles("..//..//..//Smarkets.EF6App//Data", "*.sqlite", System.IO.SearchOption.AllDirectories)
                    .Select(_ => new SQLite.SQLiteConnection(_).Table<Match>().ToArray().First());

                List<Entity.Temp.Team> teams = conn2.Table<Smarkets.Entity.Temp.Team>().ToList();

                foreach (var ty in from joined in from team1 in teams
                                                  join result in conn2.Table<Smarkets.Entity.Temp.Result>().ToList() on team1.Id equals result.Team1Id
                                                  into xx
                                                  from x in xx
                                                  join team2 in teams on x.Id equals team2.Id
                                                  select new { x, team1 = team1.Name, team2 = team2.Name }
                                   join match in conn on joined.x.Id equals match.EventId
                                   select new { match, joined })
                {
                    if (ty.joined.team1 == ty.match.GetHomeTeam() && ty.joined.team1 == ty.match.GetAwayTeam())
                    {
                        ty.match.HomeScore = ty.joined.x.Team1FullTimeScore;
                        ty.match.AwayScore = ty.joined.x.Team2FullTimeScore;

                        yield return ty.match;
                    }
                    else
                    {

                    }
                }
            }
        }

        public static IEnumerable<Match> Match2()
        {

            using (var conn2 = new SQLite.SQLiteConnection("..//..//..///Smarkets.SqliteScoreApp//bin//debug//netcoreapp2.0//Teams.sqlite"))
            {
                var conn = System.IO.Directory.EnumerateFiles("..//..//..//Smarkets.EF6App//Data", "*.sqlite", System.IO.SearchOption.AllDirectories)
                    .Select(_ => Smarkets.DAL.Sqlite.MatchRepository. SQLite.SQLiteConnection(_).Table<Match>().ToArray().First()).Take(100).ToList();

                var results = conn2.Table<Smarkets.Entity.Temp.Result>().ToList().Take(1000).ToArray();

                foreach (var ty in from result in results
                                   join match in conn on result.Id equals match.EventId
                                   select new { match, result})
                {

                        ty.match.HomeScore = ty.result.Team1FullTimeScore;
                        ty.match.AwayScore = ty.result.Team2FullTimeScore;

                        yield return ty.match;
         
                }
            }
        }
    }
}
