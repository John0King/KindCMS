using Microsoft.AspNet.Html.Abstractions;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Html;

namespace LegoPaging
{
    public interface IToolbarBuilder
    {
        Action<TagBuilder> BoxBuilder { get; set; }
        Action<IList<TagBuilder>> SectionBuilder { get; set; }
        Action<IList<TagBuilder>> ItemBuilder { get; set; }

        
    }


    public class ToolbarBuilder : IToolbarBuilder
    {
        public ToolbarBuilder()
        {
            this.BoxBuilder = (inTag) =>
           {
               inTag = new TagBuilder("div");
               inTag.AddCssClass("Page");
           };

            this.SectionBuilder = inTag =>
            {

            };
        }


        public Action<TagBuilder> BoxBuilder { get; set; }
        public Action<IList<TagBuilder>> SectionBuilder { get; set; }
        public Action<IList<TagBuilder>> ItemBuilder { get; set; }

        
    }


    
}
