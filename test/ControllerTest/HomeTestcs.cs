using KindCMS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Mvc;

using System.Net.Http;
using Microsoft.Data.Entity;
namespace ControllerTest
{
    public class HomeTestcs
    {
        private IServiceProvider Provider;
        public HomeTestcs()
        {
            var collection = new ServiceCollection();
            collection.AddMvc();
            Provider = collection.BuildServiceProvider();
            
        }
        [Fact]
        public void IndexTest()
        {
            //var Home = Provider.GetRequiredService<HomeController>();
            var Services = new ServiceCollection();
            Services.AddEntityFramework()
                .AddSqlite()
                .AddDbContext<KindCMS.Models.ApplicationDbContext>(a => a.UseSqlite("Data Source=KindCMS-0a1e5cd0.sqlite"));
            var Provider = Services.BuildServiceProvider();

            var Db = Provider.GetService<KindCMS.Models.ApplicationDbContext>();
            var Home = new HomeController(Db);
            var r = Home.Index();
            var viewResult = Assert.IsType<ViewResult>(r);
            var ViewName = viewResult.ViewName;
        }
       

        
    }
}
