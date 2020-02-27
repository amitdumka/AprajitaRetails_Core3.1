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
    }
}
