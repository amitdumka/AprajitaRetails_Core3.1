using Telegram.Bot;
using Microsoft.Extensions.Options;
using MihaZupan;
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
}
