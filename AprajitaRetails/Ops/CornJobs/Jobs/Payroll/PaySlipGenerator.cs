using System;
using System.Collections.Generic;
using System.Linq;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Models.Helpers;
using AprajitaRetails.Ops.TAS.Mails;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Ops.CornJobs.Jobs.Payroll
{
    public sealed class ErrorMessage
    {
        public const string AttendanceRecordNotFound = "Attendance Record Not Found";
        public const string NoofDaysNotMatched = "No of Day Not Matched#";
        public const string PaySlipAlreadyGenerated = "Payslip already Generated!";
        public const string SalaryHeadNotFound = "Salary Head Not Found";
    }

    /// <summary>
    /// It will generate PaySlip on 2nd of every month.
    /// </summary>
    public class PaySlipGenerator
    {
        public void EmailPaySlip(AprajitaRetailsContext db, List<SalaryHead> salaryHeads)
        {
            //TODO: here deduction need to be handled.
            string MonthOf = salaryHeads.First ().Month + "/" + salaryHeads.First ().Year;
            string toAddress = "amitnarayansah@gmail.com,thearvindstoredumka@gmail.com";
            string msg = $"Payslip auto generated for Month of {MonthOf}. \n\n";
            int count = 1;
            foreach ( var item in salaryHeads )
            {
                msg += $"#{count++}.\nPayslip of  {item.StaffName} for the Month of {MonthOf}.\n";
                msg += $"No of Days Present: {item.NoOfDaysPresent} \t Gross Salary: {item.GrossSalary:#.##}\n";
                if ( item.EmpType == EmpType.Salesman )
                {
                    msg += $"Total Sale         : {item.TotalSale:#.##} \t Incentive: {item.Incentive:#.##}\n";
                    msg += $"Total WOWBill Value: {item.TotalWOWBillSaleAmount:#.##} \t WOWBill Incentive: {item.WowBill:#.##}\n";
                    msg += $"Total LastPcs   Value: {item.TotalLastPcsValue:#.##} \t Last Pcs Incentive: {item.LastPcs:#.##}\n";
                }
                if ( item.EmpType == EmpType.StoreManager )
                {
                    msg += $"Total Sale: {item.TotalSale:#.##} \t Incentive: {item.Incentive:#.##}\n";
                }

                msg += $"Basic Salary: {item.BasicSalary:#.##} \n";
                if ( item.IsTailoring )
                {
                    msg += "Payslip for Tailoring Division\n";
                }
                msg += $"\n\n Net Payable Salary: {item.GrossSalary:#.##}\n Your Salary Advance has not been taken into consideration, hence it will be deducted at actuals from Gross Salary.\n";
                msg += "=========================================================================================\n\n";
            }

            msg += "End of Mail. \n This eMail is electronically generated.";

            MyMail.SendEmails ($"PaySlip Generated For Month {MonthOf}.", msg, toAddress);
        }

        /// <summary>
        /// Generate Last Month PaySlip for All Employees
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public List<SalaryHead> GeneratePaySlip(AprajitaRetailsContext db)
        {
            return GeneratePaySlip (db, DateTime.Today.AddMonths (-1).Month, DateTime.Today.AddMonths (-1).Year);
        }

        /// <summary>
        /// Generate Payslip for given Month/Year for All Employee
        /// </summary>
        /// <param name="db"></param>
        /// <param name="Month"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public List<SalaryHead> GeneratePaySlip(AprajitaRetailsContext db, int Month, int Year)
        {
            List<SalaryHead> list = new List<SalaryHead> ();
            var empList = db.Employees.Where (c => c.IsWorking).Select (c => c.EmployeeId).ToList ();
            foreach ( var item in empList )
            {
                list.Add (GeneratePaySlip (db, item, Month, Year));
            }

            return list;
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
            SalaryHead head = new SalaryHead () { EmpId = EmpID, Month = Month, Year = Year, IsError = false, ErrMsg = "" };
            var att = db.Attendances.Include (c => c.Employee).Where (c => c.EmployeeId == EmpID && c.AttDate.Year == Year && c.AttDate.Month == Month).ToList ();
            if ( IsPaySlipGenerated (db, EmpID, Year, Month) )
            {

                head.StaffName = db.Employees.Find (EmpID).StaffName;
                head.IsError = true;
                head.ErrMsg = ErrorMessage.PaySlipAlreadyGenerated;
            }
            else if ( att != null && att.Count > 0 )
            {
                int present = att.Where (c => c.Status == AttUnit.Present || c.Status == AttUnit.Sunday || c.Status == AttUnit.SundayHoliday).Count ();
                int absent = att.Where (c => c.Status == AttUnit.Absent).Count ();
                int halfday = att.Where (c => c.Status == AttUnit.HalfDay).Count ();
                int holiday = att.Where (c => c.Status == AttUnit.Holiday || c.Status == AttUnit.StoreClosed).Count ();

                int totaldays = present + absent + halfday + holiday;
                int noofdays = DateTime.DaysInMonth (Year, Month);
                int NoOfSunday = DateHelper.CountDays (DayOfWeek.Sunday, new DateTime (Year, Month, 1));

                head.StaffName = att.First ().Employee.StaffName;
                head.WorkingDays = noofdays;

                if ( totaldays == noofdays )
                {
                    double netdayPresent = present + ( halfday / 2 ) + holiday;
                    head.NoOfDaysPresent = netdayPresent;

                    var salary = GetCurrentSalary (db, Year, Month, EmpID);

                    if ( salary != null )
                    {
                        head.SalaryId = salary.CurrentSalaryId;
                        var empType = att.First ().Employee.Category;

                        switch ( empType )
                        {
                            case EmpType.Salesman:
                                int SMID = GetSalesManId (db, EmpID);
                                head = CalculateBasicSalary (salary, head, netdayPresent, noofdays, NoOfSunday);
                                head = CalculateIncentive (db, salary, head, Month, Year, SMID, true);
                                head = CalculateWowBill (db, salary, head, SMID, Year, Month);
                                head = CalculateLastPcs (db, salary, head, Year, Month, SMID);
                                break;

                            case EmpType.StoreManager:

                                head = CalculateBasicSalary (salary, head, netdayPresent, noofdays, NoOfSunday);
                                head = CalculateIncentive (db, salary, head, Month, Year, 0, true);
                                break;

                            case EmpType.HouseKeeping:
                                head = CalculateBasicSalary (salary, head, netdayPresent, noofdays, NoOfSunday);
                                break;

                            case EmpType.Accounts:
                                head = CalculateBasicSalary (salary, head, netdayPresent, noofdays, NoOfSunday);
                                break;

                            case EmpType.TailorMaster:

                            case EmpType.Tailors:

                            case EmpType.TailoringAssistance:
                                head = CalculateBasicSalary (salary, head, netdayPresent, noofdays, NoOfSunday);
                                break;
                        }
                    }
                    else
                    {
                        head.IsError = true;
                        head.ErrMsg = ErrorMessage.SalaryHeadNotFound;
                    }
                }
                else if ( totaldays < noofdays )
                {
                    //TODO: check for joining date.
                    if ( IsNewEmployee (db, EmpID, Month, Year) )
                    {
                        double netdayPresent = present + ( halfday / 2 )+holiday;
                        head.NoOfDaysPresent = netdayPresent;
                        var salary = GetCurrentSalary (db, Year, Month, EmpID);

                        if ( salary != null )
                        {
                            head.SalaryId = salary.CurrentSalaryId;
                            var empType = att.First ().Employee.Category;

                            switch ( empType )
                            {
                                case EmpType.Salesman:
                                    int SMID = GetSalesManId (db, EmpID);
                                    head = CalculateBasicSalary (salary, head, netdayPresent, noofdays, NoOfSunday);
                                    head = CalculateIncentive (db, salary, head, Month, Year, SMID, true);
                                    head = CalculateWowBill (db, salary, head, SMID, Year, Month);
                                    head = CalculateLastPcs (db, salary, head, Year, Month, SMID);
                                    break;

                                case EmpType.StoreManager:

                                    head = CalculateBasicSalary (salary, head, netdayPresent, noofdays, NoOfSunday);
                                    head = CalculateIncentive (db, salary, head, Month, Year, 0, true);
                                    break;

                                case EmpType.HouseKeeping:
                                    head = CalculateBasicSalary (salary, head, netdayPresent, noofdays, NoOfSunday);
                                    break;

                                case EmpType.Accounts:
                                    head = CalculateBasicSalary (salary, head, netdayPresent, noofdays, NoOfSunday);
                                    break;

                                case EmpType.TailorMaster:

                                case EmpType.Tailors:

                                case EmpType.TailoringAssistance:
                                    head = CalculateBasicSalary (salary, head, netdayPresent, noofdays, NoOfSunday);
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
                    head.ErrMsg = ErrorMessage.NoofDaysNotMatched;
                    int diff = noofdays - totaldays;
                    head.ErrMsg += "D" + diff;
                }
            }
            else
            {
                head.StaffName = db.Employees.Find (EmpID).StaffName;
                head.IsError = true;
                head.ErrMsg = ErrorMessage.AttendanceRecordNotFound;
            }
            return head;
        }

        public bool IsNewEmployee(AprajitaRetailsContext db, int empId, int month, int year)
        {
            var empjoiningdate = db.Employees.Find (empId).JoiningDate.Date;
            if ( empjoiningdate.Year == year )
            {
                if ( empjoiningdate.Month == month )
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public string ProcessPaySlip(AprajitaRetailsContext db)
        {
            // DateTime forDate = DateTime.Today.AddMonths(-1).Date;
            List<SalaryHead> salaryHeads = GeneratePaySlip (db);
            string errMsg = $"PaySlip AutoGeneration Report:\n On {DateTime.Now}\n Total Count: {salaryHeads.Count}\n";
            foreach ( var item in salaryHeads )
            {
                if ( !item.IsError )
                {
                    PaySlip slip = new PaySlip
                    {
                        CurrentSalaryId = item.SalaryId,
                        BasicSalary = item.BasicSalary,
                        EmployeeId = item.EmpId,
                        IsTailoring = item.IsTailoring,
                        LastPcsAmount = item.TotalLastPcsValue,
                        LastPCsIncentive = item.LastPcs,
                        OnDate = DateTime.Today.Date,
                        Month = item.Month,
                        Year = item.Year,
                        TotalSale = item.TotalSale,
                        NoOfDaysPresent = (int) item.NoOfDaysPresent,//TODO: need to convert to double place
                        Remarks = "AutoGenerated",
                        AdvanceDeducations = 0,
                        OtherDeductions = 0,
                        TDSDeductions = 0,
                        OthersIncentive = 0,
                        PFDeductions = 0,
                        WOWBillAmount = item.TotalWOWBillSaleAmount,
                        WOWBillIncentive = item.WowBill,
                        UserName = "AutoAdded",
                        StandardDeductions = 0,
                        SaleIncentive = item.Incentive
                    };
                    slip.GrossSalary = slip.SaleIncentive + slip.BasicSalary + slip.WOWBillIncentive + slip.LastPCsIncentive + slip.OthersIncentive;
                    slip.GrossSalary -= ( slip.TDSDeductions - slip.StandardDeductions - slip.PFDeductions - slip.OtherDeductions - slip.AdvanceDeducations );
                    item.GrossSalary = slip.GrossSalary;
                    db.PaySlips.Add (slip);
                }
                else
                {
                    errMsg += $"\nError Occurred On {DateTime.Now}\n Error Details Below.\n {item.ErrMsg}\n. Hash {item.ToString ()}";
                    //TODO: Error Handling and Reporting need to be done here
                }
            }
            db.SaveChanges ();
            MyMail.SendEmail ("Error Occurred in Payslip Generation", errMsg, "amitnarayansah@gmail.com");
            EmailPaySlip (db, salaryHeads);
            return errMsg;
        }
        /// <summary>
        /// calculate basic Salary
        /// </summary>
        /// <param name="salary"></param>
        /// <param name="NetPresentDay"></param>
        /// <param name="WorkingDay"></param>
        /// <param name="Sunday"></param>
        /// <returns></returns>
        private SalaryHead CalculateBasicSalary(CurrentSalary salary, SalaryHead head, double NetPresentDay, int WorkingDay, int Sunday)
        {
            if ( salary.IsSundayBillable )
            {
                decimal ratePerDay = salary.BasicSalary / WorkingDay;
                head.BasicSalary = ratePerDay * ( (decimal) ( NetPresentDay + Sunday ) );
                head.IsTailoring = false;
            }
            else if ( salary.IsTailoring == true ) // Can be used for full days working
            {
                decimal ratePerDay = salary.BasicSalary / WorkingDay;
                decimal basicSalary = ratePerDay * (decimal) NetPresentDay;
                head.BasicSalary = basicSalary;
                head.IsTailoring = true;
            }
            else
            {
                decimal ratePerDay = salary.BasicSalary / WorkingDay;
                decimal basicSalary = ratePerDay * (decimal) NetPresentDay;
                head.BasicSalary = basicSalary;
                head.IsTailoring = false;
            }
            return head;
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
        private SalaryHead CalculateIncentive(AprajitaRetailsContext db, CurrentSalary salary, SalaryHead head, int Month, int Year, int SMID, bool IsSM)
        {
            //TODO: Now Considering DailySale late have to consider manual Sale and regualSale

            decimal totalSale = 0;
            if ( IsSM )
            {
                totalSale = db.DailySales.Where (c => !c.IsManualBill && c.SaleDate.Year == Year && c.SaleDate.Month == Month).Select (c => c.Amount).Sum ();
            }
            else
            {
                totalSale = db.DailySales.Where (c => !c.IsManualBill && c.SalesmanId == SMID && c.SaleDate.Year == Year && c.SaleDate.Month == Month).Select (c => c.Amount).Sum ();
            }

            head.TotalSale = totalSale;

            if ( totalSale >= salary.IncentiveTarget )
            {
                head.Incentive = totalSale * salary.IncentiveRate;
            }
            else
            {
                decimal per = ( totalSale * 100 ) / salary.IncentiveTarget;
                decimal incentive = totalSale * salary.IncentiveRate;

                if ( per >= 50 )
                {
                    incentive = incentive * (decimal) 0.75;
                }
                else
                {
                    incentive = incentive * ( ( per * (decimal) 1.25 ) / 100 );
                }

                head.Incentive = incentive;
            }

            return head;
        }

        private SalaryHead CalculateLastPcs(AprajitaRetailsContext db, CurrentSalary salary, SalaryHead head, int Year, int Month, int SMID)
        {
            //TODO: implement Last Pcs
            return head;
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
        private SalaryHead CalculateWowBill(AprajitaRetailsContext db, CurrentSalary salary, SalaryHead head, int SMID, int Year, int Month)
        {
            var wowbill = db.DailySales.Where (c => c.IsManualBill == false && c.SalesmanId == SMID && c.SaleDate.Year == Year && c.SaleDate.Month == Month && c.Amount >= salary.WOWBillTarget).Select (c => c.Amount).Sum ();

            head.Incentive = wowbill * salary.WOWBillRate;
            head.TotalWOWBillSaleAmount = wowbill;
            return head;
        }
        /// <summary>
        /// Get Current Salary Structure
        /// </summary>
        /// <param name="db"></param>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="EmpId"></param>
        /// <returns></returns>
        private CurrentSalary GetCurrentSalary(AprajitaRetailsContext db, int Year, int Month, int EmpId)
        {
            //TODO: Bug there is bug in calculation of salary  head
            var sal = db.CurrentSalaries.Where (c => c.EmployeeId == EmpId && c.EffectiveDate.Year <= Year && c.EffectiveDate.Month <= Month).OrderByDescending (c => c.CurrentSalaryId).ToList ();

            if ( sal.Count > 1 )
            {
                foreach ( var item in sal )
                {
                    if ( item.CloseDate != null )
                    {
                        if ( item.CloseDate.Value.Year <= Year && item.CloseDate.Value.Month <= Month )
                            return item;
                    }
                    else
                    {
                        return item;
                    }
                }
                return sal.OrderByDescending (c => c.CurrentSalaryId).First ();
            }
            else if ( sal.Count == 1 )
            {
                return sal.First ();
            }
            else
                return null;
        }
        /// <summary>
        /// Get Salesman Name for Given EmployeeId
        /// </summary>
        /// <param name="db"></param>
        /// <param name="EmpId"></param>
        /// <returns></returns>
        private int GetSalesManId(AprajitaRetailsContext db, int EmpId)
        {
            string Name = db.Employees.Find (EmpId).StaffName;
            int smId = db.Salesmen.Where (c => c.SalesmanName == Name).Select (c => c.SalesmanId).FirstOrDefault ();
            return smId;
        }
        /// <summary>
        /// Verify if payslip for desired month is generated or not
        /// </summary>
        /// <param name="db"></param>
        /// <param name="EmpId"></param>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        private bool IsPaySlipGenerated(AprajitaRetailsContext db, int EmpId, int Year, int Month)
        {
            try
            {
                int data = db.PaySlips.Where (c => c.EmployeeId == EmpId && c.Year == Year && c.Month == Month).Select (c => c.PaySlipId).FirstOrDefault ();
                if ( data > 0 )
                    return true;
                else
                    return false;
            }
            catch ( Exception )
            {
                return false;
            }
        }

    }

    public class SalaryHead
    {
        public decimal BasicSalary { get; set; }
        public int EmpId { get; set; }
        public EmpType EmpType { get; set; }
        public string ErrMsg { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal Incentive { get; set; }
        public bool IsError { get; set; }
        public bool IsTailoring { get; set; }
        public decimal LastPcs { get; set; }
        public int Month { get; set; }
        public double NoOfDaysPresent { get; set; }
        public int SalaryId { get; set; }
        public string StaffName { get; set; }
        public decimal TotalLastPcsValue { get; set; }
        public decimal TotalSale { get; set; }
        public decimal TotalWOWBillSaleAmount { get; set; }
        public int WorkingDays { get; set; }
        public decimal WowBill { get; set; }
        public int Year { get; set; }
    }
}