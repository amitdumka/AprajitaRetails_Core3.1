using AprajitaRetails.Areas.Uploader.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.Voyager.Models
{

    public class Customer
    {
        public int CustomerID { set; get; }

        [Display(Name = "First Name")]
        public string FirstName { set; get; }
        [Display(Name = " Last Name")]
        public string LastName { set; get; }
        public int Age { set; get; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }
        public string City { set; get; }
        [Display(Name = "Contact No")]
        public string MobileNo { set; get; }
        public Genders Gender { set; get; }
        [Display(Name = "Bill Count")]
        public int NoOfBills { set; get; }
        [Display(Name = "Purchase Amount")]
        public decimal TotalAmount { set; get; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get { return FirstName + " " + LastName; } }

    }
    public class Store
    {
        public int StoreID { get; set; }
        [Display(Name = "Store Code")]
        public string StoreCode { get; set; }
        [Display(Name = "Store Name")]
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        [Display(Name = "Pin Code")]
        public string PinCode { get; set; }
        [Display(Name = "Contact No")]
        public string PhoneNo { get; set; }
        [Display(Name = "Store Manager Name")]
        public string StoreManagerName { get; set; }
        [Display(Name = "SM Contact No")]
        public string StoreManagerPhoneNo { get; set; }
        [Display(Name = "PAN No")]
        public string PanNo { get; set; }
        [Display(Name = "GSTIN ")]
        public string GSTNO { get; set; }
        [Display(Name = "Employees Count")]
        public int NoOfEmployees { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Opening Date")]
        public DateTime OpeningDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Closing Date")]
        public DateTime? ClosingDate { get; set; }
        [Display(Name = "Operative")]
        public bool Status { get; set; }
    }


    public class Supplier
    {
        public int SupplierID { get; set; }
        public string SuppilerName { get; set; }
        public string Warehouse { get; set; }
        public ICollection<ProductPurchase> ProductPurchases { get; set; }
    }

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

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalBasicAmount { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal ShippingCost { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalTax { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalAmount { get; set; }

        public string Remarks { get; set; }

        public int SupplierID { get; set; }
        public virtual Supplier Supplier { get; set; }

        public bool IsPaid { get; set; }

        public ICollection<PurchaseItem> PurchaseItems { get; set; }

    }

    public class PurchaseItem
    {
        public int PurchaseItemId { get; set; }//Pk

        public int ProductPurchaseId { get; set; }//FK
        public virtual ProductPurchase ProductPurchase { get; set; }

        public int ProductItemId { get; set; } //FK 
        public virtual ProductItem ProductItem { get; set; }
        public string Barcode { get; set; }// TODO: if not working then link with productitemid

        public double Qty { get; set; }
        public Units Unit { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Cost { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TaxAmout { get; set; }

        public int? PurchaseTaxTypeId { get; set; } //TODO: Temp Purpose. need to calculate tax here
        public virtual PurchaseTaxType PurchaseTaxType { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CostValue { get; set; }


        //Navigation Properties


    }
    public class PurchaseTaxType
    {
        public int PurchaseTaxTypeId { get; set; }
        public string TaxName { get; set; }
        public TaxType TaxType { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CompositeRate { get; set; }

        //Navigation
        public ICollection<PurchaseItem> PurchaseItems { get; set; }
    }

    public class ProductItem
    {
        public int ProductItemId { set; get; }

        public string Barcode { get; set; }

        public int BrandId { get; set; }
        public virtual Brand BrandName { get; set; }

        public string StyleCode { get; set; }
        public string ProductName { get; set; }
        public string ItemDesc { get; set; }

        public ProductCategorys Categorys { get; set; }

        public Category MainCategory { get; set; }
        public Category ProductCategory { get; set; }
        public Category ProductType { get; set; }


        public decimal MRP { get; set; }
        public decimal TaxRate { get; set; }    // TODO:Need to Review in final Edition
        public decimal Cost { get; set; }

        public Sizes Size { get; set; }
        public Units Units { get; set; }


        public virtual ICollection<PurchaseItem> PurchaseItems { get; set; }


    }

    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsPrimaryCategory { get; set; }
        public bool IsSecondaryCategory { get; set; }
    }
    public class Brand
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string BCode { get; set; }
    }

    public class Stock
    {
        public int StockID { set; get; }

        public int ProductItemId { set; get; }
        public virtual ProductItem ProductItem { get; set; }

        public double Quantity { set; get; }
        public double SaleQty { get; set; }
        public double PurchaseQty { get; set; }
        public Units Units { get; set; }


    }
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

    public class Salesman
    {
        public int SalesmanId { set; get; }
        public string SalesmanName { get; set; }
        public virtual ICollection<SaleItem> SaleItems { get; set; }
    }


    public class SaleItem
    {
        public int SaleItemId { get; set; }

        public int SaleInvoiceId { get; set; }

        public int ProductItemId { get; set; }
        public virtual ProductItem ProductItem { get; set; }
        public string BarCode { get; set; }

        public double Qty { get; set; }
        public Units Units { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal MRP { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal BasicAmount { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Discount { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TaxAmount { get; set; }

        public int? SaleTaxTypeId { get; set; }
        public virtual SaleTaxType SaleTaxType { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal BillAmount { get; set; }
        // SaleTax options and it will Done

        public int SalesmanId { get; set; }

        public virtual SaleInvoice SaleInvoice { get; set; }
        public virtual Salesman Salesman { get; set; }
    }

    public class SaleTaxType
    {
        public int SaleTaxTypeId { get; set; }

        public string TaxName { get; set; }
        public TaxType TaxType { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CompositeRate { get; set; }

        //Navigation
        public ICollection<SaleItem> SaleItems { get; set; }
    }

    public class SalePaymentDetail
    {
        [ForeignKey("SaleInvoice")]
        public int SalePaymentDetailId { get; set; }

        //public int SaleInvoiceId { get; set; }

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

        public virtual CardPaymentDetail CardDetails { get; set; }

        public virtual SaleInvoice SaleInvoice { get; set; }

    }



    public class CardPaymentDetail
    {
        [ForeignKey("SalePaymentDetail")]
        public int CardPaymentDetailId { get; set; }


        public int SaleInvoiceId { get; set; } // FK of SalesInvoice

        public int CardType { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }
        public int AuthCode { get; set; }
        public int LastDigit { get; set; }

        public virtual SalePaymentDetail SalePaymentDetail { get; set; }
    }

    public class VoyagerContext : DbContext
    {
        public VoyagerContext() //: base("Voyager")
        {
            //Database.SetInitializer<VoyagerContext>(new CreateDatabaseIfNotExists<VoyagerContext>());
            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<VoyagerContext, Migrations.Configuration>());
        }


        public DbSet<Store> Stores { get; set; }
        public DbSet<Customer> Customers { get; set; }

        //Import Table
        public DbSet<ImportInWard> ImportInWards { get; set; }
        public DbSet<ImportPurchase> ImportPurchases { get; set; }
        public DbSet<ImportSaleItemWise> ImportSaleItemWises { get; set; }
        public DbSet<ImportSaleRegister> ImportSaleRegisters { get; set; }



        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<Brand> Brands { get; set; }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Stock> Stocks { get; set; }


        //Purchase Entry System
        public DbSet<ProductPurchase> ProductPurchases { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }
        public DbSet<PurchaseTaxType> PurchaseTaxTypes { get; set; }


        // Sale Entry System

        public DbSet<Salesman> Salesmen { get; set; }
        public DbSet<SaleInvoice> SaleInvoices { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<SaleTaxType> SaleTaxTypes { get; set; }


        public DbSet<SalePaymentDetail> SalePaymentDetails { get; set; }
        public DbSet<CardPaymentDetail> CardPaymentDetails { get; set; }
    }




    //Processed Tables


    internal class SaleReturnInvoice
    {
        public int SaleReturnInvoiceID { get; set; }
        public int CustomerID { get; set; }
        public string SaleInvoiceNo { get; set; }
        public string ReturnInvoiceNo { get; set; }
        public double TotalQty { get; set; }
        public int TotalReturnItem { get; set; }
        public double Amount { get; set; }
        public double TaxAmount { get; set; }
        public double DiscountAmount { get; set; }
        public DateTime OnDate { get; set; }
        public string NewSaleInvoiceNo { get; set; }
        public string Debit_CreditNotesNo { get; set; }
        public int SalesmanId { get; set; }
    }



    internal class PaymentDetails
    {
        public int PaymentDetailsID { get; set; }
        public string InvoiceNo { get; set; }
        public SalePayMode PayMode { get; set; }
        public decimal CashAmount { get; set; }
        public decimal CardAmount { get; set; }
        public int CardDetailsID { get; set; }
    }


}
