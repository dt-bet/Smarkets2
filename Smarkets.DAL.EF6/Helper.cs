
using Smarkets.Entity.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smarkets.DAL
{
    public static class Helper
    {
        static int i = 0;
        public static bool ToDb(this OddsContext session, IList<Match> matches)
        {

            // save both stores, this saves everything else via cascading
            List<Match> amatches = new List<Match>();
            List<Market> amarkets = new List<Market>();
            List<Contract> acontracts = new List<Contract>();
            List<Price> aprices = new List<Price>();

            var gmatches = matches.GroupBy(_ => _.Id);

            foreach (var match in gmatches)
            {
                var fmatch = match.First();
                var matchId = fmatch.Id;

                //foreach (var market in gmatches.SelectMany(_ => _.SelectMany(xc => xc.Markets)))
                //{
                //    market.Match = fmatch;

                //}

                //if (session.QueryOver<Match>().RowCount() == 0 || session.QueryOver<Match>().Where(x => x.Id == match.Key).RowCount() == 0)
                amatches.Add(fmatch);

                var gmarkets = match.SelectMany(_ => _.Markets.GroupBy(_j => _j.Id));

                foreach (var market in gmarkets)
                {
                    var fmarket = market.First();


                    //if (session.QueryOver<Market>().RowCount() == 0 || session.QueryOver<Market>().Where(x => x.Id == market.Key).RowCount() == 0)
                    //{

                    //fmarket.Match = fmatch;
                    // session.Markets.Add(fmarket);
                    amarkets.Add(fmarket);
                    //}

                    var gcontracts = market.SelectMany(_ => _.Contracts.GroupBy(_j => _j.Id));

                    foreach (var contract in gcontracts)
                    {
                        var fcontract = contract.First();


                        //if (session.QueryOver<Contract>().RowCount() == 0 || session.QueryOver<Contract>().Where(x => x.Id == contract.Key).RowCount() == 0)
                        //{
                        //fcontract.Market = fmarket;
                        //session.Contracts.Add(fcontract);
                        acontracts.Add(fcontract);
                        //}
                        foreach (var price in contract.SelectMany(x => x.Prices))
                        {
                            // var id = session.QueryOver<Contract>().RowCount();
                            //if (session.QueryOver<Contract>().RowCount() == 0 || session.QueryOver<Contract>().Where(x => x.Id == market.Key).RowCount() == 0)
                            //{

                            //price.Id = i + 1;
                            //price.Contract = fcontract;
                            //session.Prices.Add(price);
                            aprices.Add(price);
                            //i++;
                            // }
                        }



                    }

                    //var gteams= match.SelectMany(_ => _.Teams.GroupBy(_j => _j.Key));

                    //foreach (var matchteam in gteams)
                    //{
                    //    var fmt = matchteam.First();
                    //    fmt.Id = i;
                    //    session.Save(matchteam);
                    //    i++;
                    //}
                }



            }
            session.Matches.AddRange(amatches);
            session.Markets.AddRange(amarkets);
            session.Contracts.AddRange(acontracts);
            session.Prices.AddRange(aprices);

            return true;
        }




        public static bool ToDb(this OddsContext session, IList<Entity.NHibernate.EventParent> competitions)
        {

            // save both stores, this saves everything else via cascading
            var gcomps = competitions.GroupBy(_ => _.Key);

            foreach (var competition in gcomps)
            {
                var fcompetition = competition.First();
                var competitionId = fcompetition.Id;
                if (session.EventParents.Find(competitionId) == null)
                    session.EventParents.Add(competition.First());

                var gmatches = gcomps.SelectMany(_ => _.SelectMany(c => c.Matches));

                session.ToDb(gmatches.ToList());

                session.BatchSaveChanges();
            }

            return true;
        }



        public static List<Match> ToDb(this OddsContext session, IList<Entity.NHibernate.EventParent> competitions, DateTime dt)
        {

            PrepareForDb(ref competitions);

            List<Match> matches = new List<Match>();
            // save both stores, this saves everything else via cascading
            // save both stores, this saves everything else via cascading
            var gcomps = competitions.GroupBy(_ => _.Key);

            // populate the database


            foreach (var competition in gcomps)
            {

                var fcompetition = competition.First();
                var competitionId = fcompetition.Id;

                var gmatches = gcomps.SelectMany(_ => _.SelectMany(c => c.Matches.Where(m => new DateTime(m.Start) < dt))).ToList();
                //foreach (var match in gmatches)
                //    match.EventParent = fcompetition;

                if (session.EventParents.Find(competitionId) == null)
                    session.EventParents.Add(competition.First());

                //var gmatches = gcomps.SelectMany(_ => _.SelectMany(c => c.Matches));

                //foreach (var m in gmatches)
                //    m.EventParent = fcompetition;
                var gml = gmatches.ToList();
                matches.AddRange(gml);
                session.ToDb(gml);

            }


            session.BatchSaveChanges();

            return matches;


        }



        public static bool PrepareForDb(ref IList<EventParent> parents)
        {

            // save both stores, this saves everything else via cascading
            int i = 0;
            foreach (var par in parents)
            {

                foreach (var match in par.Matches)
                {
                    match.EventParent = par;

                    foreach (var market in match.Markets)
                    {
                        market.Match = match;

                        foreach (var contract in market.Contracts)
                        {
                            contract.Market = market;

                            foreach (var price in contract.Prices)
                            {
                                i++;
                                price.Contract = contract;
                                price.Id = i;
                            }
                        }
                    }
                    match.Teams = null;
                    //foreach (var mt in match.Teams)
                    //{

                    //    mt.Match = match;
                    //    if(mt.MatchPlayers!=null)
                    //    foreach (var d in mt.MatchPlayers)
                    //    {
                    //        d.MatchTeam = mt;
                    //        foreach (var s in d.PlayerStatGroups)

                    //            s.MatchPlayer = d;

                    //    }
                    //    if(mt.TeamStatGroups!=null)
                    //    foreach (var sdc in mt.TeamStatGroups)
                    //    {
                    //        sdc.MatchTeam = mt;
                    //        foreach (var cc in sdc.TeamStats)
                    //            cc.TeamStatGroup = sdc;

                    //    }
                    //}

                }
            }
            return true;
        }
    }
}