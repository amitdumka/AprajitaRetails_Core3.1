using AprajitaRetails.Areas.Accountings.Models;
using AprajitaRetails.Areas.Uploader.Models;
using AprajitaRetails.Areas.Voyager.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Version 4.0
/// </summary>
namespace AprajitaRetails.Areas.Accountings.Models
{
    //Banking 

    
    public class Bank
    {
        public int BankId { get; set; }
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        public ICollection<BankAccount> Accounts { get; set; }
        public ICollection<BankAccountInfo> BankAccounts { get; set; }
        public ICollection<BankSetting> BankSettings { get; set; }
    }

    public class BankAccount
    {
        public int BankAccountId { get; set; }

        [Display(Name = "Bank Name")]
        public int BankId { get; set; }
        public virtual Bank Bank { get; set; }

        [Display(Name = "Account Number")]
        public string Account { get; set; }

        [Display(Name = "Branch")]
        public string BranchName { get; set; }

        [Display(Name = "Account Type")]
        public AccountType AccountType { get; set; }

        public ICollection<BankTranscation> BankTranscations { get; set; }


    }

    public class BankTranscation
    {
        public int BankTranscationId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Date")]
        public DateTime OnDate { get; set; }

        public int BankAccountId { get; set; }
        public BankAccount Account { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "In Amount")]
        public decimal InAmount { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Out Amount")]
        public decimal OutAmount { get; set; }

        [Display(Name = "Cheques Details")]
        public string ChequeNo { get; set; }

        [Display(Name = "Self/Named")]
        public string InNameOf { get; set; }

        [Display(Name = "Signed By")]
        public string SignedBy { get; set; }

        [Display(Name = "Approved By")]
        public string ApprovedBy { get; set; }

        [DefaultValue(true)]
        public bool IsInHouse { get; set; }

        public PaymentMode PaymentModes { get; set; }
        public string PaymentDetails { get; set; }

        public string Remarks { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }


    }

}

namespace AprajitaRetails.Areas.Accountings.ViewModels
{
    public class BankDeposit
    {
        public int Id { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Deposit Date")]
        public DateTime OnDate { get; set; }

        public int BankAccountId { get; set; }
        public virtual BankAccount Account { get; set; }

        [Display(Name = "Cheques Details")]
        public string ChequeNo { get; set; }

        [Display(Name = "Self/Named")]
        public string InNameOf { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Payment Mode")]
        public PaymentMode PayMode { get; set; }

        [Display(Name = "Transaction Details")]
        public string Details { get; set; }
        public string Remarks { get; set; }

        [DefaultValue(true)]
        public bool IsInHouse { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }



    }
    public class BankWithdrawal
    {
        public int Id { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Withdrawal Date")]
        public DateTime OnDate { get; set; }

        public int BankAccountId { get; set; }
        public virtual BankAccount Account { get; set; }

        [Display(Name = "Cheques Details")]
        public string ChequeNo { get; set; }

        [Display(Name = "Self/Named")]
        public string InNameOf { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Payment Mode")]
        public PaymentMode PayMode { get; set; }

        [Display(Name = "Transaction Details")]
        public string Details { get; set; }
        [Display(Name = "Signed By")]
        public string SignedBy { get; set; }
        [Display(Name = "Approved By")]
        public string ApprovedBy { get; set; }
        public string Remarks { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }



    }

}