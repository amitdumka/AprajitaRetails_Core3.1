using System;

/// <summary>
/// Version 3.0
/// </summary>
namespace AprajitaRetails.Areas.Accountings.Models
{
    public class AppInfo
    {
        public int AppInfoId { get; set; }
        public string Version { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime UpdateOn { get; set; }
        public string DatabaseVersion { get; set; }
        public bool IsEffective { get; set; }
    }


}

//public class Payment_old
//{
//    public int PaymentId { get; set; }

//    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
//    [Display(Name = "Payment Date")]
//    public DateTime PayDate { get; set; }

//    [Display(Name = "Payment Party")]
//    public string PaymentPartry { get; set; }
//    [Display(Name = "Payment Mode")]
//    public PaymentMode PayMode { get; set; }
//    [Display(Name = "Payment Details")]
//    public string PaymentDetails { get; set; }
//    [DataType(DataType.Currency), Column(TypeName = "money")]
//    public decimal Amount { get; set; }
//    [Display(Name = "Payment Slip No")]
//    public string PaymentSlipNo { get; set; }

//    public string Remarks { get; set; }

//    [Display(Name = "Party")]
//    public int? PartyId { get; set; }

//}
//public class Expense_old
//{
//    public int ExpenseId { get; set; }

//    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
//    [Display(Name = "Expense Date")]
//    public DateTime ExpDate { get; set; }

//    public string Particulars { get; set; }

//    [DataType(DataType.Currency), Column(TypeName = "money")]
//    public decimal Amount { get; set; }

//    [Display(Name = "Payment Mode")]
//    public PaymentMode PayMode { get; set; }

//    [Display(Name = "Payment Details")]
//    public string PaymentDetails { get; set; }

//    [Display(Name = "Paid By")]
//    public int EmployeeId { get; set; }
//    public virtual AprajitaRetails.Models.Employee PaidBy { get; set; }

//    [Display(Name = "Paid To")]
//    public string PaidTo { get; set; }

//    public string Remarks { get; set; }

//    [Display(Name = "Party")]
//    public int? PartyId { get; set; }


//}
//public class Receipt_old
//{
//    public int ReceiptId { get; set; }

//    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
//    [Display(Name = "Expense Date")]
//    public DateTime RecieptDate { get; set; }

//    [Display(Name = "Receipt From ")]
//    public string ReceiptFrom { get; set; }


//    [Display(Name = "Payment Mode")]
//    public PaymentMode PayMode { get; set; }
//    [Display(Name = "Receipt Details ")]
//    public string ReceiptDetails { get; set; }

//    [DataType(DataType.Currency), Column(TypeName = "money")]
//    public decimal Amount { get; set; }
//    [Display(Name = "Receipt Slip No ")]
//    public string RecieptSlipNo { get; set; }
//    public string Remarks { get; set; }
//    [Display(Name = "Party")]
//    public int? PartyId { get; set; }


//}



