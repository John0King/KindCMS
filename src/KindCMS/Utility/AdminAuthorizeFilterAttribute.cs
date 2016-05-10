using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.AspNet.Http;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace KindCMS.Utility
{
    public class AdminAuthorizeFilterAttribute:AuthorizationFilterAttribute
    {

        public const string CookieName = "KindCMSAuth";
        public override void OnAuthorization(AuthorizationContext context)
        {
            

            HttpContext httpContext = context.HttpContext;
            var AuthString = httpContext.Request.Cookies[CookieName].ToString();
            ITextEncodeDecode EDCoder = new AuthEncodeDecode();
            var RawAuthString = EDCoder.Decode(AuthString);
            Result =  JsonConvert.DeserializeObject<AdminAuthInfo>(RawAuthString);
            if (HasAllowAnonymous(context))
            {
                return;
            }
            else
            {
                if(Result == null)
                {
                    string CurrentUrl = httpContext.Request.Path + httpContext.Request.QueryString.Value;
                    context.Result = new RedirectToActionResult("Login", "Account", new Dictionary<string, object> {
                        { "area", "Admin" }, { "returnUrl", CurrentUrl }
                    } );
                }
            }
        }

        public static AdminAuthInfo Result { get; set; }
        private void UpdateUser(AuthorizationContext context)
        {
            HttpContext httpContext = context.HttpContext;
        }

    }

    public class AdminAuthInfo
    {
        public AdminAuthInfo()
        {
            this.IsLogin = true;
            this.Issuer = "Admin";
        }
        public string Issuer { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsLogin { get; set; }
        public IDictionary<string,string> Clames { get; set; }

    }
}
