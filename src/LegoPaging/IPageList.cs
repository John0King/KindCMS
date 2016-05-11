using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegoPaging
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPageModel
    {
        int PageCount { get; }
        int TotalItemCount { get; }
        int CurrentPage { get; }
        int PageSize { get; }
    }

    public interface IPageList<out T> : IPageModel,IEnumerable<T>
    {
        T this[int Index] { get; }
    }

    public static class IPageModelExt
    {
        public static bool IsFirstPage(this IPageModel model)
        {
            return model.CurrentPage <= 1;
        }
    }
    
}
