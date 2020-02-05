using Microsoft.AspNetCore.Authorization;    using System;
//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace AprajitaRetails.Models
{
    public class StaffAdvanceReceipt
    {
        public int StaffAdvanceReceiptId { get; set; }

        [Display(Name = "Staff Name")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Receipt Date")]
        public DateTime ReceiptDate { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Payment Mode")]
        public PayModes PayMode { get; set; }
        public string Details { get; set; }
    }

    //TODO: List
    //TODO: Dues Recovery options
    //TODO: Tailoring 
    //TODO: Sales return policy update and check 
    //TODO: Purchase of Items/Assets
    //TODO: Arvind Payments
    //TODO: Purchase Invoice Entry

}
