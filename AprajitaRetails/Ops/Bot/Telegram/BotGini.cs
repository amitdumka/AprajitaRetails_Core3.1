using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AprajitaRetails.Data;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace AprajitaRetails.Ops.Bot.Telegram
{
    public sealed class BotConfig
    {
        public static readonly string AccessToken = "1052323717:AAGQ5KLR0akg6LLa0a3XB1b2sfdZ_gdOQ-o";
        public static readonly string BotName = "Gini_ARBot";
        public static readonly long AmitKumarChatId = 775142634;
    }

    //https://api.telegram.org/bot{my_bot_token}/getWebhookInfo
    //https://api.telegram.org/bot123456:ABC-DEF1234ghIkl-zyx57W2v1u123ew11/setWebhook?url=https://www.example.com/my-telegram-bot

    /// <summary>
    /// This Class is main class for BotGini which will send telegram update to users on events.
    /// May be used to chat also based on AI Command.// But Future USe
    /// </summary>
    public class BotGini
    {
        private static GiniHandler handler;
        private static ITelegramBotClient botClient;

        public void AssignHandler(EventHandler<MessageEventArgs> OnMessageHandler)
        {
            botClient.OnMessage += OnMessageHandler;
        }

            public void SetupGini(EventHandler<MessageEventArgs> OnMessageHandler = null)
        {
            if ( botClient == null )
            {
                botClient = new TelegramBotClient (BotConfig.AccessToken);
                var me = botClient.GetMeAsync ().Result;
                Console.WriteLine ($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");
                if ( OnMessageHandler == null )
                {
                    handler = new GiniHandler ();
                    botClient.OnMessage += GiniHandler.OnMessageWithApi;
                }
                else
                    botClient.OnMessage += OnMessageHandler;
            }
            else
            {
                if ( OnMessageHandler == null )
                {
                    if ( handler == null )
                        handler = new GiniHandler ();
                    botClient.OnMessage += GiniHandler.OnMessageWithApi;
                }
                else
                    botClient.OnMessage += OnMessageHandler;
            }
            botClient.StartReceiving ();
        }

        /// <summary>
        /// Send Message to User
        /// </summary>
        /// <param name="chatId">Chat id of Users</param>
        /// <param name="message">Message to user</param>
        public static async Task SendMessage(long chatId, string message)
        {
            await botClient.SendTextMessageAsync (chatId: chatId, text: message);
        }

        /// <summary>
        /// It Send messages to List of users.
        /// </summary>
        /// <param name="chatIds">List of ChatID</param>
        /// <param name="message">Message</param>
        public static async void SendMessage(List<long> chatIds, string message)
        {
            foreach ( var chatId in chatIds )
            {
                await botClient.SendTextMessageAsync (chatId: chatId, text: message);
            }
        }

        public void StopGini()
        {
            if ( botClient != null )
            {
                botClient.StopReceiving ();
                botClient = null;
            }
        }
    }

    public class Gini
    {
        private static BotGini bot;

        public static void Start()
        {
            if ( bot == null )
            {
                bot = new BotGini ();
                bot.SetupGini ();
                _ = BotGini.SendMessage (BotConfig.AmitKumarChatId, "Gini Service is started");
            }
            else
                bot.SetupGini ();
        }

       

        public static void Stop()
        {
            _ = BotGini.SendMessage (BotConfig.AmitKumarChatId, "Gini Service is stopping");
            bot.StopGini ();
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