using System;
using System.Diagnostics;
using System.IO;
using GTT.Common;

namespace GTT.Terminal
{
    internal class Program
    {
        private const string XlsxExtension = ".xlsx";
        private const string LooExtension = ".loo";

        private static void Main(string[] args)
        {
            try
            {
                if (args.Length <= 1) //If no file was supplied exit.
                {
                    PrintHelp();
                }
                else
                {
                    var file = args[0];
                    var codePage = int.Parse(args[1]);
                    var looParser = new LooParser();
                    var xlsxParser = new XlsxParser();
                    switch (file)
                    {
                        case var xlsxFile when Path.GetExtension(file)
                            .Equals(XlsxExtension, StringComparison.CurrentCultureIgnoreCase)
                        : //if xlsxl file -> convert to loo
                            var xlsxLines = xlsxParser.LoadXlsFile(xlsxFile, args.Length > 2 ? args[2] : null);
                            looParser.SaveLooFile(xlsxLines, Path.GetFileNameWithoutExtension(xlsxFile) + LooExtension,
                                codePage);
                            break;
                        case var looFile when Path.GetExtension(file)
                            .Equals(LooExtension, StringComparison.CurrentCultureIgnoreCase)
                        : //if loo file -> convert to xlsx 
                            var looLines = looParser.LoadLooFile(looFile, codePage);
                            xlsxParser.SaveXlsFile(looLines, Path.GetFileNameWithoutExtension(looFile) + XlsxExtension);
                            break;
                        default:
                            PrintHelp();
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                PrintHelp();
                throw e;
            }
        }

        private static void PrintHelp()
        {
            Console.WriteLine(Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName) +
                              " <file> <codepage> [FormatFile]");
            Console.WriteLine("Code Pages: ");
            Console.WriteLine("\tRussian: 1251");
            Console.WriteLine("\tRussian with chinese binary mod: 936");
            Console.WriteLine("\tEnglish, French, German, Italian, Spanish: 1252");
            Console.WriteLine("FormatFile: Optional, an xlsx file containing format information for the tool.");
        }
    }
}