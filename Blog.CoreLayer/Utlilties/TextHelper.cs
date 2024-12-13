using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blog.CoreLayer.Utlilties
{
    public static class TextHelper
    {
        public static string ToSlug(this string Value)
        {

            return Value.Trim().ToLower().Replace("!", "")
                .Replace("~", "")
                .Replace("@", "")
                .Replace("#", "")
                .Replace("$", "")
                .Replace("%", "")
                .Replace("^", "")
                .Replace("&", "")
                .Replace("*", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("+", "")
                .Replace("/", "")
                .Replace(" ", "-")
                .Replace(@"\", "")
                .Replace("<", "")
                .Replace(">", "");

        }
        public static string ConvertHtmlToText(this string text)
        {
            return Regex.Replace(text, "<.*?>", " ")
                .Replace(":&nbsp;", " ");
        }
        public static string RemoveMediaUrls (this string input)
        {
            // الگوی عبارت منظم برای پیدا کردن آدرس‌های عکس (jpg, png, gif) و ویدیو (mp4, avi, mov)
            string pattern = @"(http(s?):)([/|.|\w|\s|-])*\.(jpg|png|gif|mp4|avi|mov)";
            string output = Regex.Replace(input, pattern, string.Empty, RegexOptions.IgnoreCase);

            return output;
        }
        public static string TextDescription(this string text)
        {
            text = text.ConvertHtmlToText().RemoveMediaUrls();
            if (text.Length > 200)
            {
                return text.Substring(0,200);
            }
            return text;
        }
    }
}
