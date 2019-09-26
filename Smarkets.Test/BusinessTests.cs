using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Smarkets.Test
{
    public class UnitTest2
    {
        [Fact]
        public void Test1()
        {
            var xxx = BLL.EventsService.Get(DateTime.Now.AddDays(-6));

        }



        [Fact]
        public void Test2()
        {

            var xxx = Smarkets.BLL.EventsService.Get2(dateTime: DateTime.Now);
            //var xxxx = xxx.Select(_ => Smarkets.Entity.SmarketsMap.MapFromEntity(_)).ToArray();
            Xunit.Assert.NotEmpty(xxx);
        }

        [Fact]
        public void TestFailure()
        {

            var xxx = Smarkets.BLL.EventsService.Get2(dateTime: DateTime.Now,@"..\..\Directory");
            ;
            Xunit.Assert.Empty(xxx);
            //var xxxx = xxx.Select(_ => Smarkets.Entity.SmarketsMap.MapFromEntity(_)).ToArray();

        }
    }
}
