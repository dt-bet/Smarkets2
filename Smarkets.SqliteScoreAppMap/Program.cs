using FootballSystem.Entity.Sqlite;
using System;

namespace Smarkets.SqliteScoreAppMap
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var conn2 = new SQLite.SQLiteConnection("..//..//..//Smarkets.SqliteScoreApp.Teams.sqlite"))
            {

                var teams = conn2.Table<Smarkets.Entity.Temp.Team>().ToList();
                var results = conn2.Table<Smarkets.Entity.Temp.Result>().ToList();

                foreach (var result in results)
                {
                    var mt = new MatchTeam
                    {
                        TeamId = result.Team1Id,
                        Key = (byte)Football.Enum.MatchTeamType.Home,
                        Statistics = new System.Collections.Generic.List<TeamStat>
                            {
                                new TeamStat
                                {
                                 Key =(byte)Football.Enum.TeamStatType.FullTimeGoal,
                                Value=result.Team1FullTimeScore,
                                }
                                ,
                                new TeamStat
                                {
                                Key=(byte)Football.Enum.TeamStatType.HalfTimeGoal,
                                Value=result.Team1HalfTimeScore,

                                }
                            }
                    };

                    var mt2 = new MatchTeam
                    {
                        TeamId = result.Team2Id,
                        Key = (byte)Football.Enum.MatchTeamType.Away,
                        Statistics = new System.Collections.Generic.List<TeamStat>
                            {
                                new TeamStat
                                {
                                 Key =(byte)Football.Enum.TeamStatType.FullTimeGoal,
                                Value=result.Team2FullTimeScore,
                                }
                                ,
                                new TeamStat
                                {
                                Key=(byte)Football.Enum.TeamStatType.HalfTimeGoal,
                                Value=result.Team2HalfTimeScore,

                                }
                            }
                    };

                    var match = new Match
                    {
                        Id = result.Id,
                        Status = (byte)UtilityEnum.ProcessState.Terminated,
                        Teams = new System.Collections.Generic.List<MatchTeam>
                        {
                            mt,mt2
                        }


                    };


                };


            }
        }
    }
}
