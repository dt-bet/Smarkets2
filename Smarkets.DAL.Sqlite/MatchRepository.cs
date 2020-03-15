using Betting.Enum;
using Smarkets.Entity;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Smarkets.DAL.Sqlite
{


    public class MatchRepository
    {
        const string directoryName = "../../../Data";


        public static int TransferToDB(IEnumerable<Entity.XML.Result> results, string connectionName)
        {
            using (var connection = new SQLiteConnection(connectionName))
            {
                connection.CreateTable<Entity.XML.Result>();
                return connection.InsertAll(results);
            }
        }
        public static int TransferToDB(IEnumerable<Entity.XML.MatchTeam> results, string connectionName)
        {
            using (var connection = new SQLiteConnection(connectionName))
            {
                connection.CreateTable<Entity.XML.MatchTeam>();
                return connection.InsertAll(results);
            }
        }



        public static int TransferToDB(IEnumerable<Entity.Match> matches, string directoryName = directoryName)
        {
            int count = 0;
            foreach (var ematch in matches)
            {
                var directory = System.IO.Directory.CreateDirectory(directoryName);
                directory = System.IO.Directory.CreateDirectory(directory.FullName + "/" + Common.FileNameHelper.MakeDateDirectoryName(new DateTime(ematch.Start)));
                string connectionName = $"{directory.FullName}/{Smarkets.Common.FileNameHelper.MakeValidFileName($"{(ematch.Key)}.sqlite")}";
                bool b = System.IO.File.Exists(connectionName);
                try
                {
                    using (var connection = new SQLiteConnection(connectionName))
                    {
                        if (!b)
                        {
                            Console.WriteLine("Inserting - " + ematch.Url);
                            connection.CreateTable<Match>();
                            connection.CreateTable<Market>();
                            connection.CreateTable<Contract>();
                            connection.CreateTable<Price>();
                            connection.Insert(ematch);
                            count++;
                        }
                        else
                        {
                            //Console.WriteLine("Updating - " + ematch.Url);
                            var dbmatch = connection.Table<Match>().ToArray().First();
                            if (ematch.Start != dbmatch.Start)
                            {
                                dbmatch.Start = ematch.Start;
                                connection.Update(dbmatch);
                            }
                        }

                        var markets = connection.Table<Market>().ToList();
                        var contracts = connection.Table<Contract>().ToList();
                        var prices = connection.Table<Price>().ToList();


                        List<Market> emarkets = new List<Market>();
                        List<Contract> econtracts = new List<Contract>();
                        List<Price> eprices = new List<Price>();

                        foreach (var smarket in ematch.Markets.Where(m => m.Key == MarketType.FullTimeResult))
                        {
                            smarket.ParentId = ematch.Id;
                            if (!markets.Contains(smarket))
                            {
                                emarkets.Add(smarket);
                            }
                            foreach (var contract in smarket.Contracts)
                            {
                                contract.ParentId = smarket.Id;
                                //contract.Parent = smarket;
                                if (!contracts.Contains(contract))
                                {
                                    econtracts.Add(contract);
                                }
                                foreach (var price in contract.Prices)
                                {
                                    price.ParentId = contract.Id;
                                    //price.Parent = contract;
                                    //if (!prices.Contains(price))
                                    //{
                                    eprices.Add(price);

                                }

                            }
                            if (smarket.Contracts.Count > 0)
                            {
                                connection.Execute("Drop table Contract");
                                connection.CreateTable<Contract>();
                                connection.InsertAll(smarket.Contracts);
                            }
                        }

                        if(econtracts.Count>0)
                        {
                            connection.Execute("Drop table Contract");
                            connection.CreateTable<Contract>();
                            connection.InsertAll(econtracts);
                        }

                        connection.InsertAll(emarkets);
                        connection.InsertAll(eprices);
                        //var exceptMarkets=
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }

            return count;
        }

        public static int TransferToDB(League[] leagues, string directoryName = directoryName)
        {
            var directory = System.IO.Directory.CreateDirectory(directoryName);
            string connectionName = $"{directory.FullName}/{nameof(League)}.sqlite";
            bool b = System.IO.File.Exists(connectionName);
            int count = 0;
            try
            {
                using (var connection = new SQLiteConnection(connectionName))
                {
                    //if (!b)
                    //{
                    connection.CreateTable<League>();
                    //}
                    var dbleagues = connection.Table<League>().ToArray();
                    var exceptleagues = leagues.Distinct().Except(dbleagues).ToArray();
                    count = exceptleagues.Count();
                    connection.InsertAll(exceptleagues);
                    return count;
                }
            }
            catch (Exception exception)
            {
                TransferToDB(leagues); ; ;
                Console.Write(exception.Message);
                return 0;
            }


        }

        public static IEnumerable<(string name, long id)> SelectInformation(string directoryName)
        {

            foreach (var sqliteFile in System.IO.Directory.EnumerateFiles(directoryName))
            {
                using (var connection = new SQLiteConnection(sqliteFile))
                {
                    yield return (sqliteFile, connection.Table<Match>().ToArray().Single().Id);
                }
            }
        }


        public static IEnumerable<Entity.Match> Select(DateTime dateTime, string directoryName = directoryName)
        {
            return SelectAll(directoryName, _ => _.SingleOrDefault(__ =>
            Common.FileNameHelper.GetDateTimeFromDirectory(__.Name).Date == dateTime.Date)?.GetFiles());
        }

        public static IEnumerable<Entity.Match> SelectAll(string directoryName = directoryName, Func<IEnumerable<DirectoryInfo>, IEnumerable<FileInfo>> predicate = null)
        {
            if (new System.IO.DirectoryInfo(directoryName) is DirectoryInfo directory && directory.Exists)
            {
                IEnumerable<FileInfo> xx = null;
                if (predicate != null)
                    xx = predicate.Invoke(directory.GetDirectories("??_??_??"))?.Where(_ => _.Extension == ".sqlite");
                else
                    xx = directory.GetDirectories("??_??_??").SelectMany(_ => _.GetFiles("*.sqlite"));

                if (xx != null)
                    foreach (var connInfo in xx)
                        foreach (var match in Select(connInfo.FullName))
                            yield return match;
            }
        }

        public static IEnumerable<Match> Select(string connName)
        {


            using (var connection = new SQLiteConnection(connName))
            {
                connection.CreateTable<Entity.XML.Result>();
                connection.CreateTable<Entity.XML.MatchTeam>();

                foreach (var jevent in from match in connection.Table<Match>().ToArray()
                                       join jmarket in from market in connection.Table<Market>().ToArray()
                                                       join jcontract in from contract in connection.Table<Contract>().ToArray()
                                                                         join price in connection.Table<Price>().ToArray() on
                                                                         contract.ContractId equals price.ParentId
                                                                         into temp
                                                                         where temp.Any()
                                                                         select new { temp, contract }
                                                         on market.MarketId equals jcontract.contract.ParentId
                                                          into temp
                                                       where temp.Any()
                                                       select new { temp, market } on
                                                     match.EventId equals jmarket.market.ParentId
                                          into temp
                                       where temp.Any()
                                       select new { temp, match })
                {
                    List<Market> nmarkets = new List<Market>();
                    foreach (var jmarket in jevent.temp)
                    {
                        List<Contract> ncontracts = new List<Contract>();
                        foreach (var jcontract in jmarket.temp)
                        {
                            jcontract.contract.Prices = jcontract.temp.ToList();
                            ncontracts.Add(jcontract.contract);
                        }

                        jmarket.market.Contracts = ncontracts;
                        nmarkets.Add(jmarket.market);
                    }

                    jevent.match.Markets = nmarkets;
                    jevent.match.Results = connection.Table<Entity.XML.Result>().ToList();
                    jevent.match.MatchTeams =connection.Table<Entity.XML.MatchTeam>().ToList();
                    yield return jevent.match;
                }
            }
        }
    }



}
