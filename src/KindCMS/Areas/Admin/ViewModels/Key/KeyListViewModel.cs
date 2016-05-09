using KindCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KindCMS.Areas.Admin.ViewModels.Key
{
    public class KeyListViewModel
    {
        public Classes CLS { get; set; }
        /// <summary>
        /// 子集数量
        /// </summary>
        public long xCount { get; set; }
        /// <summary>
        /// 暂未使用
        /// </summary>
        public long ArticleCount { get; set; }
    }
}
