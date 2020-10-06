using AprajitaRetails.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Version 3.0
/// </summary>
namespace AprajitaRetails.Areas.Accountings.Models
{

    public enum VoucherType { Sale, Purchase, Expenses, Reciepts, Payment, BankDeposit, BankWidthral, Others }

    //Expenses system
    public class ExpenseVM : BaseVM { }
    public class PaymentVM : BaseVM { }
    public class RecieptVM : BaseVM { }

    public class BaseVM
    {
        public int PartyId { get; set; }
        //public int LedgerId { get; set; }
        public int LedgerEnrtryId { get; set; }
        public DateTime OnDate { get; set; }
        public string Narations { get; set; }
    }

 
    //Payroll

    public class BasicPayrollVocher
    {
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "On Date")]
        public DateTime OnDate { get; set; }

        [Display(Name = "Staff Name")]
        public int EmployeeId { get; set; }

        public AprajitaRetails.Models.Employee Employee { get; set; }


        [Display(Name = "Payment Mode")]
        public PaymentMode PayMode { get; set; }

        [Display(Name = "Payment Details")]
        public string PaymentDetails { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Amount")]
        public decimal Amount { get; set; }


        public string Remarks { get; set; }

        [Display(Name = "Party")]
        public int? PartyId { get; set; }

        [Display(Name = "Leger")]
        public int? LedgerEnteryId { get; set; }

        public bool? IsTailor { get; set; }
    }

    public class SalaryPayment : BasicPayrollVocher
    {
        public int SalaryPaymentId { get; set; }

        [Display(Name = "Salary/Year")]
        public string SalaryMonth { get; set; }

        [Display(Name = "Payment Reason")]
        public SalaryComponet SalaryComponet { get; set; }

    }
    public class AdvanceReceipt : BasicPayrollVocher
    {
        public int AdvanceReceiptId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Receipt Date")]
        public new DateTime OnDate { get; set; }
    }
    public class AdvancePayment
    {
        public int AdvancePaymentId { get; set; }
    }
    
    //public class BankDeposit
    //{
    //    public int BankDepositId { get; set; }

    //    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    //    [Display(Name = "Deposit Date")]
    //    public DateTime DepoDate { get; set; }

    //    public int AccountNumberId { get; set; }
    //    public AccountNumber Account { get; set; }

    //    [DataType(DataType.Currency), Column(TypeName = "money")]
    //    public decimal Amount { get; set; }

    //    [Display(Name = "Payment Mode")]
    //    public BankPayMode PayMode { get; set; }

    //    [Display(Name = "Transaction Details")]
    //    public string Details { get; set; }
    //    public string Remarks { get; set; }
    //}
    //public class BankWithdrawal
    //{
    //    public int BankWithdrawalId { get; set; }

    //    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    //    [Display(Name = "Withdrawal Date")]
    //    public DateTime DepoDate { get; set; }

    //    public int AccountNumberId { get; set; }
    //    public AccountNumber Account { get; set; }

    //    [DataType(DataType.Currency), Column(TypeName = "money")]
    //    public decimal Amount { get; set; }

    //    [Display(Name = "Cheques Details")]
    //    public string ChequeNo { get; set; }
    //    [Display(Name = "Signed By")]
    //    public string SignedBy { get; set; }
    //    [Display(Name = "Approved By")]
    //    public string ApprovedBy { get; set; }
    //    [Display(Name = "Self/Named")]
    //    public string InNameOf { get; set; }
    //}

    public class BankAccountInfo
    {
        public int BankAccountInfoId { get; set; }
        public string AccountHolder { get; set; }
        public string AccountNumber { get; set; }
        public int BankId { get; set; }
        public virtual Bank Bank { get; set; }
        public string BranchName { get; set; }
        public string IFSCCode { get; set; }
        public AccountType AccountType { get; set; }
        [Display(Name = "Client Account")]
        public bool IsClientAccount { get; set; }
        public virtual BankAccountSecurityInfo AccountSecurityInfo { get; set; }

    }

    public class BankAccountSecurityInfo
    {
        [ForeignKey("BankAccountInfo")]
        public int BankAccountSecurityInfoId { get; set; }
        public string CustomerId { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string TaxPassword { get; set; }
        public string ExtraPassword { get; set; }
        public string ATMCardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public int CVVNo { get; set; }
        public int ATMPin { get; set; }
        public int TPIN { get; set; }

        public virtual BankAccountInfo BankAccountInfo { get; set; }
    }

    // Debit/Credit Notes
    public class DebitNote : BasicNotes
    {
        public int DebitNoteId { get; set; }

        [Display(Name = "Debit Amount")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public new decimal Amount { get; set; }
    }
    public class CreditNote : BasicNotes
    {
        public int CreditNoteId { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Credit Amount")]
        public new decimal Amount { get; set; }
    }

    public class BasicNotes
    {
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime OnDate { get; set; }

        public int PartyId { get; set; }
        public Accounts.Models.Party PartyName { get; set; }

        public string Particulars { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
    }

    //Purchase system 
    public class GoodsPurchase
    {
        public int PurchaseId { get; set; }
        public DateTime EntryDate { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Particulars { get; set; }
        public decimal BasicAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
    }
    public class PurchaseInWard
    {
        public int PurchaseInWardId { get; set; }

        public int PurchaseId { get; set; }
        public GoodsPurchase Purchase { get; set; }
        public DateTime InWardDate { get; set; }
        public string InWardRefernce { get; set; }
        public string Remarks { get; set; }
    }

    public class GoodsReturn
    {
        public DateTime EntryDate { get; set; }
        public string Particulars { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Remarks { get; set; }
    }
    public class PurchaseReturn : GoodsReturn
    {
        public int PurchaseReturnId { get; set; }
    }
    public class DefectiveGoodsReturn : GoodsReturn
    {
        public int DefectiveGoodsReturnId { get; set; }

    }


}

namespace AprajitaRetails.Areas.Accountings.Operations
{
    public class TranscationOperation
    {
        public void OnInsert(AprajitaRetailsContext db, PayMode modes)
        {

            if (modes == PayMode.Cash) { }
            else
            {
                if (modes == PayMode.Coupons || modes == PayMode.Points || modes == PayMode.Others) { }
                else if (modes == PayMode.MixPayments)
                {

                }
                else { }
            }
        }
        public void OnUpdate(AprajitaRetailsContext db, PayMode modes)
        {
            if (modes == PayMode.Cash) { }
            else
            {
                if (modes == PayMode.Coupons || modes == PayMode.Points || modes == PayMode.Others) { }
                else if (modes == PayMode.MixPayments)
                {

                }
                else { }
            }
        }
        public void OnDelete(AprajitaRetailsContext db, PayMode modes)
        {
            if (modes == PayMode.Cash) { }
            else
            {
                if (modes == PayMode.Coupons || modes == PayMode.Points || modes == PayMode.Others) { }
                else if (modes == PayMode.MixPayments)
                {

                }
                else { }
            }
        }


        public void CashOperations(AprajitaRetailsContext db)
        {

        }
        public void NonCashOperatins(AprajitaRetailsContext db)
        {

        }
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



