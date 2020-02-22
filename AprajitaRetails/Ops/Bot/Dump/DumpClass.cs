using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AprajitaRetails.Ops.Bot.Dump
{
    public class DumpClass
    {
    }
    public class Telegram_My
    {
        IConfiguration _configuration;
        ILogger _logger;

        readonly string _telegramAPI;
        readonly string _botToken;
        readonly string _channel;

        public Telegram_My(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;

            _telegramAPI = _configuration.GetSection("Telegram:API").Value;
            _botToken = _configuration.GetSection("Telegram:BotToken").Value;
            _channel = _configuration.GetSection("Telegram:Channel").Value;
        }

        /// <summary>
        /// Publish to Telegram channel.
        /// </summary>
        /// <returns>0 - successful posting, 1 - published only picture, 2 - nothing has been published</returns>
        /// <param name="post">post to publish</param>
        /// <param name="picture">URL of the image to publish</param>
        public int PublishToTelegramChannel(string post, string picture)
        {
            if (!SendPicture(picture, _channel))
            {
                return 2;
            }

            if (!SendMessage(post, _channel))
            {
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// Send a message to a Telegram chat/channel
        /// </summary>
        /// <param name="msg">Message text</param>
        /// <param name="sendTo">Recepient</param>
        public bool SendMessage(string msg, string sendTo)
        {
            try
            {
                msg = WebUtility.UrlEncode(msg);

                using (var httpClient = new HttpClient())
                {
                    var res = httpClient.GetAsync($"{_telegramAPI}{_botToken}/sendMessage?chat_id={sendTo}&text={msg}&parse_mode=HTML&disable_web_page_preview=true").Result;
                    if (res.StatusCode != HttpStatusCode.OK)
                    {
                        //string content = res.Content.ReadAsStringAsync().Result;
                        //string status = res.StatusCode.ToString();
                        throw new Exception($"Couldn't send a message via Telegram. Response from Telegram API: {res.Content.ReadAsStringAsync().Result}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Send a picture to a Telegram chat/channel
        /// </summary>
        /// <param name="picture">URL of the image to send</param>
        /// <param name="sendTo">Recepient</param>
        public bool SendPicture(string picture, string sendTo)
        {
            try
            {
                picture = WebUtility.UrlEncode(picture);

                using (var httpClient = new HttpClient())
                {
                    var res = httpClient.GetAsync($"{_telegramAPI}{_botToken}/sendPhoto?chat_id={sendTo}&photo={picture}").Result;
                    if (res.StatusCode != HttpStatusCode.OK)
                    {
                        //string content = res.Content.ReadAsStringAsync().Result;
                        //string status = res.StatusCode.ToString();
                        throw new Exception($"Couldn't send a picture to Telegram. Response from Telegram API: {res.Content.ReadAsStringAsync().Result}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
            return true;
        }
    }
}
