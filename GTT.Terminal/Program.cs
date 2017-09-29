using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GTT.Common;

namespace GTT.Terminal
{
    class Program
    {
        private const string XlsxExtension = ".xlsx";
        private const string LooExtension = ".loo";

        static void Main(string[] args)
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
                            .Equals(XlsxExtension, StringComparison.CurrentCultureIgnoreCase): //if xlsxl file -> convert to loo
                            var xlsxLines = xlsxParser.LoadXlsFile(xlsxFile);
                            looParser.SaveLooFile(xlsxLines,Path.GetFileNameWithoutExtension(xlsxFile) + LooExtension, codePage);
                            break;
                        case var looFile when Path.GetExtension(file)
                            .Equals(LooExtension, StringComparison.CurrentCultureIgnoreCase): //if loo file -> convert to xlsx 
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

        static void PrintHelp()
        {
            Console.WriteLine(Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + " <file> <codepage>");
            Console.WriteLine("Code Pages: ");
            Console.WriteLine("\tRussian: 1251");
            Console.WriteLine("\tEnglish, French, German, Italian, Spanish: 1252");
        }
    }
}
