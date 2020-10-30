using AprajitaRetails.Areas.Voyager.Models;
using System;
using System.ComponentModel;

//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Models
{
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

        //Version 3.0
        [DefaultValue(1)]
        public int? StoreId { get; set; }

        public virtual Store Store { get; set; }
        public string UserName { get; set; }
    }

    // Entry mode  on any transcation on bank.
    // statement upload.

    public class BankTranscation
    {
        public int BankTranscationId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime OnDate { get; set; }

        public string Particulars { get; set; }
        public string Details { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal InAmount { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal OutAmount { get; set; }
        public bool IsDeposited { get; set; }
        public int RefId { get; set; }
        public string UserName { get; set; }
    }

    //S No.Value Date Transaction Date Cheque Number Transaction Remarks Withdrawal Amount (INR ) Deposit Amount (INR ) Balance (INR )
    public class BankStatement
    {
        public int ID { get; set; }
        public int AccountNumberId { get; set; }
        public virtual AccountNumber AccountNumber { get; set; }
        public DateTime OnDateValue { get; set; }
        public DateTime OnDateTranscation { get; set; }
        public string ChequeNumber { get; set; }
        public string TransactionRemarks { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal WithdrawalAmount { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal DepositAmount { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Balance { get; set; }
        public Remark Remark { get; set; }
    }
}