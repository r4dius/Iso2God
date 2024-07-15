using System.Text.RegularExpressions;

namespace Chilano.Iso2God
{
    internal class Utils
    {
        public static string sanitizePath(string path)
        {
            return new Regex(@"[\\/:*?""<>|]").Replace(path, "").Trim();
        }
    }
}
