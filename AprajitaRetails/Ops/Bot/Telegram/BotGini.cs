using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using AprajitaRetails.Data;
using System.Net.Http;
using System.Net;

namespace AprajitaRetails.Ops.Bot.Telegram
{
    public class GiniChannel
    {
        static string Channel = "@AprajitaRetails";
        static string PublicChannel = "@TheArvindStoreDumka";
        static string token = "";

        public static void SendToChannel(string message)
        {
            using (var httpClient = new HttpClient())
            {
                var res = httpClient.GetAsync(
                    $"https://api.telegram.org/bot{token}/sendMessage?chat_id={Channel}&text=ololo"
                    ).Result;
                if (res.StatusCode == HttpStatusCode.OK)
                { /* done, go check your channel */ }
                else
                { /* something went wrong */ }
            }
        }
    }
    //https://api.telegram.org/bot{my_bot_token}/getWebhookInfo
    //https://api.telegram.org/bot123456:ABC-DEF1234ghIkl-zyx57W2v1u123ew11/setWebhook?url=https://www.example.com/my-telegram-bot


    /// <summary>
    /// This Class is main class for BotGini which will send telegram update to users on events.
    /// May be used to chat also based on AI Command.// But Future USe
    /// </summary>
    public class BotGini
    {
        static GiniHandler handler;
        static ITelegramBotClient botClient;
        private const string AccessToken = "1052323717:AAGQ5KLR0akg6LLa0a3XB1b2sfdZ_gdOQ-o";
        public void AssignHandler(EventHandler<MessageEventArgs> OnMessage_Handler) { botClient.OnMessage += OnMessage_Handler; }
        public async Task SetupGini(AprajitaRetailsContext db)
        {
            if (botClient == null)
            {
                botClient = new TelegramBotClient(AccessToken);
                var me = botClient.GetMeAsync().Result;
                Console.WriteLine($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");
                handler = new GiniHandler(db);
                botClient.OnMessage += GiniHandler.OnMessage;
                botClient.StartReceiving();

            }
            else
            {
                botClient.OnMessage += GiniHandler.OnMessage;
            }
        }
        public async Task SetupGini(EventHandler<MessageEventArgs> OnMessage_Handler = null, string Token = AccessToken)
        {
            if (botClient == null)
            {
                botClient = new TelegramBotClient(AccessToken);
                var me = botClient.GetMeAsync().Result;
                Console.WriteLine($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");

                if (OnMessage_Handler == null)
                    botClient.OnMessage += Bot_OnMessage;
                else
                    botClient.OnMessage += OnMessage_Handler;
                botClient.StartReceiving();

            }
            else
            {
                if (OnMessage_Handler == null)
                    botClient.OnMessage += Bot_OnMessage;
                else
                    botClient.OnMessage += OnMessage_Handler;
            }
        }
        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");
                await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: "User said:\n" + e.Message.Text + "(chatId:" + e.Message.Chat.Id + ")");
            }



        }
        /// <summary>
        /// Send Message to User
        /// </summary>
        /// <param name="chatId">Chat id of Users</param>
        /// <param name="message">Message to user</param>
        public static async Task SendMessage(long chatId, string message)
        {
            await botClient.SendTextMessageAsync(chatId: chatId, text: message);
        }
        /// <summary>
        /// It Send messages to List of users.
        /// </summary>
        /// <param name="chatIds">List of ChatID</param>
        /// <param name="message">Message</param>
        public static async void SendMessage(List<long> chatIds, string message)
        {
            foreach (var chatId in chatIds)
            {
                await botClient.SendTextMessageAsync(chatId: chatId, text: message);

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


