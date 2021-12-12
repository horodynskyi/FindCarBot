using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FindCarBot.Domain.Abstractions;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FindCarBot.Domain.Commands
{
    public class DeleteCacheCommand: TelegramCommand
    {
        public override string Name { get; } = "/deletecache";
        private readonly IDistributedCache _cache;
        private readonly IConnectionMultiplexer _multiplexer;
        private readonly IDatabase _database;
        private readonly IServer _server;
    

        public DeleteCacheCommand(IDistributedCache cache, IConnectionMultiplexer multiplexer)
        {
            _cache = cache;
            _multiplexer = multiplexer;
            _server = multiplexer.GetServer("localhost",6379);
            _database = multiplexer.GetDatabase();
        }
        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            var keys = _server.Keys();
            var keysList = keys.Select(key => (string) key).ToList();
            foreach (var key in keysList)
            {
                if (key.Contains(message.From.Id.ToString()) && !key.Contains("bag"))
                {
                    _database.KeyDelete(key);
                }
            }
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