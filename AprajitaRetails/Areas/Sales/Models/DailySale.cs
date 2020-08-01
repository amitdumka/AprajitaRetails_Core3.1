using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Models
{
    //Version 3.0 
    // Added Feature
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

        [Display(Name = "Due")]
        public bool IsDue { get; set; }

        [Display(Name = "Manual Bill")]
        public bool IsManualBill { get; set; }

        [Display(Name = "Tailoring Bill")]
        public bool IsTailoringBill { get; set; }

        [Display(Name = "Sale Return")]
        public bool IsSaleReturn { get; set; }

        public string Remarks { get; set; }

        [DefaultValue(false)]
        public bool IsMatchedWithVOy { get; set; }

        //public virtual DuesList DuesList { get; set; }


        //Version 3.0
        [DefaultValue(1)]
        public int? StoreLocationId { get; set; }
        public virtual StoreLocation Store {get;set;}
    }




}