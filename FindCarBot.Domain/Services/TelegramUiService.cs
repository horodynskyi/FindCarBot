using System.Collections.Generic;
using FindCarBot.Domain.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;

namespace FindCarBot.Domain.Services
{
    public class TelegramUiService<T>:ITelegramUiService<T> where T:class
    {
        public IEnumerable<KeyboardButton> KeyboardByParameters(IEnumerable<T> items)
        {
            throw new System.NotImplementedException();
        }
    }
}