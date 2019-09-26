//using Betting.Entity.Sqlite;
//using SportsBetting.Entity.Sqlite;
//using SQLite;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Smarkets.SqliteApp2
//{
//    class Repo
//    {
//        public static bool TransferToDB(IEnumerable<Match> matches)
//        {

//            //var x = new SQLiteConnection("Matches.sqlite");

//            //var table = x.Table<MatchFile>();
//            var directory = System.IO.Directory.CreateDirectory("../../Data");
//            //var files = directory.GetFiles().Select(_=>new { _.FullName, key=FootballSystem.KeyHelper.UnpackKey(_.Name) });


//            foreach (var match in matches)
//            {
//                bool b = System.IO.File.Exists($"{directory.FullName}/{match.Key.Replace("@","_").Replace("-", "_").Replace(":", "_")}.sqlite");

//                using (var connection = new SQLiteConnection($"{directory.FullName}/{match.Key.Replace("@", "_").Replace("-", "_").Replace(":", "_")}.sqlite"))
//                {

//                    connection.CreateTable<Entity1>();
//                    if (!b)
//                    {
//                        //connection.CreateTable<Match>();
//                        connection.CreateTable<Market>();
//                        connection.CreateTable<Contract>();
//                        connection.CreateTable<Price>();

//                    }

//                    connection.Insert(match);
//                    connection.InsertAll(match.Markets);
//                    connection.InsertAll(match.Markets.SelectMany(_ => _.Contracts));
//                    connection.InsertAll(match.Markets.SelectMany(_ => _.Contracts).Select(_ => _.Bids));
//                    connection.InsertAll(match.Markets.SelectMany(_ => _.Contracts).Select(_ => _.Offers));
//                }
//            }

//            return true;
//        }
//    }

//    public class Entity1

//    {


//        public int Id { get; set; }
//        //ToDO place ignore across all entities

//        [Ignore]
//        [UtilityAttribute.Child]
//        public List<Entity2> Entity2s { get; set; }

//    }
//    public class Entity2

//    {

//        public int Id { get; set; }


//    }


//}
