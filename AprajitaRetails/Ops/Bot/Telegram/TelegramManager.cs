using System.Linq;
using Telegram.Bot.Types;
using AprajitaRetails.Data;
using AprajitaRetails.Models;

namespace AprajitaRetails.Ops.Bot.Manager
{   //TODO:Create API To Insert and fetch Data 
    public static class TelegramManager
    {
        public static bool AddUser(AprajitaRetailsContext db, Message message, string mobileNo, string passwd)
        {
            TelegramAuthUser user = new TelegramAuthUser
            {
                TelegramChatId = message.Chat.Id,
                TelegramUserName = message.Chat.FirstName + " " + message.Chat.LastName??"",
                MobileNo = mobileNo,
                EmpType = EmpType.Others,
                Password = passwd

            };

            var emp = db.Employees.Where (c => c.MobileNo == mobileNo).FirstOrDefault ();
            
            if ( emp != null )
            {
                user.EmpType = emp.Category;
                user.EmployeeId = emp.EmployeeId;

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


//TODO:
// Add Passport Suuport.
//Add File Upload Support
// Add AI Comand Support LIke my Addtance , total Sale, For Sale Staff, 
// Command For StoreManager Like Attance of Staff, sale, Dues Pendinng Order, Alerts, Etc.


//List of Options to send message Implement which ever is requried
//1 Formated Text
//Message message = await botClient.SendTextMessageAsync(
//chatId: e.Message.Chat, // or a chat id: 123456789
//text: "Trying *all the parameters* of `sendMessage` method",
//parseMode: ParseMode.Markdown,
//disableNotification: true,
//replyToMessageId: e.Message.MessageId,
//replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithUrl(
//"Check sendMessage method",
//"https://core.telegram.org/bots/api#sendmessage"
//))
//);

//2 Photo
//Message message = await botClient.SendPhotoAsync(
//  chatId: e.Message.Chat,
//  photo: "https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg",
//  caption: "<b>Ara bird</b>. <i>Source</i>: <a href=\"https://pixabay.com\">Pixabay</a>",
//  parseMode: ParseMode.Html
//);

//3 Contacts 
//Message msg = await botClient.SendContactAsync(
//    chatId: e.Message.Chat.Id,
//    phoneNumber: "+1234567890",
//    firstName: "Han",
//    lastName: "Solo"
//);
//4 send via card contact
//Message msg = await botClient.SendContactAsync(
//    chatId: e.Message.Chat.Id,
//    phoneNumber: "+1234567890",
//    firstName: "Han",
//    vCard: "BEGIN:VCARD\n" +
//            "VERSION:3.0\n" +
//            "N:Solo;Han\n" +
//            "ORG:Scruffy-looking nerf herder\n" +
//            "TEL;TYPE=voice,work,pref:+1234567890\n" +
//            "EMAIL:hansolo@mfalcon.com\n" +
//            "END:VCARD"
//);

//5 Map Location 
//Message msg = await botClient.SendVenueAsync(
//    chatId: e.Message.Chat.Id,
//    latitude: 50.0840172f,
//    longitude: 14.418288f,
//    title: "Man Hanging out",
//    address: "Husova, 110 00 Staré Město, Czechia"
//);

//6 point location
//Message message = await botClient.SendLocationAsync(
//    chatId: e.Message.Chat.Id,
//    latitude: 33.747252f,
//    longitude: -112.633853f
//);

//7

//https://92796f08.ngrok.io/key: url, value:https://92796f08.ngrok.io/api/update

//https://yoursubdomain.ngrok.io/api/update


