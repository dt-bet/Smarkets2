using System;
using System.Collections.Generic;
using System.Text;

namespace Smarkets.Entity.Temp
{
    public struct Result
    {
        [SQLite.PrimaryKey]
        public long Id { get; set; }
        public bool HasResult { get; set; }
        public long Team1Id { get; set; }
        public long Team2Id { get; set; }
        public byte Team1HalfTimeScore { get; set; }
        public byte Team2HalfTimeScore { get; set; }
        public byte Team1FullTimeScore { get; set; }
        public byte Team2FullTimeScore { get; set; }

        public bool State { get; set; }

    }
}
