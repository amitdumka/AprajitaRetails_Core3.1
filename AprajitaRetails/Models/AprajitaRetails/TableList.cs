//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace AprajitaRetails.Models
{

    //Banking Section
    public class Bank
    {
        public int BankId { get; set; }
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        public ICollection<AccountNumber> Accounts { get; set; }
        public ICollection<BankAccountInfo> BankAccounts { get; set; }
        public ICollection<Areas.Uploader.Models.BankSetting> BankSettings { get; set; }
        public ICollection<Areas.Accountings.Models.BankAccount> BankAcc { get; set; }

    }

}
