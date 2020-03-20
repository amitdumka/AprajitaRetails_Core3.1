using AprajitaRetails.Areas.Purchase.Models;
using Microsoft.AspNetCore.Authorization;    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Areas.Sales.Models
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

    public class ManualInvoice
    {
        public int ManualInvoiceId { get; set; } //Pk
        [Display (Name = "Customer Name")]
        public int CustomerId { get; set; }
        [DataType (DataType.Date), DisplayFormat (DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true), Display (Name = "Sale Date")]
        public DateTime OnDate { get; set; }
        public string InvoiceNo { get; set; }
        [Display (Name = "Total Items")]
        public int TotalItems { get; set; }
        [Display (Name = "Qty")]
        public double TotalQty { get; set; }
        [DataType (DataType.Currency), Column (TypeName = "money"), Display (Name = "Bill Amt")]
        public decimal TotalBillAmount { get; set; }
        [DataType (DataType.Currency), Column (TypeName = "money"), Display (Name = "Discount")]
        public decimal TotalDiscountAmount { get; set; }
        [DataType (DataType.Currency), Column (TypeName = "money"), Display (Name = "Round off")]
        public decimal RoundOffAmount { get; set; }
        [DataType (DataType.Currency), Column (TypeName = "money"), Display (Name = "Taxes")]
        public decimal TotalTaxAmount { get; set; }
        public virtual ManualSalePaymentDetail PaymentDetail { get; set; }
        public virtual ICollection<ManualSaleItem> SaleItems { get; set; }
    }
    public class ManualCardPaymentDetail
    {
        [ForeignKey ("ManualSalePaymentDetail")]
        public int ManualCardPaymentDetailId { get; set; }
        public int ManualSaleInvoiceId { get; set; } // FK of SalesInvoice
        public int CardType { get; set; }
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal Amount { get; set; }
        public int AuthCode { get; set; }
        public int LastDigit { get; set; }
        [ForeignKey ("ManualSalePaymentDetailId")]
        public virtual ManualSalePaymentDetail SalePaymentDetail { get; set; }
    }
    public class ManualSaleItem
    {
        public int ManualSaleItemId { get; set; }

        public int ManualInvoiceId { get; set; }

        public int ProductItemId { get; set; }
        public virtual ProductItem ProductItem { get; set; }
        public string BarCode { get; set; }

        public double Qty { get; set; }
        public Units Units { get; set; }

        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal MRP { get; set; }

        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal BasicAmount { get; set; }

        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal Discount { get; set; }

        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal TaxAmount { get; set; }

        public int? SaleTaxTypeId { get; set; }
        public virtual SaleTaxType SaleTaxType { get; set; }

        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal BillAmount { get; set; }
        // SaleTax options and it will Done

        public int SalesPersonId { get; set; }

        public virtual ManualInvoice SaleInvoice { get; set; }
        public virtual SalesPerson Salesman { get; set; }
    }
    public class ManualSalePaymentDetail
    {
        [ForeignKey ("ManualInvoice")]
        public int ManualSalePaymentDetailId { get; set; }
        //public int SaleInvoiceId { get; set; }
        public SalePayMode PayMode { get; set; }
        [DefaultValue (0)]
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal CashAmount { get; set; }
        [DefaultValue (0)]
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal CardAmount { get; set; }
        [DefaultValue (0)]
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal MixAmount { get; set; }
        public virtual ManualCardPaymentDetail CardDetails { get; set; }
        public virtual ManualInvoice ManualInvoice { get; set; }
    }
    
}

namespace AprajitaRetails.Areas.Sales.Models.Views
{
    public class Invoice {
        [Display (Name = "Customer Name")]
        public int CustomerId { get; set; }
        [DataType (DataType.Date), DisplayFormat (DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true), Display (Name = "Sale Date")]
        public DateTime OnDate { get; set; }
        public string InvoiceNo { get; set; }
        [Display (Name = "Total Items")]
        public int TotalItems { get; set; }
        [Display (Name = "Qty")]
        public double TotalQty { get; set; }
        [DataType (DataType.Currency), Column (TypeName = "money"), Display (Name = "Bill Amt")]
        public decimal TotalBillAmount { get; set; }
        [DataType (DataType.Currency), Column (TypeName = "money"), Display (Name = "Discount")]
        public decimal TotalDiscountAmount { get; set; }
        [DataType (DataType.Currency), Column (TypeName = "money"), Display (Name = "Round off")]
        public decimal RoundOffAmount { get; set; }
        [DataType (DataType.Currency), Column (TypeName = "money"), Display (Name = "Taxes")]
        public decimal TotalTaxAmount { get; set; }
    }
    public class SaleItem {
        public int ProductItemId { get; set; }
        public virtual ProductItem ProductItem { get; set; }
        public string BarCode { get; set; }
        public double Qty { get; set; }
        public Units Units { get; set; }
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal MRP { get; set; }
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal BasicAmount { get; set; }
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal Discount { get; set; }
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal TaxAmount { get; set; }
        public int? SaleTaxTypeId { get; set; }
        public virtual SaleTaxType SaleTaxType { get; set; }
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal BillAmount { get; set; }
        // SaleTax options and it will Done
        public int SalesPersonId { get; set; }
        public virtual ManualInvoice SaleInvoice { get; set; }
        public virtual SalesPerson Salesman { get; set; }
    }
    public class PaymentDetail {
        public SalePayMode PayMode { get; set; }
        [DefaultValue (0)]
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal CashAmount { get; set; }
        [DefaultValue (0)]
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal CardAmount { get; set; }
        [DefaultValue (0)]
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal MixAmount { get; set; }
    }
    public class CardDetails {
        public int CardType { get; set; }
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal Amount { get; set; }
        public int AuthCode { get; set; }
        public int LastDigit { get; set; }
        
    }


    //ViewModel for Invoice 

    public class SaleInvoice
    {
        public Invoice Invoice { get; set; }
        public ICollection<SaleItem> Items { get; set; }
        public PaymentDetail PaymentDetail { get; set; }
        public CardDetails CardDetails { get; set; }
    }


}

