using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FindCarBot.Domain.Abstractions;

public interface ICallBackService
{
    Task ProccesCallback(ITelegramBotClient client,CallbackQuery callbackQuery);
}