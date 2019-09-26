
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Smarkets.Entity.NHibernate
//{
//    //public class World
//    //{
//    //    public virtual ICollection< Country> Countries { get; set; }
//    //    public virtual ICollection<Team> Teams { get; set; }
//    //}

//    //public class Country
//    //{
//    //    public Country()
//    //    {
//    //    }

   
//    //    public virtual Int64 Id { get;  protected set; }

//    //    public virtual String Key { get; set; }

//    //    public virtual ICollection <Competition> Competitions { get; set; }
//    //}



//    public partial class EventParent
//    {
//        public EventParent()
//        {
//        }


//        public virtual Int64 Id { get; protected set; }

//        //public virtual Country Country { get; set; }

//        public virtual String Key { get; set; }

//        public virtual String Type { get; set; }

//        public virtual ICollection <Match> Matches { get; set; }

//    }




//    public partial class Match  //, IParent<Market>
//    {
//        public Match()
//        {
//        }


//        public virtual Int64 Id { get; set; }

//        public virtual EventParent EventParent { get; set; }

//        public virtual ICollection<MatchTeam> Teams { get; set; }

//        public virtual string Name { get; set; }

//        public virtual Byte Status { get; set; }

//        public virtual Int64 Start { get; set; }

//        public virtual Int64 End { get; set; }

//        public virtual ICollection <Market> Markets { get; set; }
     

    

//    }



//    public class Market 
//    {
//        public Market()
//        {
//        }


//        public virtual Int64 Id { get;   set; }

//        public virtual Match Match { get; set; }

//        public virtual String Key { get; set; }

//        public virtual ICollection <Contract> Contracts { get; set; }
  
//    }

//    public class Contract 
//    {
//        public Contract()
//        {
//        }


//        public virtual Int64 Id { get;   set; }
//        public virtual Market Market { get; set; }
//        public virtual String Key { get; set; }
//        //public virtual ICollection<Condition> Conditions { get; set; }
//        public virtual ICollection <Price> Prices { get; set; }
     
//    }

//    public class Condition
//    {
//        public Condition()
//        {
//        }

//        public virtual Int64 Id { get; protected set; }
//        public virtual string Key { get; set; }
//        public virtual Int64 Value { get; set; }
//        public virtual Contract Contract { get; set; }
//    }





//    public class Price 
//    {
//        //public Price(long id, long time, long value, long source) : this()
//        //{
//        //    Id = id;
//        //    Time = time;
//        //    Value = value;
//        //    Source = source;
//        //}


//        public virtual Int64 Id { get;  set; }
//        public virtual Contract Contract { get; set; }
//        public virtual Int64 Time { get; set; }
//        public virtual Int64 Value { get; set; }
//        public virtual String Source { get; set; }
      
//    }


//    public enum ContractType
//    {
//        Bid,Offer

//    }


//    //public Interface IDbTimeValue
//    //{
//    //    Int64 Time { get; set; }
//    //    Int64 Value { get; set; }
//    //}



//    public class MatchTeam
//    {

//        public virtual Int64 Id { get;   set; }
//        public virtual Int64 TeamId { get;  set; }
//        public virtual Match Match { get; set; }
//        public virtual String Key { get; set; }

//        public virtual ICollection<TeamStatGroup> TeamStatGroups { get; set; }
//        public virtual ICollection<MatchPlayer> MatchPlayers { get; set; }

//    }


//    public class Team
//    {

//        public virtual Int64 Id { get;   set; }

//        public virtual String Key { get; set; }
//        //public virtual ICollection<Team> Teams { get; set; }
//        public virtual ICollection<MatchTeam> MatchTeams { get; set; }
//        public virtual ICollection <Player> Players { get; set; }

//    }


//    public class Player
//    {
//        public virtual Int64 Id { get;  protected set; }
//        public virtual String Key { get; set; }
//        public virtual Team Team { get; set; }

//        public virtual ICollection<MatchPlayer> MatchPlayers { get; set; }
//    }


//    public class MatchPlayer
//    {
//        public virtual Int64 Id { get; protected set; }
//        public virtual String Key { get; set; }
//        public virtual MatchTeam MatchTeam { get; set; }
//        //public virtual Player Player { get; set; }
//        public virtual ICollection<PlayerStatGroup> PlayerStatGroups { get; set; }

//    }

//    public class PlayerStatGroup
//    {
//        public virtual Int64 Id { get;  protected set; }
//        public virtual String Key { get; set; }
//        public virtual MatchPlayer MatchPlayer { get; set; }
//        public virtual ICollection<PlayerStat> PlayerStats { get; set; }
//    }


//    public class TeamStatGroup
//    {
//        public virtual Int64 Id { get;  protected set; }
//        public virtual String Key { get; set; }
//        public virtual MatchTeam MatchTeam { get; set; }
//        public virtual ICollection<TeamStat> TeamStats { get; set; }
//    }


//    public class TeamStat
//    {
//        public virtual Int64 Id { get;  protected set; }
//        public virtual TeamStatGroup TeamStatGroup { get; set; }
//        public virtual double Value { get; set; }
//        public virtual Int64 Time { get; set; }
//    }



//    public class PlayerStat
//    {
//        public virtual Int64 Id { get;  protected set; }
//        public virtual PlayerStatGroup PlayerStatGroup { get; set; }
//        public virtual double Value { get; set; }
//        public virtual Int64 Time { get; set; }
//    }


//    //public partial class Score
//    //    {
//    //        [PrimaryKey, AutoIncrement]
//    //        public virtual Int64 Id { get;  protected set; }

//    //        [NotNull]
//    //        public virtual Int32 MatchId { get; set; }

//    //        [NotNull]
//    //        public virtual String TeamName { get; set; }

//    //        [NotNull]
//    //        public virtual Int16 Value { get; set; }

//    //    }


//    //    public enum ContractType
//    //    {
//    //        Home,
//    //        Draw,
//    //        Away


//    //    }


//    //    public enum MatchStatus
//    //    {

//    //        FullTime
//    //    }

//    //public enum OddSource
//    //{
//    //    BEAvg,
//    //    BEMax,
//    //    SkyBet,
//    //    OddsPortalMax,
//    //}


//    //}

//}




