using System.Collections.Generic;
using FindCarBot.Domain.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;

namespace FindCarBot.Domain.Services
{
    public class SearchService :ISearchService
    {
        private readonly List<ReplyKeyboardMarkup> _commands;

        public SearchService(List<ReplyKeyboardMarkup> commands)
        {
            _commands = commands;
        }

        public List<TelegramCommand> GetAutoAtributes()
        {
            throw new System.NotImplementedException();
        }
    }
}