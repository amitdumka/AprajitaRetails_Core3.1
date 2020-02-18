using System;
using System.Collections.Generic;

namespace  StoneWorks.Models
{
    public class Staff
    {
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public DateTime? DateofBirth { get; set; }
        public DateTime JoiningDate { get; set; }
        public decimal MonthlySalary { get; set; }
        public bool IsWorking { get; set; }
        public DateTime? LeavingDate { get; set; }

        public  ICollection<StaffSalary> Salaries { get; set; }

    }
}


//Bolder
// Labor
//Machine matains
//JCB
// ELE
//
