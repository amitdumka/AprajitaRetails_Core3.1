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
    public class AppInfo
    {
        public int AppInfoId { get; set; }
        public string Version { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime UpdateOn { get; set; }
        public string DatabaseVersion { get; set; }
        public bool IsEffective { get; set; }
    }
    //Ledger System
    public class LedgerMaster
    {
        public int LedgerMasterId { get; set; }

        [ForeignKey("Parties")]
        public int PartyId { get; set; }
        public Party Party { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime CreatingDate { get; set; }

        [Display(Name = "Ledger Type")]
        public LedgerType LedgerType { get; set; }


    }

    public class Party
    {
        public int PartyId { get; set; }
        public string PartyName { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "On Date")]
        public DateTime OpenningDate { get; set; }

        [Display(Name = "Opening Balance")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal OpenningBalance { get; set; }

        public string Address { get; set; }
        public string PANNo { get; set; }
        public string GSTNo { get; set; }

        [Display(Name = "Ledger Type")]
        public LedgerType LedgerType { get; set; }

        public LedgerMaster LedgerMaster { get; set; }
        public virtual ICollection<LedgerEntry> Ledgers { get; set; }
        public virtual ICollection<BasicLedgerEntry> BasicLedgers { get; set; }
    }

    //TODO : There will no direct entry for Ledger Entry , just listing  and editing purpose. 
    //       Editing will be in advance stage, Delete should be there
    public class LedgerEntry
    {
        public int LedgerEntryId { get; set; }

        [Display(Name = "Party Name")]
        public int PartyId { get; set; }
        public virtual Party Party { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime EntryDate { get; set; }

        public string Particulars { get; set; }

        [Display(Name = "Amount In")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal AmountIn { get; set; }

        [ForeignKey("Parties"), Display(Name = "On Account")]
        public int OwnPartyId { get; set; }
        public virtual Party OnParty { get; set; }
        //TODO: Debit /Credit on Own on Ledger like , Cash, Bank account etc.

        [Display(Name = "Amount Out")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal AmountOut { get; set; }

        [Display(Name = "Balance")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Balance { get; set; }
    }
    public class BasicLedgerEntry
    {
        public int BasicLedgerEntryId { get; set; }

        [Display(Name = "Party Name")]
        public int PartyId { get; set; }
        public virtual Party Party { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime EntryDate { get; set; }
        [Display(Name = "On Account Off")]
        public LedgerEntryType EntryType { get; set; }
        public int ReferanceId { get; set; }
        public string Particulars { get; set; }
        [Display(Name = "Amount In")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal AmountIn { get; set; }
        [Display(Name = "Amount Out")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal AmountOut { get; set; }
    }
    //Expenses system
    public class ExpenseVM : BaseVM { }
    public class PaymentVM : BaseVM { }
    public class RecieptVM : BaseVM { }

    public class BaseVM
    {
        public int PartyId { get; set; }
        public int LedgerId { get; set; }
        public int LedgerEnrtryId { get; set; }
        public DateTime DateTime { get; set; }
        public string Narations { get; set; }
    }

    public class BasicVoucher
    {
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "On Date")]
        public DateTime OnDate { get; set; }

        [Display(Name = "Party Name")]
        public string PartryName { get; set; }

        [Display(Name = "Payment Mode")]
        public PaymentModes PayMode { get; set; }

        [Display(Name = "Payment Details")]
        public string PaymentDetails { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Amount")]
        public decimal Amount { get; set; }


        public string Remarks { get; set; }

        [Display(Name = "Party")]
        public int? PartyId { get; set; }

        [Display(Name = "Leger")]
        public int? LedgerEnteryId { get; set; }

    }

    public class Payment : BasicVoucher
    {
        public int PaymentId { get; set; }

        [Display(Name = "Payment Party")]
        public string PartyName { get; set; }


        [Display(Name = "Payment Slip No")]
        public string PaymentSlipNo { get; set; }

    }

    public class Receipt : BasicVoucher
    {
        public int ReceiptId { get; set; }

        [Display(Name = "Receipt From ")]
        public string PartName { get; set; }

        [Display(Name = "Receipt Slip No ")]
        public string RecieptSlipNo { get; set; }
    }

    public class Expense : BasicVoucher
    {
        public int ExpenseId { get; set; }
        public string Particulars { get; set; }
        [Display(Name = "Paid By")]
        public int EmployeeId { get; set; }
        public virtual AprajitaRetails.Models.Employee PaidBy { get; set; }

        [Display(Name = "Paid To")]
        public string PartyName { get; set; }


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
        public PaymentModes PayMode { get; set; }

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

    //Banking 
    public class Bank
    {
        public int BankId { get; set; }
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        public ICollection<AccountNumber> Accounts { get; set; }
        public ICollection<BankAccountInfo> BankAccounts { get; set; }
    }
    public class AccountNumber
    {
        public int AccountNumberId { get; set; }

        [Display(Name = "Bank Name")]
        public int BankId { get; set; }
        public Bank Bank { get; set; }

        [Display(Name = "Account Number")]
        public string Account { get; set; }

        public ICollection<BankDeposit> Deposits { get; set; }
        public ICollection<BankWithdrawal> Withdrawals { get; set; }
    }
    public class BankDeposit
    {
        public int BankDepositId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Deposit Date")]
        public DateTime DepoDate { get; set; }

        public int AccountNumberId { get; set; }
        public AccountNumber Account { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Payment Mode")]
        public BankPayModes PayMode { get; set; }

        [Display(Name = "Transaction Details")]
        public string Details { get; set; }
        public string Remarks { get; set; }
    }
    public class BankWithdrawal
    {
        public int BankWithdrawalId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Withdrawal Date")]
        public DateTime DepoDate { get; set; }

        public int AccountNumberId { get; set; }
        public AccountNumber Account { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Cheques Details")]
        public string ChequeNo { get; set; }
        [Display(Name = "Signed By")]
        public string SignedBy { get; set; }
        [Display(Name = "Approved By")]
        public string ApprovedBy { get; set; }
        [Display(Name = "Self/Named")]
        public string InNameOf { get; set; }
    }

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
        public void OnInsert(AprajitaRetailsContext db, PayModes modes)
        {

            if (modes == PayModes.Cash) { }
            else
            {
                if (modes == PayModes.Coupons || modes == PayModes.Points || modes == PayModes.Others) { }
                else if (modes == PayModes.MixPayments)
                {

                }
                else { }
            }
        }
        public void OnUpdate(AprajitaRetailsContext db, PayModes modes)
        {
            if (modes == PayModes.Cash) { }
            else
            {
                if (modes == PayModes.Coupons || modes == PayModes.Points || modes == PayModes.Others) { }
                else if (modes == PayModes.MixPayments)
                {

                }
                else { }
            }
        }
        public void OnDelete(AprajitaRetailsContext db, PayModes modes)
        {
            if (modes == PayModes.Cash) { }
            else
            {
                if (modes == PayModes.Coupons || modes == PayModes.Points || modes == PayModes.Others) { }
                else if (modes == PayModes.MixPayments)
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
//    public PaymentModes PayMode { get; set; }
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
//    public PaymentModes PayMode { get; set; }

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
//    public PaymentModes PayMode { get; set; }
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



