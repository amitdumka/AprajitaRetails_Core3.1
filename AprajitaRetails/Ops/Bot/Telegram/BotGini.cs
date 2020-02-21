using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Args;
using System.IO;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.Options;
using MihaZupan;
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
namespace AprajitaRetails.Ops.Service
{
    public class BotConfiguration
    {
        public string BotToken { get; set; }

        public string Socks5Host { get; set; }

        public int Socks5Port { get; set; }
    }
    public interface IBotService
    {
        TelegramBotClient Client { get; }
    }
    public interface IUpdateService
    {
        Task EchoAsync(Update update);
    }
    public class BotService : IBotService
    {
        private readonly BotConfiguration _config;

        public BotService(IOptions<BotConfiguration> config)
        {
            _config = config.Value;
            // use proxy if configured in appsettings.*.json
            Client = string.IsNullOrEmpty (_config.Socks5Host)
                ? new TelegramBotClient (_config.BotToken)
                : new TelegramBotClient (
                    _config.BotToken,
                    new HttpToSocks5Proxy (_config.Socks5Host, _config.Socks5Port));
        }

        public TelegramBotClient Client { get; }
    }
    public class UpdateService : IUpdateService
    {
        private readonly IBotService _botService;
        private readonly ILogger<UpdateService> _logger;

        public UpdateService(IBotService botService, ILogger<UpdateService> logger)
        {
            _botService = botService;
            _logger = logger;
        }

        public async Task EchoAsync(Update update)
        {
            if ( update.Type != UpdateType.Message )
                return;

            var message = update.Message;

            _logger.LogInformation ("Received Message from {0}", message.Chat.Id);

            switch ( message.Type )
            {
                case MessageType.Text:
                    // Echo each Message
                    await _botService.Client.SendTextMessageAsync (message.Chat.Id, message.Text);
                    break;

                case MessageType.Photo:
                    // Download Photo
                    var fileId = message.Photo.LastOrDefault ()?.FileId;
                    var file = await _botService.Client.GetFileAsync (fileId);

                    var filename = file.FileId + "." + file.FilePath.Split ('.').Last ();
                    using ( var saveImageStream = System.IO.File.Open (filename, FileMode.Create) )
                    {
                        await _botService.Client.DownloadFileAsync (file.FilePath, saveImageStream);
                    }

                    await _botService.Client.SendTextMessageAsync (message.Chat.Id, "Thx for the Pics");
                    break;
            }
        }
    }
}
namespace AprajitaRetails.Ops.Bot.Telegram
{


    /// <summary>
    /// This Class is main class for BotGini which will send telegram update to users on events.
    /// May be used to chat also based on AI Command.// But Future USe
    /// </summary>
    public class BotGini
    {
        static ITelegramBotClient botClient;
        private const string AccessToken = "1052323717:AAGQ5KLR0akg6LLa0a3XB1b2sfdZ_gdOQ-o";
        public void AssignHandler(EventHandler<MessageEventArgs> OnMessage_Handler) { botClient.OnMessage += OnMessage_Handler; }
        public async Task SetupGini(EventHandler<MessageEventArgs> OnMessage_Handler = null, string Token = AccessToken)
        {
            if ( botClient == null )
            {
                botClient = new TelegramBotClient (AccessToken);
                var me = botClient.GetMeAsync ().Result;
                Console.WriteLine ($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");
                if ( OnMessage_Handler == null )
                    botClient.OnMessage += Bot_OnMessage;
                else
                    botClient.OnMessage += OnMessage_Handler;
                botClient.StartReceiving ();
            } else
            {
                if ( OnMessage_Handler == null )
                    botClient.OnMessage += Bot_OnMessage;
                else
                    botClient.OnMessage += OnMessage_Handler;
            }
        }
        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if ( e.Message.Text != null )
            {
                Console.WriteLine ($"Received a text message in chat {e.Message.Chat.Id}.");
                await botClient.SendTextMessageAsync (chatId: e.Message.Chat, text: "You said:\n" + e.Message.Text + "(chatId:" + e.Message.Chat.Id + ")");
            }
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