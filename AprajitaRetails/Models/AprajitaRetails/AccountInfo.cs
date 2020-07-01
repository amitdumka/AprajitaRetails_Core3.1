using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Models
{

    //Bank Name   
    //Branch 
    //Account Holder Account No 
    //CUSTOMER ID USERID  Password Tax Password Extra Password ATM CARD ATM PIN Exp Date CCV TIP ACCOUNT TYPE Account Balance Date    IFSCE Code

   

    public class BankAccountInfo
    {
        public int BankAccountInfoId { get; set; }
        public string AccountHolder { get; set; }
        public string AccountNumber { get; set; }
        public int BankId { get; set; }
        public virtual Bank Bank { get; set; }
        public string BranchName { get; set; }
        public string IFSCCode { get; set; }
        public AccountType  AccountType { get; set; }
        [Display(Name="Client Account")]
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
}
