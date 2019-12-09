using System;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using Smarkets.Model.JSON;
using System.Net.Http;
using System.Threading.Tasks;
using Smarkets.DAL.Sqlite;
using Smarkets.Entity;
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
            DateTime minDate = File.ReadAllLines("../../../Data/filesparsed.txt")
                .Where(_ => !string.IsNullOrEmpty(_))
                .Select(_ => DateTime.ParseExact(_, Constants.dateTimeFormat, CultureInfo.InvariantCulture))
                .Max();

            var get = repo.Get();
            dictTeams = get.Teams;
            hsResults = get.Results;

            HashSet<long> dates = new HashSet<long>();
            // Only deserialize files with unique days
            Func<DateTime, bool> func = d => dates.Add(d.Date.Ticks);
            foreach (var file in Smarkets.DAL.XML.Repo.SelectFiles(minDate, selector:func))
            {
                var ids = GetIds(file).ToArray();
                var newResults = Download(100, file.TimeStamp, ids.Except(results.Select(_ => _.Id.ToString())).ToArray()).ToArray();
                repo.Persist(teams, newResults);
                results.AddRange(newResults);
                Console.WriteLine($"results added {newResults.Count()}");
                Console.WriteLine(file.TimeStamp);
                File.AppendAllLines("../../../Data/filesparsed.txt", new[] { file.TimeStamp.ToString(Smarkets.Constants.dateTimeFormat) });
            }
            Console.WriteLine("Finished");
            Console.ReadLine();
        }


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
                        foreach (var result in sfd())
                            yield return result;

                }
            }

        }


        static IEnumerable<Result> sfd()
        {
            foreach (var p in Newtonsoft.Json.JsonConvert.DeserializeObject<Model.JSON.States>(System.IO.File.ReadAllText("Smarkets.json")).event_states)
            {
                Result result = default;
                try
                {
                    result = Parse.GetResult(p, dictTeams, teams);
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





    }


}