
//using SQLite;
//using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Smarkets.Model.XML
{
    public class EventParent
    {
        //[PrimaryKey, NotNull]
        public string Name { get; set; }

        //[ForeignKey(typeof(EventType), Name = "Name")]
        public String EventTypeName { get; set; }

        [Newtonsoft.Json.JsonProperty("events")]
        //OneToMany(CascadeOperations = CascadeOperation.All)]
        public Event[] Events { get; set; }


    }

}
