using System;
using System.Collections.Generic;
using System.Text;

namespace Smarkets.Entity.Temp
{

   public  struct Team
    {
        [SQLite.PrimaryKey]
        public long Id { get; set; }
        public string Name { get; set; }
    }

}
