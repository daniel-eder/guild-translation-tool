using CommandLine;

namespace GTT.LocaleConverter
{
    public class CommandLineOptions
    {
        [Value(0, MetaName = "File", Required = true,
            HelpText = "The file to convert. Can be any file that matches the given source encoding.")]
        public string File { get; set; }

        [Option('s', Default = "utf-8", Required = true, 
            HelpText = "The original encoding of the file. Can be UTF-8 or an ANSI code page.")]
        public string SourceEncoding { get; set; }

        [Option('t', Default = "1251", Required = true,
            HelpText = "The new encoding of the file. Can be UTF-8 or an ANSI code page")]
        public string TargetEncoding { get; set; }
    }
}