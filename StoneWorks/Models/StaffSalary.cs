using System;

namespace  StoneWorks.Models
{
    public class StaffSalary {
        public int StaffSalaryId { get; set; }
        public DateTime OnDate { get; set; }
        public int StaffId { get; set; }
        
        public virtual  Staff Staff { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal CalcualteAmount { get; set; }
        public string Remarks { get; set; }

    }
}


//Bolder
// Labor
//Machine matains
//JCB
// ELE
//
