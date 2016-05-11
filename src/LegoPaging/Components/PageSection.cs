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
    public abstract class PageSection:IPageSection
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

        public virtual TagBuilder Section { get; private set; }

        public virtual void Append(IHtmlContent item)
        {
            this.Section.InnerHtml.Append(item);
        }

        public virtual void WriteTo(TextWriter writer, IHtmlEncoder encoder)
        {
            Section.WriteTo(writer, encoder);
        }

        //public override string ToString()
        //{
        //    StringWriter TW = new StringWriter();
        //    WriteTo(TW, Microsoft.Extensions.WebEncoders.HtmlEncoder.Default);
        //    return TW.GetStringBuilder().ToString();
        //}
    }
}