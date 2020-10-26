using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace AprajitaRetails.Ops.Bot.Telegram
{

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
            if (botClient == null)
            {
                botClient = new TelegramBotClient(BotConfig.AccessToken);
                var me = botClient.GetMeAsync().Result;
                Console.WriteLine($"Hello, I am user {me.Id} and my name is {me.FirstName}.");
                if (OnMessageHandler == null)
                {
                    handler = new GiniHandler();
                    botClient.OnMessage += GiniHandler.OnMessageWithApi;
                }
                else
                    botClient.OnMessage += OnMessageHandler;
            }
            else
            {
                if (OnMessageHandler == null)
                {
                    if (handler == null)
                    {
                        handler = new GiniHandler();
                        botClient.OnMessage += GiniHandler.OnMessageWithApi;
                    }
                }
                else
                    botClient.OnMessage += OnMessageHandler;
            }
            botClient.StartReceiving();
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
        public static async Task AskLocationMessage(long chatId)
        {
            await botClient.SendTextMessageAsync(chatId: chatId, text: "Share your contact & location",
                       replyMarkup: new ReplyKeyboardMarkup(new[] {
                        new [] { KeyboardButton.WithRequestContact ("Share Contact") },
                        new [] { KeyboardButton.WithRequestLocation ("Share Location") } }
                       ));
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

        public void StopGini()
        {
            if (botClient != null)
            {
                botClient.StopReceiving();
                botClient = null;
            }
        }

        public static bool IsGiniRunning()
        {
            if (botClient == null || !botClient.IsReceiving || botClient.BotId <= 0)
                return false;
            else
                return true;
        }

    }

    public class Gini
    {
        private static BotGini bot;
        public static void Start()
        {
            if (bot == null)
            {
                bot = new BotGini();
                bot.SetupGini();
                _ = BotGini.SendMessage(BotConfig.AmitKumarChatId, "Gini Service is started");
            }
            else
                bot.SetupGini();
        }
        public static void Stop()
        {
            _ = BotGini.SendMessage(BotConfig.AmitKumarChatId, "Gini Service is stopping");
            bot.StopGini();
        }
    }
}

