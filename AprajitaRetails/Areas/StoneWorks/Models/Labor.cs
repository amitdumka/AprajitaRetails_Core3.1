using System;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.StoneWorks.Models
{
    public class RawStock
    {
        public int RawStockId { get; set; }
        public DateTime OnDate { get; set; }
        public double Quantity { get; set; }
        public decimal Amount { get; set; }
    }
    public class SparePart : BasicEntry
    {
        public int SparePartId { get; set; }

    }
    public class RepairCost : BasicEntry
    {
        public int RepairCostId { get; set; }
    }
    public class ElectricityBill : BasicEntry
    {
        public int ElectricityBillId { get; set; }
        public string BillingMonth { get; set; }
        public decimal MeterReading { get; set; }
        public DateTime? MeterReadingDate { get; set; }
    }
    public class Fooding : BasicEntry
    {
         public int FoodingId { get; set; }
        public string ItemDetails { get; set; }
    }
    public class BasicEntry
    {
        public DateTime OnDate { get; set; }
        public string Particulars { get; set; }
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
