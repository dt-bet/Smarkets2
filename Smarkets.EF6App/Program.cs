
//using Smarkets.Model.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UtilityDAL;

namespace Smarkets.SqliteApp
{
    static class Program
    {


        private static void Main(string[] args)
        {
            System.IO.Directory.CreateDirectory("../../../Data");
            var sqlite = new UtilityDAL.Sqlite.Repository<UtilityDAL.Model.KeyValueDate, string>(_ => _.Key);
            object lck = new object();
            foreach (var filename in from file in System.IO.Directory.EnumerateFiles(Smarkets.Constants.XMLDirectory)
                                     let name = System.IO.Path.GetFileNameWithoutExtension(file)
                                     where sqlite.SelectById(name) == null
                                     select new { file, name })
            {

                var deserialisedFile = DAL.XML.Repo.GetOdds(filename.file);
                Console.WriteLine("file:" + deserialisedFile.TimeStamp + "       @ " + DateTime.Now);

                var events = deserialisedFile.Events.Where(DAL.XML.Repo.GetPredicate()).ToArray();

                var entities = Smarkets.XML.Map.EntityMap.MapToEntity(events, deserialisedFile.TimeStamp);

                var leagues = Smarkets.XML.Map.EntityMap.MapToLeague(events);

                Console.WriteLine("Inserted Matches - " + Smarkets.DAL.Sqlite.MatchRepository.TransferToDB(entities.ToArray()));

                Console.WriteLine("Inserted Leagues - " + Smarkets.DAL.Sqlite.MatchRepository.TransferToDB(leagues.ToArray()));

                lock (lck)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        try
                        {
                            sqlite.Insert(new UtilityDAL.Model.KeyValueDate { Key = filename.name, Value = filename.file, Date = DateTime.Now.Ticks });
                            break;
                        }
                        catch(Exception ex)
                        {
                            Thread.Sleep(1000);
                        }
                        if(i==10)
                        {
                            throw new Exception("Exceeded retry count");
                        }
                    }
                }
            }

            // Console.WriteLine("ended @ " + fileend);
            Console.ReadLine();
        }






        //private static void Main(string[] args)
        //{

        //    var repo = new Smarkets.DAL.Sqlite.Repo();

        //    foreach (var file in Smarkets.BLL.Helper.GetDateTimeFromFileName().OrderByDescending(_=>_.Item1))
        //    {
        //        var deserialisedFile = Smarkets.DAL.XMLRepo.GetOdds(file.Item2);
        //        var entities = Smarkets.Entity.EntityMap.MapToEntity(deserialisedFile.Events.Where(DAL.XMLRepo.GetPredicate()).Select(_ => (Model.XML.Event)_).ToArray(), deserialisedFile.TimeStamp);

        //        if (Repo.TransferToDB(entities.ToArray(),"../../../RecentData"))
        //        {
        //            Console.WriteLine("file:" + deserialisedFile.TimeStamp + "       @ " + DateTime.Now);
        //        }
        //    }

        //    // Console.WriteLine("ended @ " + fileend);
        //    Console.ReadLine();
        //}

    }





























    //    static class Program2
    //{


    //    private static void Main2(string[] args)
    //    {

    //       //var conn = new SportsBetting.DAL.Sqlite.Repo( @"Data/Smarkets." + UtilityDAL.Constants.SqliteDbExtension,false);

    //       // var sqlite = new UtilityDAL.Sqlite<UtilityDAL.Model.KeyValueDate, string>(_ => _.Key);

    //       // var repo = new Smarkets.DAL.Sqlite.Repo();


    //        #region deprecated
    //        //var conn2 = new Smarkets.DAL.Sqlite.Repo("" + "Smarkets." + UtilityDAL.Constants.SqliteDbExtension);

    //        //if (!System.IO.File.Exists(file))
    //        //{
    //        //    var fileStream = System.IO.File.Create(file);
    //        //    fileStream.Close();
    //        //}
    //        //string lastfilesaved = System.IO.File.ReadAllLines(file).LastOrDefault();

    //        //DateTime minDate = lastfilesaved!=null? Smarkets.DAL.Deserialize.ConvertToDateTime(lastfilesaved) : new DateTime();
    //        #endregion deprecated
    //        //string fileend = @"E:\SMarketXML\smarkets2017_01_11T18_12_23.xml";

    //        //foreach (var file in System.IO.Directory.GetFiles(Smarkets.Constants.XMLDirectory))
    //        //{
    //        //    if (file != fileend )
    //        //    {
    //        //        string name = System.IO.Path.GetFileNameWithoutExtension(file);
    //        //        if (sqlite.FindById(name) != null)
    //        //            continue;

    //        //        var deserialisedFile = Smarkets.DAL.XMLRepo.GetOdds(file);
    //        //        var entities = Smarkets.Map.EntityMap.MapToEntity(deserialisedFile.Events.Where(GetPredicate()).ToArray(), deserialisedFile.TimeStamp);

    //        //        repo.Persist(entities);

    //        //        conn.TransferToDB(entities);

    //        //        sqlite.Insert(UtilityDAL.Factory.Filex.Create(file, name).ToKeyValueDate());

    //        //        Console.WriteLine("file:"  + deserialisedFile.TimeStamp + "       @ " +DateTime.Now);
    //        //    }
    //        //    else
    //        //        break;
    //        //}

    //        //Console.WriteLine("ended @ " +fileend);
    //        Console.ReadLine();
    //    }

    //    //private static Func<Smarkets.Model.XML.Event,bool> GetPredicate() => (ee) => ee.Type == "Football match" && ee.ParentName.ToLower() != "outright";

    //}




}