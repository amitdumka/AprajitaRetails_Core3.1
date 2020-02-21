using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Ops.Bot.Telegram;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Args;

namespace AprajitaRetails.Areas.Bot.Controllers
{
    [Area("Bot")]
    public class GiniBotController : Controller
    {
        BotGini bot;
      static   string Msg = "";
        public GiniBotController()
        {
            // bot = new BotGini ();
           // bot.SetupGini (Bot_OnMessage);

        }
        public async Task<IActionResult> Index(string TXTMessage)
        {
           
            if ( !string.IsNullOrEmpty (TXTMessage) )
            {
                bot = new BotGini ();
                bot.SetupGini (Bot_OnMessage);
                ViewBag.SendMessage = TXTMessage;
               await BotGini.SendMessage (00917667178482, TXTMessage);
                ViewBag.RecMessage = Msg;
                

            }
            return View();
        }
        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if ( e.Message.Text != null )
            {
                Console.WriteLine ($"Received a text message in chat {e.Message.Chat.Id}.");

                 Msg=( e.Message.Text + "(chatId:" + e.Message.Chat.Id + ")");

            }
        }
    }
}