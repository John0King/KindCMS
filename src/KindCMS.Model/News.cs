using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace KindCMS.Models
{
    public class News
    {
        public long Id { get; set; }
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name = "简介")]
        public string Summary { get; set; }
        [Display(Name ="点击量")]
        public long Hits { get; set; }
        [Display(Name = "编辑者")]
        public string Writor { get; set; }
        [Display(Name = "来源")]
        public string Source { get; set; }
        [Display(Name = "转载网址")]
        public string SourceUrl { get; set; }
        [Display(Name = "内容")]
        public string Content { get; set; }
        [Display(Name = "发布时间")]

        public DateTime PublishDate { get; set; }
        [Display(Name = "是否允许")]
        public bool IsAllow { get; set; }
        /// <summary>
        /// 以此分类
        /// </summary>
        [Display(Name = "键")]
        public string Key { get; set; }

        [Display(Name = "关键词")]
        public string Keywords { get; set; }
        [Display(Name = "描述")]
        public string Description { get; set; }
    }
}
