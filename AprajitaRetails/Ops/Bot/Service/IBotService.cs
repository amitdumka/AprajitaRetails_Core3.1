using Telegram.Bot;
namespace AprajitaRetails.Ops.Service
{
    public interface IBotService
    {
        TelegramBotClient Client { get; }
    }
}
