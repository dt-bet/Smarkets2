using Betting.Enum;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Smarkets.Entity
{




    public class Market :  IEquatable<Market> 
    {
        public Market()
        {
        }
        [SQLite.PrimaryKey]
        public long Id { get; set; }
        [SQLite.Indexed]
        public long MarketId { get; set; }

        public long ParentId { get; set; }


        [Indexed]
        public Byte Key { get; set; }

        [Ignore]
        public List<Contract> Contracts { get; set; }

        public Dictionary<ContractType, Contract> IndexedContracts => (from c in Contracts group c by 
                                                                       (ContractType)Enum.ToObject(typeof(ContractType),c.Key) 
                                                                       into vv select vv).ToDictionary(a => a.Key, a => a.Single());


        #region IEquatable

        public bool Equals(Market market) => this.Key == (market as Market).Key;

        public override int GetHashCode()
        {
            var hashCode = -228512026;
            hashCode = hashCode * -1521134295 + Key.GetHashCode();
            hashCode = hashCode * -1521134295;
            return hashCode;
        }

        public override bool Equals(object market) => this.Equals(market as Market);


        #endregion comparison
    }






}

