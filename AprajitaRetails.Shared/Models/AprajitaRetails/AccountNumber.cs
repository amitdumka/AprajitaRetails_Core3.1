//using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace AprajitaRetails.Models
{
    public class AccountNumber
    {
        public int AccountNumberId { get; set; }

        [Display(Name = "Bank Name")]
        public int BankId { get; set; }
        public Bank Bank { get; set; }
        [Display(Name = "Account Number")]
        public string Account { get; set; }
        [Display(Name = "Account Type")]
        public AccountType? AccountType { get; set; }

        public ICollection<BankDeposit> Deposits { get; set; }
        public ICollection<BankWithdrawal> Withdrawals { get; set; }
        public ICollection<BankStatement> BankStatements { get; set; }
    }

}
