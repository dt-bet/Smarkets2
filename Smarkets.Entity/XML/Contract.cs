using Betting.Enum;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smarkets.Entity
{

    public class Contract : IEquatable<Contract>
    {
        public Contract()
        {
        }
        [SQLite.PrimaryKey]
        public long Id { get; set; }
        [SQLite.Indexed]
        public long ParentId { get; set; }

        public long ContractId { get; set; }

        public ContractType Key { get; set; }

        public long Condition { get; set; }

        public string Name { get; set; }

        [Ignore]
        public List<Price> Prices { get; set; }

        public IOrderedEnumerable<Price> MaxOffers => Prices
                                                        .Where(a => a.Type.Equals((byte)Betting.Enum.PriceType.Offer))
                                                        .GroupBy(p => p.Time)
                                                        .SelectMany(a => a.GroupBy(v => v.Value).OrderBy(aa => aa.Key).Last())
                                                        .OrderBy(a => a.Time);

        #region IEquatable

        public bool Equals(Contract contract) => Key == contract?.Key
                                                 && Condition == contract?.Condition;


        public override int GetHashCode()
        {
            var hashCode = 868662804;
            hashCode = hashCode * -1521134295 + Key.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object contract) => this.Equals(contract as Contract);



        #endregion IEquatable
    }


}
