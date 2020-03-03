using AprajitaRetails.Data;
using Microsoft.EntityFrameworkCore;
using System;
//using System.Data.Entity;
using System.Linq;
//using TAS_AprajiataRetails.Models.Data;

namespace AprajitaRetails.Models.Helpers
{
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

}