using AprajitaRetails.Data;
using AprajitaRetails.Ops.TAS.Mails;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AprajitaRetails.Ops.CornJobs.JobHelpers
{
    public class JobHelper
    {
        public const string EntryTime = "10:00";
        public const int HR = 10;
        public const int MIN = 00;
      
        
        public static void CorrectCashInHand(AprajitaRetailsContext db, int StoreId = 1)
        {
            //TODO: add cashin hand correction for today or back day
        }
        
        public static async Task CheckTodayAttendanceAsync(AprajitaRetailsContext db, int StoreId=1)
        {
            try
            {
                var todayPresent = await db.Attendances.Include(c => c.Employee).Where(c => c.StoreId == StoreId && c.AttDate.Date == DateTime.Today.Date).OrderBy(c => c.IsTailoring).ToListAsync();
                var EmpList = await db.Employees.Where(c => c.IsWorking && c.StoreId == StoreId).ToListAsync();
                int count = 0;
                string EmailMsg = "List of Employee whose Attendance are marked.\n ";
                foreach (var item in todayPresent)
                {
                    count++;
                    EmailMsg += $"{count}#  {item.Employee.StaffName}  is  {GetAttUnitName(item.Status)} and came at {item.EntryTime} {IsLate(item.EntryTime)}\n";
                    EmpList.Remove(item.Employee);
                }
                if (todayPresent.Count < 1) return;
                EmailMsg += "\n\n List of Employee whose attendance are not marked.\n";
                count = 0;
                foreach (var item in EmpList)
                {
                    count++;
                    EmailMsg += $"{count}#  {item.StaffName}  attendance is not  marked.\n";
                }
                EmailMsg += $"\n\n  This report is generate automatically on {DateTime.Now.ToString()}\n";
                string eAddress = "amitnarayansah@gmail.com, amit.dumka@gmail.com";
                Console.WriteLine("Email=" + eAddress);
                MyMail.SendEmails($"Attendance Report On {DateTime.Now.ToString()}", EmailMsg, eAddress);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;

            }
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

        public static string IsLate(string time)
        {
            try
            {
                var resultString = Regex.Match(time, @"\d+").Value;
                int hr = Int32.Parse(resultString.Trim());
                time = time.Replace(resultString, "");
                resultString = Regex.Match(time, @"\d+").Value;
                int min = Int32.Parse(resultString.Trim());
                TimeSpan time1 = new TimeSpan(hr, min, 00);
                TimeSpan time2 = new TimeSpan(HR, MIN, 00);
                TimeSpan timeSpan = time2.Subtract(time1);
                if (timeSpan.TotalMinutes > -10)
                {
                    return "";

                }
                else
                {
                    return "is Late";
                }

            }
            catch (Exception)
            {
                return "";
                
            }
           

            
        }
    }
}
