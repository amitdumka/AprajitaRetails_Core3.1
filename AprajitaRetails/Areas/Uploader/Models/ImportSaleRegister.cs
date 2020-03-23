using Microsoft.AspNetCore.Authorization;    using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinqToExcel.Attributes;
using AprajitaRetails.Areas.Voyager.Models;

namespace AprajitaRetails.Areas.Uploader.Models
{
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
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal MRP { get; set; }

        [ExcelColumn("Discount")]
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal Discount { get; set; }
        [ExcelColumn("Basic Amt")]
        [Display(Name = "Basic Rate")]
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal BasicRate { get; set; }

        [ExcelColumn("Tax Amt")]
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal Tax { get; set; }

        [ExcelColumn("Round Off")]
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal RoundOff { get; set; }

        [ExcelColumn("Bill Amt")]
        [Display(Name = "Bill Amount")]
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal BillAmnt { get; set; }

        [ExcelColumn("Payment Mode")]
        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }

        public bool IsConsumed { get; set; } = false;
        //Store Based Started
        public int StoreId { get; set; }
        public virtual Store Store { get; set; }

    }
}
