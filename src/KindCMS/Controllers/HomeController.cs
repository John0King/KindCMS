using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using KindCMS.Models;

namespace KindCMS.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _Db;

        public HomeController(ApplicationDbContext Db)
        {
            _Db = Db;
            _Db.Database.EnsureCreated();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
        [Route("[action]/{statusCode}")]
        public IActionResult NotFound(int statusCode)
        {
            ViewBag.StatusCode = statusCode;
            return PartialView();
        }

        public IActionResult SomeThing()
        {
            return new HttpNotFoundObjectResult(this);
        }
    }
}
