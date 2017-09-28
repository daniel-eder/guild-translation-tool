using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

                if (args.Length <= 0) //If no file was supplied exit.
                {
                    Console.WriteLine("No input file supplied.");
                }
                else
                {
                    var file = args[0];
                    var looParser = new LooParser();
                    var xlsxParser = new XlsxParser();
                    switch (file)
                    {
                        case var xlsxFile when Path.GetExtension(file)
                            .Equals(XlsxExtension, StringComparison.CurrentCultureIgnoreCase): //if xlsxl file -> convert to loo
                            var xlsxLines = xlsxParser.LoadXlsFile(xlsxFile);
                            looParser.SaveLooFile(xlsxLines,Path.GetFileNameWithoutExtension(xlsxFile) + LooExtension);
                            break;
                        case var looFile when Path.GetExtension(file)
                            .Equals(LooExtension, StringComparison.CurrentCultureIgnoreCase): //if loo file -> convert to xlsx 
                            var looLines = looParser.LoadLooFile(looFile);
                            xlsxParser.SaveXlsFile(looLines, Path.GetFileNameWithoutExtension(looFile) + XlsxExtension);
                            break;
                        default:
                            Console.WriteLine("Invalid input file.");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }
    }
}
