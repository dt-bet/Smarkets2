//using SQLite;
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
    public partial class Market
    {

        // [PrimaryKey, AutoIncrement]
        [Newtonsoft.Json.JsonProperty("id")]
        [System.Xml.Serialization.XmlAttributeAttribute("id")]
        public Int64 Id { get; set; }

        [Newtonsoft.Json.JsonProperty("event_id")]
        //  [ForeignKey(typeof(Event))]
        public Int64 EventId { get; set; }


        public Market() { }

        //[OneToMany(CascadeOperations = CascadeOperation.All)]
        [System.Xml.Serialization.XmlElementAttribute("contract")]
        //[XmlIgnore()]
        public Contract[] Contracts { get; set; }


        [System.Xml.Serialization.XmlAttributeAttribute("slug")]
        public String Slug { get; set; }


        [Newtonsoft.Json.JsonProperty("name")]
        public String Name { get; set; }

        [Newtonsoft.Json.JsonProperty("winner_count")]
        [System.Xml.Serialization.XmlAttributeAttribute("winners")]
        public String Winners { get; set; }



        [Newtonsoft.Json.JsonProperty("volume")]
        public Int64? Volume { get; set; }

        public Market_Type market_type { get; set; }

    }

    public class Market_Type
    {
        public string name { get; set; }
        public string param { get; set; }
    }

}






