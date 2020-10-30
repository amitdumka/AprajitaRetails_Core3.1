//using System.Data.Entity;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AprajitaRetails.Areas.Voyager.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Models
{
    public class MonthEnd
    {
        //Table Data
        public int MonthEndId { get; set; }
        public DateTime EntryDate { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }

        //Sale Info

        public double TotalBill { get; set; }
        public double TotalFabric { get; set; }
        public double TotalRMZ { get; set; }
        public double TotalAccess { get; set; }
        public double TotalOthers { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]

        public decimal TotalAmountFabric { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")] public decimal TotalAmountRMZ { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")] public decimal TotalAmountAccess { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")] public decimal TotalAmountOthers { get; set; }


        [DataType(DataType.Currency), Column(TypeName = "money")] public decimal TotalSaleIncome { get; set; } //Done
        [DataType(DataType.Currency), Column(TypeName = "money")] public decimal TotalTailoringIncome { get; set; } //Done
        [DataType(DataType.Currency), Column(TypeName = "money")] public decimal TotalOtherIncome { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")] public decimal TotalInward { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")] public decimal TotalInwardByAmitKumar { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")] public decimal TotalInwardOthers { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")] public decimal TotalDues { get; set; }//DOne
        [DataType(DataType.Currency), Column(TypeName = "money")] public decimal TotalDuesOfMonth { get; set; }//Done
        [DataType(DataType.Currency), Column(TypeName = "money")] public decimal TotalDuesRecovered { get; set; }//Check for Correct table is required

        [DataType(DataType.Currency), Column(TypeName = "money")] public decimal TotalExpenses { get; set; } //Check it 

        [DataType(DataType.Currency), Column(TypeName = "money")] public decimal TotalOnBookExpenes { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")] public decimal TotalCashExpenses { get; set; }//Check it

        [DataType(DataType.Currency), Column(TypeName = "money")] public decimal TotalSalary { get; set; }//Done
        [DataType(DataType.Currency), Column(TypeName = "money")] public decimal TotalTailoringExpenses { get; set; }//Done
        [DataType(DataType.Currency), Column(TypeName = "money")] public decimal TotalTrimsAndOtherExpenses { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")] public decimal TotalHomeExpenses { get; set; }//Check 
        [DataType(DataType.Currency), Column(TypeName = "money")] public decimal TotalOtherHomeExpenses { get; set; }//Check

        [DataType(DataType.Currency), Column(TypeName = "money")] public decimal TotalOtherExpenses { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")] public decimal TotalPayments { get; set; }//done
        [DataType(DataType.Currency), Column(TypeName = "money")] public decimal TotalRecipts { get; set; }//done

        //Version 3.0
        [DefaultValue(1)]
        public int? StoreId { get; set; }
        public virtual Store Store { get; set; }
    }



}
