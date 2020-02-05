using Microsoft.AspNetCore.Authorization;    using System;
//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace AprajitaRetails.Models
{
    public class BankDeposit
    {
        public int BankDepositId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Deposit Date")]
        public DateTime DepoDate { get; set; }

        public int AccountNumberId { get; set; }
        public AccountNumber Account { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Display(Name = "Payment Mode")]
        public BankPayModes PayMode { get; set; }

        [Display(Name = "Transcation Details")]
        public string Details { get; set; }
        public string Remarks { get; set; }
    }

    //TODO: List
    //TODO: Dues Recovery options
    //TODO: Tailoring 
    //TODO: Sales return policy update and check 
    //TODO: Purchase of Items/Assets
    //TODO: Arvind Payments
    //TODO: Purchase Invoice Entry

}
