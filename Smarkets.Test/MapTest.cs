using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Smarkets.Test
{
    public class UnitTest3
    {
        [Fact]
        public void Test1()
        {

            Entity.Match[] xxx = Smarkets.DAL.Sqlite.MatchRepository.SelectAll(@"..\..\..\..\Smarkets.EF6App\Data\").TakeLast(20).ToArray();

            SportsBetting.Entity.Sqlite.Match[] xxxx = xxx.Select(_ => Smarkets.Map.SmarketsMap.MapFromEntity(_)).ToArray();

        }

        [Fact]
        public void Test2()
        {

            Entity.Match[] xxx = Smarkets.DAL.Sqlite.MatchRepository.SelectAll(@"..\..\..\..\Smarkets.EF6App\Data\").Take(20).ToArray();

            Scrape.Entity.ScrapeMatch[] xxxx = xxx.Select(_ => Smarkets.Map.SmarketsMap2.MapToEntity(_)).ToArray();

        }

        [Fact]
        public void Test3()
        {

            var xxx = Smarkets.DAL.Sqlite.MatchRepository.Select(DateTime.Now.AddDays(-365),@"..\..\..\..\Smarkets.EF6App\Data\").Take(20).ToArray();

            Scrape.Entity.ScrapeMatch[] xxxx = xxx.Select(_ => Smarkets.Map.SmarketsMap2.MapToEntity(_)).ToArray();

        }
    }
}
