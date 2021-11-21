using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace FindCarBot.Domain.Abstractions
{
    public interface ITelegramUiService<T> where T :class
    {
        IEnumerable<KeyboardButton> KeyboardByParameters(IEnumerable<T> items);
    }
}