using System.Text.RegularExpressions;

namespace IdentityMVC.Toolbox.Extensions
{
    public static class StringExtensions
    {
        public static string ToUrl(this string text)
        {
            string result = text.ToLower().Trim();
            result = Regex.Replace(result, @"[^\w\s]", "");
            result = Regex.Replace(result, @"\s+", " ");
            return result.Trim().Replace(" ", "-");
        }
    }
}
