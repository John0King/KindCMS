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

    public interface IPageItem : IHtmlContent
    {
    }
    public class PageItem : IPageItem
    {
        public PageItem(string itemTag)
        {
            if (string.IsNullOrWhiteSpace(itemTag) && itemTag.IndexOfAny(new[] { ' ', '<', '>', ',' }) < 0)
            {
                throw new ArgumentException("根标签必须是可用的值");
            }
            this.Tag = new TagBuilder(itemTag);
        }



        public TagBuilder Tag { get; private set; }

        public IHtmlContent ToHtml()
        {
            throw new NotImplementedException();
        }

        public void WriteTo(TextWriter writer, IHtmlEncoder encoder)
        {
            Tag.WriteTo(writer, encoder);
        }

        public void AppendTo(PageSection section)
        {
            section.Append(this);
        }
    }
}