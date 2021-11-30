using System;
using System.Reflection;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using FindCarBot.Domain.Abstractions;
using FindCarBot.Domain.Enums;
using FindCarBot.Domain.Models;
using Microsoft.Extensions.Caching.Memory;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FindCarBot.Domain.Services
{
    
    public class HandleService:IHandleService
    {
        private readonly ITelegramBotClient _client;
        private IMemoryCache _cache;
        private readonly ISearchService _service;
        private readonly IAutoRiaService _riaService;
        public HandleService(ITelegramBotClient client, IMemoryCache cache, ISearchService service, IAutoRiaService riaService)
        {
            _client = client;
            _cache = cache;
            _service = service;
            _riaService = riaService;
        }
        
        public async Task<bool> Contains(Message message)
        {
            
            var model = new PickedParameters();
            model.Id = message.Chat.Id;
            foreach (var item in (ParametersEnum[]) Enum.GetValues(typeof(ParametersEnum)))
            {
                
                Type type = Type.GetType($"FindCarBot.Domain.Models.{item.ToString()}");
                var userButtons = await _riaService.GetParameters(Activator.CreateInstance(type));
                foreach (var userItem in userButtons)
                {
                    if (message.Text == userItem.Name)
                    {
                        //await _client.SendTextMessageAsync(model.Id,userItem.Name);
                        return await Task.FromResult(true);
                    }
                }
            }
            return await Task.FromResult(false);
        }

        public async Task Execute(Message message)
        {
            await _client.SendTextMessageAsync(message.Chat.Id, message.Text,
                parseMode: ParseMode.Html, false, false, 0);
        }
    }
}