using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Version 4.0
/// </summary>
namespace AprajitaRetails.Areas.Accountings.Models
{
    public partial class BasicVoucher
    {
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "On Date")]
        public DateTime OnDate { get; set; }

        [Display(Name = "Party Name")]
        public string PartyName { get; set; }

        [Display(Name = "Payment Mode")]
        public PaymentMode PayMode { get; set; }

        [Display(Name ="From Account")]
        public int? BankAccountId { get; set; }
        public virtual BankAccount FromAccount { get; set; }

        [Display(Name = "Payment Details")]
        public string PaymentDetails { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Amount")]
        public decimal Amount { get; set; }

        public string Remarks { get; set; }

        [Display(Name = "Party")]
        public int? PartyId { get; set; }

        [Display(Name = "Leger")]
        public int? LedgerEnteryId { get; set; }

        
        [DefaultValue(false), Display(Name ="Cash")]
        public bool IsCash { get; set; }

        [DefaultValue(1)]
        public int StoreId { get; set; }
        
        public string UserName { get; set; }

        public virtual Party Party { get; set; }
        public virtual LedgerEntry LedgerEntry { get; set; }
        public virtual Voyager.Models.Store Store { get; set; }

    }

    public class Expense : BasicVoucher
    {
        public int ExpenseId { get; set; }
        
        public string Particulars { get; set; }

        [Display(Name = "Paid To")]
        public new string PartyName { get; set; }

        [Display(Name = "Paid By")]
        public int EmployeeId { get; set; }
        public virtual AprajitaRetails.Models.Employee PaidBy { get; set; }

    }

    public class Payment : BasicVoucher
    {
        public int PaymentId { get; set; }
        
        [Display(Name = "Paid To")]
        public new string PartyName { get; set; }
        
        [Display(Name = "Payment Slip No")]
        public string PaymentSlipNo { get; set; }

    }
    public class Receipt : BasicVoucher
    {
        public int ReceiptId { get; set; }

        [Display(Name = "Receipt From ")]
        public new string PartyName { get; set; }

        [Display(Name = "Receipt Slip No ")]
        public string RecieptSlipNo { get; set; }
    }


}
