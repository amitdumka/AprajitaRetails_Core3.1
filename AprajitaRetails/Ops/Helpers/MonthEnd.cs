using AprajitaRetails.Data;
using AprajitaRetails.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Web;
//using TAS_AprajiataRetails.Models.Data;

namespace TAS_AprajiataRetails.Models.Helpers
{

    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-1 * diff).Date;
        }
        public static DateTime EndOfWeek(this DateTime dt, DayOfWeek endOfWeek = DayOfWeek.Sunday)
        {
            if (dt.DayOfWeek == endOfWeek)
            {
                return dt.Date.Date.AddDays(1).AddMilliseconds(-1);
            }
            else
            {
                var diff = dt.DayOfWeek - endOfWeek;
                return dt.AddDays(7 - diff).Date.AddDays(1).AddMilliseconds(-1);
            }
        }
    }

    //TODO: Create Table Class for MonthEnd Data
    public class MonthEndProcesser
    {
        private readonly AprajitaRetailsContext _db;

        public MonthEndProcesser(AprajitaRetailsContext context)
        {
            _db = context;
        }

        public void ProcessMonthEnd(DateTime onDate)
        {
            MonthEnd monthEnd = null;
            using AprajitaRetailsContext db = _db;
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
            mEnd.TotalSalary = db.SalaryPayments.Where(c =>(c.PaymentDate).Month ==(onDate).Month).Sum(c => c.Amount);
            mEnd.TotalTailoringExpenses = db.TailoringSalaryPayments.Where(c =>(c.PaymentDate).Month ==(onDate).Month).Sum(c => c.Amount);
            mEnd.TotalCashExpenses = db.PettyCashExpenses.Where(c =>(c.ExpDate).Month ==(onDate).Month).Sum(c => c.Amount);

            mEnd.TotalHomeExpenses = db.CashPayments.Where(c =>(c.PaymentDate).Month ==(onDate).Month).Sum(c => c.Amount);
            mEnd.TotalPayments = db.Payments.Where(c =>(c.PayDate).Month ==(onDate).Month).Sum(c => c.Amount);

            mEnd.TotalDuesOfMonth = db.DuesLists.Include(c => c.DailySale).Where(c => c.IsRecovered == false &&c.DailySale.SaleDate.Month ==onDate.Month).Sum(c => c.Amount);
            mEnd.TotalDues = db.DuesLists.Where(c => !c.IsRecovered).Sum(c => c.Amount);
            mEnd.TotalDuesRecovered = db.DuesLists.Where(c =>c.RecoveryDate.Value.Month ==onDate.Month).Sum(c => c.Amount);

            return mEnd;

        }

        public MonthEnd CalculateSaleData(AprajitaRetailsContext db, DateTime onDate, MonthEnd mEnd)
        {
            var endofdays = db.EndOfDays.Where(c =>(c.EOD_Date).Month ==(onDate).Month);
            if (endofdays != null)
            {
                mEnd.TotalFabric = endofdays.Sum(c => c.Shirting) + endofdays.Sum(c => c.Suiting);
                mEnd.TotalAccess = endofdays.Sum(c => c.Access);
                mEnd.TotalRMZ = endofdays.Sum(c => c.FM_Arrow) + endofdays.Sum(c => c.RWT) + endofdays.Sum(c => c.USPA);
                mEnd.TotalOthers = 0;


            }
            return mEnd;
        }

        public MonthEnd CalculateSaleFinData(AprajitaRetailsContext db, DateTime onDate, MonthEnd mEnd)
        {

            mEnd.TotalAmountAccess =mEnd.TotalAmountFabric = mEnd.TotalAmountOthers = mEnd.TotalAmountRMZ = 0;
            //TODO: to be in version 2 & 3 onwards
            return mEnd;
        }

        private void UpdateMonthEnd(AprajitaRetailsContext db, MonthEnd mEnd, bool upDate)
        {
            //TODO: Save MonthEnd data to database.
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