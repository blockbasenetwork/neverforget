using System;
using System.Text.RegularExpressions;

namespace BlockBase.Dapps.NeverForget.Common
{
    public class Helpers
    {
        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static DateTime FromUnixTime(int unixTime)
        {
            return epoch.AddSeconds(unixTime);
        }

        public static string CleanComment(string body)
        {
            var unquotedString = Regex.Replace(body, @"\'", "''");
            return unquotedString;
        }
    }
}
