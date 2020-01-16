using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.ComponentModel;
using LinqToExcel.Attributes;

namespace AprajitaRetails.Areas.Uploader.Models
{

    public class ImportInWard
    {
        //Inward No	Inward Date	Invoice No	Invoice Date	Party Name	Total Qty	Total MRP Value	Total Cost

        public int ImportInWardId { get; set; }

        [ExcelColumn("Inward No")]
        public string InWardNo { get; set; }

        // 4/4/2018  5:34:56 PM
        // [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{dd/MM/yyyy HH:mm:ss tt}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [ExcelColumn("Inward Date")]
        [Column(TypeName = "DateTime2")]
        public DateTime InWardDate { get; set; }

        [ExcelColumn("Invoice No")]
        public string InvoiceNo { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [ExcelColumn("Invoice Date")]
        [Column(TypeName = "DateTime2")]
        public DateTime InvoiceDate { get; set; }

        [ExcelColumn("Party Name")]
        public string PartyName { get; set; }

        [ExcelColumn("Total Qty")]
        public decimal TotalQty { get; set; }

        [ExcelColumn("Total MRP Value")]
        public decimal TotalMRPValue { get; set; }

        [ExcelColumn("Total Cost")]
        public decimal TotalCost { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? ImportDate { get; set; } = DateTime.Now;


    }
    public class ImportPurchase
    {
        //GRNNo	GRNDate	Invoice No	Invoice Date	Supplier Name	Barcode	Product Name	Style Code	Item Desc	Quantity	MRP	MRP Value	Cost	Cost Value	TaxAmt	ExmillCost	Excise1	Excise2	Excise3

        public int ImportPurchaseId { get; set; }

        [ExcelColumn("GRNNo")]
        public string GRNNo { get; set; }

        [ExcelColumn("GRNDate")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime GRNDate { get; set; }

        [ExcelColumn("Invoice No")]
        public string InvoiceNo { get; set; }

        [ExcelColumn("Invoice Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate { get; set; }

        [ExcelColumn("Supplier Name")]
        public string SupplierName { get; set; }

        [ExcelColumn("Barcode")]
        public string Barcode { get; set; }

        [ExcelColumn("Product Name")]
        public string ProductName { get; set; }

        [ExcelColumn("Style Code")]
        public string StyleCode { get; set; }

        [ExcelColumn("Item Desc")]
        public string ItemDesc { get; set; }

        [ExcelColumn("Quantity")]
        public double Quantity { get; set; }

        [ExcelColumn("MRP")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal MRP { get; set; }

        [ExcelColumn("MRP Value")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal MRPValue { get; set; }

        [ExcelColumn("Cost")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Cost { get; set; }

        [ExcelColumn("Cost Value")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CostValue { get; set; }

        [ExcelColumn("TaxAmt")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TaxAmt { get; set; }

        public bool IsVatBill { get; set; }
        public bool IsLocal { get; set; }

        [DefaultValue(false)]
        public bool IsDataConsumed { get; set; } = false;// is data imported to relevent table

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? ImportTime { get; set; } = DateTime.Now; // Date of Import



    }


    //TODO: Need to Create View So String Date error problem will be solved
    public class ImportSaleItemWise
    {
        //Invoice No	Invoice Date	Invoice Type	Brand Name	Product Name	Item Desc	HSN Code	BAR CODE	Style Code	Quantity	MRP	Discount Amt	Basic Amt	Tax Amt	SGST Amt	CGST Amt	Line Total	Round Off	Bill Amt	Payment Mode	SalesMan Name	Coupon %	Coupon Amt	SUB TYPE	Bill Discount	LP Flag	Inst Order CD	TAILORING FLAG


        public int ImportSaleItemWiseId { get; set; }

        [ExcelColumn("Invoice Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate { get; set; }

        [ExcelColumn("Invoice No")]
        [Display(Name = "Invoice No")]
        public string InvoiceNo { get; set; }

        [ExcelColumn("Invoice Type")]
        [Display(Name = "Invoice Type")]
        public string InvoiceType { get; set; }

        [ExcelColumn("Brand Name")]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }

        [ExcelColumn("Product Name")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [ExcelColumn("Item Desc")]
        [Display(Name = "Item Desc")]
        public string ItemDesc { get; set; }

        [ExcelColumn("HSN Code")]
        [Display(Name = "HSN Code")]
        public string HSNCode { get; set; }

        [ExcelColumn("BAR CODE")]
        public string Barcode { get; set; }

        [ExcelColumn("Style Code")]
        [Display(Name = "Style Code")]
        public string StyleCode { get; set; }

        [ExcelColumn("Quantity")]
        public double Quantity { get; set; }

        [ExcelColumn("MRP")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal MRP { get; set; }

        [ExcelColumn("Discount Amt")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Discount { get; set; }

        [ExcelColumn("Basic Amt")]
        [Display(Name = "Basic Rate")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal BasicRate { get; set; }

        [ExcelColumn("Tax Amt")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Tax { get; set; } // Can be use for IGST

        [ExcelColumn("SGST Amt")]

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal SGST { get; set; }

        [ExcelColumn("CGST Amt")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CGST { get; set; }

        [ExcelColumn("Line Total")]
        [Display(Name = "Line Total")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal LineTotal { get; set; }

        [ExcelColumn("Round Off")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal RoundOff { get; set; }

        [ExcelColumn("Bill Amt")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        [Display(Name = "Bill Amount")]
        public decimal BillAmnt { get; set; }

        [ExcelColumn("Payment Mode")]

        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }

        [ExcelColumn("SalesMan Name")]
        public string Saleman { get; set; }

        [DefaultValue(false)]
        public bool IsDataConsumed { get; set; } = false;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? ImportTime { get; set; } = DateTime.Now; // Date of Import
                                                                  // is data imported to relevent table


    }

    public class ImportSaleItemWiseVM
    {
        //Invoice No	Invoice Date	Invoice Type	Brand Name	Product Name	Item Desc	HSN Code	BAR CODE	Style Code	Quantity	MRP	Discount Amt	Basic Amt	Tax Amt	SGST Amt	CGST Amt	Line Total	Round Off	Bill Amt	Payment Mode	SalesMan Name	Coupon %	Coupon Amt	SUB TYPE	Bill Discount	LP Flag	Inst Order CD	TAILORING FLAG
        public static ImportSaleItemWise ToTable(ImportSaleItemWiseVM data)
        {


            ImportSaleItemWise item = new ImportSaleItemWise
            {
                Barcode = data.Barcode,
                BasicRate = data.BasicRate,
                BillAmnt = data.BillAmnt,
                BrandName = data.BrandName,
                CGST = data.CGST,
                Discount = data.Discount,
                HSNCode = data.HSNCode,
                InvoiceDate = DateTime.ParseExact(data.InvoiceDate.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture),
                InvoiceNo = data.InvoiceNo,
                InvoiceType = data.InvoiceType,
                IsDataConsumed = data.IsDataConsumed,
                ImportSaleItemWiseId = data.ImportSaleItemWiseId,
                Quantity = data.Quantity,
                ItemDesc = data.ItemDesc,
                LineTotal = data.LineTotal,
                MRP = data.MRP,
                PaymentType = data.PaymentType,
                ImportTime = data.ImportTime,
                ProductName = data.ProductName,
                RoundOff = data.RoundOff,
                Saleman = data.Saleman,
                SGST = data.SGST,
                StyleCode = data.StyleCode,
                Tax = data.Tax
            };
            return item;
        }

        public int ImportSaleItemWiseId { get; set; }

        [ExcelColumn("Invoice Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string InvoiceDate { get; set; }

        [ExcelColumn("Invoice No")]
        [Display(Name = "Invoice No")]
        public string InvoiceNo { get; set; }

        [ExcelColumn("Invoice Type")]
        [Display(Name = "Invoice Type")]
        public string InvoiceType { get; set; }

        [ExcelColumn("Brand Name")]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }

        [ExcelColumn("Product Name")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [ExcelColumn("Item Desc")]
        [Display(Name = "Item Desc")]
        public string ItemDesc { get; set; }

        [ExcelColumn("HSN Code")]
        [Display(Name = "HSN Code")]
        public string HSNCode { get; set; }

        [ExcelColumn("BAR CODE")]
        public string Barcode { get; set; }

        [ExcelColumn("Style Code")]
        [Display(Name = "Style Code")]
        public string StyleCode { get; set; }

        [ExcelColumn("Quantity")]
        public double Quantity { get; set; }

        [ExcelColumn("MRP")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal MRP { get; set; }

        [ExcelColumn("Discount Amt")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Discount { get; set; }

        [ExcelColumn("Basic Amt")]
        [Display(Name = "Basic Rate")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal BasicRate { get; set; }

        [ExcelColumn("Tax Amt")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Tax { get; set; } // Can be use for IGST

        [ExcelColumn("SGST Amt")]

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal SGST { get; set; }

        [ExcelColumn("CGST Amt")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CGST { get; set; }

        [ExcelColumn("Line Total")]
        [Display(Name = "Line Total")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal LineTotal { get; set; }

        [ExcelColumn("Round Off")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal RoundOff { get; set; }

        [ExcelColumn("Bill Amt")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        [Display(Name = "Bill Amount")]
        public decimal BillAmnt { get; set; }

        [ExcelColumn("Payment Mode")]

        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }

        [ExcelColumn("SalesMan Name")]
        public string Saleman { get; set; }

        [DefaultValue(false)]
        public bool IsDataConsumed { get; set; } = false;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? ImportTime { get; set; } = DateTime.Now; // Date of Import
                                                                  // is data imported to relevent table


    }


    public class ImportSaleRegister
    {
        public int ImportSaleRegisterId { get; set; }

        [ExcelColumn("Invoice No")]
        [Display(Name = "Invoice No")]
        public string InvoiceNo { get; set; }

        [ExcelColumn("Invoice Type")]
        [Display(Name = "Invoice Type")]
        public string InvoiceType { get; set; }

        [ExcelColumn("Invoice Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public string InvoiceDate { get; set; }

        [ExcelColumn("Quantity")]
        public double Quantity { get; set; }

        [ExcelColumn("MRP")]
        public decimal MRP { get; set; }

        [ExcelColumn("Discount")]
        public decimal Discount { get; set; }
        [ExcelColumn("Basic Amt")]
        [Display(Name = "Basic Rate")]
        public decimal BasicRate { get; set; }

        [ExcelColumn("Tax Amt")]
        public decimal Tax { get; set; }

        [ExcelColumn("Round Off")]
        public decimal RoundOff { get; set; }

        [ExcelColumn("Bill Amt")]
        [Display(Name = "Bill Amount")]
        public decimal BillAmnt { get; set; }

        [ExcelColumn("Payment Mode")]
        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }

        public bool IsConsumed { get; set; } = false;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? ImportTime { get; set; } = DateTime.Now; // Date of Import
    }
}
