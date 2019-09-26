//using FluentNHibernate.Mapping;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Smarkets.Entity.NHibernate
//{
//    public class MarketMap : ClassMap<Market>
//    {
//        public MarketMap()
//        {

//            Id(x => x.Id);
//            HasMany(x => x.Contracts);
//         Map(x => x.Key);
//            References(x => x.Match).Not.Nullable();



//        }
//    }
//    public class ContractMap : ClassMap<Contract>
//    {
//        public ContractMap()
//        {
//            Id(x => x.Id);
//            HasMany(x => x.Prices);
//         Map(x => x.Key);
//            //HasMany(x => x.Conditions);
//            References(x => x.Market).Not.Nullable();

//        }
//    }

//    public class PriceMap : ClassMap<Price>
//    {
//        public PriceMap()
//        {
//            Id(x => x.Id);//.GeneratedBy.Increment();
 
//            Map(x => x.Time);
//            Map(x => x.Value);
//            Map(x => x.Source);
//            References(x => x.Contract).Not.Nullable();
//            //.Cascade.All()
//            //.Inverse()
//            //.Table("StoreProduct");

//            //Component(x => x.Location);
//        }
//    }
//}
