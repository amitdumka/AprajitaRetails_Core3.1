//using System.Data.Entity;
//using TAS_AprajiataRetails.Models.Data;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Models.Helpers
{
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

    public class DayMonthHelper
    {
        // private EndofDayDetails ProcessEndOfDay(AprajitaRetailsContext db, EndOfDay day)
        //{

        //    // Process opening & closing balance of day.
        //    // Utils.ProcessOpenningClosingBalance(db, day.EOD_Date, true);
        //    // Utils.ProcessOpenningClosingBankBalance(db, day.EOD_Date, true); //TODO: Check in future

        //    //Create Next day openning Balance
        //    Utils.CreateNextDayOpenningBalance(db, day.EOD_Date, true);
        //    // process for a report to be sms

        //    var todaySale = db.DailySales.Where(c =>(c.SaleDate) ==(day.EOD_Date));
        //    decimal totalExp = 0;
        //    decimal tRec = 0;
        //    decimal tPay = 0;
        //    totalExp = (decimal?)db.Expenses.Where(c =>(c.ExpDate) ==(day.EOD_Date)).Sum(c => (decimal?)c.Amount) ?? 0;
        //    totalExp += (decimal?)db.CashExpenses.Where(c =>(c.ExpDate) ==(day.EOD_Date)).Sum(c => (decimal?)c.Amount) ?? 0;

        //    tPay = (decimal?)db.Payments.Where(c =>(c.PayDate) ==(day.EOD_Date)).Sum(c => (decimal?)c.Amount) ?? 0;
        //    tPay += (decimal?)db.CashPayments.Where(c =>(c.PaymentDate) ==(day.EOD_Date)).Sum(c => (decimal?)c.Amount) ?? 0;
        //    tPay += (decimal?)db.Salaries.Where(c =>(c.PaymentDate) ==(day.EOD_Date)).Sum(c => (decimal?)c.Amount) ?? 0;
        //   


        //    tRec = (decimal?)db.Receipts.Where(c =>(c.RecieptDate) ==(day.EOD_Date)).Sum(c => (decimal?)c.Amount) ?? 0;
        //    tRec += (decimal?)db.CashReceipts.Where(c =>(c.InwardDate) ==(day.EOD_Date)).Sum(c => (decimal?)c.Amount) ?? 0;
        //    tRec += (decimal?)db.Withdrawals.Where(c =>(c.DepoDate) ==(day.EOD_Date)).Sum(c => (decimal?)c.Amount) ?? 0;
        //    tRec += (decimal?)db.StaffAdvanceReceipts.Where(c =>(c.ReceiptDate) ==(day.EOD_Date)).Sum(c => (decimal?)c.Amount) ?? 0;


        //    EndofDayDetails details = new EndofDayDetails()
        //    {

        //        TodaySale = (decimal?)todaySale.Sum(c => (decimal?)c.Amount) ?? 0,
        //        TodayCardSale = (decimal?)todaySale.Where(c => c.PayMode == PayMode.Card).Sum(c => (decimal?)c.Amount) ?? 0,
        //        TodayManualSale = (decimal?)todaySale.Where(c => c.IsManualBill == true).Sum(c => (decimal?)c.Amount) ?? 0,
        //        TodayOtherSale = (decimal?)todaySale.Where(c => c.PayMode != PayMode.Card && c.PayMode != PayMode.Cash).Sum(c => (decimal?)c.Amount) ?? 0,
        //        TodayTailoringSale = (decimal?)todaySale.Where(c => c.IsTailoringBill == true).Sum(c => (decimal?)c.Amount) ?? 0,
        //        TodayCashInHand = (decimal?)db.CashInHands.Where(c =>(c.CIHDate) ==(day.EOD_Date)).FirstOrDefault().InHand ?? 0,
        //        TodayTailoringBooking = (int?)db.Bookings.Where(c =>(c.BookingDate) ==(day.EOD_Date)).Count() ?? 0,
        //        TodayTotalUnit = (int?)db.Bookings.Where(c =>(c.BookingDate) ==(day.EOD_Date)).Sum(c => (int?)c.TotalQty) ?? 0,
        //        TodayTotalExpenses = totalExp,
        //        TotalPayments = tPay,
        //        TotalReceipts = tRec,
        //        Access = day.Access,
        //        CashInHand = day.CashInHand,
        //        EOD_Date = day.EOD_Date,
        //        FM_Arrow = day.FM_Arrow,
        //        RWT = day.RWT,
        //        Shirting = day.Shirting,
        //        Suiting = day.Suiting,
        //        USPA = day.USPA

        //    };



        //     return details;



        // }

    }


}