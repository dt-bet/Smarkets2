////using FluentNHibernate.Mapping;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Smarkets.Entity.NHibernate
//{


//    //public class CompetitionMap : ClassMap<Competition>
//    //{
//    //    public CompetitionMap()
//    //    {
//    //        Id(x => x.Id).GeneratedBy.Assigned();
//    //     Map(x => x.Key);
//    //        HasMany(x => x.Tournaments);

//    //            //.Cascade.All()
//    //            //.Inverse()
//    //            //.Table("StoreProduct");

//    //        //Component(x => x.Location);
//    //    }
//    //}



//    //public class TournamentMap : ClassMap<Tournament>
//    //{
//    //    public TournamentMap()
//    //    {
//    //        Id(x => x.Id).GeneratedBy.Assigned();
//    //     Map(x => x.Key);
//    //        HasMany(x => x.Stages);
//    //        References(x => x.Competition);
//    //        //.Cascade.All()
//    //        //.Inverse()
//    //        //.Table("StoreProduct");

//    //        //Component(x => x.Location);
//    //    }
//    //}

//    //public class StageMap : ClassMap<Stage>
//    //{
//    //    public StageMap()
//    //    {
//    //        Id(x => x.Id).GeneratedBy.Assigned();
//    //     Map(x => x.Key);
//    //        HasMany(x => x.Matches);
//    //        References(x => x.Tournament);
//    //        //.Cascade.All()
//    //        //.Inverse()
//    //        //.Table("StoreProduct");

//    //        //Component(x => x.Location);
//    //    }
//    //}


//    public class EventParentMap : ClassMap<EventParent>
//    {
//        public EventParentMap()
//        {
//            Id(x => x.Id).GeneratedBy.Increment();
//            Map(x => x.Key);
//            Map(x => x.Type);
//            HasMany(x => x.Matches);

//            //.Cascade.All()
//            //.Inverse()
//            //.Table("StoreProduct");

//            //Component(x => x.Location);
//        }
//    }



//    public class MatchMap : ClassMap<Match>
//    {
//        public MatchMap()
//        {


//            Id(x => x.Id);
//            References(x => x.EventParent).ReadOnly(); 
//            HasMany(x => x.Markets);
//            HasMany(x => x.Teams);
//            Map(x => x.Status);
//            Map(x => x.Start);
//            Map(x => x.End);
//            Map(x => x.Name);
//            //.Cascade.All()
//            //.Inverse()
//            //.Table("StoreProduct");

//            //Component(x => x.Location);
//        }
//    }

//    public class MatchTeamMap : ClassMap<MatchTeam>
//    {
//        public MatchTeamMap()
//        {
//             Id(x => x.Id);/*.GeneratedBy.Assigned();*/
//            References(x => x.Match);
//            //Map(x => x.TeamId);
//            HasMany(x => x.TeamStatGroups)
//                .Cascade.All()
//                .Inverse();
//            //HasMany(x => x.MatchPlayers);

//         Map(x => x.Key);
   

//            //.Cascade.All()
//            //.Inverse()
//            //.Table("StoreProduct");

//            //Component(x => x.Location);
//        }
//    }

//    public class TeamMap : ClassMap<Team>
//    {
//        public TeamMap()
//        {
//            Id(x => x.Id); //.GeneratedBy.Assigned();
//            HasMany(x => x.Players);
//            //HasMany(x => x.MatchTeams);
//         Map(x => x.Key);


//            //.Cascade.All()
//            //.Inverse()
//            //.Table("StoreProduct");

//            //Component(x => x.Location);
//        }
//    }


//    public class ConditionMap : ClassMap<Condition>
//    {
//        public ConditionMap()
//        {
//            //Id(x => x.Id).GeneratedBy.Assigned();
//            Id(x => x.Id);
//         Map(x => x.Key);
//            Map(x => x.Value);
//            References(x => x.Contract);

//            //.Cascade.All()
//            //.Inverse()
//            //.Table("StoreProduct");

//            //Component(x => x.Location);
//        }
//    }


//    public class PlayerMap : ClassMap<Player>
//    {
//        public PlayerMap()
//        {

//            Id(x => x.Id);
//            References(x => x.Team);
//         Map(x => x.Key);

//            //.Cascade.All()
//            //.Inverse()
//            //.Table("StoreProduct");

//            //Component(x => x.Location);
//        }
//    }


//    public class MatchPlayerMap : ClassMap<MatchPlayer>
//    {
//        public MatchPlayerMap()
//        {

//            Id(x => x.Id);
//            References(x => x.MatchTeam);
//            //References(x => x.Player);
//         Map(x => x.Key);
//            HasMany(x => x.PlayerStatGroups);
//            //.Cascade.All()
//            //.Inverse()
//            //.Table("StoreProduct");

//            //Component(x => x.Location);
//        }
//    }


//    public class PlayerStatGroupMap : ClassMap<PlayerStatGroup>
//    {
//        public PlayerStatGroupMap()
//        {

//            Id(x => x.Id);

//            References(x => x.MatchPlayer);
//         Map(x => x.Key);
//            HasMany(x => x.PlayerStats);
//            //.Cascade.All()
//            //.Inverse()
//            //.Table("StoreProduct");

//            //Component(x => x.Location);
//        }
//    }


//    public class TeamStatGroupMap : ClassMap<TeamStatGroup>
//    {
//        public TeamStatGroupMap()
//        {
  
//            Id(x => x.Id);

//            References(x => x.MatchTeam);
//         Map(x => x.Key);
//            HasMany(x => x.TeamStats)
//            .Cascade.All()
//            .Inverse();
//            //.Table("StoreProduct");

//            //Component(x => x.Location);
//        }
//    }


//    public class TeamStatMap : ClassMap<TeamStat>
//    {
//        public TeamStatMap()
//        {

//            Id(x => x.Id);

//            References(x => x.TeamStatGroup);
//            Map(x => x.Time);
//            Map(x => x.Value);
//            //.Cascade.All()
//            //.Inverse()
//            //.Table("StoreProduct");

//            //Component(x => x.Location);
//        }
//    }


//    public class PlayerStatMap : ClassMap<PlayerStat>
//    {
//        public PlayerStatMap()
//        {
    
//            Id(x => x.Id);

//            References(x => x.PlayerStatGroup);
//            Map(x => x.Time);
//            Map(x => x.Value);
//            //.Cascade.All()
//            //.Inverse()
//            //.Table("StoreProduct");

//            //Component(x => x.Location);
//        }
//    }
//}


