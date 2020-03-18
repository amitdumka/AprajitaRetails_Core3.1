//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
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

        public ICollection<BankDeposit> Deposits { get; set; }
        public ICollection<BankWithdrawal> Withdrawals { get; set; }
    }


    //public enum Modes { ON, OFF, Play, Pause, Stop, Upload, Delete}

    //public class PlaySongs
    //{
    //    public int PlaySongsId { get; set; }
    //    public string SongName { get; set; }
    //    public Modes PlayModes { get; set; }
    //}

    //TODO: List
    //TODO: Dues Recovery options
    //TODO: Tailoring 
    //TODO: Sales return policy update and check 
    //TODO: Purchase of Items/Assets
    //TODO: Arvind Payments
    //TODO: Purchase Invoice Entry

}
