using System.Threading.Tasks;
using Telegram.Bot.Types;
namespace AprajitaRetails.Ops.Service
{
    public interface IUpdateService
    {
        Task EchoAsync(Update update);
    }
}
