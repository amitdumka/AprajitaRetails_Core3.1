using Microsoft.AspNetCore.Authorization;    using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Models
{
    public class TailoringStaffAdvanceReceipt
    {
        public int TailoringStaffAdvanceReceiptId { get; set; }

        [Display(Name = "Staff Name")]
        public int TailoringEmployeeId { get; set; }

        public TailoringEmployee Employee { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Receipt Date")]
        public DateTime ReceiptDate { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Payment Mode")]
        public PayMode PayMode { get; set; }

        public string Details { get; set; }
    }
}