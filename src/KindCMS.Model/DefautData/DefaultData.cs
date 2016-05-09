using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using KindCMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KindCMS.Models.DefautData
{
    public class DefaultData
    {
        public static void AddNecessaryData(IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            AddNecessaryData(provider);
        }
        public IServiceProvider Provider { get; private set; }
        public static async void AddNecessaryData(IServiceProvider provider)
        {
            await AddRoles(provider);
        }
        private static async Task AddRoles(IServiceProvider p)
        {
            var _roleManager = p.GetService<RoleManager<IdentityRole>>();
            if (!_roleManager.Roles.Any())
            {
                var r = await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin", NormalizedName = "管理员" });
            }
            
        }
       
    }
}
