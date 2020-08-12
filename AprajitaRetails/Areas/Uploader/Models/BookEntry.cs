using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinqToExcel.Attributes;

namespace AprajitaRetails.Areas.Uploader.Models
{
    public class BookEntry
    {
        public int BookEntryId { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [ExcelColumn("Inward Date")]
        [Column(TypeName = "DateTime2")]
        public DateTime OnDate { get; set; }
        public LedgerBy LedgerBy { get; set; }
        public LedgerTo LedgerTo { get; set; }
        public decimal Amount { get; set; }
        public VoucherType VoucherType { get; set; }
        public string Naration { get; set; }
        public bool IsConsumed { get; set; }
    }
}
