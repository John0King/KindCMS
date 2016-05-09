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
    /// <summary>
    /// 分页控件根标签
    /// </summary>
    public class PageBox:IHtmlContent
    {
        /// <summary>
        /// 初始化一个 "div"为 根标签 
        /// </summary>
        public PageBox():this("div")
        {

        }
        /// <summary>
        /// 初始化一个 标签
        /// </summary>
        /// <param name="RootTag">标签名字</param>
        public PageBox(string RootTag)
        {
            if (string.IsNullOrWhiteSpace(RootTag) && RootTag.IndexOfAny(new[] { ' ', '<', '>', ',' }) < 0)
            {
                throw new ArgumentException("根标签必须是可用的值");
            }
            this.Root = new TagBuilder(RootTag);
        }
        /// <summary>
        /// 代表的标签
        /// </summary>
        public TagBuilder Root { get; private set; }
        /// <summary>
        /// 往里面添加 <see cref="PageSection"/>
        /// </summary>
        /// <param name="section"></param>
        public void AppendSection(PageSection section)
        {
            Root.InnerHtml.AppendHtml(section.ToString());
        }
        /// <summary>
        /// 设置 根标签
        /// </summary>
        /// <param name="action"></param>
        public void Apply(Action<TagBuilder> action)
        {
            action(this.Root);
        }
        /// <summary>
        /// 实现 IHtmlCotnent 接口
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="encoder"></param>
        public void WriteTo(TextWriter writer, IHtmlEncoder encoder)
        {
            Root.WriteTo(writer, encoder);
        }
    }
}
