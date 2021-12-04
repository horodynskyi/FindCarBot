using System;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using FindCarBot.Domain.Abstractions;
using FindCarBot.Domain.Enums;
using FindCarBot.Domain.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FindCarBot.Domain.Services
{
    
    public class HandleService:IHandleService
    {
        private readonly ITelegramBotClient _client;
       // private IMemoryCache _cache;
        private readonly ISearchService _service;
        private readonly IAutoRiaService _riaService;
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _options;
        private PickedParameters _model;
        private ReplyKeyboardMarkup _buttons;
        public HandleService(ITelegramBotClient client, ISearchService service, IAutoRiaService riaService, IDistributedCache cache)
        {
            _client = client;
            _options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddHours(6))
                .SetSlidingExpiration(TimeSpan.FromMinutes(10));
            _service = service;
            _riaService = riaService;
            _cache = cache;
        }
        
        public async Task<bool> Contains(Message message)
        {
            _model = await CacheModel.TryGetCache(_cache, message.Chat.Id);
            foreach (var item in (ParametersEnum[]) Enum.GetValues(typeof(ParametersEnum)))
            {
                Type type = Type.GetType($"FindCarBot.Domain.Models.{item.ToString()}");
                if (type != null)
                {
                    var userButtons = await _riaService.GetParameters(Activator.CreateInstance(type));
                    foreach (var userItem in userButtons)
                    {
                        if (message.Text == userItem.Name)
                        {
                            if (_model.FieldsIsNull())
                            {
                                _model.SetField(item.ToString(),userItem);
                                //await _cache.SetStringAsync(message.Chat.Id.ToString(),JsonConvert.SerializeObject(_model),_options);
                                await CacheModel.SetCache(_cache, _model);
                                return await Task.FromResult(true);
                            }
                            //await _client.SendTextMessageAsync(_model.Id,userItem.Name);
                        }
                    }
                }
            }
            return await Task.FromResult(false);
        }

        public async Task Execute(Message message)
        {
            _model = await CacheModel.TryGetCache(_cache, message.Chat.Id);
            var param = _model.Next();
            
            if (param != null && _model.FieldsIsNull()==true)
            {
                if (param is ModelAuto)
                {
                    _buttons = await _service.GetSearchButtons(_model.GetMark().Value);
                }
                else
                    _buttons = await _service.GetSearchButtons(param);
                var replyMessage = _model.GetMessageFromField(param);
                
                await _client.SendTextMessageAsync(message.Chat.Id, replyMessage,
                    parseMode: ParseMode.Html,replyMarkup:_buttons);
            }
            else
            {
                var keys = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("google", "www.google.com")
                    } 
                });
                await _client.SendTextMessageAsync(message.Chat.Id, "You choosed all parameters...\n <b> see your result here</b>",
                    parseMode: ParseMode.Html,replyMarkup:keys);
            }
        }
    }
}