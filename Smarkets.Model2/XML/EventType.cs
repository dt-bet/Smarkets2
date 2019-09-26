
//using SQLite;
//using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Smarkets.Model.XML
{
    public class EventType
    {
       // [PrimaryKey, NotNull]
        public string Name { get; set; }

      //  [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<EventParent> EventParents { get; set; }


    }


}
