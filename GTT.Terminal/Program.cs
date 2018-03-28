using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CommandLine;
using GTT.Common;

namespace GTT.Terminal
{
    internal class Program
    {
        private const string XlsxExtension = ".xlsx";
        private const string LooExtension = ".loo";

        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed(opts => Run(opts));
        }

        private static void Run(CommandLineOptions options)
        {
            try
            {
                    var looParser = new LooParser();
                    var xlsxParser = new XlsxParser();
                    switch (options.File)
                    {
                        case var xlsxFile when Path.GetExtension(options.File)
                            .Equals(XlsxExtension, StringComparison.CurrentCultureIgnoreCase)
                        : //if xlsxl file -> convert to loo
                            foreach(var language in Enum.GetValues(typeof(ParserLanguage)).Cast<ParserLanguage>())
                            {
                                Console.WriteLine($"Preparing loo file for {language} language.");
                                var xlsxLines = xlsxParser.LoadXlsFile(xlsxFile, options.FormatFile, language);
                                Console.WriteLine($"{xlsxLines.Count} keys found for {language} language.");
                                var looFilename = options.CreateLanguageFiles ? $"locdirect_{language.ToString().ToLower()}{LooExtension}" : $"{((ParserLanguageShort) language).ToString().ToLower()}\\locdirect_english{LooExtension}";
                                var looDirectory = Path.GetDirectoryName(looFilename);
                                if(!string.IsNullOrWhiteSpace(looDirectory) && !Directory.Exists(looDirectory))
                                    Directory.CreateDirectory(looDirectory);
                                looParser.SaveLooFile(xlsxLines, looFilename);
                                Console.WriteLine($"Saved {looFilename} for {language} language.");
                            }
                            
                            break;
                        case var looFile when Path.GetExtension(options.File)
                            .Equals(LooExtension, StringComparison.CurrentCultureIgnoreCase)
                        : //if loo file -> convert to xlsx 
                            Console.WriteLine("Converting .loo file to .xslx");
                            var looLines = looParser.LoadLooFile(looFile);
                            var xlsxFilename = Path.GetFileNameWithoutExtension(looFile) + XlsxExtension;
                            xlsxParser.SaveXlsFile(looLines, xlsxFilename);
                            Console.WriteLine($"Saved {xlsxFilename}.");
                            break;
                        default:
                            Console.WriteLine("Invalid file type supplied. Only .loo and .xlsx are valid.");
                            break;
                    }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured: {0}", e);
            }
        }
    }
}