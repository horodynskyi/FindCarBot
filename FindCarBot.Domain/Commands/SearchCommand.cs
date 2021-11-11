using System.Threading.Tasks;
using FindCarBot.Domain.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FindCarBot.Domain.Commands
{
    public class SearchCommand:TelegramCommand
    {
        public override string Name => "Search";
        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            var keyBoard = new ReplyKeyboardMarkup
            {
                Keyboard = new[]
                {
                    new[]
                    {
                        new KeyboardButton("Main"),
                        new KeyboardButton("Help"),
                        new KeyboardButton("Search")
                    }
                }
            };
           // await client.
        }

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;
            return message.Text.Contains(Name);
        }
    }
}