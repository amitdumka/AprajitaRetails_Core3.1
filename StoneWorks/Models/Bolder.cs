using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoneWorks.Models
{
    public class Bolder
    {
        public int BolderId { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OnDate { get; set; }
        public string VendorName { get; set; }
        public double Qty { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Rate { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Payment { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? PaymentDate { get; set; }
        public string Remarks { get; set; }
        public string TruckNo { get; set; }
        public bool IsOwnTruck { get; set; }
    }

    
   public class BasicExpense
    {
        public int BasicExpenseId { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OnDate { get; set; }
        public string Particular { get; set; }
        public string PaidBy { get; set; }
        public string PaidTo { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
    }
}


//Bolder
// Labor
//Machine matains
//JCB
// ELE
//
