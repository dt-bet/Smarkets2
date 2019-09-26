using System;
using System.IO;
using System.Linq;
using Xunit;

namespace Smarkets.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

            foreach(var directory in System.IO.Directory.GetDirectories(@"C:\Users\rytal\Documents\Visual Studio 2019\Projects\MasterProject\Smarkets2\Smarkets.EF6App\bin\Data"))
            {
                var di = new System.IO.DirectoryInfo(directory);
                string[] name = di.Name.Split('_');

                if ((name[2] == "16" || name[2] == "17") && name[0]!=name[2])
                {
 
                    Directory.Move(directory, Path.Combine(System.IO.Directory.GetParent(di.FullName).FullName, $"{name[2]}_{name[1]}_{name[0]}"));
                }
            }


        }
    }
}
