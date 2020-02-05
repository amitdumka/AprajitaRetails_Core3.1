using Microsoft.AspNetCore.Authorization;    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AprajitaRetails.Models
{
    //Tailoring
    public class TailoringEmployee
    {
        public int TailoringEmployeeId { get; set; }

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

        public ICollection<TailorAttendance> Attendances { get; set; }
        public ICollection<TailoringSalaryPayment> SalaryPayments { get; set; }
        public ICollection<TailoringStaffAdvancePayment> AdvancePayments { get; set; }
        public ICollection<TailoringStaffAdvanceReceipt> AdvanceReceipts { get; set; }
    }
}