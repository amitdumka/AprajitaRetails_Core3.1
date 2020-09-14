
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public enum FunctionName { Payment, Reciept, Expenses, CashExpenses, CashPayment, Booking, Delivery, Salary, AdvReciept, DuesReport, DailySale, ManualSale }

public enum FileType { PDF, XLS, OnScreen }
namespace AprajitaRetails.Areas.Reports.Data
{

    public class ReportExporterHelper
    {
        public void ReportExporter(AprajitaRetailsContext db, FileType outputType, FunctionName tableName, DateTime fromDate, DateTime toDate, int StoreId)
        {

            switch (tableName)
            {
                case FunctionName.Payment:

                    break;
                case FunctionName.Reciept:
                    break;
                case FunctionName.CashExpenses:
                    break;
                case FunctionName.CashPayment:
                    break;
                case FunctionName.Booking:
                    break;
                case FunctionName.Delivery:
                    break;
                case FunctionName.DailySale:
                    break;
                case FunctionName.Expenses:
                    break;
                case FunctionName.Salary:
                    break;
                case FunctionName.AdvReciept:
                    break;
                case FunctionName.DuesReport:
                    break;
                case FunctionName.ManualSale:
                    break;
                default:
                    break;
            }

        }


        private string GetPayment(AprajitaRetailsContext db, FileType outputType, DateTime fromDate, DateTime toDate, int StoreId)
        {
            var data = db.Payments.Where(c => c.StoreId == StoreId && (c.PayDate.Date >= fromDate.Date && c.PayDate.Date <= toDate.Date)).OrderBy(c => c.PayDate).ToList();
            string fileName = "Payments_" + fromDate.Date.ToString("DMMYYYY") + "_" + toDate.Date.ToString("DMMYYYY");
            switch (outputType)
            {
                case FileType.PDF:
                    break;
                case FileType.XLS:
                    fileName = ToExcel(data, fileName);
                    break;
                case FileType.OnScreen:
                    break;
                default:
                    break;
            }
            return fileName;
        }
        private string GetExpenses(AprajitaRetailsContext db, FileType outputType, DateTime fromDate, DateTime toDate, int StoreId)
        {
            var data = db.Expenses.Where(c => c.StoreId == StoreId && (c.ExpDate.Date >= fromDate.Date && c.ExpDate.Date <= toDate.Date)).OrderBy(c => c.ExpDate).ToList();
            string fileName = "Payments_" + fromDate.Date.ToString("DMMYYYY") + "_" + toDate.Date.ToString("DMMYYYY");
            switch (outputType)
            {
                case FileType.PDF:
                    break;
                case FileType.XLS:
                    fileName = ToExcel(data, fileName);
                    break;
                case FileType.OnScreen:
                    break;
                default:
                    break;
            }
            return fileName;
        }
        private string GetReciept(AprajitaRetailsContext db, FileType outputType, DateTime fromDate, DateTime toDate, int StoreId)
        {
            var data = db.Receipts.Where(c => c.StoreId == StoreId && (c.RecieptDate.Date >= fromDate.Date && c.RecieptDate.Date <= toDate.Date)).OrderBy(c => c.RecieptDate).ToList();
            string fileName = "Payments_" + fromDate.Date.ToString("DMMYYYY") + "_" + toDate.Date.ToString("DMMYYYY");
            switch (outputType)
            {
                case FileType.PDF:
                    break;
                case FileType.XLS:
                    fileName = ToExcel(data, fileName);
                    break;
                case FileType.OnScreen:
                    break;
                default:
                    break;
            }
            return fileName;
        }

        private string GetCashExpenses(AprajitaRetailsContext db, FileType outputType, DateTime fromDate, DateTime toDate, int StoreId)
        {
            var data = db.PettyCashExpenses.Include(c => c.PaidBy).Where(c => c.StoreId == StoreId && (c.ExpDate.Date >= fromDate.Date && c.ExpDate.Date <= toDate.Date)).OrderBy(c => c.ExpDate).ToList();
            string fileName = "Payments_" + fromDate.Date.ToString("DMMYYYY") + "_" + toDate.Date.ToString("DMMYYYY");
            switch (outputType)
            {
                case FileType.PDF:
                    break;
                case FileType.XLS:
                    fileName = ToExcel(data, fileName);
                    break;
                case FileType.OnScreen:
                    break;
                default:
                    break;
            }
            return fileName;
        }

        private string GetCashPayment(AprajitaRetailsContext db, FileType outputType, DateTime fromDate, DateTime toDate, int StoreId)
        {
            var data = db.CashPayments.Include(c => c.Mode).Where(c => c.StoreId == StoreId && (c.PaymentDate.Date >= fromDate.Date && c.PaymentDate.Date <= toDate.Date)).OrderBy(c => c.PaymentDate).ToList();
            string fileName = "Payments_" + fromDate.Date.ToString("DMMYYYY") + "_" + toDate.Date.ToString("DMMYYYY");
            switch (outputType)
            {
                case FileType.PDF:
                    break;
                case FileType.XLS:
                    fileName = ToExcel(data, fileName);
                    break;
                case FileType.OnScreen:
                    break;
                default:
                    break;
            }
            return fileName;
        }

        private string GetSalaryPayment(AprajitaRetailsContext db, FileType outputType, DateTime fromDate, DateTime toDate, int StoreId)
        {
            var data = db.SalaryPayments.Include(c => c.Employee).Where(c => c.StoreId == StoreId && (c.PaymentDate.Date >= fromDate.Date && c.PaymentDate.Date <= toDate.Date)).OrderBy(c => c.PaymentDate).ToList();
            string fileName = "Payments_" + fromDate.Date.ToString("DMMYYYY") + "_" + toDate.Date.ToString("DMMYYYY");
            switch (outputType)
            {
                case FileType.PDF:
                    break;
                case FileType.XLS:
                    fileName = ToExcel(data, fileName);
                    break;
                case FileType.OnScreen:
                    break;
                default:
                    break;
            }
            return fileName;
        }
        private string GetAdvanceReceipts(AprajitaRetailsContext db, FileType outputType, DateTime fromDate, DateTime toDate, int StoreId)
        {
            var data = db.StaffAdvanceReceipts.Include(c => c.Employee).Where(c => c.StoreId == StoreId && (c.ReceiptDate.Date >= fromDate.Date && c.ReceiptDate.Date <= toDate.Date)).OrderBy(c => c.ReceiptDate).ToList();
            string fileName = "Payments_" + fromDate.Date.ToString("DMMYYYY") + "_" + toDate.Date.ToString("DMMYYYY");
            switch (outputType)
            {
                case FileType.PDF:
                    break;
                case FileType.XLS:
                    fileName = ToExcel(data, fileName);
                    break;
                case FileType.OnScreen:
                    break;
                default:
                    break;
            }
            return fileName;
        }



        private void ToPdf(List<Payment> listItem, string fileName)
        {

        }



        private string ToExcel(List<Payment> payments, string fileName)
        {
            fileName += ".xls";
            FileInfo file = new FileInfo(Path.Combine("wwwroot", fileName));
            using ExcelPackage package = new ExcelPackage(file);
            ExcelWorksheet worksheet2 = package.Workbook.Worksheets.Add("Payments");

            worksheet2.Cells[1, 3].Value = "Aprajita Retails";
            worksheet2.Cells[2, 3].Value = "Bhagalpur Road, Dumka";
            worksheet2.Cells[3, 2].Value = "Date: " + DateTime.Now.Date.ToShortDateString();
            worksheet2.Cells[5, 3].Value = "Payment(s)";

            worksheet2.Cells[7, 1].Value = "Date";
            worksheet2.Cells[7, 2].Value = "PartyName";
            worksheet2.Cells[7, 3].Value = "SlipNo";
            worksheet2.Cells[7, 4].Value = "Mode";
            worksheet2.Cells[7, 5].Value = "Remarks";
            worksheet2.Cells[7, 6].Value = "Amount";

            int row = 8;
            decimal TotalAmount = 0;
            foreach (var item in payments)
            {
                worksheet2.Cells[row, 1].Value = item.PayDate;
                //worksheet2.Cells[row, 2].Value = item.PartyName;
                worksheet2.Cells[row, 3].Value = item.PaymentSlipNo;
                worksheet2.Cells[row, 4].Value = item.PayMode;
                worksheet2.Cells[row, 5].Value = item.Remarks;
                worksheet2.Cells[row, 6].Value = item.Amount;
                TotalAmount += item.Amount;
                row++;

            }
            worksheet2.Cells[row, 1].Value = "Count";
            worksheet2.Cells[row, 2].Value = payments.Count;
            worksheet2.Cells[row, 5].Value = "Total";
            worksheet2.Cells[row, 6].Value = TotalAmount;
            package.Save();
            return fileName;

        }
        private string ToExcel(List<Expense> listItem, string fileName)
        {
            fileName += ".xls";
            FileInfo file = new FileInfo(Path.Combine("wwwroot", fileName));
            using ExcelPackage package = new ExcelPackage(file);
            ExcelWorksheet worksheet2 = package.Workbook.Worksheets.Add("Payments");

            worksheet2.Cells[1, 4].Value = "Aprajita Retails";
            worksheet2.Cells[2, 4].Value = "Bhagalpur Road, Dumka";
            worksheet2.Cells[3, 3].Value = "Date: " + DateTime.Now.Date.ToShortDateString();
            worksheet2.Cells[5, 4].Value = "Expense(s)";

            worksheet2.Cells[7, 1].Value = "Date";
            worksheet2.Cells[7, 2].Value = "Particular(s)";
            worksheet2.Cells[7, 3].Value = "PartyName";
            worksheet2.Cells[7, 4].Value = "Paid By";
            worksheet2.Cells[7, 5].Value = "Mode";
            worksheet2.Cells[7, 6].Value = "Payment Details";
            worksheet2.Cells[7, 7].Value = "Remarks";
            worksheet2.Cells[7, 8].Value = "Amount";

            int row = 8;
            decimal TotalAmount = 0;
            foreach (var item in listItem)
            {
                worksheet2.Cells[row, 1].Value = item.ExpDate;
                worksheet2.Cells[row, 2].Value = item.Particulars;
                worksheet2.Cells[row, 3].Value = item.PaidTo;
                worksheet2.Cells[row, 4].Value = item.PaidBy.StaffName;
                worksheet2.Cells[row, 5].Value = item.PayMode;
                worksheet2.Cells[row, 6].Value = item.PaymentDetails;
                worksheet2.Cells[row, 7].Value = item.Remarks;
                worksheet2.Cells[row, 8].Value = item.Amount;


                TotalAmount += item.Amount;
                row++;

            }
            worksheet2.Cells[row, 1].Value = "Count";
            worksheet2.Cells[row, 2].Value = listItem.Count;
            worksheet2.Cells[row, 7].Value = "Total";
            worksheet2.Cells[row, 8].Value = TotalAmount;
            package.Save();
            return fileName;

        }
        private string ToExcel(List<Receipt> listItem, string fileName)
        {
            fileName += ".xls";
            FileInfo file = new FileInfo(Path.Combine("wwwroot", fileName));
            using ExcelPackage package = new ExcelPackage(file);
            ExcelWorksheet worksheet2 = package.Workbook.Worksheets.Add("Payments");

            worksheet2.Cells[1, 4].Value = "Aprajita Retails";
            worksheet2.Cells[2, 4].Value = "Bhagalpur Road, Dumka";
            worksheet2.Cells[3, 3].Value = "Date: " + DateTime.Now.Date.ToShortDateString();
            worksheet2.Cells[5, 4].Value = "Expense(s)";

            worksheet2.Cells[7, 1].Value = "Date";

            worksheet2.Cells[7, 2].Value = "PartyName";
            worksheet2.Cells[7, 3].Value = "SlipNo";
            worksheet2.Cells[7, 4].Value = "Mode";
            worksheet2.Cells[7, 5].Value = "Payment Details";
            worksheet2.Cells[7, 6].Value = "Remarks";
            worksheet2.Cells[7, 7].Value = "Amount";

            int row = 8;
            decimal TotalAmount = 0;
            foreach (var item in listItem)
            {
                worksheet2.Cells[row, 1].Value = item.RecieptDate;
                worksheet2.Cells[row, 2].Value = item.ReceiptFrom;
                worksheet2.Cells[row, 3].Value = item.RecieptSlipNo;

                worksheet2.Cells[row, 4].Value = item.PayMode;
                worksheet2.Cells[row, 5].Value = item.ReceiptDetails;
                worksheet2.Cells[row, 6].Value = item.Remarks;
                worksheet2.Cells[row, 7].Value = item.Amount;


                TotalAmount += item.Amount;
                row++;

            }
            worksheet2.Cells[row, 1].Value = "Count";
            worksheet2.Cells[row, 2].Value = listItem.Count;
            worksheet2.Cells[row, 6].Value = "Total";
            worksheet2.Cells[row, 7].Value = TotalAmount;
            package.Save();
            return fileName;

        }

        private string ToExcel(List<CashPayment> payments, string fileName)
        {
            fileName += ".xls";
            FileInfo file = new FileInfo(Path.Combine("wwwroot", fileName));
            using ExcelPackage package = new ExcelPackage(file);
            ExcelWorksheet worksheet2 = package.Workbook.Worksheets.Add("Payments");

            worksheet2.Cells[1, 3].Value = "Aprajita Retails";
            worksheet2.Cells[2, 3].Value = "Bhagalpur Road, Dumka";
            worksheet2.Cells[3, 2].Value = "Date: " + DateTime.Now.Date.ToShortDateString();
            worksheet2.Cells[5, 3].Value = "Cash Payment(s)";

            worksheet2.Cells[7, 1].Value = "Date";
            worksheet2.Cells[7, 2].Value = "PartyName";
            worksheet2.Cells[7, 3].Value = "SlipNo";
            worksheet2.Cells[7, 4].Value = "Mode";
            //worksheet2.Cells[7, 5].Value = "Remarks";
            worksheet2.Cells[7, 5].Value = "Amount";

            int row = 8;
            decimal TotalAmount = 0;
            foreach (var item in payments)
            {
                worksheet2.Cells[row, 1].Value = item.PaymentDate;
                worksheet2.Cells[row, 2].Value = item.PaidTo;
                worksheet2.Cells[row, 3].Value = item.SlipNo;
                worksheet2.Cells[row, 4].Value = item.Mode.Transcation;
                //worksheet2.Cells[row, 5].Value = item.Remark;
                worksheet2.Cells[row, 5].Value = item.Amount;
                TotalAmount += item.Amount;
                row++;

            }
            worksheet2.Cells[row, 1].Value = "Count";
            worksheet2.Cells[row, 2].Value = payments.Count;
            worksheet2.Cells[row, 4].Value = "Total";
            worksheet2.Cells[row, 5].Value = TotalAmount;
            package.Save();
            return fileName;

        }
        private string ToExcel(List<PettyCashExpense> listItem, string fileName)
        {
            fileName += ".xls";
            FileInfo file = new FileInfo(Path.Combine("wwwroot", fileName));
            using ExcelPackage package = new ExcelPackage(file);
            ExcelWorksheet worksheet2 = package.Workbook.Worksheets.Add("Payments");

            worksheet2.Cells[1, 4].Value = "Aprajita Retails";
            worksheet2.Cells[2, 4].Value = "Bhagalpur Road, Dumka";
            worksheet2.Cells[3, 3].Value = "Date: " + DateTime.Now.Date.ToShortDateString();
            worksheet2.Cells[5, 4].Value = "Cash Expense(s)";

            worksheet2.Cells[7, 1].Value = "Date";
            worksheet2.Cells[7, 2].Value = "Particular(s)";
            worksheet2.Cells[7, 3].Value = "PartyName";
            worksheet2.Cells[7, 4].Value = "Paid By";
            worksheet2.Cells[7, 5].Value = "Paid To";
            //worksheet2.Cells[7, 6].Value = "Payment Details";
            worksheet2.Cells[7, 6].Value = "Remarks";
            worksheet2.Cells[7, 7].Value = "Amount";

            int row = 8;
            decimal TotalAmount = 0;
            foreach (var item in listItem)
            {
                worksheet2.Cells[row, 1].Value = item.ExpDate;
                worksheet2.Cells[row, 2].Value = item.Particulars;
                worksheet2.Cells[row, 3].Value = item.PaidTo;
                worksheet2.Cells[row, 4].Value = item.PaidBy.StaffName;
                worksheet2.Cells[row, 5].Value = item.PaidTo;
                worksheet2.Cells[row, 6].Value = item.Remarks;
                worksheet2.Cells[row, 7].Value = item.Amount;
                //worksheet2.Cells[row, 8].Value = item.Amount;


                TotalAmount += item.Amount;
                row++;

            }
            worksheet2.Cells[row, 1].Value = "Count";
            worksheet2.Cells[row, 2].Value = listItem.Count;
            worksheet2.Cells[row, 6].Value = "Total";
            worksheet2.Cells[row, 7].Value = TotalAmount;
            package.Save();
            return fileName;

        }

        private string ToExcel(List<SalaryPayment> listItem, string fileName)
        {
            fileName += ".xls";
            FileInfo file = new FileInfo(Path.Combine("wwwroot", fileName));
            using ExcelPackage package = new ExcelPackage(file);
            ExcelWorksheet worksheet2 = package.Workbook.Worksheets.Add("Payments");

            worksheet2.Cells[1, 4].Value = "Aprajita Retails";
            worksheet2.Cells[2, 4].Value = "Bhagalpur Road, Dumka";
            worksheet2.Cells[3, 3].Value = "Date: " + DateTime.Now.Date.ToShortDateString();
            worksheet2.Cells[5, 4].Value = "Salary Payment";

            worksheet2.Cells[7, 1].Value = "Date";
            worksheet2.Cells[7, 2].Value = "Staff Name";
            worksheet2.Cells[7, 3].Value = "Salary Type";
            worksheet2.Cells[7, 4].Value = "Details";
            worksheet2.Cells[7, 5].Value = "Month";
            //worksheet2.Cells[7, 6].Value = "Payment Details";
            worksheet2.Cells[7, 6].Value = "Mode";
            worksheet2.Cells[7, 7].Value = "Amount";

            int row = 8;
            decimal TotalAmount = 0;
            foreach (var item in listItem)
            {
                worksheet2.Cells[row, 1].Value = item.PaymentDate;
                worksheet2.Cells[row, 2].Value = item.Employee.StaffName;
                worksheet2.Cells[row, 3].Value = item.SalaryComponet;
                worksheet2.Cells[row, 4].Value = item.Details;
                worksheet2.Cells[row, 5].Value = item.SalaryMonth;
                worksheet2.Cells[row, 6].Value = item.PayMode;
                worksheet2.Cells[row, 7].Value = item.Amount;
                


                TotalAmount += item.Amount;
                row++;

            }
            worksheet2.Cells[row, 1].Value = "Count";
            worksheet2.Cells[row, 2].Value = listItem.Count;
            worksheet2.Cells[row, 6].Value = "Total";
            worksheet2.Cells[row, 7].Value = TotalAmount;
            package.Save();
            return fileName;

        }

        private string ToExcel(List<StaffAdvanceReceipt> listItem, string fileName)
        {
            fileName += ".xls";
            FileInfo file = new FileInfo(Path.Combine("wwwroot", fileName));
            using ExcelPackage package = new ExcelPackage(file);
            ExcelWorksheet worksheet2 = package.Workbook.Worksheets.Add("Payments");

            worksheet2.Cells[1, 4].Value = "Aprajita Retails";
            worksheet2.Cells[2, 4].Value = "Bhagalpur Road, Dumka";
            worksheet2.Cells[3, 3].Value = "Date: " + DateTime.Now.Date.ToShortDateString();
            worksheet2.Cells[5, 4].Value = "Cash Expense(s)";

            worksheet2.Cells[7, 1].Value = "Date";
            worksheet2.Cells[7, 2].Value = "StaffName";
            worksheet2.Cells[7, 3].Value = "Details";
            worksheet2.Cells[7, 4].Value = "Mode";
            worksheet2.Cells[7, 5].Value = "Amount";
            //worksheet2.Cells[7, 6].Value = "Payment Details";
            //worksheet2.Cells[7, 6].Value = "Remarks";
            //worksheet2.Cells[7, 7].Value = "Amount";

            int row = 8;
            decimal TotalAmount = 0;
            foreach (var item in listItem)
            {
                worksheet2.Cells[row, 1].Value = item.ReceiptDate;
                worksheet2.Cells[row, 2].Value = item.Employee.StaffName;
                worksheet2.Cells[row, 3].Value = item.Details;
                worksheet2.Cells[row, 4].Value = item.PayMode;
                //worksheet2.Cells[row, 5].Value = item.;
                //worksheet2.Cells[row, 6].Value = item.Remarks;
                worksheet2.Cells[row, 5].Value = item.Amount;
                //worksheet2.Cells[row, 8].Value = item.Amount;


                TotalAmount += item.Amount;
                row++;

            }
            worksheet2.Cells[row, 1].Value = "Count";
            worksheet2.Cells[row, 2].Value = listItem.Count;
            worksheet2.Cells[row, 4].Value = "Total";
            worksheet2.Cells[row, 5].Value = TotalAmount;
            package.Save();
            return fileName;

        }
    }
}
