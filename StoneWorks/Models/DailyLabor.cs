using System;
using System.ComponentModel.DataAnnotations;

namespace  StoneWorks.Models
{
    public class DailyLabor
    {
        public int DailyLaborId { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OnDate { get; set; }
        public bool IsPresent { get; set; }
        public bool IsDailyBillable { get; set; }
        public decimal Amount { get; set; }
        public decimal ExtraAmount { get; set; }
        public string Remarks { get; set; }
    }


}


//Bolder
// Labor
//Machine matains
//JCB
// ELE
//
