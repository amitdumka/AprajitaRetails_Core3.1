using AprajitaRetails.Data;
using AprajitaRetails.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.RestAPI.Models
{
    public class DBHelper
    {
        public static TelegramAuthUser GetChatUser(AprajitaRetailsContext db, long chatId)
        {
            return db.TelegramAuthUsers.Where(c => c.TelegramChatId == chatId).FirstOrDefault();

        }

        public static bool IsValidEmployee(AprajitaRetailsContext db, int EmpId, string UserName)
        {
            return db.EmployeeUsers.Where(c => c.EmployeeId == EmpId && c.UserName == UserName).Select(c => c.IsWorking).FirstOrDefault();
           
        }
    }
}
