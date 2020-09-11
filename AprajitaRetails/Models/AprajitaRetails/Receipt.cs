using Microsoft.AspNetCore.Authorization;    using System;
using System.ComponentModel;
//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AprajitaRetails.Areas.Voyager.Models;

namespace AprajitaRetails.Models
{
    public class Receipt
    {
        public int ReceiptId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expense Date")]
        public DateTime RecieptDate { get; set; }

        [Display(Name = "Receipt From ")]
        public string ReceiptFrom { get; set; }


        [Display(Name = "Payment Mode")]
        public PaymentModes PayMode { get; set; }
        [Display(Name = "Receipt Details ")]
        public string ReceiptDetails { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }
        [Display(Name = "Receipt Slip No ")]
        public string RecieptSlipNo { get; set; }
        public string Remarks { get; set; }
        [Display(Name = "Party")]
        public int? PartyId { get; set; }
        //Version 3.0
        [DefaultValue(1)]
        public int? StoreId { get; set; }
        public virtual Store Store { get; set; }

        public string UserName { get; set; }

    }



}
