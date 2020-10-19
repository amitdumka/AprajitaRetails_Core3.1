using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AprajitaRetails.Models;
using AprajitaRetails.Areas.Reports.Models;

namespace AprajitaRetails.Models.ViewModels
{
    public class ManaulSaleReport
    {
        public decimal DailySale { get; set; }
        public decimal MonthlySale { get; set; }
        public decimal YearlySale { get; set; }
        public decimal PendingSale { get; set; }
        public decimal SaleAdjustest { get; set; }
        public decimal TotalFixedSale { get; set; }
    }


    public class MasterViewReport
    {
        public DailySaleReport SaleReport { get; set; }
        public TailoringReport TailoringReport { get; set; }
        public List<EmployeeInfo> EmpInfoList { get; set; }
        // public ManaulSaleReport ManaulSale { get; set; }
        //public List<EmpStatus> PresentEmp { get; set; }
        public AccountsInfo AccountsInfo { get; set; }
        public List<BookingOverDue>? BookingOverDues { get; set; }

    }


    public class TailoringReport
    {
        [Display(Name = "Today")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TodaySale { get; set; }
        [Display(Name = "Montly")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal MonthlySale { get; set; }
        [Display(Name = "Yearly")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal YearlySale { get; set; }
        //public decimal QuaterlySale { get; set; }

        [Display(Name = "Booking")]
        public decimal TodayBooking { get; set; }
        [Display(Name = "Item")]
        public decimal TodayUnit { get; set; }
        [Display(Name = "Booking")]
        public decimal MonthlyBooking { get; set; }
        [Display(Name = "Item")]
        public decimal MonthlyUnit { get; set; }
        [Display(Name = "Booking")]
        public decimal YearlyBooking { get; set; }
        [Display(Name = "Item")]
        public decimal YearlyUnit { get; set; }
    }

    public class DailySaleReport
    {

        [Display(Name = "Today")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal DailySale { get; set; }

        [Display(Name = "Monthly")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal MonthlySale { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        [Display(Name = "Yearly")]
        public decimal YearlySale { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        [Display(Name = "Weekly")]
        public decimal WeeklySale { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        [Display(Name = "Quarterly")]
        public decimal QuaterlySale { get; set; }

    }

    public class EmployeeInfo
    {
        [Display(Name = "Emp ID")]
        public int EmpId { get; set; }
        [Display(Name = "Staff Name")]
        public string Name { get; set; }

        [Display(Name = "Present Today")]
        public string Present { get; set; }

        [Display(Name = "No of Days Present")]
        public double PresentDays { get; set; }

        [Display(Name = "No of Days Absent")]
        public double AbsentDays { get; set; }

        [Display(Name = "Ratio Of Attendance")]
        public double Ratio { get; set; }

        [Display(Name = "Current Month Sale")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalSale { get; set; }

        [Display(Name = "No Of Bills")]
        public int NoOfBills { get; set; }
        public bool IsSalesman { get; set; }

    }

    public class EmpBasicInfo
    {
        [Display(Name = "Emp ID")]
        public int EmpId { get; set; }
        [Display(Name = "Staff Name")]
        public string Name { get; set; }

        //[Display(Name = "Present Today")]
        //public string Present { get; set; }
        [Display(Name = "Current Month Sale")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalSale { get; set; }
        public bool IsSalesman { get; set; }

    }


    public class EmpStatus
    {
        public string StaffName { get; set; }
        public bool IsPresent { get; set; }
    }

    public class EndofDayDetails
    {
        // public EndOfDay EndofDay { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "DSR Date")]
        public DateTime EOD_Date { get; set; }

        public float Shirting { get; set; }
        public float Suiting { get; set; }
        public int USPA { get; set; }

        [Display(Name = "FM/Arrow/Others")]
        public int FM_Arrow { get; set; }

        [Display(Name = "Arvind RTW")]
        public int RWT { get; set; }

        [Display(Name = "Accessories")]
        public int Access { get; set; }
        [Display(Name = "Cash at Store")]
        public decimal CashInHand { get; set; }

        [Display(Name = "Total Sale")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TodaySale { get; set; }
        [Display(Name = "Card Sale")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TodayCardSale { get; set; }
        [Display(Name = "OtherMode Sale")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TodayOtherSale { get; set; }
        [Display(Name = "Manual Sale")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TodayManualSale { get; set; }

        [Display(Name = "Tailoring Delivery Sale")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TodayTailoringSale { get; set; }
        [Display(Name = "Tailoring Booking ")]
        public int TodayTailoringBooking { get; set; }
        [Display(Name = "Total Unit")]
        public int TodayTotalUnit { get; set; }

        [Display(Name = "Total Expenses")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TodayTotalExpenses { get; set; }
        [Display(Name = "Total Payments")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalPayments { get; set; }
        [Display(Name = "Total Receipts")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalReceipts { get; set; }

        [Display(Name = "Cash In Hand")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TodayCashInHand { get; set; }


    }

    public class AccountsInfo
    {
        [Display(Name = "Cash-In-Hand")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CashInHand { get; set; }
        [Display(Name = "Opening Balance")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal OpenningBal { get; set; }
        [Display(Name = "Income")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CashIn { get; set; }
        [Display(Name = "Expenses")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CashOut { get; set; }
        [Display(Name = "Cash Payments")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalCashPayments { get; set; }
        [Display(Name = "Bank Balance")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CashInBank { get; set; }
        [Display(Name = "Deposited")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CashToBank { get; set; }
        [Display(Name = "Withdrawal")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CashFromBank { get; set; }
    }


    public class CashBook
    {
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Date")]
        public DateTime EDate { get; set; }
        public string Particulars { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CashIn { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CashOut { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CashBalance { get; set; }
    }



    public class IncomeExpensesReport
    {
        public int IncomeExpensesReportId { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]

        public DateTime OnDate { get; set; }

        //Income
        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Total Sale")]
        public decimal TotalSale { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Tailoring Sale")]
        public decimal TotalTailoringSale { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Manual Sale")]
        public decimal TotalManualSale { get; set; }


        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Total Receipts")]
        public decimal TotalRecipts { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Cash Receipts")]
        public decimal TotalCashRecipts { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Other Income")]
        public decimal TotalOtherIncome { get; set; }


        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Total Income")]
        public decimal TotalIncome { get { return (TotalSale + TotalTailoringSale + TotalManualSale + TotalRecipts + TotalCashRecipts + TotalOtherIncome); } }


        //Expenses
        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Staff Payments")]
        public decimal TotalStaffPayments { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Tailor Payments")]
        public decimal TotalTailoringPayments { get; set; }


        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Expenses")]
        public decimal TotalExpenses { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Home Expenses")]
        public decimal TotalHomeExpenses { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Total Payments")]
        public decimal TotalPayments { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Cash Payments")]
        public decimal TotalCashPayments { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Other Expenses")]
        public decimal TotalOthersExpenses { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Total Expenses")]
        public decimal TotalAllExpenses { get { return (TotalStaffPayments + TotalTailoringPayments + TotalExpenses + TotalPayments + TotalCashPayments + TotalOthersExpenses + TotalHomeExpenses); } }



        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Total Dues")]
        public decimal TotalDues { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Dues Recovered")]
        public decimal TotalRecovery { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Pending Dues")]
        public decimal TotalPendingDues { get; set; }

    }


    public class IERVM
    {
        public IncomeExpensesReport Today { get; set; }
        public IncomeExpensesReport CurrentWeek { get; set; }
        public IncomeExpensesReport Monthly { get; set; }
        public IncomeExpensesReport Yearly { get; set; }
    }

    public class IncomeExpensesVM
    {
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Date")]
        public DateTime OnDate { get; set; }
        public string Particulars { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }
        public bool IsNonCash { get; set; }
    }

    public class DetailIEVM
    {
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OnDate { get; set; }
        public IERVM IncomeExpenseRepot { get; set; }
        public ICollection<IncomeExpensesVM> Income { get; set; }  // Details of Current Day/ or week
        public ICollection<IncomeExpensesVM> Expenses { get; set; } //Details of Current Day/ or week
    }

}
