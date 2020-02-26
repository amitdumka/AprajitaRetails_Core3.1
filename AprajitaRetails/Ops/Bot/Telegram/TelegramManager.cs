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
       
        public static bool AddUserByApi(Message message, string mobileNo, string passwd)
        {
            TelegramAuthUser user = new TelegramAuthUser
            {
                TelegramChatId = message.Chat.Id,
                MobileNo = mobileNo,
                Password = passwd,
                TelegramUserName = "AddInfo"
            };


            return ApiHelper.AddTelegramUser(user);

        }


    }
    public class ApiHelper
    {
       // public const string Url = "https://www.aprajitaretails.in/api/";
        public const string Url = "https://localhost:44334/api/";
        public static Employee GetEmployee(string mobileNo)
        {

            using ( var client = new HttpClient () )
            {
                client.BaseAddress = new Uri (Url);
                //HTTP GET
                var responseTask = client.GetAsync ("Employees/" + mobileNo);
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
        public static Employee GetEmployee(int id)
        {

            using ( var client = new HttpClient () )
            {
                client.BaseAddress = new Uri (Url);
                //HTTP GET
                var responseTask = client.GetAsync ("Employees/" + id);
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
                var responseTask = client.GetAsync ("TelegramAuthUsers/" + MobileNo);
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
        public static string GetMyInfo(long chatid)
        {
            using ( var client = new HttpClient () )
            {
                client.BaseAddress = new Uri (Url);
                //HTTP GET
                var responseTask = client.GetAsync ("ARInfo/" + chatid);
                responseTask.Wait ();
                var result = responseTask.Result;
                if ( result.IsSuccessStatusCode )
                {
                    var readTask = result.Content.ReadAsStringAsync ();
                    readTask.Wait ();
                    return readTask.Result;
                }
                else
                {
                    return "Error";
                }
            }
        }
        public static SortedList<string, decimal> GetSaleData(long chatid)
        {
            using ( var client = new HttpClient () )
            {
                client.BaseAddress = new Uri (Url);
                //HTTP GET
                var responseTask = client.GetAsync ("SaleData/" + chatid   );
                responseTask.Wait ();
                var result = responseTask.Result;
                if ( result.IsSuccessStatusCode )
                {
                    var readTask = result.Content.ReadAsAsync<SortedList<string, decimal>> ();
                    readTask.Wait ();
                    return readTask.Result;
                }
                else
                {
                    return null;
                }
            }
        }
        public static SortedList<string, decimal> GetSaleData(long chatid, bool today)
        {
            using ( var client = new HttpClient () )
            {
                client.BaseAddress = new Uri (Url);
                //HTTP GET
                var responseTask = client.GetAsync ("SaleData/" + chatid+"/1");
                responseTask.Wait ();
                var result = responseTask.Result;
                if ( result.IsSuccessStatusCode )
                {
                    var readTask = result.Content.ReadAsAsync<SortedList<string, decimal>> ();
                    readTask.Wait ();
                    return readTask.Result;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}



