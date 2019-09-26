
////using Smarkets.Model.JSON;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using UtilityDAL;

//namespace Smarkets.SqliteApp2
//{
//    static class Program
//    {


//        private static void Main(string[] args)
//        {

//           // var conn = new SportsBetting.DAL.Sqlite.Repo( @"Data/Smarkets." + UtilityDAL.Constants.SqliteDbExtension,false);

//            //var sqlite = new UtilityDAL.Sqlite<UtilityDAL.Model.KeyValueDate, string>(_ => _.Key);

//            var repo = new Smarkets.DAL.Sqlite.Repo();


//            #region deprecated
//           // var conn2 = new Smarkets.DAL.Sqlite.Repo("" + "Smarkets." + UtilityDAL.Constants.SqliteDbExtension);

//            //if (!System.IO.File.Exists(file))
//            //{
//            //    var fileStream = System.IO.File.Create(file);
//            //    fileStream.Close();
//            //}
//            //string lastfilesaved = System.IO.File.ReadAllLines(file).LastOrDefault();

//            //DateTime minDate = lastfilesaved != null ? Smarkets.DAL.Deserialize.ConvertToDateTime(lastfilesaved) : new DateTime();
//            #endregion deprecated
//            //string fileend = @"E:\SMarketXML\smarkets2017_01_11T18_12_23.xml";

//            foreach (var file in System.IO.Directory.GetFiles(Smarkets.Constants.XMLDirectory))
//            {
//                //if (file != fileend)
//                //{
//                //string name = System.IO.Path.GetFileNameWithoutExtension(file);
//                //if (sqlite.FindById(name) != null)
//                //    continue;

//                var deserialisedFile = Smarkets.DAL.XMLRepo.GetOdds(file);
//                var entities = Smarkets.Map.EntityMap.MapToEntity(deserialisedFile.Events.Where(Program.GetPredicate()).ToArray(), deserialisedFile.TimeStamp);

//                //repo.Persist(entities);

//                //conn.TransferToDB(entities);

//                Repo.TransferToDB(entities);

//                Console.WriteLine("file:" + deserialisedFile.TimeStamp + "       @ " + DateTime.Now);
//                //}
//                //else
//                //    break;
//            }

//           // Console.WriteLine("ended @ " + fileend);
//            Console.ReadLine();
//        }

//        private static Func<Smarkets.Model.XML.Event, bool> GetPredicate() => (ee) => ee.Type == "Football match" && ee.ParentName.ToLower() != "outright";

//    }









//        static class Program2
//    {
      

//        private static void Main2(string[] args)
//        {

//           //var conn = new SportsBetting.DAL.Sqlite.Repo( @"Data/Smarkets." + UtilityDAL.Constants.SqliteDbExtension,false);

//           // var sqlite = new UtilityDAL.Sqlite<UtilityDAL.Model.KeyValueDate, string>(_ => _.Key);

//           // var repo = new Smarkets.DAL.Sqlite.Repo();


//            #region deprecated
//            //var conn2 = new Smarkets.DAL.Sqlite.Repo("" + "Smarkets." + UtilityDAL.Constants.SqliteDbExtension);

//            //if (!System.IO.File.Exists(file))
//            //{
//            //    var fileStream = System.IO.File.Create(file);
//            //    fileStream.Close();
//            //}
//            //string lastfilesaved = System.IO.File.ReadAllLines(file).LastOrDefault();

//            //DateTime minDate = lastfilesaved!=null? Smarkets.DAL.Deserialize.ConvertToDateTime(lastfilesaved) : new DateTime();
//            #endregion deprecated
//            //string fileend = @"E:\SMarketXML\smarkets2017_01_11T18_12_23.xml";

//            //foreach (var file in System.IO.Directory.GetFiles(Smarkets.Constants.XMLDirectory))
//            //{
//            //    if (file != fileend )
//            //    {
//            //        string name = System.IO.Path.GetFileNameWithoutExtension(file);
//            //        if (sqlite.FindById(name) != null)
//            //            continue;

//            //        var deserialisedFile = Smarkets.DAL.XMLRepo.GetOdds(file);
//            //        var entities = Smarkets.Map.EntityMap.MapToEntity(deserialisedFile.Events.Where(GetPredicate()).ToArray(), deserialisedFile.TimeStamp);

//            //        repo.Persist(entities);

//            //        conn.TransferToDB(entities);

//            //        sqlite.Insert(UtilityDAL.Factory.Filex.Create(file, name).ToKeyValueDate());

//            //        Console.WriteLine("file:"  + deserialisedFile.TimeStamp + "       @ " +DateTime.Now);
//            //    }
//            //    else
//            //        break;
//            //}

//            //Console.WriteLine("ended @ " +fileend);
//            Console.ReadLine();
//        }

//        //private static Func<Smarkets.Model.XML.Event,bool> GetPredicate() => (ee) => ee.Type == "Football match" && ee.ParentName.ToLower() != "outright";

//    }




//}