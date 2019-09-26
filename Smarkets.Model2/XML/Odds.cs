using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Smarkets.Model.XML
{


    [System.Xml.Serialization.XmlRootAttribute("odds", Namespace = "", IsNullable = false)]
    public partial class Odds // : ITimeStamp
    {

        [System.Xml.Serialization.XmlAttribute(DataType = "dateTime",AttributeName = "timestamp")]
        public DateTime TimeStamp {            get;            set; }

        public Odds() { }

        [System.Xml.Serialization.XmlElementAttribute("event")]
        public Event[] Events { get; set; }
    }




}












/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class odds
{

    private oddsEvent[] eventField;

    private System.DateTime timestampField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("event")]
    public oddsEvent[] @event
    {
        get
        {
            return this.eventField;
        }
        set
        {
            this.eventField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public System.DateTime timestamp
    {
        get
        {
            return this.timestampField;
        }
        set
        {
            this.timestampField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class oddsEvent
{

    private oddsEventMarket[] marketField;

    private System.DateTime dateField;

    private uint idField;

    private string nameField;

    private string parentField;

    private string parent_slugField;

    private string slugField;

    private System.DateTime timeField;

    private string typeField;

    private string urlField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("market")]
    public oddsEventMarket[] market
    {
        get
        {
            return this.marketField;
        }
        set
        {
            this.marketField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
    public System.DateTime date
    {
        get
        {
            return this.dateField;
        }
        set
        {
            this.dateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public uint id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string parent
    {
        get
        {
            return this.parentField;
        }
        set
        {
            this.parentField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string parent_slug
    {
        get
        {
            return this.parent_slugField;
        }
        set
        {
            this.parent_slugField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string slug
    {
        get
        {
            return this.slugField;
        }
        set
        {
            this.slugField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute(DataType = "time")]
    public System.DateTime time
    {
        get
        {
            return this.timeField;
        }
        set
        {
            this.timeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string type
    {
        get
        {
            return this.typeField;
        }
        set
        {
            this.typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string url
    {
        get
        {
            return this.urlField;
        }
        set
        {
            this.urlField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class oddsEventMarket
{

    private oddsEventMarketContract[] contractField;

    private uint idField;

    private string slugField;

    private string winnersField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("contract")]
    public oddsEventMarketContract[] contract
    {
        get
        {
            return this.contractField;
        }
        set
        {
            this.contractField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public uint id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string slug
    {
        get
        {
            return this.slugField;
        }
        set
        {
            this.slugField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string winners
    {
        get
        {
            return this.winnersField;
        }
        set
        {
            this.winnersField = value;
        }
    }
}







/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class oddsEventMarketContract
{

    private oddsEventMarketContractPrice[] bidsField;

    private oddsEventMarketContractPrice1[] offersField;

    private uint idField;

    private string nameField;

    private string slugField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("price", IsNullable = false)]
    public oddsEventMarketContractPrice[] bids
    {
        get
        {
            return this.bidsField;
        }
        set
        {
            this.bidsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("price", IsNullable = false)]
    public oddsEventMarketContractPrice1[] offers
    {
        get
        {
            return this.offersField;
        }
        set
        {
            this.offersField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public uint id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string slug
    {
        get
        {
            return this.slugField;
        }
        set
        {
            this.slugField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class oddsEventMarketContractPrice
{

    private decimal decimalField;

    private decimal percentField;

    private decimal backers_stakeField;

    private decimal liabilityField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal @decimal
    {
        get
        {
            return this.decimalField;
        }
        set
        {
            this.decimalField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal percent
    {
        get
        {
            return this.percentField;
        }
        set
        {
            this.percentField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal backers_stake
    {
        get
        {
            return this.backers_stakeField;
        }
        set
        {
            this.backers_stakeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal liability
    {
        get
        {
            return this.liabilityField;
        }
        set
        {
            this.liabilityField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class oddsEventMarketContractPrice1
{

    private decimal decimalField;

    private decimal percentField;

    private decimal backers_stakeField;

    private decimal liabilityField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal @decimal
    {
        get
        {
            return this.decimalField;
        }
        set
        {
            this.decimalField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal percent
    {
        get
        {
            return this.percentField;
        }
        set
        {
            this.percentField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal backers_stake
    {
        get
        {
            return this.backers_stakeField;
        }
        set
        {
            this.backers_stakeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal liability
    {
        get
        {
            return this.liabilityField;
        }
        set
        {
            this.liabilityField = value;
        }
    }
}

