﻿using System;
using System.ComponentModel;
//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AprajitaRetails.Areas.Voyager.Models;

namespace AprajitaRetails.Models
{
    public class Expense
    {
        public int ExpenseId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expense Date")]
        public DateTime ExpDate { get; set; }

        public string Particulars { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Payment Mode")]
        public PaymentModes PayMode { get; set; }

        [Display(Name = "Payment Details")]
        public string PaymentDetails { get; set; }

        [Display(Name = "Paid By")]
        public int EmployeeId { get; set; }
        public virtual Employee PaidBy { get; set; }

        [Display(Name = "Paid To")]
        public string PaidTo { get; set; }

        public string Remarks { get; set; }

        [Display(Name = "Party")]
        public int? PartyId { get; set; }
        //Version 3.0
        [DefaultValue(1)]
        public int? StoreId { get; set; }
        public virtual Store Store { get; set; }

    }



}