using Telegram.Bot.Args;
using Telegram.Bot.Types;
using AprajitaRetails.Data;
using AprajitaRetails.Ops.Bot.Manager;
namespace AprajitaRetails.Ops.Bot.Telegram
{
    public class GiniHandler
    {
        private  static AprajitaRetailsContext db;

        public GiniHandler(AprajitaRetailsContext con)
        {
            db = con;
        }

        const string usage = "Usage:\n" +
                        "/register   - send register\n" +
                        "/help - Help Message\n" +

                        "/request  - request Information";

        private static async void SecondLevelHandler(Message message, string text)
        {
            if ( text.StartsWith ("/mobile ") )
            {
                string [] d = text.Split (",");
                string mob = d [0].Replace ("/mobile", "").Trim ();
                string pass = d [1];

                if ( TelegramManager.AddUser (db, message, mob, pass) )
                {
                    await BotGini.SendMessage (message.Chat.Id, "Congrats, Now You can use this service!");
                }
                else
                {
                    await BotGini.SendMessage (message.Chat.Id, "Sorry, Some error occured while registering you, Kindly try again or contact admin!");
                }
            }else if(text.StartsWith("/staffName ") )
            {

            }
            else
            {
                await BotGini.SendMessage (message.Chat.Id, "Command NotSupported");
                await BotGini.SendMessage (message.Chat.Id, usage);
            }

        }
        public static async void OnMessage(object sender, MessageEventArgs e)
        {
            if ( e.Message.Text != null )
            {
                switch ( e.Message.Text )
                {
                    case "/":
                        break;
                    case "/ATT":
                        break;
                    case "/sale":
                        break;
                    case "/todaysale":
                        break;
                    case "/yearlysale":
                        break;
                    case "/incentive":
                        break;
                    case "/LP":
                        break;
                    case "/staffinfo":
                        break;
                    case "/myInfo":
                        break;
                    case "/register":
                        await BotGini.SendMessage (e.Message.Chat.Id, "type /mobile space your-mobileno, your-password");
                        break;
                    case "/help":
                        await BotGini.SendMessage (e.Message.Chat.Id, usage);
                        break;
                    default:
                        SecondLevelHandler (e.Message, e.Message.Text);
                        break;
                }

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


