using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Areas.Voyager.Models
{
    public class SaleInvoice
    {
        public int SaleInvoiceId { get; set; } //Pk

        [Display(Name = "Customer Name")]
        public int CustomerId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true), Display(Name = "Sale Date")]
        public DateTime OnDate { get; set; }

        public string InvoiceNo { get; set; }
        [Display(Name = "Total Items")]
        public int TotalItems { get; set; }
        [Display(Name = "Qty")]
        public double TotalQty { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Bill Amt")]
        public decimal TotalBillAmount { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Discount")]
        public decimal TotalDiscountAmount { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Round off")]
        public decimal RoundOffAmount { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Taxes")]
        public decimal TotalTaxAmount { get; set; }

        public virtual SalePaymentDetail PaymentDetail { get; set; }

        public virtual ICollection<SaleItem> SaleItems { get; set; }
    }


}
