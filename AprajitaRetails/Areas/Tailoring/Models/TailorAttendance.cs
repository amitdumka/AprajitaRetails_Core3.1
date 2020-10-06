using Microsoft.AspNetCore.Authorization;    using System;
using System.ComponentModel.DataAnnotations;

namespace AprajitaRetails.Models
{
    public class TailorAttendance
    {
        public int TailorAttendanceId { get; set; }

        [Display(Name = "Staff Name")]
        public int TailoringEmployeeId { get; set; }

        public TailoringEmployee Employee { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Attendance Date")]
        public DateTime AttDate { get; set; }

        [Display(Name = "Entry Time")]
        public string EntryTime { get; set; }

        public AttUnit Status { get; set; }
        public string Remarks { get; set; }
    }
}