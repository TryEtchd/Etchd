using System.Text.RegularExpressions;

namespace Etchd.Model
{
    public class Tag
    {
        public static Regex replaceSpecialChars = new Regex(@"[^a-zA-Z0-9\s]", RegexOptions.Compiled);
        private string tagName;

        public string TagName
        {
            get
            {
                return tagName;
            }

            set
            {
                tagName = replaceSpecialChars.Match(value).Value;
            }
        }
    }
}