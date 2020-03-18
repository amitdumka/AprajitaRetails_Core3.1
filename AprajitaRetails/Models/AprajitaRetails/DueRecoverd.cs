using System;
//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace AprajitaRetails.Models
{
    public class DueRecoverd
    {
        public int DueRecoverdId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Recovery Date")]
        public DateTime PaidDate { get; set; }

        public int DuesListId { get; set; }
        public virtual DuesList DuesList {get;set;}
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal AmountPaid { get; set; }
        [Display(Name ="Is Partial Payment")]
        public bool IsPartialPayment { get; set; }
        public PaymentModes Modes { get; set; }
        public string Remarks { get; set; }

    }

   

}
