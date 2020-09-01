using System;
using System.ComponentModel;
//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AprajitaRetails.Areas.Voyager.Models;

namespace AprajitaRetails.Models
{
    // Daily Sale  and Cash Management


    public class CashInHand
    {
        public int CashInHandId { get; set; }
       // [Index(IsUnique = true)]
        [Display(Name = "Cash-in-hand Date")]
        public DateTime CIHDate { get; set; }
        [Display(Name = "Opening Balance")]
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal OpenningBalance { get; set; }
        [Display(Name = "ClosingBalance")]
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal ClosingBalance { get; set; }
        [Display(Name = "Cash-In Amount")]
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal CashIn { get; set; }
        [Display(Name = "Cash-Out Amount")]
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal CashOut { get; set; }

        [Display(Name = "CashInHand")]
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
