using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegoPaging
{
    public class PageBuilder
    {
        public PageBuilder()
        {
            services = new ServiceCollection();
        }
        public IServiceCollection services { get; private set; }
        public PageBoxBuilder AddPageBox(Func<PageBoxBuilder> action)
        {
            var pageBoxBuilder = action() ;
            services.Add(new ServiceDescriptor(typeof(PageBoxBuilder), pageBoxBuilder));
            return pageBoxBuilder;
        }

        
    }

    public static class PageBuilderExt
    {
        public static PageBuilder AddPageBuilder(this IServiceCollection services)
        {
            var builder = new PageBuilder();
            services.Add(new ServiceDescriptor(typeof(PageBuilder),builder ));
            return builder;
        }
    }
}
