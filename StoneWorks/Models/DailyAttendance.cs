using System;
using System.ComponentModel.DataAnnotations;

namespace  StoneWorks.Models
{
    public class DailyAttendance {

        public int DailyAttendanceId { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OnDate { get; set; }
        public string StaffLabourName { get; set; }
        public AttUnits Status { get; set; }
        public string Remarks { get; set; }

    }
}


//Bolder
// Labor
//Machine matains
//JCB
// ELE
//
