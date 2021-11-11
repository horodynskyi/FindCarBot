using System.Threading.Tasks;
using FindCarBot.Domain.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FindCarBot.Domain.Commands
{
    public class MainCommand:TelegramCommand
    {
        public override string Name => @"Main";
        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            await client.SendTextMessageAsync(message.Chat.Id, "Choose search parameters:",
                parseMode: ParseMode.Html, disableWebPagePreview: false, disableNotification:false, replyToMessageId: 0);
        }

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }
        
    }
}