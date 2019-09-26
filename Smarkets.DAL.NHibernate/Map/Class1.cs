//using System;
//using System.Collections.Generic;
//using System.Text;
//using Smarkets.Model;
//using System.Linq;
//using AutoMapper;
//using System.Collections.ObjectModel;

//namespace Smarkets.Map
//{
//    public static class NHibernateMap
//    {

//        static NHibernateMap()
//        {

//            AutoMapper.Mapper.Initialize(cfg =>
//            {
//                cfg.CreateMap<DateTime, Int64>().ConvertUsing(x => x.Ticks);
//                cfg.CreateMap<Double, Int64>().ConvertUsing(x => (long)(x * 100));
//                cfg.CreateMap<Model.XML.Event, Smarkets.Entity.NHibernate.Match>()
//                .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.StartTimeAsDateTime))
//                .ForMember(dest => dest.Teams, opt => opt.MapFrom(src => new List<Smarkets.Entity.NHibernate.MatchTeam> { new Smarkets.Entity.NHibernate.MatchTeam { Key = src.HomeTeam}, new Smarkets.Entity.NHibernate.MatchTeam { Key =src.HomeTeam } }))

//               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => Helper.MakeKey(src)))
//               .ForMember(dest => dest.Markets, opt => opt.MapFrom(src => src.Markets.Select(_ => _.MapToEntity(src.DateAsDateTime))))
//               .ForMember(dest => dest.Status, opt => opt.Ignore())
//               .ForMember(dest => dest.End, opt => opt.Ignore());

//            cfg.CreateMap<Model.XML.Market, Smarkets.Entity.NHibernate.Market>()
//             .ForMember(dest => dest.Key, opt => opt.MapFrom(_ => _.Slug))
//                 .ForMember(dest => dest.Contracts, opt => opt.ResolveUsing<CustomResolver2>());



//                cfg.CreateMap<Model.XML.Contract, Smarkets.Entity.NHibernate.Contract>()
//                 .ForMember(dest => dest.Key, opt => opt.MapFrom(_ => _.Name))
//                .ForMember(dest => dest.Prices, opt => opt.ResolveUsing<CustomResolver>());
//            });

//        }



//        public static Smarkets.Entity.NHibernate.Match MapToEntity(this Model.XML.Event fdm, DateTime dt)
//        {
//            return Mapper.Map<Model.XML.Event, Smarkets.Entity.NHibernate.Match>(fdm, opt => opt.Items["Date"] = dt);


//        }

//        public static Smarkets.Entity.NHibernate.Market MapToEntity(this Model.XML.Market fdm, DateTime dt)
//        {
//            return Mapper.Map<Model.XML.Market, Smarkets.Entity.NHibernate.Market>(fdm, opt => opt.Items["Date"] = dt);


//        }

//        public static Smarkets.Entity.NHibernate.Contract MapToEntity(this Model.XML.Contract fdm, DateTime dt)
//        {
//            return Mapper.Map<Model.XML.Contract, Smarkets.Entity.NHibernate.Contract>(fdm, opt => opt.Items["Date"] = dt);


//        }



//        public static IEnumerable<Smarkets.Entity.NHibernate.EventParent> MapToCompetitions(Model.XML.Odds odds)
//        {

//            var oes = odds.Events
//                .Where(_ => _.Type == "Football match" && !_.Name.Contains("Winner"))
//                .Select(_ => new { _.ParentName, _.Type, @event = _.MapToEntity(odds.TimeStamp) })
//                .Select(xx => new Smarkets.Entity.NHibernate.EventParent
//                {
//                    Key = xx.ParentName,
//                    Type = xx.Type,
//                    Matches = new Collection<Entity.NHibernate.Match> { xx.@event }

//                }).ToList();

//            foreach (var o in oes)
//                foreach (var e in o.Matches)
//                    e.EventParent = o;

//            return oes;
//        }
//    }




//    public static class Helper
//    {



//        public static string MakeKey(Model.XML.Event src) =>  FootballSystem.Map.Helper.MakeKey(src.HomeTeam,src.AwayTeam,src.StartTimeAsDateTime);



//    }


//    public class CustomResolver : IValueResolver<Model.XML.Contract, Smarkets.Entity.NHibernate.Contract, ICollection<Smarkets.Entity.NHibernate.Price>>
//    {
//        public ICollection<Smarkets.Entity.NHibernate.Price> Resolve(Model.XML.Contract source, Smarkets.Entity.NHibernate.Contract destination,ICollection<Smarkets.Entity.NHibernate.Price> member, ResolutionContext context)
//        {
//            // var date = source.DateAsDateTime;

//            return source.Bids.Select(m =>
//            {
//                return new Smarkets.Entity.NHibernate.Price { Source = Smarkets.Entity.NHibernate.ContractType.Bid.ToString(), Time = ((DateTime)context.Items["Date"]).Ticks, Value = m.Integer };

//            }).Concat(source.Offers.Select(m =>
//            {
//                return new Smarkets.Entity.NHibernate.Price { Source = Smarkets.Entity.NHibernate.ContractType.Offer.ToString(), Time = ((DateTime)context.Items["Date"]).Ticks, Value = m.Integer };

//            })).ToList();



//        }


//    }


//    public class CustomResolver2 : IValueResolver<Model.XML.Market, Smarkets.Entity.NHibernate.Market, ICollection<Smarkets.Entity.NHibernate.Contract>>
//    {
//        public ICollection<Smarkets.Entity.NHibernate.Contract> Resolve(Model.XML.Market source, Smarkets.Entity.NHibernate.Market destination, ICollection<Smarkets.Entity.NHibernate.Contract> member, ResolutionContext context)
//        {
//            // var date = source.DateAsDateTime;

//            return source.Contracts?.Select(_ => _.MapToEntity((DateTime)context.Items["Date"])).ToList();

//        }


//    }
//}
