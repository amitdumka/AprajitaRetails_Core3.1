using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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
    public class RawStock
    {
        public int RawStockId { get; set; }
        public DateTime OnDate { get; set; }
        public double Quantity { get; set; }
        public decimal Amount { get; set; }
    }

    public class Fuel
    {
        public int FuelId { get; set; }
        public DateTime OnDate { get; set; }
        public string PartyName { get; set; }
        public double Qty { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Remarks { get; set; }
        public bool IsOnVechile { get; set; }

    }

    public class FuelConsumtion
    {
        public int FuelConsumtionId { get; set; }
        public double Qty { get; set; }
        public DateTime OnDate { get; set; }
        public string AreaOfUse { get; set; }
        public string Remark { get; set; }

    }

    public class SparePart : BasicEntry
    {
        public int SparePartId { get; set; }

    }
    public class RepairCost : BasicEntry
    {
        public int RepairCostId { get; set; }
    }
    public class StaffSalary { }
    public class DailyAttendance { }
    public class ElectricityBill : BasicEntry
    {
        public int ElectricityBillId { get; set; }
    }
    public class Fooding : BasicEntry
    {

        public int FoodingId { get; set; }
    }
    public class Truck
    {
        public int TruckId { get; set; }
        public string TruckNumber { get; set; }
        public string ChasisNo { get; set; }
        public string EngineNo { get; set; }

        public DateTime DateofRegistration { get; set; }
        public DateTime InsuranceExpiryDate { get; set; }
        public DateTime LastServiceDate { get; set; }
        public bool IsHired { get; set; }
        public string OwnerName { get; set; }
    }
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

    public class ChipSales
    {
        public int ChipSalesId { get; set; }
        public DateTime OnDate { get; set; }
        public string PartyName { get; set; }
        public string TruckNumber { get; set; }
        public string PartyMobileNo { get; set; }
        public string DriverName { get; set; }
        public string DriverMobileNo { get; set; }
        public string Size { get; set; }
        public double Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime ClearDate { get; set; }
        public string Remarks { get; set; }
        public string BillMaker { get; set; }
        public string SlipNo { get; set; }

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
