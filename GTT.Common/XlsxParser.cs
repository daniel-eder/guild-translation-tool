using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ClosedXML.Excel;

namespace GTT.Common
{
    public class XlsxParser
    {
        /// <summary>
        /// Creates an xlsx file from a list of key-text tuples.
        /// </summary>
        /// <param name="parsedLoo"></param>
        /// <param name="file"></param>
        public void SaveXlsFile(List<Tuple<string, string>> parsedLoo, string file)
        {
            var workbook = new XLWorkbook();
            var table = new DataTable();
            table.Columns.Add("Key");
            table.Columns.Add("Text");
            foreach (var keyValue in parsedLoo)
            {
                var newRow = table.NewRow();
                newRow[0] = keyValue.Item1;
                newRow[1] = keyValue.Item2;
                table.Rows.Add(newRow);
            }
            workbook.Worksheets.Add(table, "Translations");
            file = FileHelper.GetUniqueFile(file).FullName;
            workbook.SaveAs(file);
        }

        /// <summary>
        /// Parses an xlsx file into a list of key-text tuples. Parses strictly, all conventions have to be kept.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="formatFile">The format file.</param>
        /// <returns></returns>
        public List<Tuple<string, string>> LoadXlsFile(string file, string formatFile, ParserLanguage language)
        {
            var result = new List<Tuple<string, string>>();
            try
            {
                var workbook = new XLWorkbook(file);
                var formatWorksheet = formatFile != null ? new XLWorkbook(formatFile).Worksheets.Worksheet(1) : null;

                var worksheet = workbook.Worksheets.Worksheet(1);

                foreach (var row in worksheet.Rows().Skip(2)) //Skip license and header
                {
                    var key = row.Cell(1).GetValue<string>();
                    var value = row.Cell((int) language).GetValue<string>(); //Go to right language column

                    var formatKeyCell = formatWorksheet?.Column(1).CellsUsed(cell => cell.GetValue<string>() == key).FirstOrDefault();
                    if (formatKeyCell != null)
                    {
                        var formatValueCell = formatKeyCell.WorksheetRow().Cell(2);
                        var maxLength = !string.IsNullOrWhiteSpace(formatValueCell.GetValue<string>()) ? formatValueCell.GetValue<int>() : int.MaxValue;
                        value = value.Length <= maxLength ? value : value.Substring(0, maxLength);
                    }

                    result.Add(new Tuple<string, string>(key, value)); 
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            return result;
        }
    }
}
