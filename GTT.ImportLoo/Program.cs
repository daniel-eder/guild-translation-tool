using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using CommandLine;
using GTT.Common;

namespace GTT.ImportLoo
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
               //Load XLS FIl 
               //Load Loo File 
               //for each loo key, insert insert text into xls if key exists too
               
               var looParser = new LooParser();
               var workbook = new XLWorkbook(options.SheetFile);

                var worksheet = workbook.Worksheets.Worksheet(1);
                var language = (ParserLanguage)Enum.Parse(typeof(ParserLanguage), options.Language, true);  

                var looLines = looParser.LoadLooFile(options.LooFile);
                foreach(var line in looLines)
                {
                    var cell = worksheet.Columns(1, 1).CellsUsed().Where((IXLCell c )=> c.GetString() == line.Item1).FirstOrDefault();
                    if(cell != null)
                    {
                        cell.WorksheetRow().Cell((int) language).SetValue(line.Item2);
                    }

                }
                workbook.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured: {0}", e);
            }
        }
    }
}
