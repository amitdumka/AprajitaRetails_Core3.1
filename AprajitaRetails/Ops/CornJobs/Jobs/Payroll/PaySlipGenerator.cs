using AprajitaRetails.Data;
using AprajitaRetails.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AprajitaRetails.Ops.CornJobs.Jobs.Payroll
{
    public sealed class ErrorMessage
    {
        public const string SalaryHeadNotFound = "Salary Head Not Found";
        public const string AttendanceRecordNotFound = "Attendance Record Not Found";
        public const string NoofDaysNotMatched = "No of Day Not Matched#";
    }

    public class SalaryHead
    {
        public int EmpId { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal WowBill { get; set; }
        public decimal Incentive { get; set; }
        public decimal LastPcs { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string ErrMsg { get; set; }
        public bool IsError { get; set; }
    }

    /// <summary>
    /// It will generate PaySlip on 2nd of every month.
    /// </summary>
    public class PaySlipGenerator
    {

        public void ProcessPaySlip(AprajitaRetailsContext db)
        {
            DateTime forDate = DateTime.Today.AddMonths(-1).Date;


        }



        /// <summary>
        /// Generate Last Month PaySlip for All Employees
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public List<SalaryHead> GeneratePaySlip(AprajitaRetailsContext db)
        {
            return GeneratePaySlip(db, DateTime.Today.AddMonths(-1).Month, DateTime.Today.AddMonths(-1).Year);
        }

        /// <summary>
        /// Generate Payslip for Employee  for year  and month
        /// </summary>
        /// <param name="db"></param>
        /// <param name="EmpID"></param>
        /// <param name="Month"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public SalaryHead GeneratePaySlip(AprajitaRetailsContext db, int EmpID, int Month, int Year)
        {
            SalaryHead head = new SalaryHead() { EmpId = EmpID, Month = Month, Year = Year, IsError = false, ErrMsg = "" };
            var att = db.Attendances.Where(c => c.EmployeeId == EmpID && c.AttDate.Year == Year && c.AttDate.Month == Month).ToList();
            if (att != null && att.Count > 0)
            {
                int present = att.Where(c => c.Status == AttUnits.Present || c.Status == AttUnits.Sunday).Count();
                int absent = att.Where(c => c.Status == AttUnits.Absent).Count();
                int halfday = att.Where(c => c.Status == AttUnits.HalfDay).Count();
                int holiday = att.Where(c => c.Status == AttUnits.Holiday || c.Status == AttUnits.StoreClosed).Count();
                int totaldays = present + absent + halfday + holiday;
                int noofdays = DateTime.DaysInMonth(Year, Month);
                int sunday = 0;
                if (totaldays == noofdays)
                {
                    double netdayPresent = present + (halfday / 2);
                    var salary = GetCurrentSalary(db, Year, Month, EmpID);
                    if (salary != null)
                    {
                        var empType = att.First().Employee.Category;
                        switch (empType)
                        {
                            case EmpType.Salesman:
                                int SMID = GetSalesManId(db, EmpID);
                                head.BasicSalary = CalculateBasicSalary(salary, netdayPresent, noofdays, sunday);
                                head.Incentive = CalculateIncentive(db, salary, Month, Year, SMID, true);
                                head.WowBill = CalculateWowBill(db, salary, SMID, Year, Month);
                                head.LastPcs = CalculateLastPcs(db, salary, Year, Month, SMID);
                                break;

                            case EmpType.StoreManager:

                                head.BasicSalary = CalculateBasicSalary(salary, netdayPresent, noofdays, sunday);
                                head.Incentive = CalculateIncentive(db, salary, Month, Year, 0, true);
                                break;

                            case EmpType.HouseKeeping:
                                head.BasicSalary = CalculateBasicSalary(salary, netdayPresent, noofdays, sunday);
                                break;

                            case EmpType.Accounts:
                                head.BasicSalary = CalculateBasicSalary(salary, netdayPresent, noofdays, sunday);
                                break;

                            case EmpType.TailorMaster:

                            case EmpType.Tailors:

                            case EmpType.TailoringAssistance:
                                head.BasicSalary = CalculateBasicSalary(salary, netdayPresent, noofdays, sunday);
                                break;
                        }
                    }
                    else
                    {
                        head.IsError = true;
                        head.ErrMsg = ErrorMessage.SalaryHeadNotFound;
                    }
                }
                else
                {
                    head.IsError = true;
                    head.ErrMsg = ErrorMessage.NoofDaysNotMatched;
                    int diff = noofdays - totaldays;
                    head.ErrMsg += "D" + diff;
                }
            }
            else
            {
                head.IsError = true;
                head.ErrMsg = ErrorMessage.AttendanceRecordNotFound;
            }
            return head;
        }

        /// <summary>
        /// Generate Payslip for given Month/Year for All Empployee
        /// </summary>
        /// <param name="db"></param>
        /// <param name="Month"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public List<SalaryHead> GeneratePaySlip(AprajitaRetailsContext db, int Month, int Year)
        {
            List<SalaryHead> list = new List<SalaryHead>();
            var empList = db.Employees.Where(c => c.IsWorking).Select(c => c.EmployeeId).ToList();
            foreach (var item in empList)
            {
                list.Add(GeneratePaySlip(db, item, Month, Year));
            }

            return list;
        }

        private CurrentSalary GetCurrentSalary(AprajitaRetailsContext db, int Year, int Month, int EmpId)
        {
            var sal = db.CurrentSalaries.Where(c => c.EmployeeId == EmpId && c.EffectiveDate.Year >= Year && c.EffectiveDate.Month >= Month).ToList();

            if (sal.Count > 1)
            {
                foreach (var item in sal)
                {
                    if (item.CloseDate != null)
                    {
                        if (item.CloseDate.Value.Year <= Year && item.CloseDate.Value.Month <= Month)
                            return item;
                    }
                    else
                    {
                        return item;
                    }
                }
                return sal.OrderByDescending(c => c.CurrentSalaryId).First();
            }
            else if (sal.Count == 1)
            {
                return sal.First();
            }
            else return null;
        }

        private decimal CalculateLastPcs(AprajitaRetailsContext db, CurrentSalary salary, int Year, int Month, int SMID)
        {
            //TODO: implement Last Pcs
            return 0.0M;
        }

        /// <summary>
        /// Calculate WowBill
        /// </summary>
        /// <param name="db"></param>
        /// <param name="salary"></param>
        /// <param name="SMID"></param>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        private decimal CalculateWowBill(AprajitaRetailsContext db, CurrentSalary salary, int SMID, int Year, int Month)
        {
            var wowbill = db.DailySales.Where(c => c.IsManualBill == false && c.SalesmanId == SMID && c.SaleDate.Year == Year && c.SaleDate.Month == Month && c.Amount >= salary.WOWBillTarget).Select(c => c.Amount).Sum();

            decimal incetive = wowbill * salary.WOWBillRate;
            return incetive;
        }

        /// <summary>
        /// Calculate Incentive
        /// </summary>
        /// <param name="db"></param>
        /// <param name="salary"></param>
        /// <param name="Month"></param>
        /// <param name="Year"></param>
        /// <param name="EmpId"></param>
        /// <returns></returns>
        private decimal CalculateIncentive(AprajitaRetailsContext db, CurrentSalary salary, int Month, int Year, int SMID, bool IsSM)
        {
            //TODO: Now Consriding DailySale late have to conside manual Sale and regualSale

            decimal totalSale = 0;
            if (IsSM)
            {
                totalSale = db.DailySales.Where(c => !c.IsManualBill && c.SaleDate.Year == Year && c.SaleDate.Month == Month).Select(c => c.Amount).Sum();
            }
            else
            {
                totalSale = db.DailySales.Where(c => !c.IsManualBill && c.SalesmanId == SMID && c.SaleDate.Year == Year && c.SaleDate.Month == Month).Select(c => c.Amount).Sum();
            }

            if (totalSale >= salary.IncentiveTarget)
            {
                decimal incentive = totalSale * salary.IncentiveRate;
                return incentive;
            }
            else
            {
                decimal per = (totalSale * 100) / salary.IncentiveTarget;
                decimal incentive = totalSale * salary.IncentiveRate;

                if (per >= 50)
                {
                    incentive = incentive * (decimal)0.75;
                }
                else
                {
                    incentive = incentive * ((per * (decimal)1.25) / 100);
                }

                return incentive;
            }
        }

        /// <summary>
        /// Get Salesman Name for Given EmployeeId
        /// </summary>
        /// <param name="db"></param>
        /// <param name="EmpId"></param>
        /// <returns></returns>
        private int GetSalesManId(AprajitaRetailsContext db, int EmpId)
        {
            string Name = db.Employees.Find(EmpId).StaffName;
            int smId = db.Salesmen.Where(c => c.SalesmanName == Name).Select(c => c.SalesmanId).FirstOrDefault();
            return smId;
        }

        /// <summary>
        /// calculate basic Salary
        /// </summary>
        /// <param name="salary"></param>
        /// <param name="NetPresentDay"></param>
        /// <param name="WorkingDay"></param>
        /// <param name="Sunday"></param>
        /// <returns></returns>
        private decimal CalculateBasicSalary(CurrentSalary salary, double NetPresentDay, int WorkingDay, int Sunday)
        {
            if (salary.IsSundayBillable)
            {
                int day = WorkingDay - Sunday;
                decimal ratePerDay = salary.BasicSalary / day;
                decimal basicSalary = ratePerDay * (decimal)NetPresentDay;
                return basicSalary;
            }
            else if (salary.IsTailoring == true) // Can be used for full days working
            {
                decimal ratePerDay = salary.BasicSalary / WorkingDay;
                decimal basicSalary = ratePerDay * (decimal)NetPresentDay;
                return basicSalary;
            }
            else
            {
                int day = WorkingDay - Sunday;
                decimal ratePerDay = salary.BasicSalary / day;
                decimal basicSalary = ratePerDay * (decimal)NetPresentDay;
                return basicSalary;
            }
        }
    }
}