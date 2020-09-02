using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Areas.Purchase.Models;
using AprajitaRetails.Areas.Voyager.Models;
using AprajitaRetails.Models;

namespace AprajitaRetails.Areas.Sales.Models.Views
{
    #region BaseInvoice
    public class Invoice
    {
        [Key]
        public string InvoiceNo { get; set; }

        //Store Info
        [Display(Name = "Store"), DefaultValue((int)1)]
        public int StoreId { get; set; }
        public Store Store { get; set; }

        [Display(Name = "Customer Name")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true), Display(Name = "Sale Date")]
        public DateTime OnDate { get; set; }

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
        
    }

    public class SaleItem
    {
        [Display(Name = "Product")]
        public int ProductItemId { get; set; }
        [Display(Name = "Product")]
        public virtual ProductItem ProductItem { get; set; }

        public string BarCode { get; set; }

        [Display(Name = "Quantity")]
        public double Qty { get; set; }

        [Display(Name = "Unit")]
        public Units Units { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal MRP { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        [Display(Name = "Basic Amount")]
        public decimal BasicAmount { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Discount { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        [Display(Name = "Tax Amount")]
        public decimal TaxAmount { get; set; }
        
        [Display(Name ="Sale Tax")]
        public int? SaleTaxTypeId { get; set; }
        public virtual SaleTaxType SaleTaxType { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        [Display(Name = "Bill Amount")]
        public decimal BillAmount { get; set; }

        public int SalesmanId { get; set; }
        public virtual Salesman Salesman { get; set; }
     

    }

    public class PaymentDetail
    {
        public int PaymentDetailId { get; set; }
        [Key]
        public string InvoiceNo { get; set; }
        [ForeignKey("InvoiceNo")]
        public virtual RegularInvoice Invoice { get; set; }

        public SalePayMode PayMode { get; set; }

        [DefaultValue(0)]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CashAmount { get; set; }

        [DefaultValue(0)]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CardAmount { get; set; }

        [DefaultValue(0)]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal MixAmount { get; set; }
        public CardDetail? CardDetail { get; set; }

        [DefaultValue(false)]
        public bool IsManualBill { get; set; }
    }

    public class CardDetail
    {
        public int CardDetailId { get; set; }
        [Display(Name = "Card Type")]
        public int CardType { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
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
   
    public class RegularInvoice:Invoice
    {
        public int RegularInvoiceId { get; set; }
        public PaymentDetail PaymentDetail { get; set; }
        public virtual ICollection<RegularSaleItem> SaleItems { get; set; }

        [DefaultValue(false)]
        public bool IsManualBill { get; set; }
    }
    public class RegularSaleItem : SaleItem
    {
        public int RegularSaleItemId { get; set; }
        //Navigation for Invoice
        public string InvoiceNo { get; set; }
        public virtual RegularInvoice Invoice { get; set; }
    }
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
       // public virtual RegularInvoice RegularInvoice { get; set; }
        public virtual ICollection<SaleItemReturn> ItemReturns { get; set; }
        //AutoCredit Note Created Id:
    }

    public class SaleItemReturn : SaleItem
    {
        public int SaleItemReturnId { get; set; }
    }

    #endregion
   
    //TODO: Need to generalized

    public class SaleInvoice
    {
        public Invoice Invoice { get; set; }
        public ICollection<SaleItem> Items { get; set; }
        public PaymentDetail PaymentDetail { get; set; }
        public CardDetail CardDetails { get; set; }
    }
}
