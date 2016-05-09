using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.ComponentModel.DataAnnotations;
namespace KindCMS.Valid
{
    /// <summary>
    /// 验证数据不能包含空格和特殊字符
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property|AttributeTargets.Parameter,AllowMultiple =false)]
    public class NoSpaceAttribute:ValidationAttribute
    {
        private const string DefaultErrorMessage = "{0} 不能包含 [{1}] 符号";
        public NoSpaceAttribute():base(DefaultErrorMessage)
        {

        }
        public string Separator { get; set; } = ",";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var v = value as string;
            if (!string.IsNullOrEmpty(v))
            {
                if(v.Contains(Separator)|| v.Contains(" ")|| v.Contains("&"))
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            return ValidationResult.Success;
        }
        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, $"'空格',','和'&' " );
        }
    }
}
