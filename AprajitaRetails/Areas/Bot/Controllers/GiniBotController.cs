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
       // static string Msg = "";
        static long LastChatId = BotConfig.AmitKumarChatId;
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
                bot.SetupGini ();
                ViewBag.SendMessage = TXTMessage + ViewBag.SendMessage;
                await BotGini.SendMessage (LastChatId, TXTMessage);
               // ViewBag.RecMessage = Msg;

            }
            return View ();
        }
        public static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            await BotGini.SendMessage (e.Message.Chat.Id, "We got Message from you, We are processing Kindly wait....");
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
                        await BotGini.SendMessage (e.Message.Chat.Id, "type /mobile space your-mobile-no, your-password");
                        break;

                    case "/help":
                        await BotGini.SendMessage (e.Message.Chat.Id, "Helping");
                        break;

                    default:
                        await BotGini.SendMessage (e.Message.Chat.Id, "You Said:"+e.Message.Text);
                        break;
                }
            }
        }
    }
}