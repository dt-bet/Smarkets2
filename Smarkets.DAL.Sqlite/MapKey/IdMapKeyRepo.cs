using SportsBetting.Entity.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Smarkets.DAL.Sqlite
{
    public class IdMapKeyRepo
    {
        static readonly string dbPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(System.IO.Directory.CreateDirectory(@"..\..\..\Data").FullName, "MapMatch.sqlite"));

        List<Match> lmatches = new List<Match>();

        public void Persist(IEnumerable<Match> matches)
        {
            try
            {
                using (var conn = new SQLite.SQLiteConnection(dbPath))
                {
                    conn.CreateTable<IdMapKey>();
                    var inserts = matches.ToList().Except(lmatches);
                    conn.InsertAll(inserts.Select(_ => new IdMapKey { Id = 0, ForeignId = _.Id, Key = _.Key }));

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public static string GetKey(long id)
        {
            using (var conn = new SQLite.SQLiteConnection(dbPath))
            {
                return conn.Query<IdMapKey>("select * from IdMapKey where ForeignId = ?", id).FirstOrDefault()?.Key;
            }
        }

    }
}
