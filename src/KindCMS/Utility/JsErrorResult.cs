using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindCMS.Utility
{
    /// <summary>
    /// 返回一个只包含 js alert 代码的页面，找回 php/asp 时代的感觉
    /// </summary>
    public class JsErrorResult:ActionResult
    {
        public bool UseFullError { get; private set; }

        public JsErrorResult(bool UseFullError = false)
        {
            this.UseFullError = UseFullError;
        }
        public override void ExecuteResult(ActionContext context)
        {
            if (context == null||context?.HttpContext == null||context?.ModelState ==null)
            {
                throw new ArgumentNullException(nameof(context), "{0}必须包含HttpContext等信息");
            }
            if (!UseFullError)
            {
                OutputFirstError(context);
            }else
            {
                OutputFullError(context);
            }
           
        }

        private void OutputFirstError(ActionContext context)
        {
            var firstError = context.ModelState.FirstOrDefault(e=>e.Value.ValidationState == Microsoft.AspNet.Mvc.ModelBinding.ModelValidationState.Invalid).Value?.Errors?.FirstOrDefault()?.ErrorMessage;
            var Message = firstError + "\r\n";
            var JsCode = "alert(" + StringStatement(Message) + ");\r\nwindow.history.go(-1);";
            context.HttpContext.Response.WriteAsync(JavaScriptContent(JsCode)).GetAwaiter().GetResult();
            //var ResultBytes = Encoding.UTF8.GetBytes(JavaScriptContent(JsCode));
            //context.HttpContext.Response.Body.Write(ResultBytes, 0, ResultBytes.Length);
        }

        private void OutputFullError(ActionContext context)
        {
            var MessageSB = new StringBuilder();
            var ValueEntries = context.ModelState.Values;
            foreach(var valueEntry in ValueEntries)
            {
                foreach(var error in valueEntry.Errors)
                {
                    MessageSB.AppendLine(error.ErrorMessage);
                }
            }
            var jsCode = String.Format("alert({0});\r\nwindow.history.go(-1);", StringStatement(MessageSB.ToString()));
            context.HttpContext.Response.WriteAsync(JavaScriptContent(jsCode)).GetAwaiter().GetResult();
            //var ResultBytes = Encoding.UTF8.GetBytes(JavaScriptContent(jsCode));
            //context.HttpContext.Response.Body.Write(ResultBytes, 0, ResultBytes.Length);
        }
        /// <summary>
        /// this will not return null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string JsString(string str)
        {
            string value = "";
            if (string.IsNullOrEmpty(str))
            {
                return value;
            }
            else
            {
                 value = UnicodeEncode(str);
            }
            return value;
        }

        private string StringStatement(string str)
        {
            string Statement = "\"{0}\"";
            return String.Format(Statement, JsString(str));
        }

        private string JavaScriptContent(string JsCode)
        {
            var Tag = @"<!doctype html>
<html>
<head>
<meta charset=""utf-8"" />
<title>...</title>
</head>
<body>
<script type=""text/javascript"">{0}</script>
</body>
</html>";
            if (string.IsNullOrEmpty(JsCode))
            {
                JsCode = "";
            }
            return string.Format(Tag, JsCode);
        }

        #region 以下代码大多数来自微软的.net Framework 4.6.1
        private string UnicodeEncode(string str)
        {
           
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            var b = new StringBuilder(str.Length);
            foreach (var c in str)
            {
                switch (c)
                {
                    case '\r':
                        b.Append("\\r");
                        break;
                    case '\t':
                        b.Append("\\t");
                        break;
                    case '\"':
                        b.Append("\\\"");
                        break;
                    case '\\':
                        b.Append("\\\\");
                        break;
                    case '\n':
                        b.Append("\\n");
                        break;
                    case '\b':
                        b.Append("\\b");
                        break;
                    case '\f':
                        b.Append("\\f");
                        break;

                    default:
                        if (CharRequiresJavaScriptEncoding(c))
                        {
                            b.Append("\\u");
                            b.Append(((int)c).ToString("x4"));
                        }
                        else
                        {
                            b.Append(c);
                        }

                        break;
                }
            }
            return b.ToString();
        }

        private bool CharRequiresJavaScriptEncoding(char c)
        {
            return c < 0x20 // control chars always have to be encoded
                || c == '\"' // chars which must be encoded per JSON spec
                || c == '\\'
                || c == '\'' // HTML-sensitive chars encoded for safety
                || c == '<'
                || c == '>'
                || (c == '&' && true) // Bug Dev11 #133237. Encode '&' to provide additional security for people who incorrectly call the encoding methods (unless turned off by backcompat switch)
                || c == '\u0085' // newline chars (see Unicode 6.2, Table 5-1 [http://www.unicode.org/versions/Unicode6.2.0/ch05.pdf]) have to be encoded (DevDiv #663531)
                || c == '\u2028'
                || c == '\u2029'
                || !IsUrlSafeChar(c);
        }
        public static bool IsUrlSafeChar(char ch)
        {
            if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9'))
                return true;

            switch (ch)
            {
                case '-':
                case '_':
                case '.':
                case '!':
                case '*':
                case '(':
                case ')':
                    return true;
            }

            return false;
        }
        #endregion

    }
}
