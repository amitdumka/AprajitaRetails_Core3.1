using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Data;
using AprajitaRetails.Models;

namespace AprajitaRetails.Ops.Validation
{
    public class DBValidation
    {

        /// <summary>
        /// Check for attendance possible  duplicate entry
        /// </summary>
        /// <param name="db"></param>
        /// <param name="att"></param>
        /// <returns></returns>
        public static bool AttendanceDuplicateCheck(AprajitaRetailsContext db, Attendance att)
        {
            int flag = (int?) db.Attendances.Where (c => c.EmployeeId == att.EmployeeId && c.AttDate == c.AttDate).Select (c => c.AttendanceId).FirstOrDefault () ?? 0;
            if ( flag > 0 )
                return true;
            else
                return false;

        }
    }
}
