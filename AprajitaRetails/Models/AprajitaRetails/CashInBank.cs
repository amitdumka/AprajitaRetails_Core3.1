using System;
using System.ComponentModel;
//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using AprajitaRetails.Areas.Voyager.Models;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace AprajitaRetails.Models
{
    public class CashInBank
    {
        public int CashInBankId { get; set; }
        [Display(Name = "Cash-in-Bank Date")]
       // [Index(IsUnique = true)]
        public DateTime CIBDate { get; set; }
        [Display(Name = "Openning Balance")]
        public decimal OpenningBalance { get; set; }
        [Display(Name = "ClosingBalance")]
        public decimal ClosingBalance { get; set; }
        public decimal CashIn { get; set; }
        [Display(Name = "Cash-Out Amount")]
        public decimal CashOut { get; set; }

        [Display(Name = "CashInBank")]
        public decimal InHand
        {
            get
            {
                return OpenningBalance + CashIn - CashOut;
            }
        }
        //Version 3.0
        [DefaultValue(1)]
        public int? StoreId { get; set; }
        public virtual Store Store { get; set; }
    }


   

}
