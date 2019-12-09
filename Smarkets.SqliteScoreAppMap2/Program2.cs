using System;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Collections.Generic;
using MoreLinq;

namespace Smarkets.SqliteScoreApp
{
    //class Program
    //{

        //static void Main(string[] args)
        //{


        //    using (var conn = new SQLite.SQLiteConnection("..//..//..//..//Smarkets.EF6App//bin//debug//netcoreapp2.0//Data/Smarkets.sqlite"))
        //    using (var conn2 = new SQLite.SQLiteConnection("..//..//..//..//Smarkets.SqliteScoreApp//bin//debug//netcoreapp2.0//Teams.sqlite"))
        //    using (var conn3 = new SQLite.SQLiteConnection("..//..//..//..//Smarkets.EF6App//bin//debug//netcoreapp2.0//Data//MapMatch.sqlite"))
        //    {

        //        conn.CreateTable<MatchTeam>();
        //        conn.CreateTable<TeamStat>();

        //        var teams = conn2.Table<Smarkets.Entity.Temp.Team>().ToList();
        //        var results = conn2.Table<Smarkets.Entity.Temp.Result>().ToList();
        //        var mapmatches = conn3.Table<DAL.Sqlite.IdMapKey>().ToArray();
        //        List<Match> matches = new List<Match>();
        //        foreach (var result in results)
        //        {
        //            if (!result.State)
        //                continue;

        //            var mapmatch = mapmatches.Where(_ => _.ForeignId == result.Id).ToList();
        //            if (mapmatch != null)
        //            {

        //                var matchteams = conn.Query<MatchTeam>("select * from MatchTeam where ParentId = ?", result.Id);
        //                if (matchteams.Count == 0)
        //                {

        //                    if (mapmatch.Count > 1)
        //                    {

        //                    }
        //                    else if (mapmatch.Count == 1)
        //                    {
        //                        var qmatches = conn.Query<Match>("select * from Match where Key = ?", mapmatch.First().Key);

        //                        if (qmatches.Count() > 1)
        //                        {

        //                        }

        //                        var dbmatch = qmatches.First();

        //                        var mt1 = new MatchTeam
        //                        {
        //                            Parent = dbmatch,
        //                            TeamId = result.Team1Id,
        //                            Key = (byte)Football.Enum.MatchTeamType.Home,
        //                        };

        //                        var tsf1 = new TeamStat
        //                        {
        //                            Parent = mt1,
        //                            Key = (byte)Football.Enum.TeamStatType.FullTimeGoal,
        //                            Value = result.Team1FullTimeScore,
        //                        };

        //                        var tsh1 = new TeamStat
        //                        {
        //                            Parent = mt1,
        //                            Key = (byte)Football.Enum.TeamStatType.HalfTimeGoal,
        //                            Value = result.Team1HalfTimeScore,

        //                        };
        //                        var mt2 = new MatchTeam
        //                        {
        //                            Parent = dbmatch,
        //                            TeamId = result.Team2Id,
        //                            Key = (byte)Football.Enum.MatchTeamType.Away,
        //                        };

        //                        var tsf2 = new TeamStat
        //                        {
        //                            Parent = mt2,
        //                            Key = (byte)Football.Enum.TeamStatType.FullTimeGoal,
        //                            Value = result.Team2FullTimeScore,
        //                        };

        //                        var tsh2 = new TeamStat
        //                        {
        //                            Parent = mt2,
        //                            Key = (byte)Football.Enum.TeamStatType.HalfTimeGoal,
        //                            Value = result.Team2HalfTimeScore,

        //                        };

        //                        if (dbmatch != null)
        //                        {
        //                            dbmatch.Status = dbmatch.Status;
        //                            conn.Update(dbmatch);
        //                        }
        //                        else
        //                        {
        //                            var match = new Match
        //                            {
        //                                Key = mapmatch.First().Key,
        //                                Status = (byte)UtilityEnum.ProcessState.Terminated,
        //                            };
        //                            conn.Insert(match);

        //                            mt1.Parent = match;
        //                            mt2.Parent = match;
        //                        }

        //                        conn.InsertAll(new[] { mt1, mt2 }, true);
        //                        conn.InsertAll(new[] { tsf1, tsh1, tsf2, tsh2 }, true);

        //                    }
        //                }
        //            }
        //            //matches.Add(match);

        //        };



        //    }
        //}













    }


//}