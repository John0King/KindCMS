using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegoPaging
{
    public static class Extensions
    {
        /// <summary>
        /// Get <see cref="IPageList{T}"/> by a real IQueryable Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Covert a <see cref="IEnumerable{T}"/> object to <see cref="IPageList{T}"/> type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">the source object, this value must already be paged</param>
        /// <param name="CurrentPage"></param>
        /// <param name="PageSize"></param>
        /// <param name="pageCount"></param>
        /// <param name="TotalItemCount"></param>
        /// <returns></returns>
        public static IPageList<T> ToBePagedList<T>(this IEnumerable<T> source, int CurrentPage,int PageSize,int pageCount,int TotalItemCount)
        {
            int pageSize = source.Count();
            if(PageSize < pageSize)
            {
                PageSize = pageSize;
            }
            return new PageList<T>(source, CurrentPage, PageSize,pageCount,TotalItemCount);
        }
        /// <summary>
        /// Get a <see cref="IPageList{T}"/> Object by a Full listed <see cref="IEnumerable{T}"/> object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static IPageList<T> ToPageList<T>(this IEnumerable<T> source,int CurrentPage,int PageSize)
        {
            int TotalCount = source.Count(); ;
            if (PageSize <= 0)
            {
                PageSize = 20;
            }
            int PageCount = (int)Math.Ceiling(TotalCount / (double)PageSize);

            if (CurrentPage < 1)
            {
                CurrentPage = 1;
            }
            if (CurrentPage > PageCount)
            {
                CurrentPage = PageCount;
            }
            return new PageList<T>(source.Skip(CurrentPage - 1 * PageSize).Take(PageSize), CurrentPage, PageSize, PageCount, TotalCount);
        }
        
    }
}
