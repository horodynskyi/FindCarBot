using System.Threading.Tasks;
using FindCarBot.Domain.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FindCarBot.Domain.Commands
{
    public class StartCommand : TelegramCommand
    {
        public override string Name => @"/start";

        public override bool Contains(Message message)
        {
            var str = message.Text;
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }

        public override async Task Execute(Message message, ITelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;

            var keyBoard = new ReplyKeyboardMarkup
            {
                Keyboard = new[]
                {
                    new[]
                    {
                        new KeyboardButton("Search")
                    }
                }
            };
           keyBoard.ResizeKeyboard=true;
            await botClient.SendTextMessageAsync(chatId, "hello",
                parseMode: ParseMode.Html, disableWebPagePreview: false, disableNotification:false, replyToMessageId: 0,replyMarkup: keyBoard);
        }
    }
}