using AprajitaRetails.Areas.Purchase.Models;
using Microsoft.AspNetCore.Authorization;    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Areas.Purchase.Models
{
    public class ProductPurchase
    {
        public int ProductPurchaseId { get; set; }

        public string InWardNo { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime InWardDate { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PurchaseDate { get; set; }
        public string InvoiceNo { get; set; }

        public double TotalQty { get; set; }

        [Display(Name = "Basic Amt")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalBasicAmount { get; set; }
        [Display(Name = "Shipping Cost")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal ShippingCost { get; set; }
        [Display(Name = "Tax")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalTax { get; set; }
        [Display(Name = "Total Amount")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalAmount { get; set; }

        public string Remarks { get; set; }

        [Display(Name = "Suppiler")]
        public int SupplierID { get; set; }
        public virtual Supplier Supplier { get; set; }

        [Display(Name = "Paid")]
        public bool IsPaid { get; set; }

        public ICollection<PurchaseItem> PurchaseItems { get; set; }

    }


}
