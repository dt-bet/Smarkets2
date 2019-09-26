
using FluentNHibernateEx;
using Smarkets.Entity.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Util;

namespace Smarkets.DAL
{
    public class MatchRepository : AutoDbContext<Entity.NHibernate.Match> //, UtilityDAL.IDbService<Match>
    {
        public MatchRepository(bool b):base(b)
        {

        }

        public bool ToDb(List<Match> matches)
        {

            //var mms = matches.Select(_ => _.MapToCompetition()).ToList();
            using (var session = SessionFactory.OpenSession())
            {

                // populate the database
                using (var transaction = session.BeginTransaction())
                {
                    session.ToDb(matches);

                    transaction.Commit();
                    return true;
                }


            }
            
        }


        public bool ToDb(IList<Entity.NHibernate.EventParent> competitions)
        {

            //var mms = matches.Select(_ => _.MapToCompetition()).ToList();
            using (var session = SessionFactory.OpenSession())
            {

                // populate the database
                using (var transaction = session.BeginTransaction())
                {
                    session.ToDb(competitions);

                    transaction.Commit();
                    return true;
                }


            }

        }



        public bool ToDbByDate(List<Entity.NHibernate.EventParent> competitions,DateTime dt,out List<Match> matches)
        {

            //var mms = matches.Select(_ => _.MapToCompetition()).ToList();
            using (var session = SessionFactory.OpenSession())
            {

                // populate the database
                //using (var transaction = session.BeginTransaction())
                //{

                    matches = session.ToDb(competitions,dt);
                    //transaction.Commit();
                    return true;
                //}


            }

        }



        public bool Exists(long matchid)
        {
            using (var session = SessionFactory.OpenSession())
            {
                return (session.QueryOver<Match>().Where(x => x.Id == matchid).RowCount() != 0);
            }
        }




    }
}