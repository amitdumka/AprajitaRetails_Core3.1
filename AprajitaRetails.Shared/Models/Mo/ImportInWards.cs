using AprajitaRetails.Areas.Voyager.Models;
using LinqToExcel.Attributes;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [DataType(DataType.Currency), Column(TypeName = "decimal(18,2)")]
        public decimal TotalQty { get; set; }

        [ExcelColumn("Total MRP Value")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalMRPValue { get; set; }

        [ExcelColumn("Total Cost")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalCost { get; set; }

        [DefaultValue(false)]
        public bool IsDataConsumed { get; set; } = false;// is data imported to relevent table

        //Store Based Started
        public int StoreId { get; set; }
        public virtual Store Store { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        // public DateTime? ImportDate { get; set; } = DateTime.Now;


    }
}
