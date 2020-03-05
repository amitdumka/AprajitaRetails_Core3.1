using AprajitaRetails.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using AprajitaRetails.Models.Helpers;

namespace AprajitaRetails.Data.Reports
{
    public class IEReport
    {
        public IncomeExpensesReport GetWeeklyReport(AprajitaRetailsContext db)
        {

            var start = DateTime.Now.StartOfWeek().Date;
            var end = DateTime.Now.EndOfWeek().Date;
            IncomeExpensesReport ierData = new IncomeExpensesReport
            {
                OnDate = DateTime.Today,
                IncomeExpensesReportId = 1,

                //Income
                TotalSale = db.DailySales.Where(c => c.SaleDate.Date >= start.Date && c.SaleDate.Date <= end.Date && !c.IsManualBill && !c.IsTailoringBill).Sum(c => c.Amount),
                TotalManualSale = db.DailySales.Where(c => c.SaleDate.Date >= start.Date && c.SaleDate.Date <= end.Date && c.IsManualBill && !c.IsTailoringBill).Sum(c => c.Amount),
                TotalTailoringSale = db.DailySales.Where(c => c.SaleDate.Date >= start.Date && c.SaleDate.Date <= end.Date && c.IsTailoringBill).Sum(c => c.Amount),
                TotalCashRecipts = db.CashReceipts.Where(c => c.InwardDate.Date >= start.Date && c.InwardDate.Date <= end.Date).Sum(c => c.Amount),
                TotalRecipts = db.Receipts.Where(c => c.RecieptDate.Date >= start.Date && c.RecieptDate.Date <= end.Date).Sum(c => c.Amount),
                TotalOtherIncome = 0,

                //Expenses

                TotalExpenses = db.Expenses.Where(c => c.ExpDate.Date >= start.Date && c.ExpDate.Date <= end.Date).Sum(c => c.Amount) + db.PettyCashExpenses.Where(c => c.ExpDate.Date >= start.Date && c.ExpDate.Date <= end.Date).Sum(c => c.Amount),
                TotalOthersExpenses = 0,
                TotalPayments = db.Payments.Where(c => c.PayDate.Date >= start.Date && c.PayDate.Date <= end.Date).Sum(c => c.Amount),
                TotalStaffPayments = db.SalaryPayments.Where(c => c.PaymentDate.Date >= start.Date && c.PaymentDate.Date <= end.Date).Sum(c => c.Amount) +
                                    db.StaffAdvancePayments.Where(c => c.PaymentDate.Date >= start.Date && c.PaymentDate.Date <= end.Date).Sum(c => c.Amount),
                TotalTailoringPayments = db.TailoringSalaryPayments.Where(c => c.PaymentDate.Date >= start.Date && c.PaymentDate.Date <= end.Date).Sum(c => c.Amount) +
                                        db.TailoringStaffAdvancePayments.Where(c => c.PaymentDate.Date >= start.Date && c.PaymentDate.Date <= end.Date).Sum(c => c.Amount),

                TotalCashPayments = db.CashPayments.Where(c => c.PaymentDate.Date >= start.Date && c.PaymentDate.Date <= end.Date && c.Mode.Transcation != "Home Expenses").Sum(c => c.Amount),
                TotalHomeExpenses = db.CashPayments.Where(c => c.PaymentDate.Date >= start.Date && c.PaymentDate.Date <= end.Date && c.Mode.Transcation == "Home Expenses").Sum(c => c.Amount),

                //Dues

                TotalDues = db.DuesLists.Include(c => c.DailySale).Where(c => c.DailySale.SaleDate.Date >= start.Date && c.DailySale.SaleDate.Date <= end.Date).Sum(c => c.Amount),
                TotalRecovery = db.DueRecoverds.Where(c => c.PaidDate.Date >= start.Date && c.PaidDate.Date <= end.Date).Sum(c => c.AmountPaid)

            };
            ierData.TotalPendingDues = ierData.TotalDues - ierData.TotalRecovery;
            return ierData;
        }
        public IncomeExpensesReport GetMonthlyReport(AprajitaRetailsContext db, DateTime onDate)
        {
            IncomeExpensesReport ierData = new IncomeExpensesReport
            {
                OnDate = onDate,
                IncomeExpensesReportId = 1,
                //Income
                TotalSale = db.DailySales.Where(c => c.SaleDate.Month == onDate.Month && !c.IsManualBill && !c.IsTailoringBill).Sum(c => c.Amount),
                TotalManualSale = db.DailySales.Where(c => c.SaleDate.Month == onDate.Month && c.IsManualBill && !c.IsTailoringBill).Sum(c => c.Amount),
                TotalTailoringSale = db.DailySales.Where(c => c.SaleDate.Month == onDate.Month && c.IsTailoringBill).Sum(c => c.Amount),
                TotalCashRecipts = db.CashReceipts.Where(c => c.InwardDate.Month == onDate.Month).Sum(c => c.Amount),
                TotalRecipts = db.Receipts.Where(c => c.RecieptDate.Month == onDate.Month).Sum(c => c.Amount),
                TotalOtherIncome = 0,
                //Expenses
                TotalExpenses = db.Expenses.Where(c => c.ExpDate.Month == onDate.Month).Sum(c => c.Amount) + db.PettyCashExpenses.Where(c => c.ExpDate.Month == onDate.Month).Sum(c => c.Amount),
                TotalOthersExpenses = 0,
                TotalPayments = db.Payments.Where(c => c.PayDate.Month == onDate.Month).Sum(c => c.Amount),
                TotalStaffPayments = db.SalaryPayments.Where(c => c.PaymentDate.Month == onDate.Month).Sum(c => c.Amount) + db.StaffAdvancePayments.Where(c => c.PaymentDate.Month == onDate.Month).Sum(c => c.Amount),
                TotalTailoringPayments = db.TailoringSalaryPayments.Where(c => c.PaymentDate.Month == onDate.Month).Sum(c => c.Amount) + db.TailoringStaffAdvancePayments.Where(c => c.PaymentDate.Month == onDate.Month).Sum(c => c.Amount),
                TotalCashPayments = db.CashPayments.Where(c => c.PaymentDate.Month == onDate.Month && c.Mode.Transcation != "Home Expenses").Sum(c => c.Amount),
                TotalHomeExpenses = db.CashPayments.Where(c => c.PaymentDate.Month == onDate.Month && c.Mode.Transcation == "Home Expenses").Sum(c => c.Amount),
                //Dues
                TotalDues = db.DuesLists.Include(c => c.DailySale).Where(c => c.DailySale.SaleDate.Month == onDate.Month).Sum(c => c.Amount),
                TotalRecovery = db.DueRecoverds.Where(c => c.PaidDate.Month == onDate.Month).Sum(c => c.AmountPaid)

            };
            ierData.TotalPendingDues = ierData.TotalDues - ierData.TotalRecovery;
            return ierData;
        }
        public IncomeExpensesReport GetYearlyReport(AprajitaRetailsContext db, DateTime onDate)
        {
            IncomeExpensesReport ierData = new IncomeExpensesReport
            {
                OnDate = onDate,
                IncomeExpensesReportId = 1,
                //Income
                TotalSale = db.DailySales.Where(c => c.SaleDate.Year == onDate.Year && !c.IsManualBill && !c.IsTailoringBill).Sum(c => c.Amount),
                TotalManualSale = db.DailySales.Where(c => c.SaleDate.Year == onDate.Year && c.IsManualBill && !c.IsTailoringBill).Sum(c => c.Amount),
                TotalTailoringSale = db.DailySales.Where(c => c.SaleDate.Year == onDate.Year && c.IsTailoringBill).Sum(c => c.Amount),
                TotalCashRecipts = db.CashReceipts.Where(c => c.InwardDate.Year == onDate.Year).Sum(c => c.Amount),
                TotalRecipts = db.Receipts.Where(c => c.RecieptDate.Year == onDate.Year).Sum(c => c.Amount),
                TotalOtherIncome = 0,
                //Expenses
                TotalExpenses = db.Expenses.Where(c => c.ExpDate.Year == onDate.Year).Sum(c => c.Amount) + db.PettyCashExpenses.Where(c => c.ExpDate.Year == onDate.Year).Sum(c => c.Amount),
                TotalOthersExpenses = 0,
                TotalPayments = db.Payments.Where(c => c.PayDate.Year == onDate.Year).Sum(c => c.Amount),
                TotalStaffPayments = db.SalaryPayments.Where(c => c.PaymentDate.Year == onDate.Year).Sum(c => c.Amount) + db.StaffAdvancePayments.Where(c => c.PaymentDate.Year == onDate.Year).Sum(c => c.Amount),
                TotalTailoringPayments = db.TailoringSalaryPayments.Where(c => c.PaymentDate.Year == onDate.Year).Sum(c => c.Amount) + db.TailoringStaffAdvancePayments.Where(c => c.PaymentDate.Year == onDate.Year).Sum(c => c.Amount),
                TotalCashPayments = db.CashPayments.Where(c => c.PaymentDate.Year == onDate.Year && c.Mode.Transcation != "Home Expenses").Sum(c => c.Amount),
                TotalHomeExpenses = db.CashPayments.Where(c => c.PaymentDate.Year == onDate.Year && c.Mode.Transcation == "Home Expenses").Sum(c => c.Amount),
                //Dues
                TotalDues = db.DuesLists.Include(c => c.DailySale).Where(c => c.DailySale.SaleDate.Year == onDate.Year).Sum(c => c.Amount),
                TotalRecovery = db.DueRecoverds.Where(c => c.PaidDate.Year == onDate.Year).Sum(c => c.AmountPaid)
            };
            ierData.TotalPendingDues = ierData.TotalDues - ierData.TotalRecovery;
            return ierData;
        }
        public IncomeExpensesReport GetDailyReport(AprajitaRetailsContext db, DateTime onDate)
        {
            IncomeExpensesReport ierData = new IncomeExpensesReport
            {
                OnDate = onDate,
                IncomeExpensesReportId = 1,
                //Income
                TotalSale = db.DailySales.Where(c => c.SaleDate.Date == onDate.Date && !c.IsManualBill && !c.IsTailoringBill).Sum(c => c.Amount),
                TotalManualSale = db.DailySales.Where(c => c.SaleDate.Date == onDate.Date && c.IsManualBill && !c.IsTailoringBill).Sum(c => c.Amount),
                TotalTailoringSale = db.DailySales.Where(c => c.SaleDate.Date == onDate.Date && c.IsTailoringBill).Sum(c => c.Amount),
                TotalCashRecipts = db.CashReceipts.Where(c => c.InwardDate.Date == onDate.Date).Sum(c => c.Amount),
                TotalRecipts = db.Receipts.Where(c => c.RecieptDate.Date == onDate.Date).Sum(c => c.Amount),
                TotalOtherIncome = 0,
                //Expenses
                TotalExpenses = db.Expenses.Where(c => c.ExpDate.Date == onDate.Date).Sum(c => c.Amount) + db.PettyCashExpenses.Where(c => c.ExpDate.Date == onDate.Date).Sum(c => c.Amount),
                TotalOthersExpenses = 0,
                TotalPayments = db.Payments.Where(c => c.PayDate.Date == onDate.Date).Sum(c => c.Amount),
                TotalStaffPayments = db.SalaryPayments.Where(c => c.PaymentDate.Date == onDate.Date).Sum(c => c.Amount) + db.StaffAdvancePayments.Where(c => c.PaymentDate.Date == onDate.Date).Sum(c => c.Amount),
                TotalTailoringPayments = db.TailoringSalaryPayments.Where(c => c.PaymentDate.Date == onDate.Date).Sum(c => c.Amount) + db.TailoringStaffAdvancePayments.Where(c => c.PaymentDate.Date == onDate.Date).Sum(c => c.Amount),
                TotalCashPayments = db.CashPayments.Where(c => c.PaymentDate.Date == onDate.Date && c.Mode.Transcation != "Home Expenses").Sum(c => c.Amount),
                TotalHomeExpenses = db.CashPayments.Where(c => c.PaymentDate.Date == onDate.Date && c.Mode.Transcation == "Home Expenses").Sum(c => c.Amount),
                //Dues
                TotalDues = db.DuesLists.Include(c => c.DailySale).Where(c => c.DailySale.SaleDate.Date == onDate.Date).Sum(c => c.Amount),
                TotalRecovery = db.DueRecoverds.Where(c => c.PaidDate.Date == onDate.Date).Sum(c => c.AmountPaid)
            };
            ierData.TotalPendingDues = ierData.TotalDues - ierData.TotalRecovery;
            return ierData;

        }
    }
}
