using KindCMS.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using KindCMS.Utility;

namespace KindCMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private ApplicationDbContext _Db;
        public HomeController(
                ApplicationDbContext db,
                SignInManager<ApplicationUser> signManager
            )
        {
            _Db = db;
            _Db.Database.EnsureCreated();
        }

        public IActionResult Index()
        {
            ViewBag.PageTitle = "仪表盘";
            return View();
        }


        #region 系统功能
        public IActionResult Shutdown([FromServices]Microsoft.AspNet.Hosting.IApplicationLifetime AppLifetime)
        {
            AppLifetime.StopApplication();
            return View();
        }
        #endregion
    }
}
