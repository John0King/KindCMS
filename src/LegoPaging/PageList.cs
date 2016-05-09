using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegoPaging
{
    /// <summary>
    /// this PageList is a implement of that Page start at 1;
    /// To Create one use <see cref="Extensions.ToPageList{T}(IQueryable{T}, int, int)"/> to initailize one
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageList<T> : List<T>, IPageList<T>
    {
        internal PageList(IEnumerable<T> source, int CurrentPage, int PageSize,int PageCount,int TotalItemCount):base(source)
        {
            this.CurrentPage = CurrentPage;
            this.PageSize = PageSize;
            this.TotalItemCount = TotalItemCount;
        }
        
        
        #region IPageList
        public int CurrentPage { get; private set; }
        public int PageCount { get; private set; }

        public int TotalItemCount { get; private set; }
        public int PageSize { get; private set; }
        #endregion
    }
}
