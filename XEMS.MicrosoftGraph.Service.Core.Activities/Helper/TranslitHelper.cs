using System.Collections.Generic;

namespace XEMS.MicrosoftGraph.Service.Core.Activities.Helper
{
    /// <summary>
    /// </summary>
    public static class TranslitHelper
    {
        private static readonly Dictionary<string, string> dictionaryChar = new Dictionary<string, string>
        {
            {"а", "a"},
            {"б", "b"},
            {"в", "v"},
            {"г", "g"},
            {"д", "d"},
            {"е", "e"},
            {"ё", "yo"},
            {"ж", "zh"},
            {"з", "z"},
            {"и", "i"},
            {"й", "y"},
            {"к", "k"},
            {"л", "l"},
            {"м", "m"},
            {"н", "n"},
            {"о", "o"},
            {"п", "p"},
            {"р", "r"},
            {"с", "s"},
            {"т", "t"},
            {"у", "u"},
            {"ф", "f"},
            {"х", "h"},
            {"ц", "ts"},
            {"ч", "ch"},
            {"ш", "sh"},
            {"щ", "sch"},
            {"ъ", "'"},
            {"ы", "yi"},
            {"ь", ""},
            {"э", "e"},
            {"ю", "yu"},
            {"я", "ya"}
        };

        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Execute(string text)
        {
            var result = "";
            foreach (var ch in text)
            {
                var ss = "";
                if (dictionaryChar.TryGetValue(ch.ToString(), out ss))
                    result += ss;
                else result += ch;
            }

            return result;
        }
    }
}