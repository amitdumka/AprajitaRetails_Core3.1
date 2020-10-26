using AprajitaRetails.Areas.Voyager.Models;
using System;
using System.ComponentModel;
//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace AprajitaRetails.Models
{
    public class CashInBank
    {
        public int CashInBankId { get; set; }
        [Display(Name = "Cash-in-Bank Date")]
        // [Index(IsUnique = true)]
        public DateTime CIBDate { get; set; }
        [Display(Name = "Opening Balance")]
        [DataType(DataType.Currency), Column(TypeName = "money")]

        public decimal OpenningBalance { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        [Display(Name = "ClosingBalance")]
        public decimal ClosingBalance { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CashIn { get; set; }
        [Display(Name = "Cash-Out Amount")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
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
