
using Newtonsoft.Json;
//using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Smarkets.Model.XML
{
    /// <remarks/>
    [System.SerializableAttribute()]

    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class Contract
    {
        public Contract() { }


        [System.Xml.Serialization.XmlAttributeAttribute("id")]
        public long Id { get; set; }


        [System.Xml.Serialization.XmlAttributeAttribute("name")]
        public string Name { get; set; }

        //  [SQLite.Ignore]
        [System.Xml.Serialization.XmlAttributeAttribute("slug")]
        public string Slug { get; set; }


        [XmlArray("offers")]
        [XmlArrayItem("price")]
        // [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Price> Offers { get; set; }

        [XmlArray("bids")]
        [XmlArrayItem("price")]
        // [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Price> Bids { get; set; }

        [Newtonsoft.Json.JsonProperty("market_id")]
        //  [ForeignKey(typeof(Market))]
        public long ParentId { get; set; }

        [Newtonsoft.Json.JsonProperty("outcome_timestamp")]
        public string outcome_timestamp { private get; set; }
        // 2019-09-08T20:39:18.169Z
        //HH:mm:ss.fffZ
        public DateTime? OutComeDateTime => outcome_timestamp != null ? DateTime.ParseExact(outcome_timestamp, "yyyy-MM-ddTHH:mm:ss.fffZ", System.Globalization.CultureInfo.CurrentCulture) : default;

        public bool Success { get; set; }
        //public Decimal UnitProfit { get; set; }
        public Contract_Type contract_type { get; set; }





    }

    public class Contract_Type
    {
        public string name { get; set; }
        public string param { get; set; }
    }

    public static class ContractExtension
    {
        public static Tuple<DateTime, decimal[]>[] OddMovementByContract(this Contract c)
        {

            if (c.Offers.Count() > 0)
            {
                var offs = c.Offers.GroupBy(_ => _.Date);

                if (c.Bids.Count() > 0)
                {
                    var bids = c.Bids.GroupBy(_ => _.Date);

                    return bids.Join(offs, b => b.Key, o => o.Key, (b, o) =>
                                  new Tuple<DateTime, decimal[]>(
                                      new DateTime(b.First().Date),
                                     // bids/offers have ptential depth of 3 - only interested in first (which will also be the max value) 
                                     new decimal[] { o.Max(_ => _.Decimal), b.Max(_ => _.Decimal) })).ToArray();

                }
                else
                    return offs.Select(_ => new Tuple<DateTime, decimal[]>(new DateTime(_.First().Date), new decimal[] { _.Max(dg => dg.Decimal), 0 })).ToArray();

            }
            else if (c.Bids.Count() > 0)

                return c.Bids.GroupBy(_ => _.Date).Select(_ => new Tuple<DateTime, decimal[]>(new DateTime(_.First().Date), new decimal[] { 0, _.Max(dg => dg.Decimal) })).ToArray();

            else
                return null;

        }




    }

}


//public class Rootobject
//{
//    public Contract[] contracts { get; set; }
//}

//public class Contract
//{
//    public Contract_Type contract_type { get; set; }
//    public int display_order { get; set; }
//    public bool hidden { get; set; }
//    public string id { get; set; }
//    public Info info { get; set; }
//    public string market_id { get; set; }
//    public string name { get; set; }
//    public DateTime outcome_timestamp { get; set; }
//    public string slug { get; set; }
//    public string state_or_outcome { get; set; }
//}

//public class Contract_Type
//{
//    public string name { get; set; }
//    public string param { get; set; }
//}

//public class Info
//{
//}
