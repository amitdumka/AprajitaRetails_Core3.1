using AprajitaRetails.Areas.Voyager.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AprajitaRetails.Models
{
    public class Attendance
    {
        public int AttendanceId { get; set; }

        [Display(Name = "Staff Name")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Attendance Date")]
        public DateTime AttDate { get; set; }

        [Display(Name = "Entry Time")]
        public string EntryTime { get; set; }

        public AttUnits Status { get; set; }
        public string Remarks { get; set; }
        public bool? IsTailoring { get; set; }
        //Version 3.0
        [DefaultValue(1)]
        public int? StoreId { get; set; }
        public virtual Store Store { get; set; }
    }



    public class AttendanceVM
    {
        public int AttendanceVMId { get; set; }
        [Display(Name = "Staff Name")]
        public string EmployeeName { get; set; }


        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Attendance Date")]
        public DateTime AttDate { get; set; }

        [Display(Name = "Entry Time")]
        public string EntryTime { get; set; }

        public int Status { get; set; }
        public string Remarks { get; set; }
        public bool? IsTailoring { get; set; }
        public int StoreCode { get; set; }
        public bool? IsDataConsumed { get; set; }
    }
}