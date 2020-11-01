using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AprajitaRetails.Ops.WidgetModel
{
    //TODO: Need to implement Store Based Model
    public static class HomeWidgetModel
    {
        public static AccountsInfo GetAccoutingRecord(AprajitaRetailsContext db)
        {
            AccountsInfo info = new AccountsInfo();
            CashInHand cih = db.CashInHands.Where(c => (c.CIHDate) == (DateTime.Today)).FirstOrDefault();

            if (cih != null)
            {
                info.CashInHand = cih.InHand;
                info.CashIn = cih.CashIn;
                info.CashOut = cih.CashOut;
                info.OpenningBal = cih.OpenningBalance;
            }

            CashInBank cib = db.CashInBanks.Where(c => (c.CIBDate) == (DateTime.Today)).FirstOrDefault();
            if (cib != null)
            {
                info.CashToBank = cib.CashIn;
                info.CashFromBank = cib.CashOut;
                info.CashInBank = cib.InHand;
            }

            var CashExp = db.PettyCashExpenses.Where(c => (c.ExpDate) == (DateTime.Today));
            var CashPay = db.CashPayments.Where(c => (c.PaymentDate) == (DateTime.Today));

            if (CashExp != null)
            {
                info.TotalCashPayments = (decimal?)CashExp.Sum(c => (decimal?)c.Amount) ?? 0;
            }
            if (CashPay != null)
            {
                info.TotalCashPayments += (decimal?)CashPay.Sum(c => (decimal?)c.Amount) ?? 0;
            }
            return info;
        }

        public static AccountsInfo GetAccoutingRecord(AprajitaRetailsContext db, int StoreId)
        {
            AccountsInfo info = new AccountsInfo();
            CashInHand cih = db.CashInHands.Where(c => (c.CIHDate) == (DateTime.Today) && c.StoreId == StoreId).FirstOrDefault();

            if (cih != null)
            {
                info.CashInHand = cih.InHand;
                info.CashIn = cih.CashIn;
                info.CashOut = cih.CashOut;
                info.OpenningBal = cih.OpenningBalance;
            }

            CashInBank cib = db.CashInBanks.Where(c => (c.CIBDate) == (DateTime.Today) && c.StoreId == StoreId).FirstOrDefault();
            if (cib != null)
            {
                info.CashToBank = cib.CashIn;
                info.CashFromBank = cib.CashOut;
                info.CashInBank = cib.InHand;
            }

            var CashExp = db.PettyCashExpenses.Where(c => (c.ExpDate) == (DateTime.Today) && c.StoreId == StoreId);
            var CashPay = db.CashPayments.Where(c => (c.PaymentDate) == (DateTime.Today) && c.StoreId == StoreId);

            if (CashExp != null)
            {
                info.TotalCashPayments = (decimal?)CashExp.Sum(c => (decimal?)c.Amount) ?? 0;
            }
            if (CashPay != null)
            {
                info.TotalCashPayments += (decimal?)CashPay.Sum(c => (decimal?)c.Amount) ?? 0;
            }
            return info;
        }

        public static List<EmpBasicInfo> GetEmpBasicInfo(AprajitaRetailsContext db)
        {
            var emps = db.Attendances.Include(c => c.Employee).
                Where(c => c.AttDate == DateTime.Today && c.IsTailoring == false && (c.Status == AttUnit.Present || c.Status == AttUnit.Sunday)).OrderByDescending(c => c.Employee.StaffName);

            var totalSale = db.DailySales.Include(c => c.Salesman).Where(c => c.SaleDate.Year == DateTime.Today.Year && c.SaleDate.Month == DateTime.Today.Month).Select(a => new { StaffName = a.Salesman.SalesmanName, a.Amount }).ToList();

            List<EmpBasicInfo> list = new List<EmpBasicInfo>();
            foreach (var item in emps)
            {
                EmpBasicInfo info = new EmpBasicInfo
                {
                    EmpId = item.EmployeeId,
                    Name = item.Employee.StaffName,
                    IsSalesman = false,
                    TotalSale = 0
                };
                if (item.Employee.Category == EmpType.Salesman)
                    info.IsSalesman = true;
                else if (totalSale != null && (item.Employee.Category == EmpType.Salesman /*|| item.Employee.Category == EmpType.StoreManager*/))
                {
                    var ts = totalSale.Where(c => c.StaffName == info.Name).ToList();
                    info.TotalSale = (decimal?)ts.Sum(c => (decimal?)c.Amount) ?? 0;
                }
                list.Add(info);
            }

            return list;
        }

        public static List<EmpBasicInfo> GetEmpBasicInfo(AprajitaRetailsContext db, int StoreId)
        {
            var emps = db.Attendances.Include(c => c.Employee).
                Where(c => c.StoreId == StoreId && c.AttDate == DateTime.Today && c.IsTailoring == false && (c.Status == AttUnit.Present || c.Status == AttUnit.Sunday)).OrderByDescending(c => c.Employee.StaffName);

            var totalSale = db.DailySales.Include(c => c.Salesman).Where(c => c.StoreId == StoreId && c.SaleDate.Year == DateTime.Today.Year && c.SaleDate.Month == DateTime.Today.Month).Select(a => new { StaffName = a.Salesman.SalesmanName, a.Amount }).ToList();

            List<EmpBasicInfo> list = new List<EmpBasicInfo>();
            foreach (var item in emps)
            {
                EmpBasicInfo info = new EmpBasicInfo
                {
                    EmpId = item.EmployeeId,
                    Name = item.Employee.StaffName,
                    IsSalesman = false
                };
                if (item.Employee.Category == EmpType.Salesman)
                    info.IsSalesman = true;

                if (item.Employee.Category == EmpType.StoreManager)
                {
                    var ts = db.DailySales.Where(c => c.StoreId == StoreId && c.SaleDate.Year == DateTime.Today.Year && c.SaleDate.Month == DateTime.Today.Month).Select(a => new { a.Amount }).Sum(c => c.Amount);
                    info.TotalSale = ts;
                }
                else if (totalSale != null && (item.Employee.Category == EmpType.Salesman /*|| item.Employee.Category == EmpType.StoreManager*/))
                {
                    var ts = totalSale.Where(c => c.StaffName == info.Name).ToList();
                    info.TotalSale = (decimal?)ts.Sum(c => (decimal?)c.Amount) ?? 0;
                }
                list.Add(info);
            }

            return list;
        }

        public static List<EmployeeInfo> GetEmpInfo(AprajitaRetailsContext db, bool WithTailor = false)
        {
            var emps = db.Attendances.Include(c => c.Employee).
                Where(c => c.AttDate == DateTime.Today && c.IsTailoring == false).OrderByDescending(c => c.Employee.StaffName);

            var empPresent = db.Attendances.Include(c => c.Employee)
                .Where(c => c.Status == AttUnit.Present && c.AttDate.Year == DateTime.Today.Year && c.AttDate.Month == DateTime.Today.Month && c.IsTailoring == false)
                .GroupBy(c => c.Employee.StaffName).OrderBy(c => c.Key).Select(g => new { StaffName = g.Key, Days = g.Count() }).ToList();

            var empAbsent = db.Attendances.Include(c => c.Employee)
                .Where(c => c.Status == AttUnit.Absent && c.AttDate.Year == DateTime.Today.Year && c.AttDate.Month == DateTime.Today.Month && c.IsTailoring == false)
                 .GroupBy(c => c.Employee.StaffName).OrderBy(c => c.Key).Select(g => new { StaffName = g.Key, Days = g.Count() }).ToList();

            var totalSale = db.DailySales.Include(c => c.Salesman).Where(c => c.SaleDate.Year == DateTime.Today.Year && c.SaleDate.Month == DateTime.Today.Month).Select(a => new { StaffName = a.Salesman.SalesmanName, a.Amount }).ToList();

            if (WithTailor)
            {
                emps = db.Attendances.Include(c => c.Employee).
                   Where(c => c.AttDate == DateTime.Today).OrderByDescending(c => c.Employee.StaffName);

                empPresent = db.Attendances.Include(c => c.Employee)
                   .Where(c => c.Status == AttUnit.Present && c.AttDate.Year == DateTime.Today.Year && c.AttDate.Month == DateTime.Today.Month)
                   .GroupBy(c => c.Employee.StaffName).OrderBy(c => c.Key).Select(g => new { StaffName = g.Key, Days = g.Count() }).ToList();

                empAbsent = db.Attendances.Include(c => c.Employee)
                   .Where(c => c.Status == AttUnit.Absent && c.AttDate.Year == DateTime.Today.Year && c.AttDate.Month == DateTime.Today.Month)
                    .GroupBy(c => c.Employee.StaffName).OrderBy(c => c.Key).Select(g => new { StaffName = g.Key, Days = g.Count() }).ToList();

                totalSale = db.DailySales.Include(c => c.Salesman).Where(c => c.SaleDate.Year == DateTime.Today.Year && c.SaleDate.Month == DateTime.Today.Month).Select(a => new { StaffName = a.Salesman.SalesmanName, a.Amount }).ToList();
            }

            List<EmployeeInfo> infoList = new List<EmployeeInfo>();

            foreach (var item in emps)
            {
                if (item.Employee.StaffName != "Amit Kumar")
                {
                    EmployeeInfo info = new EmployeeInfo()
                    {
                        Name = item.Employee.StaffName,
                        EmpId = item.Employee.EmployeeId,
                        AbsentDays = 0,
                        NoOfBills = 0,
                        Present = "",
                        PresentDays = 0,
                        Ratio = 0,
                        TotalSale = 0
                    };

                    if (item.Status == AttUnit.Present || item.Status==AttUnit.Sunday)
                        info.Present = "Present";
                    else
                        info.Present = "Absent";

                    try
                    {
                        if (item.Employee.Category == EmpType.Salesman)
                        {
                            info.IsSalesman = true;
                        }

                        if (empPresent != null)
                        {
                            var pd = empPresent.Where(c => c.StaffName == info.Name).FirstOrDefault();
                            if (pd != null)
                                info.PresentDays = pd.Days;
                            else
                                info.PresentDays = 0;
                        }
                        else
                        {
                            info.PresentDays = 0;
                        }

                        if (empAbsent != null)
                        {
                            var ad = empAbsent.Where(c => c.StaffName == info.Name).FirstOrDefault();
                            if (ad != null)
                                info.AbsentDays = ad.Days;
                            else
                                info.AbsentDays = 0;
                        }
                        else
                            info.AbsentDays = 0;

                        //var ts = db.DailySales.Include(c=>c.Salesman ).Where (c => c.Salesman.SalesmanName == info.Name && (c.SaleDate).Month == (DateTime.Today).Month).ToList();
                        if (totalSale != null && (item.Employee.Category == EmpType.Salesman || item.Employee.Category == EmpType.StoreManager))
                        {
                            var ts = totalSale.Where(c => c.StaffName == info.Name).ToList();
                            info.TotalSale = (decimal?)ts.Sum(c => (decimal?)c.Amount) ?? 0;
                            info.NoOfBills = (int?)ts.Count ?? 0;
                        }

                        if (info.PresentDays > 0 && info.TotalSale > 0)
                        {
                            info.Ratio = Math.Round((double)info.TotalSale / info.PresentDays, 2);
                        }
                    }
                    catch (Exception)
                    {
                        // Log.Error().Message("emp-present exception");
                    }
                    infoList.Add(info);
                }
            }
            return infoList;
        }

        public static TailoringReport GetTailoringReport(AprajitaRetailsContext db)
        {
            return new TailoringReport()
            {
                TodayBooking = (int?)db.TalioringBookings.Where(c => (c.BookingDate.Date) == (DateTime.Today)).Count() ?? 0,
                TodayUnit = (int?)db.TalioringBookings.Where(c => (c.BookingDate.Date) == (DateTime.Today)).Sum(c => (int?)c.TotalQty) ?? 0,

                MonthlyBooking = (int?)db.TalioringBookings.Where(c => (c.BookingDate).Month == (DateTime.Today).Month).Count() ?? 0,
                MonthlyUnit = (int?)db.TalioringBookings.Where(c => (c.BookingDate).Month == (DateTime.Today).Month).Sum(c => (int?)c.TotalQty) ?? 0,

                YearlyBooking = (int?)db.TalioringBookings.Where(c => (c.BookingDate).Year == (DateTime.Today).Year).Count() ?? 0,
                YearlyUnit = (int?)db.TalioringBookings.Where(c => (c.BookingDate).Year == (DateTime.Today).Year).Sum(c => (int?)c.TotalQty) ?? 0,

                TodaySale = (decimal?)db.TailoringDeliveries.Where(c => (c.DeliveryDate.Date) == (DateTime.Today)).Sum(c => (decimal?)c.Amount) ?? 0,
                YearlySale = (decimal?)db.TailoringDeliveries.Where(c => (c.DeliveryDate).Year == (DateTime.Today).Year).Sum(c => (decimal?)c.Amount) ?? 0,
                MonthlySale = (decimal?)db.TailoringDeliveries.Where(c => (c.DeliveryDate).Month == (DateTime.Today).Month).Sum(c => (decimal?)c.Amount) ?? 0,
            };
        }
    }
}