using MoreLinq;
using Scrape.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Smarkets.BLL

{
    using Common;

    public class EventsService
    {
        const string relativeDirectoryPath = @"..\..\..\..\Smarkets.EF6App\Data\";

        const string fullDirectoryPath = @"C:\Users\rytal\Documents\Visual Studio 2019\Projects\MasterProject\Smarkets2\Smarkets.EF6App\Data\";

        public static Model.XML.Event[] Get(DateTime dateTime)
        {
            IEnumerable<Model.XML.Event> scrapeMatches = null;

            if (dateTime > DateTime.Now)
            {
                var maxes = FileNameHelper.GetDateTimeFromFileName().MaxBy(_ => _.Item1);
                var deserialisedFile = Smarkets.DAL.XML.Repo.GetOdds(maxes.SingleOrDefault().Item2);
                scrapeMatches =deserialisedFile.Events.Where(DAL.XML.Repo.GetPredicate()).Where(_ => _.DateAsDateTime == dateTime.Date);
            }
            else
            {
                var deserialisedFile = Smarkets.DAL.XML.Repo.GetOdds(FileNameHelper.GetDateTimeFromFileName().Where(_ => dateTime.Date - _.Item1 > default(TimeSpan)).MinBy(_ => dateTime.Date - _.Item1).FirstOrDefault().Item2);
                scrapeMatches =deserialisedFile.Events.Where(DAL.XML.Repo.GetPredicate()).Where(_ => _.DateAsDateTime == dateTime.Date);
            }

            return scrapeMatches.ToArray();
        }



        public static SportsBetting.Entity.Sqlite.Match[] Get2(DateTime dateTime)
        {
            return Get2(dateTime, (!System.IO.Directory.Exists(relativeDirectoryPath)) ? fullDirectoryPath : relativeDirectoryPath);
        }

        public static SportsBetting.Entity.Sqlite.Match[] Get2(DateTime dateTime, string directory)
        {
            var xxx = Smarkets.DAL.Sqlite.MatchRepository.Select(dateTime, directory);

            var xxxx = xxx.Select(_ => Smarkets.Map.SmarketsMap.MapFromEntity(_)).ToArray();

            return xxxx.ToArray();
        }



        public static Scrape.Entity.ScrapeMatch[] Get3(DateTime dateTime)
        {
            return Get3(dateTime, (!System.IO.Directory.Exists(relativeDirectoryPath)) ? fullDirectoryPath : relativeDirectoryPath);
        }


        public static Scrape.Entity.ScrapeMatch[] Get3(DateTime dateTime, string directory)
        {
            var xxx = Smarkets.DAL.Sqlite.MatchRepository.Select(dateTime, directory);

            var xxxx = xxx.Select(_ => Smarkets.Map.SmarketsMap2.MapToEntity(_)).ToArray();

            return xxxx.ToArray();
        }
    }

}
