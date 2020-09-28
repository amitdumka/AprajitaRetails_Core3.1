using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using LinqToExcel.Attributes;

namespace AprajitaRetails.Areas.Uploader.Models
{
    public class BookEntry
    {
        public int BookEntryId { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
       // [ExcelColumn("Inward Date")]
        [Column(TypeName = "DateTime2")]
        [Display(Name = "On Date")]
        public DateTime OnDate { get; set; }
        [Display(Name = "Ledger By")]
        public LedgerBy LedgerBy { get; set; }
        [Display(Name = "Ledger To")]
        public LedgerTo LedgerTo { get; set; }
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal Amount { get; set; }
        [Display(Name = "Voucher Type")]
        public VoucherType VoucherType { get; set; }
        public string Naration { get; set; }
        [Display(Name = "Processed")]
        public bool IsConsumed { get; set; }
    }

}
