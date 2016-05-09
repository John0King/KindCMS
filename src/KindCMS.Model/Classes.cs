using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using KindCMS.Valid;

namespace KindCMS.Models
{
    public class Classes
    {
        [Key]
        [MaxLength(255,ErrorMessage ="{0} 不能超过 {1} 个字符")]
        [NoSpace]
        [Display(Name = "键")]
        public string Key { get; set; }
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Display(Name = "父级")]
        public string Parent { get; set; }
        [Display(Name = "路径索引")]
        public string PathIndexs { get; set; }
        [Display(Name ="排序")]
        public int Order { get; set; }
    }
}
