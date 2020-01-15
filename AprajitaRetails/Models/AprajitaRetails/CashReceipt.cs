using System;
//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace AprajitaRetails.Models
{
    public class CashReceipt
    {
        public int CashReceiptId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Receipt Date")]
        public DateTime InwardDate { get; set; }

        [Display(Name = "Mode")]
        public int TranscationModeId { get; set; }
        public TranscationMode Mode { get; set; }

        [Display(Name = "Receipt From"), Required]
        public string ReceiptFrom { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }
        [Display(Name = "Receipt No")]
        public string SlipNo { get; set; }

    }

    //TODO: List
    //TODO: Dues Recovery options
    //TODO: Tailoring 
    //TODO: Sales return policy update and check 
    //TODO: Purchase of Items/Assets
    //TODO: Arvind Payments
    //TODO: Purchase Invoice Entry

}
