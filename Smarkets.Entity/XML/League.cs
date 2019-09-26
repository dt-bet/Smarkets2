using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smarkets.Entity
{
    public class League : IEquatable<League>
    {
        //parent_name="France Ligue 1" parent_slug="/sport/football/france-ligue-1-2018-2019" state="upcoming" time="19:05:00" type="Football match"
        [PrimaryKey]
        [AutoIncrement]
        public long Id { get; set; }


        [SQLite.Unique]
        public string Key { get;
            set; }


        public string Name { get; set; }


        public string Type { get; set; }

        public bool Equals(League other) => other.Key == this.Key;

        public override int GetHashCode() => this.Key.First();

        public override bool Equals(object other) => this.Equals(other as League);
    }
}
