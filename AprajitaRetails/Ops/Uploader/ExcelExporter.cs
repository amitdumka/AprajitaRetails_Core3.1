using AprajitaRetails.Areas.Uploader.Models;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using AprajitaRetails.Areas.Voyager.Data;
using OfficeOpenXml;

namespace AprajitaRetails.Ops.Uploader
{
    public static class ExcelExporter
    {
        public static string ExportPurchase(VoyagerContext db, string fileName)
        {
            //string rootFolder = _hostingEnvironment.WebRootPath;
            //string fileName = @"ExportCustomers.xlsx";

            FileInfo file = new FileInfo(fileName);

            using (ExcelPackage package = new ExcelPackage(file))
            {

                IList<ImportPurchase> purchaseList = db.ImportPurchases.ToList();

                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Purchase");
                int totalRows = purchaseList.Count();

                worksheet.Cells[1, 1].Value = "Bardcode";
                worksheet.Cells[1, 2].Value = "MRP";
                worksheet.Cells[1, 3].Value = "Cost";
                worksheet.Cells[1, 4].Value = "Qty";
                worksheet.Cells[1, 5].Value = "Total";
                int i = 0;
                for (int row = 2; row <= totalRows + 1; row++)
                {
                    worksheet.Cells[row, 1].Value = purchaseList[i].Barcode;
                    worksheet.Cells[row, 2].Value = purchaseList[i].MRP;
                    worksheet.Cells[row, 3].Value = purchaseList[i].Cost;
                    worksheet.Cells[row, 4].Value = purchaseList[i].Quantity;
                    worksheet.Cells[row, 5].Value = purchaseList[i].CostValue;
                    i++;
                }

                package.Save();

            }

            return " Purchase list has been exported successfully";
        }

    }
}
