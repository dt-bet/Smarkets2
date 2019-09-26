//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Smarkets.Model;
//using AutoMapper;
//using Smarkets.Model.XML;
//using Betting.Enum;
//using MoreLinq;
//using Scrape.Entity;

//namespace Smarkets.Map
//{

//    public static class ScrapeMap2
//    {
//        static IMapper mapper;


//        static ScrapeMap2()
//        {
//            mapper = new MapperConfiguration((cfg) =>
//            {
//                cfg.CreateMap<Model.XML.Event, Scrape.Entity.ScrapeMatch>()
//                .ForMember(dest => dest.Ticks, opt => opt.MapFrom(src => src.StartTimeAsDateTime.Ticks))
//                .ForMember(dest => dest.Predictions, opt => opt.MapFrom(src => MapToPredictions(src)))
//                .ForMember(dest => dest.Recommendations, opt => opt.Ignore())
//                .ForMember(dest => dest.Result, opt => opt.Ignore())
//                .ForMember(dest => dest.Home, opt => opt.MapFrom(src => src.HomeTeam))
//                .ForMember(dest => dest.Away, opt => opt.MapFrom(src => src.AwayTeam))
//                .ForMember(dest => dest.League, opt => opt.MapFrom(src => GetLeague(src.ParentName)));
//            }).CreateMapper();
//        }


//        public static Scrape.Entity.ScrapeMatch[] MapToEntity(IEnumerable<Event> events)
//        {
//            return events.Select(_ => mapper.Map<Scrape.Entity.ScrapeMatch>(_)).ToArray();
//        }


//        public static Dictionary<string, Scrape.Entity.WinPrediction> MapToPredictions(Event @event)
//        {
//            var xx = @event.Markets
//                    .SelectMany(_ => _.Contracts.Select(__ => new { b = __.Bids.DefaultIfEmpty().Max(b => b?.Decimal ?? 0m), slug = __?.Slug ?? string.Empty, market = _?.Slug ?? string.Empty })).ToArray();

//            var home = xx.SingleOrDefault(_ => _?.slug == "home" && _?.market == "winner")?.b ?? 1000000m;
//            var draw = xx.SingleOrDefault(_ => _?.slug == "draw" && _?.market == "winner")?.b ?? 1000000m;
//            var away = xx.SingleOrDefault(_ => _?.slug == "away" && _?.market == "winner")?.b ?? 1000000m;
//            Dictionary<string, Scrape.Entity.WinPrediction> xx1 = null;
//            try
//            {
//                xx1 = GetDictionary(home, draw, away);
//            }
//            catch (Exception ex)
//            {

//            }

//            return xx1;

//        }



//        private static Dictionary<string, Scrape.Entity.WinPrediction> GetDictionary(decimal home, decimal draw, decimal away)
//        {
//            var xx1 = new Dictionary<string, Scrape.Entity.WinPrediction>
//            {
//                {
//                    "Smarkets",
//                    new Scrape.Entity.WinPrediction
//                    {
//                        IsBookMaker = true,
//                        Home = (int)(100m / (home==0m?100000m:home)),
//                        Draw = (int)(100m /  (draw==0m?100000m:draw)),
//                        Away = (int)(100m / (away==0m?100000m:away))
//                    }

//                }
//            };
//            return xx1;
//        }


//        public static League GetLeague(string leagueName)
//        {
//            if (TryGetLeague(leagueName, out League league))
//                return league;
//            else
//                return default;
//        }

//        public static bool TryGetLeague(string stringLeague, out League league)
//        {
//            try
//            {
//                league = UtilityEnum.NamesAttributeHelper.GetMatch<Scrape.Entity.League>(stringLeague);
//                return true;
//            }
//            catch
//            {
//                // missingLeagues.Add(unibetMatch.Competition);
//            }
//            league = default;

//            return false;
//        }


//    }

//}

