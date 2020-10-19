using AprajitaRetails.Data;
using AprajitaRetails.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;    using System;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Ops.WidgetModel
{
    public static class SaleWidgetModel
    {
        public static DailySaleReport GetSaleRecord(AprajitaRetailsContext db)
        {

            DailySaleReport record = new DailySaleReport
            {
                DailySale = (decimal?)db.DailySales.Where(C => (C.SaleDate.Date) == (DateTime.Today.Date)).Sum(c => (decimal?)c.Amount) ?? 0,
                MonthlySale = (decimal?)db.DailySales.Where(C => (C.SaleDate).Month == (DateTime.Today).Month  && C.SaleDate.Year == DateTime.Today.Year).Sum(c => (decimal?)c.Amount) ?? 0,
                YearlySale = (decimal?)db.DailySales.Where(C => (C.SaleDate).Year == (DateTime.Today).Year).Sum(c => (decimal?)c.Amount) ?? 0
            };

            return record;

        }
        public static DailySaleReport GetSaleRecord(AprajitaRetailsContext db, int StoreId)
        {

            DailySaleReport record = new DailySaleReport
            {
                DailySale = (decimal?)db.DailySales.Where(C => C.SaleDate.Date == DateTime.Today.Date && C.StoreId==StoreId).Sum(c => (decimal?)c.Amount) ?? 0,
                MonthlySale = (decimal?)db.DailySales.Where(C => C.SaleDate.Month == DateTime.Today.Month && C.SaleDate.Year == DateTime.Today.Year && C.StoreId == StoreId).Sum(c => (decimal?)c.Amount) ?? 0,
                YearlySale = (decimal?)db.DailySales.Where(C => C.SaleDate.Year == DateTime.Today.Year && C.StoreId == StoreId).Sum(c => (decimal?)c.Amount) ?? 0
            };

            return record;

        }

    }
}
