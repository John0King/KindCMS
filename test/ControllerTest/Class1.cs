using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ControllerTest
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class Class1
    {
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(5, Add(2, 2));
        }

        int Add(int x, int y)
        {
            return x + y;
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(6)]
        public void MyFirstTheory(int value)
        {
            Assert.True(IsOdd(value));
        }

        bool IsOdd(int value)
        {
            return value % 2 == 1;
        }


        [Fact]
        public void TestDic()
        {
            dynamic d = new DynamicDictionary();
            d.Name = "haha";
            d.Xprofile = "vas";

            

            Assert.Equal(d.Name, "haha");
            Assert.True(String.IsNullOrEmpty(d.mavc));
        }

        [Fact]
        public void TestPathString()
        {
            string sp = "";
            string vp = "";
            PathString p = new PathString("/Home/Index?a=A&好=就是好");
            sp = p.ToString();
            vp = p.Value;
            Assert.Equal(vp, "/Home/Index?a=A&好=就是好");
        }
    }
}
