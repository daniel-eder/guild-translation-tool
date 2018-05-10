using System;
using System.IO;
using System.Text;
using CommandLine;

namespace GTT.LocaleConverter
{
    class Program
    {
        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed(opts => Run(opts));
        }

        private static void Run(CommandLineOptions options)
        {
            try
            {
                var sourceEncoding = GetEncoding(options.SourceEncoding);
                var targetEncoding = GetEncoding(options.TargetEncoding);

                var text = File.ReadAllText(options.File, sourceEncoding);
                File.WriteAllText(options.File, text, targetEncoding);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured: {0}", e);
            }
        }

        private static Encoding GetEncoding(string name)
        {
            if (name.Equals("utf-8", StringComparison.InvariantCultureIgnoreCase) ||
                name.Equals("utf8", StringComparison.InvariantCultureIgnoreCase))
                return new UTF8Encoding(false); //Guild never uses BOM

            //Assume code page. Not very safe but we only need this for now.
            return Encoding.GetEncoding(int.Parse(name));
        }
    }
}