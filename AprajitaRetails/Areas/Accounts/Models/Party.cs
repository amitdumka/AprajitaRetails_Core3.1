using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.Accounts.Models
{
    public class LedgerMaster
    {
        public int LedgerMasterId { get; set; }

        [ForeignKey("Parties")]
        public int PartyId { get; set; }
        public Party Party { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime CreatingDate { get; set; }

        [Display(Name = "Ledger Type")]
        public LedgerCategory LedgerType { get; set; }


    }

    public class Party
    {
        public int PartyId { get; set; }
        public string PartyName { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "On Date")]
        public DateTime OpenningDate { get; set; }

        [Display(Name = "Opening Balance")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal OpenningBalance { get; set; }

        public string Address { get; set; }
        public string PANNo { get; set; }
        public string GSTNo { get; set; }

        [Display(Name = "Ledger Type")]
        public LedgerCategory LedgerType { get; set; }

        public LedgerMaster LedgerMaster { get; set; }
      //  public virtual ICollection<LedgerEntry> Ledgers { get; set; }
        public virtual ICollection<BasicLedgerEntry> BasicLedgers { get; set; }
    }

    //TODO : There will no direct entry for Ledger Entry , just listing  and editing purpose. 
    //       Editing will be in advance stage, Delete should be there
    public class LedgerEntry
    {
        public int LedgerEntryId { get; set; }

        [Display(Name = "Party Name")]
        public int PartyId { get; set; }
        public virtual Party Party { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime EntryDate { get; set; }

        public string Particulars { get; set; }

        [Display(Name = "Amount In")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal AmountIn { get; set; }

        [ForeignKey("Parties"), Display(Name = "On Account")]
        public int OwnPartyId { get; set; }
        public virtual Party OnParty { get; set; }
        //TODO: Debit /Credit on Own on Ledger like , Cash, Bank account etc.

        [Display(Name = "Amount Out")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal AmountOut { get; set; }

        [Display(Name = "Balance")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Balance { get; set; }
    }

    public class DebitNote : BasicNotes
    {
        public int DebitNoteId { get; set; }

        [Display(Name = "Debit Amount")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public new decimal Amount { get; set; }
    }
    public class CreditNote : BasicNotes
    {
        public int CreditNoteId { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Credit Amount")]
        public new decimal Amount { get; set; }
    }

    public class BasicNotes
    {
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime OnDate { get; set; }

        public int PartyId { get; set; }
        public Party PartyName { get; set; }

        public string Particulars { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
    }


    public class Purchase
    {
        public int PurchaseId { get; set; }
        public DateTime EntryDate { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Particulars { get; set; }
        public decimal BasicAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
    }
    public class PurchaseInWard
    {
        public int PurchaseInWardId { get; set; }

        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; }
        public DateTime InWardDate { get; set; }
        public string InWardRefernce { get; set; }
        public string Remarks { get; set; }
    }

    public class GoodsReturn
    {
        public DateTime EntryDate { get; set; }
        public string Particulars { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Remarks { get; set; }
    }
    public class PurchaseReturn : GoodsReturn
    {
        public int PurchaseReturnId { get; set; }
    }
    public class DefectiveGoodsReturn : GoodsReturn
    {
        public int DefectiveGoodsReturnId { get; set; }

    }



    public class BasicLedgerEntry
    {
        public int BasicLedgerEntryId { get; set; }

        [Display(Name = "Party Name")]
        public int PartyId { get; set; }
        public virtual Party Party { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime EntryDate { get; set; }
        [Display(Name ="On Account Off")]
        public LedgerEntryType EntryType { get; set; }
        public int ReferanceId { get; set; }
        public string Particulars { get; set; }
        [Display(Name = "Amount In")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal AmountIn { get; set; }
        [Display(Name = "Amount Out")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal AmountOut { get; set; }
    }


   

}
