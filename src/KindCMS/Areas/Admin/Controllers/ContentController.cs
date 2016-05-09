using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using KindCMS.Models;
using Microsoft.AspNet.Authorization;
using cloudscribe.Web.Pagination;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace KindCMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class ContentController : Controller
    {
        private ApplicationDbContext _Db;

        public ContentController(ApplicationDbContext Db)
        {
            _Db = Db;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List(string key,int Page,string q)
        {
            // the PageList 's Index  start from 0;
            if(Page <= 1)
            {
                Page = 0;
            }else
            {
                Page -= 1;
            }

            var KEnity = _Db.Set<Classes>().SingleOrDefault(c=>c.Key == key);
            if (KEnity == null)
            {
                return new HttpNotFoundResult();
            }
            var PList = _Db.Set<News>().Where(n=>n.Key==key).ToPagedList(Page,20);
            return View(PList);
        }
    }
}
