using GrapeCity.Documents.Excel;
using System.Reflection;

namespace IdentityMVC.Services
{
    public class ExcelService
    {
        public static void GetExcelFile()
        {
            var fs = File.Open("wwwroot\\uploads\\data2.xlsx", FileMode.Open);

            var sheets = Workbook.GetNames(fs);
            foreach (var sheet in sheets)
            {
                var data = Workbook.ImportData(fs, sheet);
                ExportExcelFile(data, sheet);
            }
        }

        public static void ExportExcelFile(object[,] data, string file_name)
        {
            //Boş excel çalışma kitabı:
            Workbook workbook = new Workbook();

            workbook.Worksheets[0].Range[0, 0, data.GetLength(0), data.GetLength(1)].Value = data;
            workbook.Save($"wwwroot\\upload\\exported_{file_name}_{DateTime.Now.ToString("ddMMyyyy_HHmmss")}.xlsx");
        }
    }
}
