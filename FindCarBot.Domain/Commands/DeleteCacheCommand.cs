using System.Threading.Tasks;
using FindCarBot.Domain.Abstractions;
using Microsoft.Extensions.Caching.Distributed;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FindCarBot.Domain.Commands
{
    public class DeleteCacheCommand: TelegramCommand
    {
        public override string Name { get; } = "/deleteCache";
        private readonly IDistributedCache _cache;

        public DeleteCacheCommand(IDistributedCache cache)
        {
            _cache = cache;
        }
        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            await _cache.RemoveAsync(message.Chat.Id.ToString());
            await client.SendTextMessageAsync(message.Chat.Id, "Cache deleted!");
        }

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }
    }
}