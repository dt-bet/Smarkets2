using System;
using System.Collections.Generic;
using System.Text;

namespace Smarkets.Entity
{

    public class Price : IEquatable<Price>
    {

        public Price()
        {
        }

        [SQLite.AutoIncrement]
        [SQLite.PrimaryKey]
        public long Id { get; set; }

        [SQLite.Indexed]
        public long ParentId { get; set; }

        public Int64 Time { get; set; }


        //Bid/Offer
        public Byte Type { get; set; }

        public long Value { get; set; }

        public long Volume { get; set; }

        public long Depth { get; set; }




        #region IEquatable


        public bool Equals(Price price)
        {
            return
                        Time == price.Time &&

              Type == price.Type &&
              Value == price.Value &&
              Volume == price.Volume &&
              Depth == price.Depth;
        }

        public override int GetHashCode()
        {
            var hashCode = -626583170;
            hashCode = hashCode * -1521134295;
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            hashCode = hashCode * -1521134295 + Time.GetHashCode();
            return hashCode;
        }


        public override bool Equals(object obj) => this.Equals(obj as Price);



        #endregion comparison
    }

}
