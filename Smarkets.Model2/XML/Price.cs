using Newtonsoft.Json;
//using SQLite;
//using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarkets.Model.XML
{


    /// <remarks/>
    [System.SerializableAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class Price
    {

        //[PrimaryKey, AutoIncrement]
        public Int64 Id { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        [JsonProperty("contract_id")]
       // [ForeignKey(typeof(Contract))]
        public Int64 ContractId { get; set; }

        public Price() { }

       // [Ignore]
        [Newtonsoft.Json.JsonProperty("timestamp")]
        [System.Xml.Serialization.XmlIgnore]
        public DateTime DateAsDateTime { get; set; }

  
        ///// <remarks/>
        [System.Xml.Serialization.XmlAttribute("liability")]
        public decimal Liability { get; set; }

 
        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute("backers_stake")]
        public decimal BackersStake { get; set; }


        ///// <remarks/>
        //[Ignore]
        //[System.Xml.Serialization.XmlAttribute("percent")]
        //public double Percent { get; set; }


        /// <remarks/>
       // [Ignore]
        [JsonProperty("last_executed_price")]
        [System.Xml.Serialization.XmlAttribute("decimal")]
        public decimal Decimal { get;  set; }

        [System.Xml.Serialization.XmlIgnore]
        public long Quantity { get; set; }





        public override string ToString()
        {
            return $"Date = {DateAsDateTime} | Price = {Decimal}";
        }

    }

    public partial class  Price
    {
        //private decimal BackersStakeAsDecimal;
        //private decimal LiabilityAsDecimal;

       // [Ignore]
        public byte SpreadSide { get; set; }


        [System.Xml.Serialization.XmlIgnore]
        public SpreadSide SpreadSideAsEnum
        {
            get => (SpreadSide)System.Enum.ToObject(typeof(SpreadSide), SpreadSide);
            set
            {
                SpreadSide = (byte)value;
            }
        }



        [System.Xml.Serialization.XmlIgnore]
        public Int64 Date

        {

            get
            {
                if (DateAsDateTime == default(DateTime))
                    return 0;
                else
                    return DateAsDateTime.ToBinary();
            }
            set
            {
                DateAsDateTime = new DateTime(value);
            }
        }


        [JsonProperty("close_price")]
        [System.Xml.Serialization.XmlIgnore]
        public double Percent
        {
            get
            {
                return 100d / (double)Decimal;
            }
            set
            {
                Decimal = 100m / (decimal)value;
            }
        }





        public Int32 Integer
        {
            get
            {
                return (int)(Decimal * 100);
            }
            set
            {
                Decimal = (decimal)(value / 100);
            }
        }

    }


}

