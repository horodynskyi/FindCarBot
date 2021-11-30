using System.Threading.Tasks;
using FindCarBot.Domain.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FindCarBot.Domain.Commands
{
    public class HelpCommand: TelegramCommand
    {
        public override string Name { get; } = "Help";
        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var keyBoard = new ReplyKeyboardMarkup
            {
                Keyboard = new[]
                {
                    new[]
                    {
                        new KeyboardButton("\U0001F3E0 Main")
                    },
                    new []
                    {
                        new KeyboardButton("\U0001F4D6 Help") 
                    }
                }
            };
            await client.SendTextMessageAsync(chatId, "\U0001F4D6 Help",
                parseMode: ParseMode.Markdown, replyMarkup:keyBoard);
        }

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }
    }
}