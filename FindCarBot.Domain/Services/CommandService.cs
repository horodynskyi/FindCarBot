using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindCarBot.Domain.Abstractions;
using FindCarBot.Domain.Commands;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FindCarBot.Domain.Services
{
    public class CommandService: ICommandService
    {
        private readonly List<TelegramCommand> _commands;
        private  ITelegramBotClient _client;
        private TelegramCommand _currentCommand;
        public CommandService(ISearchService service,IDistributedCache cache,IConfigureResultService resultService,IConnectionMultiplexer server)
        {
            _commands = new List<TelegramCommand>
            {
                new StartCommand(), 
                new SearchCommand(service),
                new DeleteCacheCommand(cache,server),
                new BagCommand(cache,resultService)
            };
        }

        public List<TelegramCommand> Get() => _commands;
        public bool Contains(Message message, ITelegramBotClient client)
        {
            _client = client;
            foreach (var command in _commands)
            {
                if (command.Contains(message))
                {
                    _currentCommand = command;
                    return true;
                }
            }
            return false;
        }
        public async Task Execute(Message message)
        {
            await _currentCommand.Execute(message, _client);
        }
        
    }
}