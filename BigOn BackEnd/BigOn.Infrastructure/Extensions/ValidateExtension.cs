using System.Text.RegularExpressions;

namespace BigOn.Infrastructure.Extensions
{
    public static partial class Extension
    {
        public static bool IsEmail (this string email)
        {
            return Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }
    }
}
