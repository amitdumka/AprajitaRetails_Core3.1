using AprajitaRetails.Data;
using AprajitaRetails.Ops.TAS.Mails;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Ops.CornJobs.JobHelper
{
    public class JobHelper
    {
        public static void CorrectCashInHand(AprajitaRetailsContext db, int StoreId = 1)
        {
            //TODO: add cashin hand correction for today or back day
        }


        public static void CheckTodayAttendance(AprajitaRetailsContext db, int StoreId=1)
        {
            var todayPresent = db.Attendances.Include(c=>c.Employee).Where(c => c.StoreId == StoreId && c.AttDate == DateTime.Today.Date).OrderBy(c => c.IsTailoring).ToList();
            var EmpList = db.Employees.Where(c => c.IsWorking && c.StoreId == StoreId).ToList();

            int count = 0;
            string EmailMsg = "List of Employee whose Attendance are marked.\n ";
            string s = "";
            foreach (var item in todayPresent)
            {
                count++;
                EmailMsg += $"{count}#  {item.Employee.StaffName}  is  {GetAttUnitName(item.Status)} came at {item.EntryTime}\n";
                EmpList.Remove(item.Employee);
                //TODO: here option need to added so late marking need to done . 
            }

            EmailMsg += "\n\n List of Employee whose attendance are not marked.\n";
            count = 0;
            foreach (var item in EmpList)
            {
                count++;
                EmailMsg += $"{count}#  {item.StaffName}  attendance is not  marked.\n";

            }
            EmailMsg += $"\n\n  This report is generate automaticly on {DateTime.Now.ToString()}\n"  ;

            string eAddress = "";
            MyMail.SendEmail($"Attendance Report On {DateTime.Now.ToString()}", EmailMsg,eAddress);

        }


        public static string GetAttUnitName(AttUnits att)
        {
            return att switch
            {
                AttUnits.Present => "Present",
                AttUnits.Absent => "Absent",
                AttUnits.HalfDay => "Half Day",
                AttUnits.Sunday => "Sunday Present",
                AttUnits.Holiday => "Holiday",
                AttUnits.StoreClosed => "Store Closed",
                _ => "Error",
            };
        }
    }
}
