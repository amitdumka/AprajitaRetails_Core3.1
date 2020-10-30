//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AprajitaRetails.Areas.Voyager.Models;
using System;
using System.ComponentModel;
//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Models
{
    public class Expense
    {
        public int ExpenseId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expense Date")]
        public DateTime ExpDate { get; set; }//Ok

        public string Particulars { get; set; }//Ok

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }//Ok

        [Display(Name = "Payment Mode")]
        public PaymentMode PayMode { get; set; }//Ok

        [Display(Name = "Payment Details")]
        public string PaymentDetails { get; set; }//Ok

        [Display(Name = "Paid By")]
        public int EmployeeId { get; set; }//Ok
        public virtual Employee PaidBy { get; set; }//Ok

        [Display(Name = "Paid To")]
        public string PaidTo { get; set; }//Ok

        public string Remarks { get; set; }//Ok

        [Display(Name = "Party")]
        public int? PartyId { get; set; }//Ok
        //Version 3.0
        [DefaultValue(1)]
        public int? StoreId { get; set; } //Ok
        public virtual Store Store { get; set; }//Ok

        public string UserName { get; set; }//Ok

    }



}
