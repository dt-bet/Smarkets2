using System;
using System.Collections.Generic;
using System.Text;

namespace Smarkets.Entity
{
    public class Match
    {
        public Match()
        {
        }
        [SQLite.PrimaryKey]
        public long Id { get; set; }

        [SQLite.Indexed]
        public long EventId { get; set; }

        public string Url { get; set; }

        public string Key { get; set; }

        public long Start { get; set; }

        public string League { get; set; }

        public int HomeScore { get; set; }

        public int AwayScore { get; set; }

        [SQLite.Ignore]
        public List<Market> Markets { get; set; }

        [SQLite.Ignore]
        public List<Entity.XML.MatchTeam> MatchTeams { get; set; }

        [SQLite.Ignore]
        public List<Entity.XML.Result> Results { get; set; }

    }

}
