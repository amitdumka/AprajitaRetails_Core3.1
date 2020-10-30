using AprajitaRetails.Areas.Voyager.Models;
using LinqToExcel.Attributes;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Areas.Uploader.Models
{
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

        [ExcelColumn("Bar code")]
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

        //Store Based Started
        public int StoreId { get; set; }
        public virtual Store Store { get; set; }
    }
}
