using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Areas.StoneWorks.Models
{
    public class HiredTruck
    {
        public int HiredTruckId { get; set; }
        [ForeignKey("Truck")]
        public int TruckId { get; set; }
        public string HiredFrom { get; set; }
        public decimal Rate { get; set; }
        public DateTime HiredDate { get; set; }
        public DateTime? SurrenderDate { get; set; }
        public virtual Truck Trucks { get; set; }
    }


}


//Bolder
// Labor
//Machine matains
//JCB
// ELE
//
