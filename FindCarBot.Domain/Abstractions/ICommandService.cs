using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FindCarBot.Domain.Abstractions
{
    public interface ICommandService
    {
        List<TelegramCommand> Get();
        bool Contains(Message message,ITelegramBotClient client);
        Task Execute(Message message);
    }
}