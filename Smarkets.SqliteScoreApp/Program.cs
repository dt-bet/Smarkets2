using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Smarkets.DAL.Sqlite;
using MoreLinq;
using Smarkets.Entity.Temp;
using Smarkets.DAL.Temp;

namespace Smarkets.SqliteScoreApp
{
    class Program
    {

        static MyWebClient Client = new MyWebClient();
        static Dictionary<long, string> dictTeams;
        static HashSet<long> hsResults;
        static List<Team> teams = new List<Team>();
        static List<Result> results = new List<Result>();
        static Repo repo = new Repo("Teams.Sqlite");


        private static void Main(string[] args)
        {
            SQLitePCL.Batteries.Init();

            if (args[0]=="Tennis" && args[1]=="Teams")
                DownloadAndParseAndStoreTennisTeams();
            if (args[0] == "Tennis" && args[1] == "Scores")
                DownloadAndParseAndStoreTennisScores();
            if (args[0] == "Football" && args[1] == "Scores")
                DownloadAndParseAndStoreFootballScores();
            if (args[0] == "Football" && args[1] == "Teams")
                DownloadAndParseAndStoreFootballTeams();
            if (args[0] == "Football" && args[1] == "Scores2")
                DownloadAndParseAndStoreScores2("football", @"../../../../Smarkets.Football.SqliteApp/Data");
        }



        private static void DownloadAndParseAndStoreTennisTeams()
        {
            DownloadAndParseAndStoreTeams("Tennis", @"../../../../Smarkets.Tennis.SqliteApp/Data");
         
        }

        private static void DownloadAndParseAndStoreTennisScores()
        {
            DownloadAndParseAndStoreScores("Tennis", @"../../../../Smarkets.Tennis.SqliteApp/Data");
        }


        private static void DownloadAndParseAndStoreFootballScores()
        {
            DownloadAndParseAndStoreScores("Football", @"../../../../Smarkets.Football.SqliteApp/Data");
        }

        private static void DownloadAndParseAndStoreFootballTeams()
        {
            DownloadAndParseAndStoreTeams("Football", @"../../../../Smarkets.Football.SqliteApp/Data");
        }

        private static void DownloadAndParseAndStoreScores(string sport, string directoryRepo)
        {
            var dir = System.IO.Directory.CreateDirectory("../../../Data/" + sport);
            var sqlite = new UtilityDAL.Sqlite.KeyValueLite(dir.FullName);

            foreach (var (date, directory) in from directory in Directory.EnumerateDirectories(directoryRepo)
                                              let name = Path.GetFileName(directory)
                                              where Common.FileNameHelper.TryGetDateFromDirectoryName(name, out _)
                                              where sqlite.FindDate(directory) == null
                                              let date = Common.FileNameHelper.GetDateTimeFromDirectory(name)
                                              select
                                              (date, directory))
            {

                var info = MatchRepository.SelectInformation(directory);

                //var ids = Smarkets.DAL.Sqlite.MatchRepository.Select(date).Select(a => a.EventId.ToString()).ToArray();
                var newResults = Download2(100, date, info.Select(a => a.id.ToString()).ToArray());

                Console.WriteLine($"results downloaded {newResults.Count()}");
                foreach (var (ifo, newResult) in from ifo in info
                                                 join newResult in newResults
                                                 on ifo.id.ToString() equals newResult.id
                                                 select (ifo, newResult))
                {
                    int transferred = MatchRepository.TransferToDB(newResult.Item2, ifo.name);
                    Console.WriteLine($"results added {transferred}");
                    Console.Write(ifo.name);
                    Console.Write(ifo.id);
                }

                sqlite.Insert(new KeyValuePair<string, long>(directory, date.Ticks));
            }
            Console.WriteLine("Finished");
            Console.ReadLine();
        }

        private static void DownloadAndParseAndStoreTeams(string sport, string directoryPath)
        {
            var dir = System.IO.Directory.CreateDirectory($"../../../Data/{sport}/Teams");
            var sqlite = new UtilityDAL.Sqlite.KeyValueLite(dir.FullName);

            foreach (var (date, directory) in from directory in Directory.EnumerateDirectories(directoryPath)
                                              let name = Path.GetFileName(directory)
                                              where Common.FileNameHelper.TryGetDateFromDirectoryName(name, out _)
                                              where sqlite.FindDate(directory) == null
                                              let date = Common.FileNameHelper.GetDateTimeFromDirectory(name)
                                              select
                                              (date, directory))
            {

                var info = MatchRepository.SelectInformation(directory);

                //var ids = Smarkets.DAL.Sqlite.MatchRepository.Select(date).Select(a => a.EventId.ToString()).ToArray();
                var newTeams = DownloadCompetitors(100, date, info.Select(a => a.id.ToString()).ToArray()).ToArray();

                Console.WriteLine($"results downloaded {newTeams.Count()}");
                foreach (var (ifo, newTeam) in from ifo in info
                                               join newTeam in newTeams
                                               on ifo.id.ToString() equals newTeam.id
                                               select (ifo, newTeam))
                {
                    int transferred = MatchRepository.TransferToDB(newTeam.Item2, ifo.name);
                    Console.WriteLine($"results added {transferred}");
                    Console.Write(ifo.name);
                    Console.Write(ifo.id);
                }

                sqlite.Insert(new KeyValuePair<string, long>(directory, date.Ticks));
            }
            Console.WriteLine("Finished");
            Console.ReadLine();
        }


        private static void DownloadAndParseAndStoreScores2(string sport, string directoryRepo)
        {
            var dir = Directory.CreateDirectory("../../../Data/" + sport);
            var kvLite = new UtilityDAL.Sqlite.KeyValueLite(dir.FullName);
            using var sqlite = new SQLite.SQLiteConnection(Path.Combine(dir.FullName, "Teams.sqlite"));
            var results = sqlite.Table<Result>().ToArray();

            foreach (var (date, directory) in from directory in Directory.EnumerateDirectories(directoryRepo)
                                              let name = Path.GetFileName(directory)
                                              where Common.FileNameHelper.TryGetDateFromDirectoryName(name, out _)
                                              where kvLite.FindDate(directory) == null
                                              let date = Common.FileNameHelper.GetDateTimeFromDirectory(name)
                                              select
                                              (date, directory))
            {

                foreach ((string ifo, Result result) in from ifo in MatchRepository.SelectInformation(directory)
                                                 join result in results
                                                 on ifo.id equals result.Id
                                                 select (ifo.name, result))
                {

                    Console.WriteLine(ifo);
                    var newResults = Map.ResultMap.ToResults(result);
                    try
                    {
                        int transferred = MatchRepository.TransferToDB(newResults, ifo);
                    }
                    catch(Exception ex)
                    {
                        
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                    }
                }
                kvLite.Insert(new KeyValuePair<string, long>(directory, date.Ticks));
            }
            Console.WriteLine("Finished");
            Console.ReadLine();
        }


        //private static void DownloadAndParseAndStoreTennisScores()
        //{

        //    var dir = System.IO.Directory.CreateDirectory("../../../Data/Tennis");
        //    var sqlite = new UtilityDAL.Sqlite.KeyValueLite(dir.FullName);

        //    foreach (var (date, directory) in from directory in System.IO.Directory.EnumerateDirectories(@"../../../../Smarkets.Tennis.SqliteApp/Data")
        //                                      let name = System.IO.Path.GetFileName(directory)
        //                                      where Common.FileNameHelper.TryGetDateFromDirectoryName(name, out _)
        //                                      where sqlite.FindDate(directory) == null
        //                                      let date = Common.FileNameHelper.GetDateTimeFromDirectory(name)
        //                                      select
        //                                      (date, directory))
        //    {

        //        var info = MatchRepository.SelectInformation(directory);

        //        //var ids = Smarkets.DAL.Sqlite.MatchRepository.Select(date).Select(a => a.EventId.ToString()).ToArray();
        //        var newResults = Download2(100, date, info.Select(a => a.id.ToString()).ToArray());

        //        Console.WriteLine($"results downloaded {newResults.Count()}");
        //        foreach (var (ifo, newResult) in from ifo in info
        //                                         join newResult in newResults
        //                                         on ifo.id.ToString() equals newResult.id
        //                                         select (ifo, newResult))
        //        {
        //            int transferred = MatchRepository.TransferToDB(newResult.Item2, ifo.name);
        //            Console.WriteLine($"results added {transferred}");
        //            Console.Write(ifo.name);
        //            Console.Write(ifo.id);
        //        }

        //        sqlite.Insert(new KeyValuePair<string, long>(directory, date.Ticks));
        //    }
        //    Console.WriteLine("Finished");
        //    Console.ReadLine();
        //}





        //private static void DownloadAndParseAndStoreFootballScores()
        //{

        //    DateTime minDate = File.ReadAllLines("../../../Data/filesparsed.txt")
        //        .Where(_ => !string.IsNullOrEmpty(_))
        //        .Select(_ => DateTime.ParseExact(_, Constants.dateTimeFormat, CultureInfo.InvariantCulture))
        //        .Max();

        //    var get = repo.Get();
        //    dictTeams = get.Teams;
        //    hsResults = get.Results;

        //    HashSet<long> dates = new HashSet<long>();

        //    // Only deserialize files with unique days
        //    Func<DateTime, bool> func = d => dates.Add(d.Date.Ticks);
        //    foreach (var file in Smarkets.DAL.XML.Repo.SelectFiles(minDate, selector: func))
        //    {
        //        var ids = GetIds(file).ToArray();
        //        var newResults = Download(100, file.TimeStamp, ids.Except(results.Select(_ => _.Id.ToString())).ToArray()).ToArray();
        //        repo.Persist(teams, newResults);
        //        results.AddRange(newResults);
        //        Console.WriteLine($"results added {newResults.Count()}");
        //        Console.WriteLine(file.TimeStamp);
        //        File.AppendAllLines("../../../Data/filesparsed.txt", new[] { file.TimeStamp.ToString(Smarkets.Constants.dateTimeFormat) });
        //    }
        //    Console.WriteLine("Finished");
        //    Console.ReadLine();
        //}


        static string[] GetIds(Model.XML.Odds file)
        {
            DateTime dtn = DateTime.Now;
            return file.Events.
                 Where(_ => _.Type == "Football match" && (_.StartTimeAsDateTime - dtn).Days < 0 && hsResults.Add(_.Id))
                 .Select(_ => _.Id.ToString()).ToArray();

        }


        static IEnumerable<Result> Download(int batchsize, DateTime dateTime, params string[] ids)
        {
            if (ids.Length > 0)
            {
                foreach (var idss in ids.Batch(batchsize))
                {
                    System.Threading.Thread.Sleep(ids.Length * 50);
                    IEnumerable<Result> results = null;
                    try
                    {
                        Client.DownloadFile(new Uri(WebAPI.AddressBuilder.getStates(idss.ToArray())), "Smarkets.json");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        if (batchsize == 1)
                        {
                            File.AppendAllLines("../../../Data/Id_Error.txt", new[] { $"{dateTime.ToString(Smarkets.Constants.dateTimeFormat)}, {idss.First()}, {e.Message}" });
                            continue;
                        }
                        results = Download((int)(batchsize / 10d), dateTime, idss.ToArray());
                    }
                    // Has entered try catch block.
                    if (results != null)
                        foreach (var result in results)
                            yield return result;
                    // Has not entered catch block so file waiting to be parsed
                    else
                        foreach (var result in Parse())
                            yield return result;

                }
            }

        }


        static IEnumerable<(string id, Smarkets.Entity.XML.Result[])> Download2(int batchsize, DateTime dateTime, params string[] ids)
        {
            if (ids.Length > 0)
            {
                foreach (var idss in ids.Batch(batchsize))
                {
                    System.Threading.Thread.Sleep(ids.Length * 50);
                    IEnumerable<(string id, Smarkets.Entity.XML.Result[])> results = null;
                    try
                    {
                        Client.DownloadFile(new Uri(WebAPI.AddressBuilder.getStates(idss.ToArray())), TennisSmarketsFile);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        if (batchsize == 1)
                        {
                            File.AppendAllLines("../../../Data/Id_Error.txt", new[] { $"{dateTime.ToString(Smarkets.Constants.dateTimeFormat)}, {idss.First()}, {e.Message}" });
                            continue;
                        }
                        results = Download2((int)(batchsize / 10d), dateTime, idss.ToArray());
                    }
                    // Has entered try catch block.
                    if (results != null)
                        foreach (var result in results)
                            yield return result;
                    // Has not entered catch block so file waiting to be parsed
                    else
                        foreach (var result in ParseTennisResults())
                            yield return result;

                }
            }
        }


        static IEnumerable<(string id, Smarkets.Entity.XML.MatchTeam[])> DownloadCompetitors(int batchsize, DateTime dateTime, params string[] ids)
        {
            if (ids.Length > 0)
            {
                foreach (var idss in ids.Batch(batchsize))
                {
                    System.Threading.Thread.Sleep(ids.Length * 50);
                    IEnumerable<(string id, Smarkets.Entity.XML.MatchTeam[])> results = null;
                    try
                    {
                        Client.DownloadFile(new Uri(WebAPI.AddressBuilder.getCompetitors(idss.ToArray())), TennisCompetitorsSmarketsFile);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        if (batchsize == 1)
                        {
                            File.AppendAllLines("../../../Data/Id_Error.txt", new[] { $"{dateTime.ToString(Smarkets.Constants.dateTimeFormat)}, {idss.First()}, {e.Message}" });
                            continue;
                        }
                        results = DownloadCompetitors((int)(batchsize / 10d), dateTime, idss.ToArray());
                    }
                    // Has entered try catch block.
                    if (results != null)
                        foreach (var result in results)
                            yield return result;
                    // Has not entered catch block so file waiting to be parsed
                    else
                        foreach (var result in ParseTennisCompetitors())
                            yield return result;

                }
            }

        }


        static IEnumerable<Result> Parse()
        {
            foreach (var p in Newtonsoft.Json.JsonConvert.DeserializeObject<Model.JSON.States>(System.IO.File.ReadAllText("Smarkets.json")).event_states)
            {
                Result result = default;
                try
                {
                    result = Map.JSONToEntity.EventStateMap.GetResult(p, dictTeams, teams);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (!result.Equals(default))
                {
                    yield return result;
                }
            }
        }

        const string TennisSmarketsFile = "TennisSmarkets.json";

        const string TennisCompetitorsSmarketsFile = "TennisCompetitorsSmarkets.json";

        static IEnumerable<(string id, Smarkets.Entity.XML.Result[])> ParseTennisResults()
        {
            foreach (var p in Newtonsoft.Json.JsonConvert.DeserializeObject<Model.JSON.States>(System.IO.File.ReadAllText(TennisSmarketsFile)).event_states)
            {
                if (p.state == "cancelled")
                    continue;
                Smarkets.Entity.XML.Result[] result = default;
                try
                {
                    result = Map.ResultMap.ToResults(p).ToArray();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (result != null)
                {
                    yield return (p.id, result);

                }
                else
                {

                }
            }
        }
        static IEnumerable<(string id, Smarkets.Entity.XML.MatchTeam[])> ParseTennisCompetitors()
        {
            foreach (var grouping in Newtonsoft.Json.JsonConvert
                                .DeserializeObject<Model.JSON.Competitors>(System.IO.File.ReadAllText(TennisCompetitorsSmarketsFile)).competitors.GroupBy(a=>a.event_id))
            {

                Smarkets.Entity.XML.MatchTeam[] result = default;

                try
                {
                    result = Map.CompetitorMap.ToMatchTeamEntities(grouping).ToArray();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (result != null)
                {
                    yield return (grouping.Key, result);

                }
                else
                {

                }
            }
        }
    }
}