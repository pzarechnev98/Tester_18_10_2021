using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Tester.Models.Entities
{
    public class FileHelpers
    {
        // Создаем файл и считываем данные из файла Excel
        public static DataTable.GetDataTableFromExcelSaveMode(string path, bool hasHeader = true)
        {
            using(var pck = new OfficeOpenXml.ExcelPackage())
            {
                using(var stream = FileHelpers.OpenRead(path))
                {
                    pck.Load(stream);
                }
                var ws = pck.WorkBook.Worksheets.First();
                DataTable tbl = new DataTable();
                foreach(var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Colums])
                {
                    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {.0}", firstRowCell.Start.Column));
                }
                var startRow = hasHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++) 
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, wsRow.Dimension.End.Columm];
                    DataRow row = tbl.Row.Add();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                }
                return tbl;
            }
        }



    }
}
