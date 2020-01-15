using System;
//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace AprajitaRetails.Models
{
    //Income 
    public class DailySale
    {
        public int DailySaleId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Sale Date")]
        public DateTime SaleDate { get; set; }

        [Display(Name = "Invoice No")]
        public string InvNo { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Payment Mode")]
        public PayModes PayMode { get; set; }

        [Display(Name = "Cash Amount")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CashAmount { get; set; }

        [ForeignKey("Salesman")]
        public int SalesmanId { get; set; }
        public virtual Salesman Salesman { get; set; }

        [Display(Name = "Is Due")]
        public bool IsDue { get; set; }

        [Display(Name = "Is Manual Bill")]
        public bool IsManualBill { get; set; }

        [Display(Name = "Is Tailoring Bill")]
        public bool IsTailoringBill { get; set; }

        [Display(Name = "Is Sale Return")]
        public bool IsSaleReturn { get; set; }
        public string Remarks { get; set; }

        //public virtual DuesList DuesList { get; set; }

    }

    //TODO: List
    //TODO: Dues Recovery options
    //TODO: Tailoring 
    //TODO: Sales return policy update and check 
    //TODO: Purchase of Items/Assets
    //TODO: Arvind Payments
    //TODO: Purchase Invoice Entry

}
