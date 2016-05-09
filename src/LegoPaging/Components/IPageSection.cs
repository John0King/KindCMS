using Microsoft.AspNet.Html.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegoPaging.Components
{
    interface IPageSection:IHtmlContent
    {
        void Append(IHtmlContent PageItem);
    }
}
