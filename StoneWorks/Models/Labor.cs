using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StoneWorks.Models
{
    public class JCB
    {
        public int JCBId { get; set; }
        public string JCBName { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InstallDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? UnInStallDate { get; set; }
        public bool IsOnRent { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public virtual ICollection<JCB> JCBs { get; set; }


    }
    public class JCBRunning
    {
        public int JCBRunningId { get; set; }
        public int JCBId { get; set; }
        public virtual JCB JCB { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OnDate { get; set; }
        public int Hour { get; set; }
        public int Min { get; set; }
        public decimal Fuels { get; set; }


    }

    public class Engine
    {
        public int EngineId { get; set; }
        public string EngineName { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InstallDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? UnInStallDate { get; set; }
        public bool IsOnRent { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public virtual ICollection<Engine> Engines { get; set; }
    }

    public class EngineRunning
    {
        public int EngineRunningId { get; set; }
        public int EngineId { get; set; }
        public virtual Engine Engine { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OnDate { get; set; }
        public int Hour { get; set; }
        public int Min { get; set; }
        public decimal Fuels { get; set; }
    }
    public class RawStock
    {
        public int RawStockId { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OnDate { get; set; }
        public double Quantity { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }
    }

    public class SparePart : BasicEntry
    {
        public int SparePartId { get; set; }
        public string SparePartName { get; set; }
        public string SparePartUsedFor { get; set; }
        public string VendorName { get; set; }
        public string VendorMobileNo { get; set; }
        public DateTime? PaymentDate { get; set; }
        public bool IsPaid { get; set; }

    }
    public class RepairCost : BasicEntry
    {
        public int    RepairCostId { get; set; }
        public string RepairReason { get; set; }
        public string ReparirDoneBy { get; set; }
        public string RepairPersonMobileNo { get; set; }
        public bool IsThisFirstRepari { get; set; }
        public DateTime? LastRepairDate { get; set; }
    }
    public class ElectricityBill : BasicEntry
    {
        public int ElectricityBillId { get; set; }
        public string BillingMonth { get; set; }
        public decimal MeterReading { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? MeterReadingDate { get; set; }
    }
    public class Fooding : BasicEntry
    {
        public int FoodingId { get; set; }
        public string ItemDetails { get; set; }
    }
    public class BasicEntry
    {
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OnDate { get; set; }
        public string Particulars { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
    }
}


