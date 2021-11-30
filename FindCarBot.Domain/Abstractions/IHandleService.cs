using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FindCarBot.Domain.Abstractions
{
    public interface IHandleService
    {
        Task<bool> Contains(Message message);
        Task Execute(Message message);

    }
}