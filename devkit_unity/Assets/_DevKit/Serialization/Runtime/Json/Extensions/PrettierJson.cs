namespace DevKit.Serialization.Json.Extensions
{
    public static class PrettierJson
    {
        /// <summary>
        /// Makes a raw JSON string prettier, and easier to read.
        /// </summary>
        public static string Prettify(this string json)
        {
            json = json.Replace("{", "{\n");
            json = json.Replace("}", "\n}");
            json = json.Replace("[", "[\n");
            json = json.Replace("]", "\n]");
            json = json.Replace(",", ",\n");
            json = json.Replace(":", ": ");
            var lines = json.Split('\n');
            json = "";
            var lineOffset = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains('}') || lines[i].Contains(']'))
                    lineOffset--;
                json += OffsetJsonLine(lineOffset) + lines[i] + (i < lines.Length - 1 ? "\n" : "");
                if (lines[i].Contains('{') || lines[i].Contains('['))
                    lineOffset++;
            }
            return json;
        }
        private static string OffsetJsonLine(int amount)
        {
            string str = "";
            for (int i = 0; i < amount; i++)
                str += "\t";
            return str;
        }
    }
}
