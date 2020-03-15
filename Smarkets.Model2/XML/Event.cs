
//using SQLite;
//using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Smarkets.Model.XML
{


    public partial class Event
    {
        const string regexr = @"\/([^\/]*)\/([^\/]*)\/?([^\/]*)?";
        const string regexr2 = @"(?:20)?(\d{2})";
    
        string _parent;

       /// [PrimaryKey, NotNull]
        [System.Xml.Serialization.XmlAttributeAttribute("id")]
        public Int64 Id { get; set; }

        //[ForeignKey(typeof(EventParent))]
       // [Newtonsoft.Json.JsonProperty("parent_id")]

        public Int64 ParentId { get; set; }


        public Event() { }


       // [Ignore]
        [System.Xml.Serialization.XmlAttributeAttribute("date")]
        public System.DateTime DateAsDateTime { get; set; }

        private int _year;
        private string _parentName;

        public int Year { get { return _year; } }
       // [OneToMany(CascadeOperations = CascadeOperation.All)]
        [System.Xml.Serialization.XmlElementAttribute("market")]
      // [Newtonsoft.Json.JsonProperty("markets")]
        //[XmlIgnore]
        public Market[] Markets { get; set; }

        //[System.Xml.Serialization.XmlTextAttribute()]
        //public string[] Text { get; set; }
        //[SQLite.Ignore]
        [System.Xml.Serialization.XmlAttributeAttribute("url")]
        public String Url { get; set; }

        //[SQLite.Ignore]
        [Newtonsoft.Json.JsonProperty("type")]
        [System.Xml.Serialization.XmlAttributeAttribute("type")]
        public String Type { get; set; }

       // [SQLite.Ignore]
        [System.Xml.Serialization.XmlAttributeAttribute("time")]
        public String Time { get; set; }

       // [SQLite.Ignore]
        [Newtonsoft.Json.JsonProperty("slug")]
        [System.Xml.Serialization.XmlAttribute("slug")]
        public String Slug { get; set; }

        //[SQLite.Ignore]
        [System.Xml.Serialization.XmlAttributeAttribute("parent_slug")]
        public String Parent_slug { get; set; }

       // [ForeignKey(typeof(EventParent), Name = "Name")]
        [System.Xml.Serialization.XmlAttribute("parent")]
        public String Parent
        {
            get { return _parent; }
            set
            {

                _parentName = value;

                if (value.ToLower().Contains("tennis"))
                {
                    if (value.Contains('/'))
                    {
                        var split = value.Split('/');
                        _parent = split[split.Length - 2];
                    }
                }
                else if (value.HasAnyDigits() && !value.ToLower().Contains("round") && !value.Contains("1/64") && !value.Contains("1/32") && !value.Contains("1/8") && !value.Contains("1/4") && !value.Contains("1/2"))
                {

                    var regex = new System.Text.RegularExpressions.Regex(regexr2);
                    System.Text.RegularExpressions.Match m = regex.Match(value);
                    if (m.Success)
                    {
                        _year = 2000 + int.Parse(m.Groups[m.Groups.Count - 1].Captures[0].Value);
                        _parent = regex.Replace(value, "").Replace("-", "").Trim();
                        //_parent = String.Join(" ", _parent.Split('-').Select(_ => StringExtension.FirstLetterToUpper(_)));

                    }
                    else if (value.Contains("/"))
                    {
                        _parent = value.Split('/').Last();
                    }
                    else
                    {
                        //throw new Exception("Cannot parse event");
                        var regex2 = new System.Text.RegularExpressions.Regex("Qualification round");
                        System.Text.RegularExpressions.Match im = regex2.Match(value);
                        if (im.Success)
                        {
                            _parent = value;
                        }
                        else
                        {

                        }
                    }
         
                }
                else
                {
                    _parent = value;
                }
                //_parent = RegexExtension.RemoveYears(value);




            }
        }

        [System.Xml.Serialization.XmlAttribute("parent_name")]
        public String ParentName
        {
            get { return _parentName; }
            set
            {
                _parentName = value;
                if (value.HasAnyDigits() && !value.Contains("Round"))
                {

                    var regex = new System.Text.RegularExpressions.Regex(regexr2);
                    System.Text.RegularExpressions.Match m = regex.Match(value);
                    if (m.Success)
                    {
                        _year = 2000 + int.Parse(m.Groups[m.Groups.Count - 1].Captures[0].Value);
                        _parent = regex.Replace(value, "").Replace("-", "").Trim();
                        //_parent = String.Join(" ", _parent.Split('-').Select(_ => StringExtension.FirstLetterToUpper(_)));

                    }
                    else
                    {
                        return ;
                        throw new Exception("Cannot parse event");
                    }


                }
                else
                {
                    _parent = value;
                }
                //_parent = RegexExtension.RemoveYears(value);

            }
        }





        // [SQLite.Ignore]
        [Newtonsoft.Json.JsonProperty("name")]
        [System.Xml.Serialization.XmlAttributeAttribute("name")]
        public String Name { get; set; }


       // [SQLite.Ignore]
        [System.Xml.Serialization.XmlIgnore]
        public String SeasonPeriod { get; set; }


       // [SQLite.Ignore]
        //[System.Xml.Serialization.XmlAttributeAttribute("url")]
        public Team[] Teams { get; set; }


    }


    public partial class Event
    {

        //[Newtonsoft.Json.JsonProperty("start_datetime")]
        public string StartTime
        {
            get
            {
                return DateAsDateTime.ToString("yyyy-MM-dd") + "T" + Time;
            }
            set
            {
                var x = DateTime.Parse(value);
                DateAsDateTime = x.Date;
                Time = x.TimeOfDay.ToString();
            }

        }




        public DateTime StartTimeAsDateTime
        {
            get
            {
                return DateAsDateTime + TimeSpan.Parse(Time);
            }
            set
            {

                DateAsDateTime = value.Date;
                Time = value.TimeOfDay.ToString();
            }

        }



       // [SQLite.Ignore]
        //[NotNull]
        public String HomeTeam=> Regex.Split( Name," vs.? " )[0]; 
        //[SQLite.Ignore]
       // [NotNull]
        public String AwayTeam => Regex.Split(Name, " vs.? ")[1]; 

        //[SQLite.Ignore]
        public Int16? HomeTeamScore { get { return Teams[0].Statistics[0].Value; } set { Teams[0].Statistics[0].Value = value ?? 0; } }

    //    [SQLite.Ignore]
        public Int16? AwayTeamScore { get { return Teams[1].Statistics[0].Value; } set { Teams[1].Statistics[0].Value = value ?? 0; } }

    }





   // JSON
     public partial class Event
     {

    //    // [SQLite.Ignore]
    //     //[Newtonsoft.Json.JsonProperty("bettable")]
    //     public Boolean Bettable { get; set; }

    //  //   [Newtonsoft.Json.JsonProperty("created")]
    //   //  [SQLite.Ignore]
    //     public DateTime Created { get; set; }


    //  [Newtonsoft.Json.JsonProperty("end_date")]
    //  [SQLite.Ignore]
    public DateTime EndDate { get; set; }


    // //    [Newtonsoft.Json.JsonProperty("modified")]
    //  //   [SQLite.Ignore]
    //     public DateTime Modified { get; set; }


    // //    [SQLite.Ignore]
    ////     [Newtonsoft.Json.JsonProperty("state")]
    //     public String State { get; set; }


    }



    public class Team
    {
      //  [PrimaryKey, AutoIncrement]
        public Int64 Id { get; set; }

    //    [ForeignKey(typeof(Event))]
        public Int64 EventId { get; set; }

      //  [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<TeamStat> Statistics { get; set; }

        //[OneToMany(CascadeOperations = CascadeOperation.All)]
        //public List<Player> Players { get; set; }


    }



    public partial class TeamStat
    {
     //   [PrimaryKey, AutoIncrement]
        public Int64 Id { get; set; }

     //   [NotNull]
        public Int32 TeamId { get; set; }

     //   [NotNull]
        public Int16 Value { get; set; }

    }
}




//description	null
//end_date	null
//full_slug	"/politics/uk/scottish-independence/scotland-referendum-2019"
//id	"806345"
//modified	"2017-03-13T14:53:27.820066Z"
//name	"Scottish Referendum before 2019?"
//parent_id	"806344"
//short_name	null
//slug	"scotland-referendum-2019"
//special_rules	null
//start_date	"2019-01-01"
//start_datetime	"2019-01-01T00:00:00Z"
//state	"upcoming"
//type	"politics"