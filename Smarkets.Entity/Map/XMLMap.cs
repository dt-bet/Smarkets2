using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smarkets.Model;
using AutoMapper;
using Smarkets.Model.XML;
using Betting.Enum;

namespace Smarkets.Entity
{

    public static class EntityMap
    {

        static EntityMap()
        {


            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DateTime, Int64>().ConvertUsing(x => x.Ticks);
                cfg.CreateMap<Double, Int64>().ConvertUsing(x => (long)(x * 100));


                cfg.CreateMap<Model.XML.Event, Smarkets.Entity.League>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Parent))
               .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
               .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.ParentName))
               .ForMember(dest => dest.Id, opt => opt.Ignore());


                cfg.CreateMap<Model.XML.Event, Smarkets.Entity.Match>()
                .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.StartTimeAsDateTime))
               .ForMember(dest => dest.Key, opt => opt.MapFrom(src => Helper.MakeKey(src)))
               .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.League, opt => opt.MapFrom(src => src.Parent))
                     .ForMember(dest => dest.Markets, opt => opt.MapFrom<CustomResolverMarket>());

                cfg.CreateMap<Model.XML.Market, Smarkets.Entity.Market>()
                .ForMember(dest => dest.MarketId, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.Key, opt => opt.MapFrom(_ => Helper.GetMarketType(_.Slug)))
                 .ForMember(dest => dest.Contracts, opt => opt.MapFrom<CustomResolverContract>());

                cfg.CreateMap<Model.XML.Contract, Smarkets.Entity.Contract>()
                    .ForMember(dest => dest.ContractId, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.Key, opt => opt.MapFrom(_ => Helper.GetContractType(_.Slug)))
                .ForMember(dest => dest.Prices, opt => opt.MapFrom<CustomResolverBids>());

            });

        }


        public static Smarkets.Entity.Match[] MapToEntity(Odds vx)
        {
            return vx.Events.Select(_ => EntityMap.MapToEntity(_, vx.TimeStamp)).ToArray();
        }


        public static Smarkets.Entity.Match MapToEntity(this Model.XML.Event fdm, DateTime dt)
        {
            return Mapper.Map<Model.XML.Event, Smarkets.Entity.Match>(fdm, opt => opt.Items["Date"] = dt);
        }
        public static IList<Smarkets.Entity.Match> MapToEntity(this IList<Model.XML.Event> fdm, DateTime dt)
        {
            return fdm.Select(_ =>
            {
                Smarkets.Entity.Match match = null;
                try
                {
                    match = Mapper.Map<Model.XML.Event, Smarkets.Entity.Match>(_, opt => opt.Items["Date"] = dt);
                }
                catch (Exception ex)
                {

                }
                return match;
            })
                .Where(a => a != null)
                .AsParallel()
            .ToArray();
        }

        public static IList<Smarkets.Entity.League> MapToLeague(this IList<Model.XML.Event> fdm)
        {
            return fdm.Select(_ => Mapper.Map<Smarkets.Entity.League>(_)).ToArray();

        }
        public static Smarkets.Entity.Market MapToEntity(this Model.XML.Market fdm, DateTime dt)
        {
            return Mapper.Map<Model.XML.Market, Smarkets.Entity.Market>(fdm, opt => { opt.Items["Date"] = dt; opt.Items["Volume"] = fdm.Volume ?? 0; });
        }

        public static Smarkets.Entity.Contract MapToEntity(this Model.XML.Contract fdm, DateTime dt, long volume)
        {
            return Mapper.Map<Model.XML.Contract, Smarkets.Entity.Contract>(fdm, opt => { opt.Items["Date"] = dt; opt.Items["Volume"] = volume; });
        }



    }




    public static class Helper
    {
        public static readonly (string[], ContractType)[] Contracts = (from field in typeof(ContractType).GetFields()
                                                                       let attribute = Attribute.GetCustomAttribute(field, typeof(UtilityEnum.NamesAttribute)) as UtilityEnum.NamesAttribute
                                                                       where attribute != null
                                                                       let names = attribute.Names.Concat(new[] { field.Name }).ToArray()
                                                                       select (names, (ContractType)field.GetValue(null)))
                                                             .ToArray();

        public static readonly (string[], MarketType)[] Markets = (from field in typeof(MarketType).GetFields()
                                                                   let attribute = Attribute.GetCustomAttribute(field, typeof(UtilityEnum.NamesAttribute)) as UtilityEnum.NamesAttribute
                                                                   where attribute != null
                                                                   let names = attribute.Names.Concat(new[] { field.Name }).ToArray()
                                                                   select (names, (MarketType)field.GetValue(null)))
            .ToArray();


        public static string MakeKey(this Model.XML.Event src) => $"{src.HomeTeam}_{src.AwayTeam}";

        public static ContractType GetContractType(string s)
        {
            if (!new[] { "any-other" }.Any(_ => s.Contains(_)))
                return Contracts.SingleOrDefault(_ => _.Item1.Any(one => one.Equals(s))).Item2;
            //if (!new[] { "any-other" }.Any(_ => s.Contains(_)))
            //    try
            //    {
            //        return UtilityEnum.NamesAttributeHelper.GetMatch<ContractType>(s);
            //    }
            //    catch
            //    {
            //        return default;
            //    }
            else
                //{
                return default;
            //}
        }
        public static MarketType GetMarketType(string s)
        {
            if (!new[] { "clean-sheet", "score-both-halves" }.Any(_ => s.Contains(_)))
                return Markets.SingleOrDefault(_ => _.Item1.Any(one => one.Equals(s))).Item2;
            //try
            //{
            //    return UtilityEnum.NamesAttributeHelper.GetMatch<MarketType>(s);
            //}
            //catch
            //{
            //    return default;
            //}
            else
            {
                return default;
            }
        }
    }


    public class CustomResolverBids : IValueResolver<Model.XML.Contract, Smarkets.Entity.Contract, List<Smarkets.Entity.Price>>
    {
        public List<Smarkets.Entity.Price> Resolve(Model.XML.Contract source, Smarkets.Entity.Contract destination, List<Smarkets.Entity.Price> member, ResolutionContext context)
        {
            return source.Bids.Select(m =>
            {
                return new Smarkets.Entity.Price { Time = ((DateTime)context.Items["Date"]).Ticks, Value = m.Integer, Type = (byte)PriceType.Bid };

            })
            .Concat(source.Offers.Select(m =>
            {
                return new Smarkets.Entity.Price { Time = ((DateTime)context.Items["Date"]).Ticks, Value = m.Integer, Type = (byte)PriceType.Offer };

            })).ToList();
        }


    }

    public class CustomResolverOffers : IValueResolver<Model.XML.Contract, Smarkets.Entity.Contract, List<Smarkets.Entity.Price>>
    {
        public List<Smarkets.Entity.Price> Resolve(Model.XML.Contract source, Smarkets.Entity.Contract destination, List<Smarkets.Entity.Price> member, ResolutionContext context)
        {
            // var date = source.DateAsDateTime;

            return source.Offers.Select(m =>
            {
                return new Smarkets.Entity.Price
                {
                    Volume = (long)context.Items["Volume"],
                    Time = ((DateTime)context.Items["Date"]).Ticks,
                    Value = m.Integer,
                    Type = (byte)PriceType.Offer
                };

            }).ToList();

        }
    }



    public class CustomResolverContract : IValueResolver<Model.XML.Market, Smarkets.Entity.Market, List<Smarkets.Entity.Contract>>
    {
        public List<Smarkets.Entity.Contract> Resolve(Model.XML.Market source, Smarkets.Entity.Market destination, List<Smarkets.Entity.Contract> member, ResolutionContext context)
        {
            return source.Contracts?
                .Where(_ => Helper.GetContractType(_.Slug) != default(ContractType))
                .Select(_ => EntityMap.MapToEntity(_, (DateTime)context.Items["Date"], (long)context.Items["Volume"])).ToList();
        }
    }

    public class CustomResolverMarket : IValueResolver<Model.XML.Event, Smarkets.Entity.Match, List<Smarkets.Entity.Market>>
    {
        public List<Smarkets.Entity.Market> Resolve(Model.XML.Event source, Smarkets.Entity.Match destination, List<Smarkets.Entity.Market> member, ResolutionContext context)
        {
            return source.Markets?
                .Where(_ => Helper.GetMarketType(_.Slug) != default(MarketType))
                .Select(_ => EntityMap.MapToEntity(_, (DateTime)context.Items["Date"])).ToList();
        }
    }

}

