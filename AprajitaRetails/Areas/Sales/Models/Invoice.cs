using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Areas.Purchase.Models;
using AprajitaRetails.Areas.Voyager.Models;

namespace AprajitaRetails.Areas.Sales.Models.Views
{
    #region BaseInvoice
    public class Invoice
    {
        //Store Info
        [Display (Name = "Store")]
        public int StoreId { get; set; }

        public Store Store { get; set; }

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

    public class SaleItem
    {
        public int ProductItemId { get; set; }

        [Display (Name = "Product")]
        public virtual ProductItem ProductItem { get; set; }

        public string BarCode { get; set; }

        [Display (Name = "Quantity")]
        public double Qty { get; set; }

        [Display (Name = "Unit")]
        public Units Units { get; set; }

        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal MRP { get; set; }

        [DataType (DataType.Currency), Column (TypeName = "money")]
        [Display (Name = "Basic Amount")]
        public decimal BasicAmount { get; set; }

        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal Discount { get; set; }

        [DataType (DataType.Currency), Column (TypeName = "money")]
        [Display (Name = "Tax Amount")]
        public decimal TaxAmount { get; set; }

        public int? SaleTaxTypeId { get; set; }
        public virtual SaleTaxType SaleTaxType { get; set; }

        [DataType (DataType.Currency), Column (TypeName = "money")]
        [Display (Name = "Bill Amount")]
        public decimal BillAmount { get; set; }

        // SaleTax options and it will Done
        public int SalesPersonId { get; set; }  // TODO: Switch to salesman
        public virtual SalesPerson Salesman { get; set; }       // TODO: Switch to salesman
    }

    public class PaymentDetail
    {
        public int PaymentDetailId { get; set; }
        [Key]
        public string InvoiceNo { get; set; }

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
        public CardDetail? CardDetail { get; set; }

        [DefaultValue (false)]
        public bool IsManualBill { get; set; }
    }

    public class CardDetail
    {
        public int CardDetailId { get; set; }
        [Display (Name = "Card Type")]
        public int CardType { get; set; }

        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal Amount { get; set; }

        public int AuthCode { get; set; }
        public int LastDigit { get; set; }

        //public int PaymentDetailId { get; set; }
       
        public string InvoiceNo { get; set; }
        [ForeignKey("InvoiceNo")]
        public virtual PaymentDetail PaymentDetail { get; set; }
    }

    #endregion


    //Tables

    #region RegularInvoiec
    //Sale Invoice Regular
    public class RegularInvoice : Invoice
    {
        public int RegularInvoiceId { get; set; }

        public virtual ICollection<RegularSaleItem> SaleItems { get; set; }

        public virtual PaymentDetail PaymentDetail { get; set; }
        //public virtual RegularCardDetail CardDetail { get; set; }
    }

    public class RegularSaleItem : SaleItem
    {
        public int RegularSaleItemId { get; set; }

        public int RegularInvoiceId { get; set; }
        public virtual RegularInvoice RegularInvoice { get; set; }
    }

    //public class RegularPaymentDetail : PaymentDetail
    //{
    //   // [ForeignKey("RegularInvoices")]
    //    public int RegularPaymentDetailId { get; set; }

    //    public int RegularInvoiceId { get; set; }
    //    public virtual RegularInvoice RegularInvoice { get; set; }
    //}

    //public class RegularCardDetail : CardDetail
    //{
    //   // [ForeignKey("RegularInvoices")]
    //    public int RegularCardDetailId { get; set; }

    //    public int RegularInvoiceId { get; set; }
    //    public virtual RegularInvoice RegularInvoice { get; set; }
    //}
    #endregion

    #region ManualInvoice
    public class ManualInvoice : Invoice
    {
        public int ManualInvoiceId { get; set; }
        public virtual ICollection<ManualSaleItem> SaleItems { get; set; }
        public virtual ManualPaymentDetail PaymentDetail { get; set; }
        public virtual ManualCardDetail CardDetail { get; set; }
    }

    public class ManualSaleItem : SaleItem
    {
        public int ManualSaleItemId { get; set; }
        public int ManualInvoiceId { get; set; }
        public virtual ManualInvoice ManualInvoice { get; set; }
    }

    public class ManualPaymentDetail : PaymentDetail
    {
        //  [ForeignKey("ManualInvoices")]
        public int ManualPaymentDetailId { get; set; }
        public int ManualInvoiceId { get; set; }
        public virtual ManualInvoice ManualInvoice { get; set; }
    }

    public class ManualCardDetail : CardDetail
    {
        //[ForeignKey("ManualInvoices")]
        public int ManualCardDetailId { get; set; }
        public int ManualInvoiceId { get; set; }
        public virtual ManualInvoice ManualInvoice { get; set; }
    }

    #endregion

    #region SalesReturn
    public class SaleReturn : Invoice
    {
        public int SaleReturnId { get; set; }
        public int RegularInvoiceId { get; set; }
        public virtual RegularInvoice RegularInvoice { get; set; }
        public virtual ICollection<SaleItemReturn> ItemReturns { get; set; }
        //AutoCredit Note Created Id:
    }

    public class SaleItemReturn : SaleItem
    {
        public int SaleItemReturnId { get; set; }
    }

    #endregion
    //Manual Sale Invoice


    //Sale Return


    //ViewModel for Invoice
    //TODO: Need to generalized

    public class SaleInvoice
    {
        public Invoice Invoice { get; set; }
        public ICollection<SaleItem> Items { get; set; }
        public PaymentDetail PaymentDetail { get; set; }
        public CardDetail CardDetails { get; set; }
    }
}
