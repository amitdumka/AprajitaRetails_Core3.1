using System.Net.Http;
using System.Net;

namespace AprajitaRetails.Ops.Bot.Telegram
{
    public class GiniChannel
    {
        static string Channel = "@AprajitaRetails";
        static string PublicChannel = "@TheArvindStoreDumka";
       // static string Token = BotConfig.AccessToken;

        public static bool SendToChannel(string message)
        {
            using ( var httpClient = new HttpClient () )
            {
                var res = httpClient.GetAsync (
                    $"https://api.telegram.org/bot{BotConfig.AccessToken}/sendMessage?chat_id={Channel}&text={message}"
                    ).Result;
                if ( res.StatusCode == HttpStatusCode.OK )
                { return true; }
                else
                { return false; }
            }
        }
        public static bool SendToPublicChannel(string message)
        {
            using ( var httpClient = new HttpClient () )
            {
                var res = httpClient.GetAsync (
                    $"https://api.telegram.org/bot{BotConfig.AccessToken}/sendMessage?chat_id={PublicChannel}&text={message}"
                    ).Result;
                if ( res.StatusCode == HttpStatusCode.OK )
                { return true; }
                else
                { return false; }
            }
        }
    }
}




