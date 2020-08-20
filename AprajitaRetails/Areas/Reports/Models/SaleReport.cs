using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Ops.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.Reports.Models
{
    //Can be Used at DailySale or any place to reduce no of different function
    public class SaleReportInfo
    {
        public decimal LastMonthSale { get; set; }
        public decimal CurrentMonthSale { get; set; }
        public decimal ManualSale { get; set; }
        public decimal TodaySale { get; set; }
        public decimal CashInHand { get; set; }
        public decimal TotalDues { get; set; }
        public decimal AdjustedAmount { get; set; }
    }
    //TODO: move to same place to do homo
    public class SaleOps
    {
        public SaleReportInfo GetSaleReportInfo(AprajitaRetailsContext db, int StoreCodeId)
        {
            SaleReportInfo info = new SaleReportInfo
            {
                TodaySale = db.DailySales.Where(c => c.IsManualBill == false && c.SaleDate.Date == DateTime.Today.Date && c.StoreId == StoreCodeId).Sum(c => (decimal?)c.Amount) ?? 0,
                ManualSale = db.DailySales.Where(c => c.IsManualBill == true && c.SaleDate.Date == DateTime.Today.Date && c.StoreId == StoreCodeId).Sum(c => (decimal?)c.Amount) ?? 0,
                CurrentMonthSale = db.DailySales.Where(c => c.SaleDate.Year == DateTime.Today.Year && c.SaleDate.Month == DateTime.Today.Month && c.StoreId == StoreCodeId).Sum(c => (decimal?)c.Amount) ?? 0,
                LastMonthSale = db.DailySales.Where(c => c.SaleDate.Year == DateTime.Today.Year && c.SaleDate.Month == DateTime.Today.Month - 1 && c.StoreId == StoreCodeId).Sum(c => (decimal?)c.Amount) ?? 0,
                TotalDues = db.DuesLists.Where(c => c.IsRecovered == false && c.StoreId == StoreCodeId).Sum(c => (decimal?)c.Amount) ?? 0,
                CashInHand = (decimal)0.00
            };
            try
            {
                info.CashInHand = db.CashInHands.Where(c => c.CIHDate == DateTime.Today && c.StoreId == StoreCodeId).FirstOrDefault().InHand;
            }
            catch (Exception)
            {
                new CashWork().Process_OpenningBalance(db, DateTime.Today, true);
                info.CashInHand = (decimal)0.00;
            }
            return info;
        }
    }
}
