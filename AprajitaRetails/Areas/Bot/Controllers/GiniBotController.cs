using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Data;
using AprajitaRetails.Ops.Bot.Telegram;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Args;

namespace AprajitaRetails.Areas.Bot.Controllers
{
    [Area ("Bot")]
    public class GiniBotController : Controller
    {
        BotGini bot;
        static string Msg = "";
        static long LastChatId = 1024002424;
        private readonly AprajitaRetailsContext _db;
        public GiniBotController(AprajitaRetailsContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(string TXTMessage)
        {

            if ( !string.IsNullOrEmpty (TXTMessage) )
            {
                bot = new BotGini ();
                bot.SetupGini (_db);
                ViewBag.SendMessage = TXTMessage + ViewBag.SendMessage;
                await BotGini.SendMessage (LastChatId, TXTMessage);
                ViewBag.RecMessage = Msg;

            }
            return View ();
        }

    }
}