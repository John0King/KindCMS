using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.AspNet.Identity;

namespace KindCMS.Utility
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,AllowMultiple =false)]
    public class MyFilterAttribute:AuthorizationFilterAttribute
    {
        public override void OnAuthorization(AuthorizationContext context)
        {
            base.OnAuthorization(context);
        }
    }

}
