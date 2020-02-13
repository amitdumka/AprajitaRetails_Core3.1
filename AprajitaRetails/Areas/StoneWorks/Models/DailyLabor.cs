using System;

namespace AprajitaRetails.Areas.StoneWorks.Models
{
    public class DailyLabor
    {
        public int DailyLaborId { get; set; }
        public string Name { get; set; }
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
