using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GTT.Common
{
    public class LooParser
    {
        /// <summary>
        /// The tag used to identify keys in the loo file.
        /// </summary>
        private const string KEY_TAG = "key";
        /// <summary>
        /// The tag used to identify texts in the loo file.
        /// </summary>
        private const string VALUE_TAG = "text";


        /// <summary>
        /// Creates a loo file from a list of key-text tuples.
        /// </summary>
        /// <param name="lines">The list of lines.</param>
        /// <param name="file">The file to create.</param>
        /// <param name="codePage">The code page for encoding.</param>
        public void SaveLooFile(List<Tuple<string, string>> lines, string file, int codePage)
        {
            var looBuilder = new StringBuilder();

            looBuilder.AppendLine("LocDirectData");
            looBuilder.AppendLine("{");
            looBuilder.AppendLine("\tStrings = array");
            looBuilder.AppendLine("\t(");

            foreach (var line in lines)
            {
                looBuilder.AppendLine("\t\tLocDirectEntry");
                looBuilder.AppendLine("\t\t{");
                looBuilder.AppendLine($"\t\t\tKey = \"{line.Item1}\";");
                looBuilder.AppendLine($"\t\t\tText = \"{line.Item2}\";");
                looBuilder.AppendLine("\t\t},");

            }

            looBuilder.AppendLine("\t)");
            looBuilder.AppendLine("};");
            file = FileHelper.GetUniqueFile(file).FullName;
            File.WriteAllText(file, looBuilder.ToString(), Encoding.GetEncoding(codePage));
        }

        /// <summary>
        /// Parses a .loo file into a list of key-text tuples. Parses strictly, all conventions have to be kept.
        /// </summary>
        /// <param name="file">The file to load.</param>
        /// <param name="codePage">The code page to use for decoding.</param>
        /// <returns></returns>
        public List<Tuple<string, string>> LoadLooFile(string file, int codePage)
        {
            var loolines = File.ReadAllLines(file, Encoding.GetEncoding(codePage));
            var result = new List<Tuple<string, string>>();
            var lastKey = string.Empty;
            foreach (var line in loolines)
            {
                var trimmedLine = line.TrimStart(' ', '\t');
                switch (trimmedLine)
                {
                    case var key when key.ToLower().StartsWith(KEY_TAG):
                        lastKey = key;
                        break;
                    case var value when value.ToLower().StartsWith(VALUE_TAG):
                        result.Add(new Tuple<string, string>(this.ExtractContent(lastKey), this.ExtractContent(value)));
                        break;
                }
            }

            return result;
        }

        private string ExtractContent(string line)
        {
            try
            {
                var splitLine = line.Split('=');
                var content = splitLine[1].TrimStart(' ', '"').TrimEnd(';').TrimEnd('"');
                return content;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
           
        }
    }
}
