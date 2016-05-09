using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Html;
using Microsoft.AspNet.Html.Abstractions;
using System.Net.Http;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Mvc.ViewFeatures;
using Microsoft.AspNet.Http;

namespace KindCMS.Utility
{
    public static class HtmlExtension
    {
        /// <summary>
        /// 从当前站点的某个路径 或 其他据对路径获取string 内容
        /// </summary>
        /// <param name="Path">当前站点的路径</param>
        /// <returns></returns>
        public static IHtmlContent RenderPath(this IHtmlHelper Html,string Path)
        {
            string PathResult = "";
            var HC = new HttpClient();
            string TUrl = "";
            Uri Url;
            Url = ConverUrl(Path);
            if(Url == null)
            {
                TUrl += Html.ViewContext.HttpContext.Request.Scheme + "://";
                TUrl += Html.ViewContext.HttpContext.Request.Host;
                TUrl += Path;
                Url = ConverUrl(TUrl);
            }
            PathResult = HC.GetStringAsync(Url).GetAwaiter().GetResult();
            //PathResult = Url;
            return new HtmlString(PathResult);
        }
        /// <summary>
        /// 返回绝对路径的URl 或 null
        /// </summary>
        /// <param name="Path">绝对路径</param>
        /// <returns></returns>
        private static Uri ConverUrl(string Path)
        {
            Uri Url;
            if (Uri.TryCreate(Path, UriKind.Absolute, out Url))
            {
                if (!string.IsNullOrEmpty(Url.Scheme))
                    return Url;
            }
            return null;
        }
        /// <summary>
        /// 判断是否居于指定的路由参数
        /// </summary>
        /// <param name="values">期望的路由参数，建议使用 <see cref="Dictionary{TKey, TValue}"/>和对象初始化器</param>
        /// <returns></returns>
        public static bool IsRoutData(this IHtmlHelper htmlHelper, IEnumerable<KeyValuePair<string,object>> values)
        {
            var data = htmlHelper.ViewContext.RouteData.Values.ToList();
            int Count = values.Count();
            int matchCount = 0;
            foreach(var routItem in data)
            {
                foreach(var testItem in values)
                {
                    if (String.Equals(routItem.Key, testItem.Key,StringComparison.OrdinalIgnoreCase))
                    {
                        if(routItem.Value is string)
                        {
                            if (String.Equals(routItem.Value.ToString(), testItem.Value.ToString(), StringComparison.OrdinalIgnoreCase))
                            {
                                matchCount += 1;
                            }
                        }
                        else if(routItem.Value is IComparable)
                        {
                            var v = routItem.Value as IComparable;
                            if (v.CompareTo(testItem.Value) == 0)
                            {
                                matchCount += 1;
                            }
                        }
                        else
                        {
                            if (routItem.Value.Equals(testItem.Value))
                            {
                                matchCount += 1;
                            }
                        }
                    }
                }
                if(matchCount >= Count)
                {
                    break;
                }
            }
            if(matchCount >= Count)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 是否是指定路径
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static bool IsPath(this IHtmlHelper htmlHelper,string Path)
        {
            var RuningPath = htmlHelper.ViewContext.HttpContext.Request.Path.Value;
            return string.Equals(RuningPath, Path,StringComparison.OrdinalIgnoreCase) ;
        }
        public static bool IsPathNQuery(this IHtmlHelper htmlHelper,Uri Url)
        {
            bool result = false;
            string basePath = Url.AbsolutePath;
            result = IsPath(htmlHelper, basePath);
            //验证query
            var CurrentyQuery = htmlHelper.ViewContext.HttpContext.Request.Query;
            var validQuery = Url.Query.Split('&');
            foreach (var item in CurrentyQuery)
            {
                foreach(var vItem in validQuery)
                {

                }
            }
            return result;
            
        }
    }
}
