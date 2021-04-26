using System.Text.RegularExpressions;

namespace BlockBase.Dapps.NeverForget.Common
{
    public class Helpers
    {
        public static string CleanComment(string body)
        {
            var unquotedString = Regex.Replace(body, @"\'", "''");
            return unquotedString;
        }
    }
}
