using AprajitaRetails.Data;
using AprajitaRetails.Ops.TAS.Mails;
using AprajitaRetails.Ops.Triggers;
using Microsoft.EntityFrameworkCore;
using System;
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

        //TODO: There should not be for just only one store, but It Should be for all Store and shop do in Loop.
        //TODO: Add Logger so that logging info can be traced
        public static void CorrectCashInHand(AprajitaRetailsContext db, int StoreId = 1)
        {
            string eAddress = "amitnarayansah@gmail.com, amit.dumka@gmail.com";
            try
            {
                CashWork cWork = new CashWork();
                cWork.JobOpeningClosingBalance(db, StoreId);

                string EmailMsg = "Cash In Correction and Cash In Bank Is done!. ";
                MyMail.SendEmails($"Cash In Hand/Bank Correction On {DateTime.Now.ToString()}", EmailMsg, eAddress);
            }
            catch (Exception exp)
            {
                MyMail.SendEmails($"Cash In Hand/Bank Correction On {DateTime.Now.ToString()}", "Error Occured: " + exp.Message, eAddress);
            }
        }

        public static async Task CheckTodayAttendanceAsync(AprajitaRetailsContext db, int StoreId = 1)
        {
            string eAddress = "amitnarayansah@gmail.com, amit.dumka@gmail.com";
            try
            {
                var todayPresent = await db.Attendances.Include(c => c.Employee).Where(c => c.StoreId == StoreId && c.AttDate.Date == DateTime.Today.Date).OrderBy(c => c.IsTailoring).OrderBy(c=>c.Status).ToListAsync();
                var EmpList = await db.Employees.Where(c => c.IsWorking && c.StoreId == StoreId && c.Category!=EmpType.Owner).ToListAsync();
                int count = 0;
                string EmailMsg = "List of Employee whose Attendance are marked.\n ";
                foreach (var item in todayPresent)
                {
                    count++;
                    if(item.Status==AttUnits.Present || item.Status==AttUnits.HalfDay)
                    EmailMsg += $"{count}#  {item.Employee.StaffName}  is  {GetAttUnitName(item.Status)} and came at {item.EntryTime} {IsLate(item.EntryTime)}.\n";
                    else
                        EmailMsg += $"{count}#  {item.Employee.StaffName}  is  {GetAttUnitName(item.Status)}.\n and came at {item.EntryTime} {IsLate(item.EntryTime)}.\n";
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

                Console.WriteLine("Email=" + eAddress);
                MyMail.SendEmails($"Attendance Report On {DateTime.Now.ToString()}", EmailMsg, eAddress);
                return;
            }
            catch (Exception ex)
            {
                MyMail.SendEmails($"Attendance Report On {DateTime.Now.ToString()}", "Error Occured: " + ex.Message, eAddress);
            }
        }

        private static string GetAttUnitName(AttUnits att)
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

        private static string IsLate(string time)
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