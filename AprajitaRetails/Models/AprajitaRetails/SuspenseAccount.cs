using System;
//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace AprajitaRetails.Models
{
    //Suspenses
    public class SuspenseAccount
    {
        public int SuspenseAccountId { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime EntryDate { get; set; }
        [Display(Name = "ReferanceDetails")]
        public string ReferanceDetails { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        [Display(Name = "In Amount(+)")]
        public decimal InAmount { get; set; }
        [Display(Name = "Out Amount(-)")]
        public decimal OutAmount { get; set; }
        [Display(Name = "Is Cleared")]
        public bool IsCleared { get; set; }
        [Display(Name = "Cleared Details")]
        public string ClearedDetails { get; set; }
        [Display(Name = "Review By")]
        public string ReviewBy { get; set; }
    }

   

}
