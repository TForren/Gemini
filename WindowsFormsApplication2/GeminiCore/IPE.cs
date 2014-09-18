/**
 * Teague Forren
 * Johanna Jan
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GeminiCore
{
    public class IPE
    {
        public string FileToParse { get; set; }

        public IPE(string filename)
        {
            this.FileToParse = filename;
        }

        public void ParseFile()
        {
            var lines = File.ReadAllLines(this.FileToParse).ToList<string>();

            foreach (var line in lines)
            {
                Regex labelStmtFormat = new Regex(@"^(?<label>.*?)\s*:$");
                var labelStmtMatch = labelStmtFormat.Match(line);
                if (labelStmtMatch.Success)
                {
                   var label = labelStmtMatch.Groups["label"].Value;
                }
            }

        }
    }
}
