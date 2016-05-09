using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ControllerTest.LegoPageTest
{
    public class PageListTest
    {
        public PageListTest()
        {
            this.List = new List<int>();
            for(var i = 0; i < 3000; i++)
            {
                List.Add(i);
            }

        }

        public List<int> List { get; private set; }
        [Fact]
        public void ToPageListTest()
        {
            LegoPaging.IPageList<int> PageList = LegoPaging.Extensions.ToBePagedList(List, 1, 3, 30000);

        }
    }
}
