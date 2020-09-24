using System.Collections.Generic;
using System.IO;
using System.Linq;
using AprajitaRetails.Areas.Uploader.Models;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Models.ViewModels;
using OfficeOpenXml;

namespace AprajitaRetails.Ops.Exporter
{
    public static class ExcelExporter
    {
        public static void CashInHandExporter(string fileName, List<CashInHand> inHandList, string name)
        {
            FileInfo file = new FileInfo(fileName);
            
           // if (!file.Directory.Exists) Directory.CreateDirectory(fileName); 
            using ExcelPackage package = new ExcelPackage(file);
            ExcelWorksheet worksheet2 = package.Workbook.Worksheets.Add(name);
            
            worksheet2.Cells[1, 1].Value = "Date";
            worksheet2.Cells[1, 2].Value = "Opening Cash";
            worksheet2.Cells[1, 3].Value = "Cash-In";
            worksheet2.Cells[1, 4].Value = "Cash-Out";
            worksheet2.Cells[1, 5].Value = "Closing Cash";

            int totalRows = inHandList.Count;
            int i = 0;
            for (int row = 2; row <= totalRows + 1; row++)
            {
                worksheet2.Cells[row, 1].Value = inHandList[i].CIHDate;
                worksheet2.Cells[row, 2].Value = inHandList[i].OpenningBalance;
                worksheet2.Cells[row, 3].Value = inHandList[i].CashIn;
                worksheet2.Cells[row, 4].Value = inHandList[i].CashOut;
                worksheet2.Cells[row, 5].Value = inHandList[i].ClosingBalance;
                i++;
            }

            package.Save();
        }

        public static void CashBookExporter(string fileName, List<CashBook> cashBooks, string name)
        {
            FileInfo file = new FileInfo(fileName);
           // if (!file.Directory.Exists) Directory.CreateDirectory(fileName);
            using ExcelPackage package = new ExcelPackage(file);

            ExcelWorksheet worksheet1 = package.Workbook.Worksheets.Add(name);

            worksheet1.Cells[1, 1].Value = "Date";
            worksheet1.Cells[1, 2].Value = "Particulars";
            worksheet1.Cells[1, 3].Value = "Cash-In";
            worksheet1.Cells[1, 4].Value = "Cash-Out";
            worksheet1.Cells[1, 5].Value = "Balance";

            int totalRows = cashBooks.Count;
            int i = 0;
            for (int row = 2; row <= totalRows + 1; row++)
            {
                worksheet1.Cells[row, 1].Value = cashBooks[i].EDate;
                worksheet1.Cells[row, 2].Value = cashBooks[i].Particulars;
                worksheet1.Cells[row, 3].Value = cashBooks[i].CashIn;
                worksheet1.Cells[row, 4].Value = cashBooks[i].CashOut;
                worksheet1.Cells[row, 5].Value = cashBooks[i].CashBalance;
                i++;
            }
            package.Save();
        }

        public static string ExportPurchase(AprajitaRetailsContext db, string fileName)
        {
            // string rootFolder = _hostingEnvironment.WebRootPath;
            //string fileName = @"ExportCustomers.xlsx";

            FileInfo file = new FileInfo(fileName);

            using (ExcelPackage package = new ExcelPackage(file))
            {
                IList<ImportPurchase> purchaseList = db.ImportPurchases.ToList();

                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Purchase");
                int totalRows = purchaseList.Count;

                worksheet.Cells[1, 1].Value = "Bar-code";
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