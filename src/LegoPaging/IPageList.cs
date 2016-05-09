using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegoPaging
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPageList
    {
        int PageCount { get; }
        int TotalItemCount { get; }
        int CurrentPage { get; }
        int PageSize { get; }
    }

    public interface IPageList<out T> : IPageList,IEnumerable<T>
    {
        
    }
    
}
