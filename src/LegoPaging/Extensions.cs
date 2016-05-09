using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegoPaging
{
    public static class Extensions
    {
        public static IPageList<T> ToPageList<T>(this IQueryable<T> source,int CurrentPage, int PageSize)
        {
            int TotalCount = source.Count();
            if(PageSize <= 0)
            {
                PageSize = 20;
            }
            int PageCount = (int)Math.Ceiling(TotalCount / (double)PageSize);

            if(CurrentPage < 1)
            {
                CurrentPage = 1;
            }
            if(CurrentPage > PageCount)
            {
                CurrentPage = PageCount;
            }
            return new PageList<T>(source.Skip(CurrentPage - 1 * PageSize).Take(PageSize), CurrentPage, PageSize, PageCount, TotalCount);
        }

        public static IPageList<T> ToPagedList<T>(this IEnumerable<T> source, int CurrentPage, int PageSize,int pageCount,int TotalItemCount)
        {
            return new PageList<T>(source, CurrentPage, PageSize,pageCount,TotalItemCount);
        }
        
    }
}
