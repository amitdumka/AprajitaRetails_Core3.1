using Microsoft.AspNetCore.Authorization;    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AprajitaRetails.Models
{
    
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Display(Name = "Staff Name")]
        public string StaffName { get; set; }

        [Display(Name = "Mobile No"), Phone]
        public string MobileNo { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Joining Date")]
        public DateTime JoiningDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Leaving Date")]
        public DateTime? LeavingDate { get; set; }

        [Display(Name = "Is Working")]
        public bool IsWorking { get; set; }
        [Display (Name ="Job Category")]
        [DefaultValue(0)]
        public EmpType Category { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<SalaryPayment> SalaryPayments { get; set; }
        public ICollection<StaffAdvancePayment> AdvancePayments { get; set; }
        public ICollection<StaffAdvanceReceipt> AdvanceReceipts { get; set; }
        public ICollection<PettyCashExpense> CashExpenses { get; set; }
        public ICollection<Expense> Expenses { get; set; }

        public virtual ICollection<CurrentSalary> CurrentSalaries { get; set; }

    }

}