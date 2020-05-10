
//using Smarkets.Model.JSON;
using Betting.Entity.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UtilityDAL;
using static Smarkets.Map.BetfairMap;

namespace Smarkets.Tennis.SqliteApp
{
    static class Program
    {
        public static void Main(string[] args)
        {
            SQLitePCL.Batteries.Init();

            if (args.Length == 0 || args[0] == "Points")
            {
                Process.InsertPoints();
                Process.GetProfits();
            }
            //else if (args[0]=="Day_Points")
            //{
            //    InsertPoints2();
            //}
            else if (args[0] == "Odds")
                InsertOdds();
            else if (args[0] == "Results")
                InsertResults();
            else if (args[0] == "Matches")
                InsertMatches();

        }




        private static void InsertOdds()
        {
       using var sqlite = new SQLite.SQLiteConnection("../../../Data/Main.sqlite");
            sqlite.CreateTable<TwoWayOdd>();
            foreach (var match in SelectMatches(sqlite))
            {
                var twoWayOdds = Smarkets.Map.BetfairMap.Map(match).ToArray();
                if (twoWayOdds.Length > 1)
                {

                }
                sqlite.InsertAll(twoWayOdds);
                Console.WriteLine(match.EventId);
            }
            Console.WriteLine("Finished");

            static IEnumerable<Entity.Match> SelectMatches(SQLite.SQLiteConnection sqlite)
            {
                return from match in Smarkets.DAL.Sqlite.MatchRepository.SelectAll(@"..\..\..\..\Smarkets.Tennis.SqliteApp\Data\")
                       join two in sqlite.Table<TwoWayOdd>()
                       on match.EventId equals FromGUIDToLong(two.Guid)
                       into temp
                       where temp.Any() == false
                       select match;
            }
        }


        private static void InsertResults()
        {


            using var sqlite = new SQLite.SQLiteConnection("../../../Data/Main.sqlite");
            sqlite.CreateTable<Result>();
            foreach (var match in SelectMatches(sqlite))
            {
                var result = Smarkets.Map.BetfairMap.MapToResult(match);
                if (result != null)
                {
                    sqlite.Insert(result);
                    Console.WriteLine(match.EventId);
                }
            }
            Console.WriteLine("Finished");

            static IEnumerable<Entity.Match> SelectMatches(SQLite.SQLiteConnection sqlite)
            {
                return from match in Smarkets.DAL.Sqlite.MatchRepository.SelectAll(@"..\..\..\..\Smarkets.Tennis.SqliteApp\Data\")
                       join two in sqlite.Table<Result>()
                       on match.Markets.Single(m => m.Key == Betting.Enum.MarketType.FullTimeResult).Id.ToString()
                       equals two.MarketId
                       into temp
                       where temp.Any() == false
                       select match;
            }
        }


        public static long FromGUIDToLong(Guid input)
        {
            byte[] buffer = input.ToByteArray();

            long l = BitConverter.ToInt64(buffer, 0);
            return l;
        }

        public static Guid ToGUID(long input)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(input).CopyTo(bytes, 0);
            return new Guid(bytes);
        }

        private static void InsertMatches()
        {

            var deserialisedFileTest = DAL.XML.Repo.GetOdds(@"C:\Users\rytal\Documents\Visual Studio 2019\Projects\MasterProject\Smarkets2\Smarkets.Tennis.SqliteApp\Data\Tennis\TennisExampleOdds.xml");

            var eventsTest = deserialisedFileTest.Events.Where(GetEventPredicate).ToArray();

            var entitiesTest = Smarkets.XML.Map.EntityMap.MapToEntity(eventsTest, deserialisedFileTest.TimeStamp);

            var dir = System.IO.Directory.CreateDirectory("../../../Data/Tennis");
            var sqlite = new UtilityDAL.Sqlite.KeyValueLite(dir.FullName);

            foreach (var filename in from file in System.IO.Directory.EnumerateFiles(Constants.XMLDirectory)
                                     let name = System.IO.Path.GetFileNameWithoutExtension(file)
                                     where sqlite.FindDate(name) == null
                                     select new { file, name })
            {

                var deserialisedFile = DAL.XML.Repo.GetOdds(filename.file);
                Console.WriteLine("file:" + deserialisedFile.TimeStamp + "       @ " + DateTime.Now);

                var events = deserialisedFile.Events.Where(GetEventPredicate).ToArray();

                var entities = Smarkets.XML.Map.EntityMap.MapToEntity(events, deserialisedFile.TimeStamp);

                var leagues = Smarkets.XML.Map.EntityMap.MapToLeague(events);

                Console.WriteLine("Inserted Matches - " + Smarkets.DAL.Sqlite.MatchRepository.TransferToDB(entities.ToArray()));

                Console.WriteLine("Inserted Leagues - " + Smarkets.DAL.Sqlite.MatchRepository.TransferToDB(leagues.ToArray()));

                sqlite.Insert(KeyValuePair.Create(filename.name, DateTime.Now));
            }


            Console.ReadLine();

            bool GetEventPredicate(Model.XML.Event ee)
            {
                if (ee.Type.Contains("tennis", StringComparison.InvariantCultureIgnoreCase)
                    && ee.Parent.Contains("outright", StringComparison.InvariantCultureIgnoreCase) == false
                    && ee.ParentName.Contains("outright", StringComparison.InvariantCultureIgnoreCase) == false
                    )
                    return true;
                return false;
            }
        }
    }

 
}

