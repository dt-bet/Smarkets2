using System;
using System.Collections.Generic;
using System.Text;

namespace Smarkets.DAL.Sqlite
{
    public class IdMapKey
    {
        [SQLite.PrimaryKey]
        [SQLite.AutoIncrement]
        public long Id { get; set; }


        public long ForeignId { get; set; }



        public string Key { get; set; }

    }
}
