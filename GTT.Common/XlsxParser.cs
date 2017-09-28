using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// <param name="file"></param>
        /// <returns></returns>
        public List<Tuple<string, string>> LoadXlsFile(string file)
        {
            var result = new List<Tuple<string, string>>();
            var workbook = new XLWorkbook(file);
            var worksheet = workbook.Worksheets.Worksheet(1);
            foreach (var row in worksheet.Rows().Skip(1))
            {
                result.Add(new Tuple<string, string>(row.Cell(1).Value as string, row.Cell(2).Value as string)); 
            }
            return result;
        }
    }
}
