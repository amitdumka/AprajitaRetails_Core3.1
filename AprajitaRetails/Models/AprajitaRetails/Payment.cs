using System;
using System.ComponentModel;
//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace AprajitaRetails.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Payment Date")]
        public DateTime PayDate { get; set; }

        [Display(Name = "Payment Party")]
        public string PaymentPartry { get; set; }
        [Display(Name = "Payment Mode")]
        public PaymentModes PayMode { get; set; }
        [Display(Name = "Payment Details")]
        public string PaymentDetails { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }
        [Display(Name = "Payment Slip No")]
        public string PaymentSlipNo { get; set; }

        public string Remarks { get; set; }

        [Display(Name ="Party")]
        public int? PartyId { get; set; }
        //Version 3.0
        [DefaultValue(1)]
        public int? StoreLocationId { get; set; }
        public virtual StoreLocation Store { get; set; }

    }

  

}
