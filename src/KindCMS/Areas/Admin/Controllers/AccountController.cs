using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KindCMS.Utility;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.AspNet.Authorization;
using Newtonsoft.Json;

namespace KindCMS.Areas.Admin.Controllers
{
    [AdminAuthorizeFilter]
    [Area("Admin")]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(AdminAuthInfo Info, string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            AuthEncodeDecode EDCoder = new AuthEncodeDecode();
            var Rawjson = JsonConvert.SerializeObject(Info);
            var json = EDCoder.Encode(Rawjson);
            HttpContext.Response.Cookies.Append(AdminAuthorizeFilterAttribute.CookieName, json, new Microsoft.AspNet.Http.CookieOptions() { HttpOnly = true,  Expires = DateTime.Now.AddSeconds(60) });
            string ReturnUrl = "/";
            if (Url.IsLocalUrl(returnUrl))
            {
                ReturnUrl = returnUrl;
            }
            return Redirect(returnUrl);
        }
    }

}
