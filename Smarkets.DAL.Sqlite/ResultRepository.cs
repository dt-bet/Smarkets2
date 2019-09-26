using Smarkets.Entity.Temp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Smarkets.DAL.Temp
{
    public class Repo
    {
        string _name;
        public Repo(string name)
        {
            _name = name;
        }

        public (Dictionary<long, string> Teams, HashSet<long> Results) Get()
        {
            using (var conn2 = new SQLite.SQLiteConnection(_name))
            {
                conn2.CreateTable<Team>();
                conn2.CreateTable<Result>();
                var dictTeams = conn2.Table<Team>().ToList().ToDictionary(_ => _.Id, _ => _.Name);
                var hsResults = new HashSet<long>(conn2.Table<Result>().ToList().Select(_ => _.Id));

                return (dictTeams, hsResults);
            }
        }

        public static IEnumerable<(Result result, Team team1, Team team2)> GetResult()
        {
            using (var conn = new SQLite.SQLiteConnection("..//..//..//..//Smarkets.SqliteScoreApp//bin//debug//netcoreapp2.0//Teams.sqlite"))
            {
                var teams = conn.Table<Team>().ToList();
                foreach ((Result result, Team team1, Team team2) ty
                                                in from result in conn.Table<Result>().ToList()
                                                   where result.HasResult
                                                   join team in teams on
                                                   result.Team1Id equals team.Id
                                                   into xx
                                                   join team in teams on
                                                 result.Team2Id equals team.Id
                                                 into xy
                                                   let xs = xx.SingleOrDefault()
                                                   let xys = xy.SingleOrDefault()
                                                   where !xs.Equals(default) && !xys.Equals(default)
                                                   select (result, xs, xys)
                                                  )

                    yield return ty;
            }
        }


        public void Persist(IList<Team> teams, IList<Result> results)
        {
            using (var conn2 = new SQLite.SQLiteConnection(_name))
            {
                var teamsinsert = teams.Except(conn2.Table<Team>());

                try
                {
                    conn2.InsertAll(teamsinsert);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }
                conn2.InsertAll(results);
            }

        }



    }
}
