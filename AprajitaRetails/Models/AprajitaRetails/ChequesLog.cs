using System;
//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace AprajitaRetails.Models
{
    public class ChequesLog
    {
        public int ChequesLogId { get; set; }
        [Display(Name ="Bank Name")]
        public string BankName { get; set; }
        [Display(Name = "Account No")]
        public string AccountNumber { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Cheques Date")]
        public DateTime? ChequesDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Deposit Date")]
        public DateTime? DepositDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Cleared Date")]
        public DateTime? ClearedDate { get; set; }
        [Display(Name = "By")]
        public string IssuedBy { get; set; }
        [Display(Name = "To")]
        public string IssuedTo { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }
        [Display(Name = "PDC")]
        public bool IsPDC { get; set; }
        [Display(Name = "By Aprajita Retails")]
        public bool IsIssuedByAprajitaRetails { get; set; }
        [Display(Name = "To Aprajita Retails")]
        public bool IsDepositedOnAprajitaRetails { get; set; }
        public string Remarks { get; set; }


        public string UserName { get; set; }
    }

  
}
