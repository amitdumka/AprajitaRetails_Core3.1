using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using System.IO;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Web.Http;
using File = System.IO.File;

namespace AprajitaRetails.Ops.Bot.BasicBot
{
    public static class Bot
    {
        public static readonly TelegramBotClient Api = new TelegramBotClient ("1052323717:AAGQ5KLR0akg6LLa0a3XB1b2sfdZ_gdOQ-o");
        public class WebHookController : ApiController
        {
            public async Task<IHttpActionResult> Post(Update update)
            {
                var message = update.Message;

                Console.WriteLine ("Received Message from {0}", message.Chat.Id);

                if ( message.Type == MessageType.Text )
                {
                    // Echo each Message
                    await Bot.Api.SendTextMessageAsync (message.Chat.Id, message.Text);
                }
                else if ( message.Type == MessageType.Photo )
                {
                    // Download Photo
                    var file = await Bot.Api.GetFileAsync (message.Photo.LastOrDefault ()?.FileId);

                    var filename = file.FileId + "." + file.FilePath.Split ('.').Last ();

                    using ( var saveImageStream = File.Open (filename, FileMode.Create) )
                    {
                        await Bot.Api.DownloadFileAsync (file.FilePath, saveImageStream);
                    }

                    await Bot.Api.SendTextMessageAsync (message.Chat.Id, "Thx for the Pics");
                }

                return Ok ();
            }
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


