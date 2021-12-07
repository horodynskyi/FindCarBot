using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ISearchService _service;
        private readonly IAutoRiaService _riaService;
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _options;
        private PickedParameters _model;
        private ReplyKeyboardMarkup _buttons;
        private readonly IConfigureResultService _resultService;
        public HandleService(ITelegramBotClient client, ISearchService service, IAutoRiaService riaService, IDistributedCache cache, IConfigureResultService resultService)
        {
            _client = client;
            _options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddHours(6))
                .SetSlidingExpiration(TimeSpan.FromMinutes(10));
            _service = service;
            _riaService = riaService;
            _cache = cache;
            _resultService = resultService;
        }
        
        public async Task<bool> Contains(Message message)
        {
            _model = await CacheModel.TryGetCache(_cache, message.Chat.Id);
            foreach (var item in (ParametersEnum[]) Enum.GetValues(typeof(ParametersEnum)))
            {
                Type type = Type.GetType($"FindCarBot.Domain.Models.{item.ToString()}");
                if (type != null)
                {
                    IEnumerable<BaseModel> userButtons;
                    if (item.ToString() == ParametersEnum.PriceRange.ToString())
                    {
                        if (_model.FieldsIsNull())
                        {
                            var prices = message.Text.Split("-");
                            _model.SetField(new PriceRange(prices[0],prices[1]));
                            await CacheModel.SetCache(_cache, _model,_options);
                            return await Task.FromResult(true);
                        }
                    }
                    else
                    {
                        userButtons = await _riaService.GetParameters(Activator.CreateInstance(type));
                        foreach (var userItem in userButtons)
                        {
                            if (message.Text == userItem.Name)
                            {
                                if (_model.FieldsIsNull())
                                {
                                    _model.SetField(userItem);
                                    await CacheModel.SetCache(_cache, _model,_options);
                                    return await Task.FromResult(true);
                                }
                            }
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
            if (param != null)
            {
                _buttons = await _service.GetSearchButtons(param);
                var replyMessage = _model.GetMessageFromField(param);
               
                await _client.SendTextMessageAsync(message.Chat.Id, replyMessage,
                    parseMode: ParseMode.Html,replyMarkup:_buttons);
            }
            else
            {
                var adsInfo = await _service.CreateRequest(_model);
                await _resultService.Result(_client, message.Chat.Id, adsInfo);
                _cache.Remove(_model.Id.ToString());
            }
        }
    }
}