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
        public override void OnAuthorization(AuthorizationContext context)
        {
            

            HttpContext httpContext = context.HttpContext;
            var AuthString = httpContext.Request.Cookies["KindCMSAuth"].ToString();
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
                    context.Result = new RedirectResult("/Admin/Login");
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
        public ICollection<KeyValuePair<string,string>> Clames { get; set; }

    }
}
