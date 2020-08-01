using System;
using System.ComponentModel;
//using System.Data.Entity;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;


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

        public decimal TotalAmountFabric { get; set; }
        public decimal TotalAmountRMZ { get; set; }
        public decimal TotalAmountAccess { get; set; }
        public decimal TotalAmountOthers { get; set; }


        public decimal TotalSaleIncome { get; set; } //Done
        public decimal TotalTailoringIncome { get; set; } //Done
        public decimal TotalOtherIncome { get; set; }

        public decimal TotalInward { get; set; }
        public decimal TotalInwardByAmitKumar { get; set; }
        public decimal TotalInwardOthers { get; set; }

        public decimal TotalDues { get; set; }//DOne
        public decimal TotalDuesOfMonth { get; set; }//Done
        public decimal TotalDuesRecovered { get; set; }//Check for Correct table is required

        public decimal TotalExpenses { get; set; } //Check it 

        public decimal TotalOnBookExpenes { get; set; }
        public decimal TotalCashExpenses { get; set; }//Check it

        public decimal TotalSalary { get; set; }//Done
        public decimal TotalTailoringExpenses { get; set; }//Done
        public decimal TotalTrimsAndOtherExpenses { get; set; }

        public decimal TotalHomeExpenses { get; set; }//Check 
        public decimal TotalOtherHomeExpenses { get; set; }//Check

        public decimal TotalOtherExpenses { get; set; }

        public decimal TotalPayments { get; set; }//done
        public decimal TotalRecipts { get; set; }//done

        //Version 3.0
        [DefaultValue(1)]
        public int? StoreLocationId { get; set; }
        public virtual StoreLocation Store { get; set; }
    }

  

}
