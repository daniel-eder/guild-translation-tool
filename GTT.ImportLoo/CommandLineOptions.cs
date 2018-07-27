using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace GTT.ImportLoo
{
    public class CommandLineOptions
    {
        [Value(0, MetaName = "LooFile", Required=true, HelpText = "The source loo file.")]
        public string LooFile { get; set; }

        [Value(1, MetaName = "SheetFile", Required=true, HelpText = "The target translation sheet file.")]
        public string SheetFile { get; set; }

        [Option(Default = null, Required = true, HelpText = "The language colum to insert into")]
        public string Language {get; set;}
    }
}
