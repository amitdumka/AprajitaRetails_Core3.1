using System.Linq;
using Telegram.Bot.Types;
using AprajitaRetails.Data;
using AprajitaRetails.Models;

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
            if ( !IsUserPresent(db,mobileNo) )
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
    }
}
