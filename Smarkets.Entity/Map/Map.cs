using AutoMapper;
using Smarkets.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smarkets.Map
{


    public static class SmarketsMap
    {
        private static IMapper _mapper;

        static SmarketsMap()
        {

            _mapper = new MapperConfiguration((cfg) =>
             {
                 cfg.CreateMap<SportsBetting.Entity.Sqlite.Match, Smarkets.Entity.Match>()
              .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.Id));

                 cfg.CreateMap<Betting.Entity.Sqlite.Market, Smarkets.Entity.Market>()
                 .ForMember(dest => dest.MarketId, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.ParentId, opt => opt.Ignore());

                 cfg.CreateMap<Betting.Entity.Sqlite.Contract, Smarkets.Entity.Contract>()
                  .ForMember(dest => dest.ContractId, opt => opt.MapFrom(src => src.Id))
                                 .ForMember(dest => dest.ParentId, opt => opt.Ignore());

                 cfg.CreateMap<Betting.Entity.Sqlite.Price, Price>()
                    .ForMember(dest => dest.ParentId, opt => opt.Ignore());


                 cfg.CreateMap<Match, SportsBetting.Entity.Sqlite.Match>();


                 cfg.CreateMap<Market, Betting.Entity.Sqlite.Market>();


                 cfg.CreateMap<Contract, Betting.Entity.Sqlite.Contract>();

                 cfg.CreateMap<Price, Betting.Entity.Sqlite.Price>();
  


             }).CreateMapper();
        }
        public static Match MapToEntity(this SportsBetting.Entity.Sqlite.Match fdm)
        {
            return _mapper.Map<SportsBetting.Entity.Sqlite.Match, Match>(fdm);
        }
        public static Market MapToEntity(this Betting.Entity.Sqlite.Market fdm)
        {
            return _mapper.Map<Betting.Entity.Sqlite.Market, Market>(fdm);
        }
        public static Contract MapToEntity(this Betting.Entity.Sqlite.Contract fdm)
        {
            return _mapper.Map<Betting.Entity.Sqlite.Contract, Contract>(fdm);
        }

        public static Price MapToEntity(this Betting.Entity.Sqlite.Price fdm)
        {
            return _mapper.Map<Betting.Entity.Sqlite.Price, Price>(fdm);
        }

        public static SportsBetting.Entity.Sqlite.Match MapFromEntity(this Match fdm)
        {
            return _mapper.Map<SportsBetting.Entity.Sqlite.Match>(fdm);
        }
        public static Betting.Entity.Sqlite.Market MapFromEntity(this Market fdm)
        {
            return _mapper.Map<Betting.Entity.Sqlite.Market>(fdm);
        }
        public static Betting.Entity.Sqlite.Contract MapFromEntity(this Contract fdm)
        {
            return _mapper.Map<Betting.Entity.Sqlite.Contract>(fdm);
        }

        public static Betting.Entity.Sqlite.Price MapFromEntity(this Price fdm)
        {
            return _mapper.Map<Betting.Entity.Sqlite.Price>(fdm);
        }
    }


}
