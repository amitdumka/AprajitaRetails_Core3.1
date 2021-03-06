﻿//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AprajitaRetails.Areas.Voyager.Models;
using System;
using System.ComponentModel;
//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Models
{
    public class SalaryPayment
    {
        public int SalaryPaymentId { get; set; }

        [Display(Name = "Staff Name")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Display(Name = "Salary/Year")]
        public string SalaryMonth { get; set; }

        [Display(Name = "Payment Reason")]
        public SalaryComponet SalaryComponet { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Payment Mode")]
        public PayMode PayMode { get; set; }

        public string Details { get; set; }
        //Version 3.0
        [DefaultValue(1)]
        public int? StoreId { get; set; }
        public virtual Store Store { get; set; }

        public string UserName { get; set; }

    }



}
