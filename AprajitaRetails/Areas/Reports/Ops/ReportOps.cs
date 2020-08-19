using AprajitaRetails.Areas.Reports.Models;
using AprajitaRetails.Areas.Sales.Controllers;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Models.Helpers;
using OpenTl.Schema.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.Reports.Ops
{
    public class ReportOps
    {

        public static EmpAttReport GetEmployeeAttendanceReport(AprajitaRetailsContext db, int EmpId, DateTime sDate, DateTime eDate)
        {
            Employee emp = db.Employees.Find(EmpId);
            if (emp == null)
            {
                return null;
            }

            var AttList = db.Attendances.Where(c => c.EmployeeId == EmpId && c.AttDate > sDate.AddDays(-1) && c.AttDate < eDate.AddDays(1)).ToList();

            EmpAttReport empAtt = new EmpAttReport
            {
                EmpAttReportId = -1,
                EmployeeId = EmpId,
                EmployeeName = emp.StaffName,
                IsWorking = emp.IsWorking,
                JoinningDate = emp.JoiningDate,
                LeavingDate = emp.LeavingDate,
                Type = emp.Category,
                Employee = emp,

                NoOfWorkingDays = 0,
                TotalDaysAbsent = 0,
                TotalDaysHalfDay = 0,
                TotalDaysPresent = 0,
                TotalFinalPresent = 0,
                TotalSundayPresent = 0


            };
            if (AttList.Count > 0)
            {
                empAtt.TotalDaysAbsent = AttList.Where(c => c.Status == AttUnits.Absent).Count();
                empAtt.TotalDaysHalfDay = AttList.Where(c => c.Status == AttUnits.HalfDay).Count();
                empAtt.TotalDaysPresent = AttList.Where(c => c.Status == AttUnits.Present).Count();
                empAtt.TotalSundayPresent = AttList.Where(c => c.Status == AttUnits.Sunday).Count();
                empAtt.TotalFinalPresent = empAtt.TotalDaysPresent + empAtt.TotalSundayPresent + (empAtt.TotalDaysHalfDay / 2);
                empAtt.EmpAttReportId = EmpId;
            }

            int NoOfNonWorkingDay = AttList.Where(c => c.Status == AttUnits.Holiday || c.Status == AttUnits.StoreClosed).Count();
            TimeSpan ts = (eDate - sDate);
            int NoofWorkingDay = ts.Days + 1 - NoOfNonWorkingDay;
            empAtt.NoOfWorkingDays = NoofWorkingDay;

            return empAtt;

        }

        public static EmpFinReport GetEmployeeFinReport(AprajitaRetailsContext db, int EmpId, DateTime sDate, DateTime endDate)
        {

            Employee emp = db.Employees.Find(EmpId);
            if (emp == null) return null;

            int smId = (int?)db.Salesmen.Where(c => c.SalesmanName == emp.StaffName).Select(c => c.SalesmanId).FirstOrDefault() ?? 0;

            var SaleList = db.DailySales.Where(c => c.SalesmanId == smId && c.SaleDate > sDate.AddDays(-1) && c.SaleDate < endDate.AddDays(1)).Select(c => new { c.Amount, c.IsManualBill, c.IsSaleReturn, c.IsTailoringBill, c.StoreId }).ToList();

            var SalryPaidList = db.SalaryPayments.Where(c => c.EmployeeId == EmpId && c.PaymentDate > sDate.AddDays(-1) && c.PaymentDate < endDate.AddDays(1))
                .Select(c => new { c.SalaryPaymentId, c.SalaryComponet, c.SalaryMonth, c.Amount }).ToList();
            var AdvPaidList = db.StaffAdvancePayments.Where(c => c.EmployeeId == EmpId && c.PaymentDate > sDate.AddDays(-1) && c.PaymentDate < endDate.AddDays(1))
                .Select(c => new { c.Amount, c.StaffAdvancePaymentId }).ToList();
            var AdvRecList = db.StaffAdvanceReceipts.Where(c => c.EmployeeId == EmpId && c.ReceiptDate > sDate.AddDays(-1) && c.ReceiptDate < endDate.AddDays(1))
               .Select(c => new { c.StaffAdvanceReceiptId, c.Amount }).ToList();


            EmpFinReport empFin = new EmpFinReport
            {
                EmpFinReportId = smId,
                EmployeeId = EmpId,
                Employee = emp,
                EmployeeName = emp.StaffName,
                IsWorking = emp.IsWorking,
                JoinningDate = emp.JoiningDate,
                LeavingDate = emp.LeavingDate,
                Type = emp.Category,
                NoOfBill = -1,
                TotalSale = -1,
                AverageSale = -1,
                TotalAdvancePaidOff = 0,
                TotalBalance = 0,
                TotalLastPcIncentive = 0,
                TotalSalaryAdvancePaid = 0,
                TotalSalaryPaid = 0,
                TotalSaleIncentive = 0,
                TotalWowBillIncentive = 0
            };

            if (smId > 0 && SaleList.Count > 0)
            {
                empFin.NoOfBill = SaleList.Where(c => !c.IsSaleReturn).Count();
                empFin.TotalSale = SaleList.Where(c => !c.IsSaleReturn).Sum(c => c.Amount);
                empFin.AverageSale = empFin.TotalSale / empFin.NoOfBill;
            }
            if (emp.Category == EmpType.Salesman)
            {
                empFin.TotalLastPcIncentive = SalryPaidList.Where(c => c.SalaryComponet == SalaryComponet.LastPcs).Sum(c => c.Amount);
                empFin.TotalWowBillIncentive = SalryPaidList.Where(c => c.SalaryComponet == SalaryComponet.WOWBill).Sum(c => c.Amount);
                empFin.TotalSaleIncentive = SalryPaidList.Where(c => c.SalaryComponet == SalaryComponet.Incentive).Sum(c => c.Amount);

            }
            else if (emp.Category == EmpType.StoreManager)
            {
                empFin.TotalSaleIncentive = SalryPaidList.Where(c => c.SalaryComponet == SalaryComponet.Incentive).Sum(c => c.Amount);

                empFin.NoOfBill = db.DailySales.Where(c =>c.StoreId==emp.StoreId && c.SaleDate > sDate.AddDays(-1) && c.SaleDate < endDate.AddDays(1) && !c.IsManualBill && !c.IsSaleReturn && !c.IsTailoringBill).Select(c => new { c.Amount }).Count();
                empFin.TotalSale = db.DailySales.Where(c => c.StoreId == emp.StoreId && c.SaleDate > sDate.AddDays(-1) && c.SaleDate < endDate.AddDays(1) && !c.IsManualBill && !c.IsSaleReturn && !c.IsTailoringBill).Select(c => new { c.Amount }).Sum(c => c.Amount);
                if(empFin.TotalSale >0 && empFin.NoOfBill >0)
                empFin.AverageSale = empFin.TotalSale / empFin.NoOfBill;
            }


            empFin.TotalSalaryPaid = SalryPaidList.Where(c => c.SalaryComponet == SalaryComponet.NetSalary).Sum(c => c.Amount);
            empFin.TotalSalaryPaid += SalryPaidList.Where(c => c.SalaryComponet == SalaryComponet.SundaySalary).Sum(c => c.Amount);
            empFin.TotalSalaryPaid += SalryPaidList.Where(c => c.SalaryComponet == SalaryComponet.Others).Sum(c => c.Amount);

            empFin.TotalSalaryAdvancePaid = AdvPaidList.Sum(c => c.Amount);
            empFin.TotalAdvancePaidOff = AdvRecList.Sum(c => c.Amount);
            empFin.TotalBalance = empFin.TotalSalaryAdvancePaid - empFin.TotalAdvancePaidOff;

            return empFin;


        }



    }
}
