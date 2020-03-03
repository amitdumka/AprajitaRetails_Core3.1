using AprajitaRetails.Data;
using AprajitaRetails.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Web;
using AprajitaRetails.Models.ViewModels;
//using TAS_AprajiataRetails.Models.Data;

namespace AprajitaRetails.Models.Helpers
{

    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if ( diff < 0 )
            {
                diff += 7;
            }

            return dt.AddDays (-1 * diff).Date;
        }
        public static DateTime EndOfWeek(this DateTime dt, DayOfWeek endOfWeek = DayOfWeek.Sunday)
        {
            if ( dt.DayOfWeek == endOfWeek )
            {
                return dt.Date.Date.AddDays (1).AddMilliseconds (-1);
            }
            else
            {
                var diff = dt.DayOfWeek - endOfWeek;
                return dt.AddDays (7 - diff).Date.AddDays (1).AddMilliseconds (-1);
            }
        }
    }

    //TODO: Create Table Class for MonthEnd Data
    public class MonthEndProcesser
    {
        private readonly AprajitaRetailsContext db;

        public MonthEndProcesser(AprajitaRetailsContext context)
        {
            db = context;
        }

        public void ProcessMonthEnd(DateTime onDate)
        {
            MonthEnd monthEnd = null;

            monthEnd = CalculateTotalIncome (db, onDate);
            monthEnd = CalculateTotalExpenses (db, onDate, monthEnd);
            monthEnd = CalculateSaleData (db, onDate, monthEnd);
            monthEnd = CalculateSaleFinData (db, onDate, monthEnd);
            monthEnd.EntryDate = DateTime.Today;
        }
        private MonthEnd CalculateTotalIncome(AprajitaRetailsContext db, DateTime onDate)
        {


            MonthEnd monthEnd = new MonthEnd
            {
                TotalSaleIncome = db.DailySales.Where (c => c.SaleDate.Month == ( onDate ).Month).Sum (c => c.Amount),
                TotalTailoringIncome = db.DailySales.Where (c => c.IsTailoringBill && ( c.SaleDate ).Month == ( onDate ).Month).Sum (c => c.Amount),
                TotalOtherIncome = 0, //TODO: Ohter Income group will be dealt with proper entry.
                TotalRecipts = db.Receipts.Where (c => ( c.RecieptDate ).Month == ( onDate ).Month).Sum (c => c.Amount)
            };

            return monthEnd;
        }
        private MonthEnd CalculateTotalExpenses(AprajitaRetailsContext db, DateTime onDate, MonthEnd mEnd)
        {
            mEnd.TotalSalary = db.SalaryPayments.Where (c => ( c.PaymentDate ).Month == ( onDate ).Month).Sum (c => c.Amount);
            mEnd.TotalTailoringExpenses = db.TailoringSalaryPayments.Where (c => ( c.PaymentDate ).Month == ( onDate ).Month).Sum (c => c.Amount);
            mEnd.TotalCashExpenses = db.PettyCashExpenses.Where (c => ( c.ExpDate ).Month == ( onDate ).Month).Sum (c => c.Amount);

            mEnd.TotalHomeExpenses = db.CashPayments.Where (c => ( c.PaymentDate ).Month == ( onDate ).Month).Sum (c => c.Amount);
            mEnd.TotalPayments = db.Payments.Where (c => ( c.PayDate ).Month == ( onDate ).Month).Sum (c => c.Amount);

            mEnd.TotalDuesOfMonth = db.DuesLists.Include (c => c.DailySale).Where (c => c.IsRecovered == false && c.DailySale.SaleDate.Month == onDate.Month).Sum (c => c.Amount);
            mEnd.TotalDues = db.DuesLists.Where (c => !c.IsRecovered).Sum (c => c.Amount);
            mEnd.TotalDuesRecovered = db.DuesLists.Where (c => c.RecoveryDate.Value.Month == onDate.Month).Sum (c => c.Amount);

            return mEnd;

        }

        public MonthEnd CalculateSaleData(AprajitaRetailsContext db, DateTime onDate, MonthEnd mEnd)
        {
            var endofdays = db.EndOfDays.Where (c => ( c.EOD_Date ).Month == ( onDate ).Month);
            if ( endofdays != null )
            {
                mEnd.TotalFabric = endofdays.Sum (c => c.Shirting) + endofdays.Sum (c => c.Suiting);
                mEnd.TotalAccess = endofdays.Sum (c => c.Access);
                mEnd.TotalRMZ = endofdays.Sum (c => c.FM_Arrow) + endofdays.Sum (c => c.RWT) + endofdays.Sum (c => c.USPA);
                mEnd.TotalOthers = 0;
            }
            return mEnd;
        }

        public MonthEnd CalculateSaleFinData(AprajitaRetailsContext db, DateTime onDate, MonthEnd mEnd)
        {

            mEnd.TotalAmountAccess = mEnd.TotalAmountFabric = mEnd.TotalAmountOthers = mEnd.TotalAmountRMZ = 0;
            //TODO: to be in version 2 & 3 onwards
            return mEnd;
        }

        private void UpdateMonthEnd(AprajitaRetailsContext db, MonthEnd mEnd, bool upDate)
        {
            //TODO: Save MonthEnd data to database.
        }

    }

    public class FinData
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


    public class DateHelper
    {
        static public int CountDays(DayOfWeek day, DateTime start, DateTime end)
        {
            TimeSpan ts = end - start;                       // Total duration
            int count = (int) Math.Floor (ts.TotalDays / 7);   // Number of whole weeks
            int remainder = (int) ( ts.TotalDays % 7 );         // Number of remaining days
            int sinceLastDay = (int) ( end.DayOfWeek - day );   // Number of days since last [day]
            if ( sinceLastDay < 0 )
                sinceLastDay += 7;         // Adjust for negative days since last [day]

            // If the days in excess of an even week are greater than or equal to the number days since the last [day], then count this one, too.
            if ( remainder >= sinceLastDay )
                count++;

            return count;
        }
        static public int CountDays(DayOfWeek day, DateTime curMnt)
        {
            DateTime start = new DateTime (curMnt.Year, curMnt.Month, 1);
            DateTime end = new DateTime (curMnt.Year, curMnt.Month, DateTime.DaysInMonth (curMnt.Year, curMnt.Month));
            TimeSpan ts = end - start;                       // Total duration
            int count = (int) Math.Floor (ts.TotalDays / 7);   // Number of whole weeks
            int remainder = (int) ( ts.TotalDays % 7 );         // Number of remaining days
            int sinceLastDay = (int) ( end.DayOfWeek - day );   // Number of days since last [day]
            if ( sinceLastDay < 0 )
                sinceLastDay += 7;         // Adjust for negative days since last [day]

            // If the days in excess of an even week are greater than or equal to the number days since the last [day], then count this one, too.
            if ( remainder >= sinceLastDay )
                count++;

            return count;
        }
    }

}