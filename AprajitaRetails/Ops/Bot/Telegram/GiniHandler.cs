﻿using Telegram.Bot.Args;
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
                    await BotGini.SendMessage (message.Chat.Id, "Sorry, Either User is already registered or  some error occurred while registering you, Kindly try again or contact admin!");
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

