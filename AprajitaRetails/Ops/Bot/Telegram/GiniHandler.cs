using AprajitaRetails.Data;
using AprajitaRetails.Ops.Bot.Manager;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace AprajitaRetails.Ops.Bot.Telegram
{
    public class GiniHandler
    {
        private const string usage = "Usage:\n" +
                        "/register   - send register\n" +
                        "/help - Help Message\n" +
                        "/request  - request Information";
        private const string requestUsage = "requests:\n" +
            "/sale    -Sale Data of Current Month\n" +
            "/ATT     - Get Current month Attendances\n" +
            "/todaysale - Get Current date sale\n" +
            "/myinfo    - get Your Information";

        private static AprajitaRetailsContext db;

        public GiniHandler()
        {
        }

        public static async void OnMessageWithApi(object sender, MessageEventArgs e)
        {
            // await BotGini.SendMessage (e.Message.Chat.Id, "We got Message from you, We are processing Kindly wait....");
            if ( e.Message.Text != null )
            {
                switch ( e.Message.Text )
                {   
                    case "/request":
                        await BotGini.SendMessage (e.Message.Chat.Id, requestUsage);
                        break;

                    case "/ATT":
                        break;

                    case "/sale":
                        var data = ApiHelper.GetSaleData (e.Message.Chat.Id);
                        string msg = "Current MonthSale: \n";
                        if ( data != null )
                        {
                            foreach ( var item in data )
                            {
                                msg += "StaffName: " + item.Key + "\t Amount: " + item.Value + ".\n";

                            }
                        }
                        else
                        {
                            msg = "Some Error Occurred, Kindly Try again!!";
                        }
                        
                        await BotGini.SendMessage (e.Message.Chat.Id, msg);
                        break;

                    case "/todaysale":
                        var data1 = ApiHelper.GetSaleData (e.Message.Chat.Id, true);
                        string msg1 = "Today Sale: \n";
                        if ( data1 != null )
                        {
                            foreach ( var item in data1 )
                            {
                                msg1 += "StaffName: " + item.Key + "\t Amount: " + item.Value + ".\n";

                            }
                        }
                        else
                        {
                            msg1 = "Some Error Occurred, Kindly Try again!!";
                        }

                        await BotGini.SendMessage (e.Message.Chat.Id, msg1);
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
                    case "/start":
                        await BotGini.SendMessage (e.Message.Chat.Id, "Welcome to Aprajita Retails Gini Bot!");
                        break;
                    default:
                        SecondLevelHandlerWithApi (e.Message, e.Message.Text);

                        break;
                }
            }
        }


        private static async void SecondLevelHandlerWithApi(Message message, string text)
        {
            if ( text.StartsWith ("/mobile ") )
            {
                string [] d = text.Split (" ");
                string mob = d [1].Trim ();
                string pass = d [2].Trim ();

                if ( TelegramManager.AddUserByApi (message, mob, pass) )
                {
                    await BotGini.SendMessage (message.Chat.Id, "Congrats, Now You can use this service!");
                }
                else
                {
                    await BotGini.SendMessage (message.Chat.Id, "Sorry, Either User is already registered or  some error occurred while registering you, Kindly try again or contact admin!");
                }
            }
            else if ( text.StartsWith ("/staffName ") )
            {
            }
            else
            {
                await BotGini.SendMessage (message.Chat.Id, "Command NotSupported");
                await BotGini.SendMessage (message.Chat.Id, usage);
            }
        }
    }
}