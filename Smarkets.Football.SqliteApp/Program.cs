
using Betting.Entity.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Smarkets.Tennis.SqliteApp
{
    static class Program
    {
        public static void Main(string[] args)
        {
            SQLitePCL.Batteries.Init();

            try
            {
                if (args.Length == 0 || args[0] == "Points")
                {
                    Process.InsertPoints();
                    Process.GetProfits();
                }
                else if (args[0] == "Odds")
                    InsertOdds();
                else if (args[0] == "Results")
                    InsertResults();
                else if (args[0] == "Matches")
                    InsertMatches();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Finished");
            Console.ReadLine();
            // Console.WriteLine("Finished");
        }




        private static void InsertResults()
        {

            using var sqlite = new SQLite.SQLiteConnection("../../../Data/Main.sqlite");
            sqlite.CreateTable<ThreeWayResult>();
            foreach (var match in SelectMatches(sqlite))
            {
                try
                {
                    var result = Smarkets.Map.BetfairMap.MapToThreeWayResult(match);
                    if (result != null)
                    {
                        sqlite.Insert(result);
                        Console.WriteLine(match.EventId);
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine("Finished");

            static IEnumerable<Entity.Match> SelectMatches(SQLite.SQLiteConnection sqlite)
                => from match in DAL.Sqlite.MatchRepository.SelectAll(@"../../../Data")
                   join three in sqlite.Table<ThreeWayResult>()
                   on match.Markets.Single(m => m.Key == Betting.Enum.MarketType.FullTimeResult).Id.ToString()
                   equals three.MarketId
                   into temp
                   where temp.Any() == false
                   select match;
        }


        private static void InsertOdds()
        {
            using var sqlite = new SQLite.SQLiteConnection("../../../Data/Main.sqlite");
            sqlite.CreateTable<ThreeWayOdd>();
            foreach (var match in SelectMatches(sqlite))
            {
                var odds = Map.BetfairMap.MapToThreeWayOdd(match).ToArray();
                if (odds.Length > 1)
                {

                }
                sqlite.InsertAll(odds);
                Console.WriteLine(match.EventId);
            }
            Console.WriteLine("Finished");

            static IEnumerable<Entity.Match> SelectMatches(SQLite.SQLiteConnection sqlite)
                => from match in from x in DAL.Sqlite.MatchRepository.SelectAll(@"../../../Data")
                                 where x.MatchTeams.Count() == 2
                                 select x
                   join three in sqlite.Table<ThreeWayOdd>()
                   on match.EventId equals Common.GUIDHelper.FromGUIDToLong(three.Guid)
                   into temp
                   where temp.Any() == false
                   select match;

        }




        private static void InsertMatches()
        {

            var deserialisedFileTest = DAL.XML.Repo.GetOdds(@"C:\Users\rytal\Documents\Visual Studio 2019\Projects\MasterProject\Smarkets2\Smarkets.EF6App\ExampleData\ExampleData_Football.xml");

            var eventsTest = deserialisedFileTest.Events.Where(GetEventPredicate).ToArray();

            var entitiesTest = Smarkets.XML.Map.EntityMap.MapToEntity(eventsTest, deserialisedFileTest.TimeStamp);

            var dir = System.IO.Directory.CreateDirectory("../../../Data/Football");
            var sqlite = new UtilityDAL.Sqlite.KeyValueLite(dir.FullName);
            var xx = sqlite.FindString("smarkets2016_12_15T21_40_44");
            foreach ((string file, string name) in from file in System.IO.Directory.EnumerateFiles(Constants.XMLDirectory)
                                                   let name = System.IO.Path.GetFileNameWithoutExtension(file)
                                                   where sqlite.FindDate(name) == null
                                                   select (file, name))
            {

                var deserialisedFile = DAL.XML.Repo.GetOdds(file);
                Console.WriteLine("file:" + deserialisedFile.TimeStamp + "       @ " + DateTime.Now);

                var events = deserialisedFile.Events.Where(GetEventPredicate).ToArray();

                var entities = XML.Map.EntityMap.MapToEntity(events, deserialisedFile.TimeStamp);

                var leagues = XML.Map.EntityMap.MapToLeague(events);


                Console.WriteLine("Inserted Matches - " + Smarkets.DAL.Sqlite.MatchRepository.TransferToDB(entities.ToArray()));

                Console.WriteLine("Inserted Leagues - " + Smarkets.DAL.Sqlite.MatchRepository.TransferToDB(leagues.ToArray()));

                sqlite.Insert(KeyValuePair.Create(name, DateTime.Now));
            }




            bool GetEventPredicate(Model.XML.Event ee)
            {
                if (ee.Type.Equals("Football match", StringComparison.InvariantCultureIgnoreCase)
                    && ee.Parent.Contains("outright", StringComparison.InvariantCultureIgnoreCase) == false
                    && ee.ParentName.Contains("outright", StringComparison.InvariantCultureIgnoreCase) == false
                    )
                    return true;
                return false;
            }
        }
    }


}
