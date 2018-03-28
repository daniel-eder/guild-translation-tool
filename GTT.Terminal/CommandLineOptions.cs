using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace GTT.Terminal
{
    public class CommandLineOptions
    {
        [Value(0, MetaName = "File", Required=true, HelpText = "The file to convert. Can be any .loo file or a .xlsx file in the format of the multi-language master sheet.")]
        public string File { get; set; }

        [Option(Default = false, Required = false, HelpText = "Creates correctly named files for each language.")]
        public bool CreateLanguageFiles { get; set; }

        [Option(Default = null, Required = false, HelpText = "Optional, an xlsx file containing format information for the tool.")]
        public string FormatFile { get; set; }
    }
}
