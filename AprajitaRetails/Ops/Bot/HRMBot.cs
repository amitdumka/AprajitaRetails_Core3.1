using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Data;
using AprajitaRetails.Ops.Bot;
using AprajitaRetails.Ops.Bot.Telegram;

namespace AprajitaRetails.Ops.Bot
{
    public class BotUser
    {
        public string MobileNo { get; set; }
        public long ChatId { get; set; }
        public string StaffName { get; set; }
        public static BotUser GetEmp(AprajitaRetailsContext db, int empId)
        {
            if (!BotGini.IsGiniRunning() )
            {
                Gini.Start ();
            }

            BotUser emp = db.TelegramAuthUsers.Where (c => c.EmployeeId == empId).Select (c => new BotUser { MobileNo = c.MobileNo, ChatId = c.TelegramChatId }).FirstOrDefault ();
            return emp;
        }
        public static BotUser GetEmp(AprajitaRetailsContext db, int staffId, bool IsStaffId)
        {
            if (!BotGini.IsGiniRunning() )
            {
                Gini.Start ();
            }
            string StaffName = db.Salesmen.Find (staffId).SalesmanName;
            BotUser emp = db.TelegramAuthUsers.Where (c => c.TelegramUserName == StaffName).Select (c => new BotUser { MobileNo = c.MobileNo, ChatId = c.TelegramChatId , StaffName=c.TelegramUserName}).FirstOrDefault ();
            if ( emp == null )
            {
                emp = new BotUser { StaffName = StaffName };
            }
            return emp;
        }



    }

    public static class SaleBot
    {
        public static async void NotifySale(AprajitaRetailsContext db, int staffId, decimal amount)
        {
            var emp = BotUser.GetEmp (db, staffId, true);
            if ( emp != null )
            {
                string msg = $"({emp.StaffName}): You have made a Sale of Amount Rs. " + amount + " on date " + DateTime.Now;
                await BotGini.SendMessage (emp.ChatId, msg);
                await BotGini.SendMessage (BotConfig.AmitKumarChatId, msg);
            }
            else
            {
                string msg = $"{emp.StaffName} had made a Sale of Amount Rs. " + amount + " on date " + DateTime.Now;
                
                await BotGini.SendMessage (BotConfig.AmitKumarChatId, msg);
            }
        }
    }


    public static class HRMBot
    {
        public static async void NotifyStaffAttandance(AprajitaRetailsContext db, string StaffName, int empId, AttUnits status, string time)
        {
            var emp = BotUser.GetEmp (db, empId);
            
            if ( emp != null )
            {
                string msg = "StaffName: " + StaffName + " is ";
                if ( status == AttUnits.Present )
                    msg += "present and entry time  is " + time + ".";
                else if ( status == AttUnits.Absent )
                    msg += "absent.";
                else if ( status == AttUnits.HalfDay )
                    msg += "present and entry time is " + time + " and marked as Half day ";
                else if ( status == AttUnits.Sunday )
                    msg += "present and entry time is " + time + ", and Sunday is marked.";
                msg += "    (Date:" + DateTime.Now + ").";
                await BotGini.SendMessage (emp.ChatId, msg);
                await BotGini.SendMessage (BotConfig.AmitKumarChatId, msg);
            }
            else
            {
                string msg = "StaffName: " + StaffName + " is ";
                if ( status == AttUnits.Present )
                    msg += "present and entry time is " + time + ".";
                else if ( status == AttUnits.Absent )
                    msg += "absent.";
                else if ( status == AttUnits.HalfDay )
                    msg += "present and entry time is " + time + " and marked as Half day ";
                else if ( status == AttUnits.Sunday )
                    msg += "present and entry time is " + time + ", and Sunday is marked.";
                msg += "    (Date:" + DateTime.Now + ").";
                await BotGini.SendMessage (BotConfig.AmitKumarChatId, msg);
            }

        }

        public static async void NotifyStaffPayment(AprajitaRetailsContext db, string StaffName, int empId, decimal amount, string remarks , bool IsRec=false)
        {
            var emp = BotUser.GetEmp (db, empId);

            if ( emp != null )
            {
                string msg = "";
                if(IsRec )
                {
                     msg = "We had received from StaffName: " + StaffName + "of Amount: Rs. " + amount + "in respect to " + remarks + ". If you  found amount is not correct kindly report.";
                }
                else
                {
                    msg = "Payment is made to StaffName: " + StaffName + "of Amount: Rs. " + amount + "in respect to " + remarks + ". If you  found amount is not correct kindly report.";
                }

                msg += "    (Date:" + DateTime.Today.Date + ").";
                await BotGini.SendMessage (emp.ChatId, msg);
                await BotGini.SendMessage (BotConfig.AmitKumarChatId, msg);

            }
            else
            {
                string msg = "";
                if ( IsRec )
                {
                    msg = "We had received from StaffName: " + StaffName + "of Amount: Rs. " + amount + "in respect to " + remarks + ". If you  found amount is not correct kindly report.";
                }
                else
                {
                    msg = "Payment is made to StaffName: " + StaffName + "of Amount: Rs. " + amount + "in respect to " + remarks + ". If you  found amount is not correct kindly report.";
                }

                msg += "    (Date:" + DateTime.Today.Date + ").";
               
                await BotGini.SendMessage (BotConfig.AmitKumarChatId, msg);
            }

        }

    }
}
