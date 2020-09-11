using System;
using System.ComponentModel;
//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AprajitaRetails.Areas.Voyager.Models;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;


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

   

}
