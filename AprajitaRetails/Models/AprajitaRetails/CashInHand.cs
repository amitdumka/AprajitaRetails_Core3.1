using System;
//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace AprajitaRetails.Models
{
    // Daily Sale  and Cash Management


    public class CashInHand
    {
        public int CashInHandId { get; set; }
       // [Index(IsUnique = true)]
        [Display(Name = "Cash-in-hand Date")]
        public DateTime CIHDate { get; set; }
        [Display(Name = "Openning Balance")]
        public decimal OpenningBalance { get; set; }
        [Display(Name = "ClosingBalance")]
        public decimal ClosingBalance { get; set; }
        [Display(Name = "Cash-In Amount")]
        public decimal CashIn { get; set; }
        [Display(Name = "Cash-Out Amount")]
        public decimal CashOut { get; set; }

        [Display(Name = "CashInHand")]
        public decimal InHand
        {
            get
            {
                return OpenningBalance + CashIn - CashOut;
            }
        }
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
