using Microsoft.AspNet.Html.Abstractions;
using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.WebEncoders;
using System.IO;

namespace LegoPaging.Components
{
    public class PageSection:IHtmlContent
    {
        public PageSection():this("span")
        {

        }
        public PageSection(string sectionTag)
        {
            if (string.IsNullOrWhiteSpace(sectionTag) && sectionTag.IndexOfAny(new[] {' ','<','>',',' }) < 0)
            {
                throw new ArgumentException("根标签必须是可用的值");
            }
            this.Section = new TagBuilder(sectionTag);
        }

        public TagBuilder Section { get; private set; }

        public void Append(IHtmlContent item)
        {
            this.Section.InnerHtml.Append(item);
        }

        public void WriteTo(TextWriter writer, IHtmlEncoder encoder)
        {
            Section.WriteTo(writer, encoder);
        }
    }
}