using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ControllerTest.LegoPageTest
{
    using LegoPaging;
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
            LegoPaging.IPageList<int> PageList = LegoPaging.Extensions.ToBePagedList(List, 1,List.Count() ,3, 30000);

            for (var i = 0; i < List.Count; i++)
            {
                Console.WriteLine(PageList[i]);
            }
            LegoPaging.IPageModel p = PageList;
            Assert.True(p.IsFirstPage());
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3000)]
        public void ToPageListTest2(int CurrentPage)
        {

            LegoPaging.IPageList<int> PageList = LegoPaging.Extensions.ToBePagedList(List, CurrentPage, List.Count(), 3, 30000);

            for (var i = 0; i < List.Count; i++)
            {
                Console.WriteLine(PageList[i]);
            }
            LegoPaging.IPageModel p = PageList;
            Assert.True(p.IsFirstPage());

        }
    }
}
