using System.Linq;
using Telegram.Bot.Types;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Web.Http.ModelBinding;

namespace AprajitaRetails.Ops.Bot.Manager
{   //TODO:Create API To Insert and fetch Data 
    public static class TelegramManager
    {
        private static bool IsUserPresent(AprajitaRetailsContext db, string MobileNo)
        {
            var ctr = db.TelegramAuthUsers.Where (c => c.MobileNo == MobileNo).Count ();
            if ( ctr == null || ctr <= 0 )
                return false;
            else
            {
                return true;
            }
        }
        public static bool AddUser(AprajitaRetailsContext db, Message message, string mobileNo, string passwd)
        {
            if ( !IsUserPresent (db, mobileNo) )
            {
                TelegramAuthUser user = new TelegramAuthUser
                {
                    TelegramChatId = message.Chat.Id,
                    MobileNo = mobileNo,
                    EmpType = EmpType.Others,
                    Password = passwd
                };
                var emp = db.Employees.Where (c => c.MobileNo == mobileNo).FirstOrDefault ();
                if ( emp != null )
                {
                    user.EmpType = emp.Category;
                    user.EmployeeId = emp.EmployeeId;
                    user.TelegramUserName = emp.StaffName;
                }
                else
                {
                    return false;
                }
                db.TelegramAuthUsers.Add (user);
                if ( db.SaveChanges () > 0 )
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        public static TelegramAuthUser GetUser(AprajitaRetailsContext db, long chatId)
        {
            var user = db.TelegramAuthUsers.Where (c => c.TelegramChatId == chatId).FirstOrDefault ();
            if ( user != null )
            { return user; }
            else
                return null;

        }

        public static bool AddUserByApi(Message message, string mobileNo, string passwd)
        {
         
            if ( !ApiHelper.GetIsUserPresent(  mobileNo) )
            {
                TelegramAuthUser user = new TelegramAuthUser
                {
                    TelegramChatId = message.Chat.Id,
                    MobileNo = mobileNo,
                    EmpType = EmpType.Others,
                    Password = passwd
                };
                var emp = ApiHelper.GetEmployee(mobileNo);
                if ( emp != null )
                {
                    user.EmpType = emp.Category;
                    user.EmployeeId = emp.EmployeeId;
                    user.TelegramUserName = emp.StaffName;
                }
                else
                {
                    return false;
                }

                return ApiHelper.AddTelegramUser (user);
                
            }
            else
            {
                return false;
            }

        }


    }


    public class ApiHelper
    {
        public const string Url = "http://localhost:44334/api/";
        public static Employee GetEmployee(string mobileNo)
        {

            using ( var client = new HttpClient () )
            {
                client.BaseAddress = new Uri (Url);
                //HTTP GET
                var responseTask = client.GetAsync ("Employees/?mobileNo=" + mobileNo);
                responseTask.Wait ();
                var result = responseTask.Result;
                if ( result.IsSuccessStatusCode )
                {
                    var readTask = result.Content.ReadAsAsync<Employee> ();
                    readTask.Wait ();
                    return readTask.Result;
                }
                else
                {
                    return null;
                }
            }
        }

        public static bool GetIsUserPresent(string MobileNo)
        {

            using ( var client = new HttpClient () )
            {
                client.BaseAddress = new Uri (Url);
                //HTTP GET
                var responseTask = client.GetAsync ("TelegramAuthUser/?mobileNo=" + MobileNo);
                responseTask.Wait ();
                var result = responseTask.Result;
                if ( result.IsSuccessStatusCode )
                {
                    var readTask = result.Content.ReadAsAsync<bool> ();
                    readTask.Wait ();
                    return readTask.Result;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool AddTelegramUser(TelegramAuthUser authUser)
        {
            using ( var client = new HttpClient () )
            {
                client.BaseAddress = new Uri (Url + "TelegramAuthUsers");
                //client.BaseAddress = new Uri ("http://localhost:64189/api/student");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<TelegramAuthUser> ("TelegramAuthUsers", authUser);
                postTask.Wait ();

                var result = postTask.Result;
                if ( result.IsSuccessStatusCode )
                {
                    return true;
                }
                else
                    return false;
            }
        }
    }
}



