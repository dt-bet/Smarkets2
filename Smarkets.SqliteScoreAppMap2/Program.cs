using System;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Collections.Generic;
using MoreLinq;

using Smarkets.Entity;

namespace Smarkets.SqliteScoreApp
{
    class Program
    {

        //static void Main(string[] args)
        //{


        //    using (var conn = new SQLite.SQLiteConnection("..//..//..//..//Smarkets.EF6App//bin//debug//netcoreapp2.0//Data/Smarkets.sqlite"))
        //    using (var conn2 = new SQLite.SQLiteConnection("..//..//..//..//Smarkets.SqliteScoreApp//bin//debug//netcoreapp2.0//Teams.sqlite"))
        //    {
        //        var teams = conn2.Table<Smarkets.Entity.Temp.Team>().ToList();
        //        foreach (var ty in from joined in from result in conn2.Table<Smarkets.Entity.Temp.Result>().ToList()
        //                                          where result.HasResult
        //                                          join team in teams on
        //                                          result.Team1Id equals team.Id
        //                                          into xx
        //                                          join team in teams on
        //                                        result.Team2Id equals team.Id
        //                                        into xy
        //                                          select (result, xx, xy);
        //        from x in xx
        //        join team2 in teams on x.Id equals team2.Id
        //        select new { x, team1 = team1.Name, team2 = team2.Name }
        //                           join match in conn.Table<Smarkets.Entity.Match>().ToArray() on joined.x.Id equals match.EventId
        //                           select new { match, joined })
        //        {
        //            if (ty.joined.team1 == ty.match.GetHomeTeam() && ty.joined.team1 == ty.match.GetAwayTeam())
        //            {
        //                ty.match.HomeScore = ty.joined.x.Team1FullTimeScore;
        //                ty.match.AwayScore = ty.joined.x.Team2FullTimeScore;
        //            }
        //            else
        //            {

        //            }
        //        }
        //    }
        //}

        //private static IEnumerable<(Entity.Temp.Result result, Smarkets.Entity.Temp.Team team1, Smarkets.Entity.Temp.Team team2)> Gd()
        //{
        //    using (var conn = new SQLite.SQLiteConnection("..//..//..//..//Smarkets.SqliteScoreApp//bin//debug//netcoreapp2.0//Teams.sqlite"))
        //    {
        //        var teams = conn.Table<Smarkets.Entity.Temp.Team>().ToList();
        //        foreach ((Entity.Temp.Result result, Smarkets.Entity.Temp.Team team1, Smarkets.Entity.Temp.Team team2) ty
        //                                        in from result in conn.Table<Smarkets.Entity.Temp.Result>().ToList()
        //                                           where result.HasResult
        //                                           join team in teams on
        //                                           result.Team1Id equals team.Id
        //                                           into xx
        //                                           join team in teams on
        //                                         result.Team2Id equals team.Id
        //                                         into xy
        //                                           select (result, xx.Single(), xy.Single()))

        //            yield return ty;
        //    }
        //}
    }
}