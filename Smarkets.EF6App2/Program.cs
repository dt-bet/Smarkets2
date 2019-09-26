
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Smarkets.Map;
//using Smarkets.Model.XML;
//using Smarkets.DAL;

//namespace Smarkets.EF6App2
//{
//    static class Program
//    {
//        private static void Main(string[] args)
//        {
//            using (var ctx = new Smarkets.DAL.OddsContext())
//            {
//                var maps = Smarkets.DAL.XMLRepo.SelectFilesAsync(/*new DateTime(2017, 4, 14) + new TimeSpan(22,26,59), new DateTime(2040, 12, 12)*/)
//            .Select(async _ => { var vx = await _; return new { comps = Smarkets.Map.NHibernateMap.MapToCompetitions(vx), vx.TimeStamp }; });

//                //var conn = new Smarkets.DAL.MatchRepository(true);

//                List<Smarkets.Entity.NHibernate.EventParent> comps = new List<Smarkets.Entity.NHibernate.EventParent>();
//                foreach (var m in maps)
//                {
//                    try
//                    {
//                        //TODO Competitions seperate events
//                        m.Wait();
//                        var r = m.Result;

//                        comps.AddRange(r.comps);
//                        comps = Aggregate(comps).ToList();

                
//                        //Task.Run(() =>
//                        //{
//                        ctx.ToDb(comps, r.TimeStamp, out List<Smarkets.Entity.NHibernate.Match> matches);
//                        //});
//                        //var yt = events.Where(_ => new DateTime(_.Start) < r.TimeStamp).ToList();
//                        System.IO.File.AppendText("../../Data/FilesCompleted.txt").WriteLine(r.TimeStamp);
//                        foreach (var s in r.comps)
//                            foreach (var ma in s.Matches)
//                                if (matches.Contains(ma))
//                                    s.Matches.Remove(ma);
//                    }
//                    catch (Exception ex)
//                    {
//                        Console.WriteLine(ex.Message);
//                    }
                 
//                }
//            }
           
//            Console.WriteLine("Demo completed.");
           
//            Console.ReadLine();
//        }



//        public static IEnumerable<Smarkets.Entity.NHibernate.EventParent> Aggregate(IEnumerable<Smarkets.Entity.NHibernate.EventParent> comps)
//        {

//            foreach (var c in comps.GroupBy(_ => _.Key))
//            {
//                var ca = c.First();
//                ca.Matches=ca.Matches.Concat(c.SelectMany(_ => _.Matches)).ToArray();
//                yield return ca;

//            }


//        }


//        public static void ToDb(this Smarkets.DAL.OddsContext ctx, List<Smarkets.Entity.NHibernate.EventParent> comps, DateTime timeStamp, out List<Smarkets.Entity.NHibernate.Match> matches)
//        {
//            matches = Smarkets.DAL.Helper.ToDb(ctx, comps, timeStamp);


//        }

//    }
//}