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
        static long LastChatId = 1024002424;
        
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
               await  bot.SetupGini (Bot_OnMessage);
               ViewBag.SendMessage = TXTMessage + ViewBag.SendMessage;
               await BotGini.SendMessage (LastChatId, TXTMessage);
               ViewBag.RecMessage = Msg ;
               
            }
            return View();
        }
        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if ( e.Message.Text != null )
            {
                Msg=( "Id(" + e.Message.Chat.Id + "):" + e.Message.Text );
                LastChatId = e.Message.Chat.Id;
               
            }
        }
    }
}