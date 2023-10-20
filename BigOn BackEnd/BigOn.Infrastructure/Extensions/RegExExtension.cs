using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BigOn.Infrastructure.Extensions
{
    public static partial class Extension
    {
        static public string ToSlug(this string context)
        {
            if (string.IsNullOrWhiteSpace(context))
                return null;

            //c# &&&&& sql => csharp-and-sql

            var replaceSet = new Dictionary<string, string>() {
                {"Ü|ü", "u"},
                {"İ|I|ı", "i"},
                {"Ş|ş", "s"},
                {"Ö|ö", "o"},
                {"Ç|ç", "c"},
                {"Ğ|ğ", "g"},
                {"Ə|ə", "e"},
                {"#", "sharp"},
                {@"(\?|/|\|\.|'|`|%|\*|!|@|\+)+", ""},
                {@"\&+", "and"},
                {@"[^a-z0-9]+", "-"},
                };

            return replaceSet.Aggregate(context, (i, m) => Regex.Replace(i, m.Key, m.Value, RegexOptions.IgnoreCase))
                .ToLower();
        }

        static public string ClearHtml(this string value,int len =0)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

          value=Regex.Replace(value, @"<[^>]*>", "");
            if (len > 20 && value.Length > len)
            {
                value = $"{ value.Substring(0, len)}...";
            }



            return value;
        }
        static public (string token, string email) RegisterConfirmToken(this string value)
        {
            var match = Regex.Match(value, @"^t=(?<token>.+)&email=(?<email>.+)$");
            if (!match.Success)
            {
                throw new Exception("BAD_DATA");
            }

            return (match.Groups["token"].Value, match.Groups["email"].Value);
        }
    }
}
