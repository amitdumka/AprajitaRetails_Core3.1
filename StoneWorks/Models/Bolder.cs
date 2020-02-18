using System;

namespace  StoneWorks.Models
{
    public class Bolder
    {
        public int BolderId { get; set; }
        public DateTime OnDate { get; set; }
        public string VendorName { get; set; }
        public double Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal Payment { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Remarks { get; set; }
        public string TruckNo { get; set; }
        public bool IsOwnTruck { get; set; }
    }


}


//Bolder
// Labor
//Machine matains
//JCB
// ELE
//
