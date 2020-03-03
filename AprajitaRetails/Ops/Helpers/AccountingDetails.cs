using AprajitaRetails.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using AprajitaRetails.Models.ViewModels;
//using TAS_AprajiataRetails.Models.Data;

namespace AprajitaRetails.Models.Helpers
{
    public class AccountingDetails
    {
        public IncomeExpensesReport CalculateIncomeExpenes(AprajitaRetailsContext db, DateTime onDate, bool IsMonth, bool IsYear)
        {
            IncomeExpensesReport ierData = null;
            if ( IsMonth )
            {
                ierData = new IncomeExpensesReport
                {
                    OnDate = onDate,
                    //Income 
                    TotalManualSale = db.DailySales.Where (c => c.IsManualBill && ( c.SaleDate ).Month == ( onDate ).Month).Sum (c => c.Amount),
                    TotalSale = db.DailySales.Where (c => c.SaleDate.Month == ( onDate ).Month && !c.IsManualBill && !c.IsTailoringBill).Sum (c => c.Amount),
                    TotalTailoringSale = db.DailySales.Where (c => c.IsTailoringBill && ( c.SaleDate ).Month == ( onDate ).Month).Sum (c => c.Amount),
                    TotalOtherIncome = 0,


                    //Expenses
                    TotalExpenses = db.Expenses.Where (c => ( c.ExpDate ).Month == ( onDate ).Month).Sum (c => c.Amount),

                    TotalHomeExpenses = db.CashPayments.Where (c => ( c.PaymentDate ).Month == ( onDate ).Month
                    && ( c.Mode.Transcation == "Home Expenses" || c.Mode.Transcation == "Amit Kumar	" || c.Mode.Transcation == "	Mukesh(Home Staff" )).Sum (c => c.Amount),

                    TotalOthersExpenses = db.CashPayments.Where (c => ( c.PaymentDate ).Month == ( onDate ).Month &&
                     ( c.Mode.Transcation != "Home Expenses" || c.Mode.Transcation != "Amit Kumar	" || c.Mode.Transcation != "	Mukesh(Home Staff" )).Sum (c => c.Amount),

                    TotalCashPayments = db.PettyCashExpenses.Where (c => ( c.ExpDate ).Month == ( onDate ).Month).Sum (c => c.Amount),

                    //Payments 
                    TotalPayments = db.Payments.Where (c => ( c.PayDate ).Month == ( onDate ).Month).Sum (c => c.Amount),

                    //Staff/Tailoring
                    TotalStaffPayments = db.SalaryPayments.Where (c => ( c.PaymentDate ).Month == ( onDate ).Month).Sum (c => c.Amount) + db.StaffAdvancePayments.Where (c => ( c.PaymentDate ).Month == ( onDate ).Month).Sum (c => c.Amount),
                    TotalTailoringPayments = db.TailoringSalaryPayments.Where (c => ( c.PaymentDate ).Month == ( onDate ).Month).Sum (c => c.Amount) + db.TailoringStaffAdvancePayments.Where (c => ( c.PaymentDate ).Month == ( onDate ).Month).Sum (c => c.Amount),



                    //Reciepts
                    TotalRecipts = db.Receipts.Where (c => ( c.RecieptDate ).Month == ( onDate ).Month).Sum (c => c.Amount) +
              db.StaffAdvanceReceipts.Where (c => ( c.ReceiptDate ).Month == ( onDate ).Month).Sum (c => c.Amount) +
              db.TailoringStaffAdvanceReceipts.Where (c => ( c.ReceiptDate ).Month == ( onDate ).Month).Sum (c => c.Amount),
                    TotalCashRecipts = db.CashReceipts.Where (c => ( c.InwardDate ).Month == ( onDate ).Month).Sum (c => c.Amount),

                    TotalRecovery = db.DuesLists.Where (c => c.RecoveryDate.Value.Month == onDate.Month).Sum (c => c.Amount),

                    //Dues
                    TotalDues = db.DuesLists.Include (c => c.DailySale).Where (c => c.IsRecovered == false && c.DailySale.SaleDate.Month == onDate.Month).Sum (c => c.Amount),
                    TotalPendingDues = db.DuesLists.Where (c => !c.IsRecovered).Sum (c => c.Amount),

                };
            }
            else if ( IsYear )
            {
                ierData = new IncomeExpensesReport
                {
                    OnDate = onDate,
                    //Income 
                    TotalManualSale = db.DailySales.Where (c => c.IsManualBill && ( c.SaleDate ).Year == ( onDate ).Year).Sum (c => c.Amount),
                    TotalSale = db.DailySales.Where (c => c.SaleDate.Year == ( onDate ).Year && !c.IsManualBill && !c.IsTailoringBill).Sum (c => c.Amount),
                    TotalTailoringSale = db.DailySales.Where (c => c.IsTailoringBill && ( c.SaleDate ).Year == ( onDate ).Year).Sum (c => c.Amount),
                    TotalOtherIncome = 0,


                    //Expenses
                    TotalExpenses = db.Expenses.Where (c => ( c.ExpDate ).Year == ( onDate ).Year).Sum (c => c.Amount),

                    TotalHomeExpenses = db.CashPayments.Where (c => ( c.PaymentDate ).Year == ( onDate ).Year
                    && ( c.Mode.Transcation == "Home Expenses" || c.Mode.Transcation == "Amit Kumar	" || c.Mode.Transcation == "	Mukesh(Home Staff" )).Sum (c => c.Amount),

                    TotalOthersExpenses = db.CashPayments.Where (c => ( c.PaymentDate ).Year == ( onDate ).Year &&
                     ( c.Mode.Transcation != "Home Expenses" || c.Mode.Transcation != "Amit Kumar	" || c.Mode.Transcation != "	Mukesh(Home Staff" )).Sum (c => c.Amount),

                    TotalCashPayments = db.PettyCashExpenses.Where (c => ( c.ExpDate ).Year == ( onDate ).Year).Sum (c => c.Amount),

                    //Payments 
                    TotalPayments = db.Payments.Where (c => ( c.PayDate ).Year == ( onDate ).Year).Sum (c => c.Amount),

                    //Staff/Tailoring
                    TotalStaffPayments = db.SalaryPayments.Where (c => ( c.PaymentDate ).Year == ( onDate ).Year).Sum (c => c.Amount) + db.StaffAdvancePayments.Where (c => ( c.PaymentDate ).Year == ( onDate ).Year).Sum (c => c.Amount),
                    TotalTailoringPayments = db.TailoringSalaryPayments.Where (c => ( c.PaymentDate ).Year == ( onDate ).Year).Sum (c => c.Amount) + db.TailoringStaffAdvancePayments.Where (c => ( c.PaymentDate ).Year == ( onDate ).Year).Sum (c => c.Amount),



                    //Reciepts
                    TotalRecipts = db.Receipts.Where (c => ( c.RecieptDate ).Year == ( onDate ).Year).Sum (c => c.Amount) +
              db.StaffAdvanceReceipts.Where (c => ( c.ReceiptDate ).Year == ( onDate ).Year).Sum (c => c.Amount) +
              db.TailoringStaffAdvanceReceipts.Where (c => ( c.ReceiptDate ).Year == ( onDate ).Year).Sum (c => c.Amount),
                    TotalCashRecipts = db.CashReceipts.Where (c => ( c.InwardDate ).Year == ( onDate ).Year).Sum (c => c.Amount),

                    TotalRecovery = db.DuesLists.Where (c => c.RecoveryDate.Value.Year == onDate.Year).Sum (c => c.Amount),

                    //Dues
                    TotalDues = db.DuesLists.Include (c => c.DailySale).Where (c => c.IsRecovered == false && c.DailySale.SaleDate.Year == onDate.Year).Sum (c => c.Amount),
                    TotalPendingDues = db.DuesLists.Where (c => !c.IsRecovered).Sum (c => c.Amount),

                };
            }
            else
            {
                ierData = new IncomeExpensesReport
                {
                    OnDate = onDate,
                    //Income 
                    TotalManualSale = db.DailySales.Where (c => c.IsManualBill && ( c.SaleDate ).Date == ( onDate ).Date).Sum (c => c.Amount),
                    TotalSale = db.DailySales.Where (c => c.SaleDate.Date == ( onDate ).Date && !c.IsManualBill && !c.IsTailoringBill).Sum (c => c.Amount),
                    TotalTailoringSale = db.DailySales.Where (c => c.IsTailoringBill && ( c.SaleDate ).Date == ( onDate ).Date).Sum (c => c.Amount),
                    TotalOtherIncome = 0,


                    //Expenses
                    TotalExpenses = db.Expenses.Where (c => ( c.ExpDate ).Date == ( onDate ).Date).Sum (c => c.Amount),

                    TotalHomeExpenses = db.CashPayments.Where (c => ( c.PaymentDate ).Date == ( onDate ).Date
                    && ( c.Mode.Transcation == "Home Expenses" || c.Mode.Transcation == "Amit Kumar	" || c.Mode.Transcation == "	Mukesh(Home Staff" )).Sum (c => c.Amount),

                    TotalOthersExpenses = db.CashPayments.Where (c => ( c.PaymentDate ).Date == ( onDate ).Date &&
                     ( c.Mode.Transcation != "Home Expenses" || c.Mode.Transcation != "Amit Kumar	" || c.Mode.Transcation != "	Mukesh(Home Staff" )).Sum (c => c.Amount),

                    TotalCashPayments = db.PettyCashExpenses.Where (c => ( c.ExpDate ).Date == ( onDate ).Date).Sum (c => c.Amount),

                    //Payments 
                    TotalPayments = db.Payments.Where (c => ( c.PayDate ).Date == ( onDate ).Date).Sum (c => c.Amount),

                    //Staff/Tailoring
                    TotalStaffPayments = db.SalaryPayments.Where (c => ( c.PaymentDate ).Date == ( onDate ).Date).Sum (c => c.Amount) + db.StaffAdvancePayments.Where (c => ( c.PaymentDate ).Date == ( onDate ).Date).Sum (c => c.Amount),
                    TotalTailoringPayments = db.TailoringSalaryPayments.Where (c => ( c.PaymentDate ).Date == ( onDate ).Date).Sum (c => c.Amount) + db.TailoringStaffAdvancePayments.Where (c => ( c.PaymentDate ).Date == ( onDate ).Date).Sum (c => c.Amount),



                    //Reciepts
                    TotalRecipts = db.Receipts.Where (c => ( c.RecieptDate ).Date == ( onDate ).Date).Sum (c => c.Amount) +
              db.StaffAdvanceReceipts.Where (c => ( c.ReceiptDate ).Date == ( onDate ).Date).Sum (c => c.Amount) +
              db.TailoringStaffAdvanceReceipts.Where (c => ( c.ReceiptDate ).Date == ( onDate ).Date).Sum (c => c.Amount),
                    TotalCashRecipts = db.CashReceipts.Where (c => ( c.InwardDate ).Date == ( onDate ).Date).Sum (c => c.Amount),

                    TotalRecovery = db.DuesLists.Where (c => c.RecoveryDate.Value.Date == onDate.Date).Sum (c => c.Amount),

                    //Dues
                    TotalDues = db.DuesLists.Include (c => c.DailySale).Where (c => c.IsRecovered == false && c.DailySale.SaleDate.Date == onDate.Date).Sum (c => c.Amount),
                    TotalPendingDues = db.DuesLists.Where (c => !c.IsRecovered).Sum (c => c.Amount),

                };
            }
            return ierData;

        }

        public void CalculateIncomeDetails(AprajitaRetailsContext db, DateTime onDate)
        {
            List<IncomeExpensesVM> IncomeDetails = new List<IncomeExpensesVM> ();

            var sales = db.DailySales.Where (c => c.SaleDate.Date == onDate.Date);
            var cashRec = db.CashReceipts.Where (c => c.InwardDate.Date == onDate.Date);
            var tailor = db.TailoringStaffAdvanceReceipts.Where (c => c.ReceiptDate.Date == onDate.Date);
            var staff = db.StaffAdvanceReceipts.Where (c => c.ReceiptDate.Date == onDate.Date);
            var rec = db.Receipts.Where (c => c.RecieptDate.Date == onDate.Date);
            var recover = db.DueRecoverds.Where (c => c.PaidDate.Date == onDate.Date);


        }
        public void CalculateExpenseDetails(AprajitaRetailsContext db, DateTime onDate)
        {
            List<IncomeExpensesVM> ExpensesDetails = new List<IncomeExpensesVM> ();

            var exp = db.Expenses.Where (c => c.ExpDate.Date == onDate.Date);
            var pettycashexp = db.PettyCashExpenses.Where (c => c.ExpDate.Date == onDate.Date);
            var cashpay = db.CashPayments.Where (c => c.PaymentDate.Date == onDate.Date);
            var tailor = db.TailoringSalaryPayments.Where (c => c.PaymentDate.Date == onDate.Date);
            var staff = db.SalaryPayments.Where (c => c.PaymentDate.Date == onDate.Date);
            var TotalDues = db.DuesLists.Include (c => c.DailySale).Where (c => !c.IsRecovered && c.DailySale.SaleDate.Date == onDate.Date).Sum (c => c.Amount);
            var paym = db.Payments.Where (c => c.PayDate.Date == onDate.Date);

        }

        public List<IncomeExpensesVM> CalculateIncomeDetails(AprajitaRetailsContext db, DateTime onDate, bool Weekly)
        {
            List<IncomeExpensesVM> IncomeDetails = new List<IncomeExpensesVM> ();

            DateTime startDate = onDate.StartOfWeek ();
            DateTime endDate = onDate.EndOfWeek ();

            var sales = db.DailySales.Where (c => c.SaleDate.Date >= startDate.Date && c.SaleDate.Date <= endDate.Date).ToList ();

            foreach ( var item in sales )
            {
                IncomeExpensesVM vmdata = new IncomeExpensesVM
                {
                    Amount = item.Amount,
                    OnDate = item.SaleDate,
                    Particulars = item.InvNo,
                    IsNonCash = (item.PayMode == PayModes.Cash ? false : true)
                };
                IncomeDetails.Add (vmdata);

            }
            var cashRec = db.CashReceipts.Where (c => c.InwardDate.Date >= startDate.Date && c.InwardDate.Date <= endDate.Date).ToList ();
            foreach ( var item in cashRec )
            {
                IncomeExpensesVM vmdata = new IncomeExpensesVM
                {
                    Amount = item.Amount,
                    OnDate = item.InwardDate,
                    Particulars = $"Slip No:{item.SlipNo}\t From: {item.ReceiptFrom}",
                    IsNonCash = false
                };
                IncomeDetails.Add (vmdata);

            }

            var tailor = db.TailoringStaffAdvanceReceipts.Include(c=>c.Employee).Where (c => c.ReceiptDate.Date >= startDate.Date && c.ReceiptDate.Date <= endDate.Date).ToList ();
            foreach ( var item in tailor )
            {
                IncomeExpensesVM vmdata = new IncomeExpensesVM
                {
                    Amount = item.Amount,
                    OnDate = item.ReceiptDate,
                    Particulars = item.Employee.StaffName,
                    IsNonCash = ( item.PayMode == PayModes.Cash ? false : true )
                };
                IncomeDetails.Add (vmdata);

            }

            var staff = db.StaffAdvanceReceipts.Include (c => c.Employee).Where (c => c.ReceiptDate.Date >= startDate.Date && c.ReceiptDate.Date <= endDate.Date).ToList ();
            foreach ( var item in staff )
            {
                IncomeExpensesVM vmdata = new IncomeExpensesVM
                {
                    Amount = item.Amount,
                    OnDate = item.ReceiptDate,
                    Particulars = item.Employee.StaffName,
                    IsNonCash = ( item.PayMode == PayModes.Cash ? false : true )
                };
                IncomeDetails.Add (vmdata);

            }
            var rec = db.Receipts.Where (c => c.RecieptDate.Date >= startDate.Date && c.RecieptDate.Date <= endDate.Date).ToList ();
            foreach ( var item in rec )
            {
                IncomeExpensesVM vmdata = new IncomeExpensesVM
                {
                    Amount = item.Amount,
                    OnDate = item.RecieptDate,
                    Particulars = $"Slip No:{item.RecieptSlipNo}\t From: {item.ReceiptFrom}",
                    IsNonCash = ( item.PayMode == PaymentModes.Cash ? false : true )
                };
                IncomeDetails.Add (vmdata);

            }

            var recover = db.DueRecoverds.Include (c => c.DuesList).Include(c=>c.DuesList.DailySale).Where (c => c.PaidDate.Date >= startDate.Date && c.PaidDate.Date <= endDate.Date).ToList ();
            foreach ( var item in recover )
            {
                IncomeExpensesVM vmdata = new IncomeExpensesVM
                {
                    Amount = item.AmountPaid,
                    OnDate = item.PaidDate,
                    Particulars = "Dues Recovered :"+item.DuesList.DailySale.InvNo,
                    IsNonCash = ( item.Modes == PaymentModes.Cash ? false : true )
                };
                IncomeDetails.Add (vmdata);

            }

            return IncomeDetails;
        }
        public void CalculateExpenseDetails(AprajitaRetailsContext db, DateTime onDate, bool Weekly)
        {
            List<IncomeExpensesVM> ExpensesDetails = new List<IncomeExpensesVM> ();
            DateTime startDate = onDate.StartOfWeek ();
            DateTime endDate = onDate.EndOfWeek ();

            var exp = db.Expenses.Where (c => c.ExpDate.Date >= startDate.Date && c.ExpDate.Date <= endDate.Date).ToList ();
            var pettycashexp = db.PettyCashExpenses.Where (c => c.ExpDate.Date >= startDate.Date && c.ExpDate.Date <= endDate.Date).ToList ();
            var cashpay = db.CashPayments.Where (c => c.PaymentDate.Date >= startDate.Date && c.PaymentDate.Date <= endDate.Date).ToList ();
            var tailor = db.TailoringSalaryPayments.Where (c => c.PaymentDate.Date >= startDate.Date && c.PaymentDate.Date <= endDate.Date).ToList ();
            var staff = db.SalaryPayments.Where (c => c.PaymentDate.Date >= startDate.Date && c.PaymentDate.Date <= endDate.Date).ToList ();
            var TotalDues = db.DuesLists.Include (c => c.DailySale).Where (c => !c.IsRecovered && c.DailySale.SaleDate.Date >= startDate.Date && c.RecoveryDate.Value.Date <= endDate.Date).ToList ();
            var paym = db.Payments.Where (c => c.PayDate.Date >= startDate.Date && c.PayDate.Date <= endDate.Date).ToList ();

        }
    }

}