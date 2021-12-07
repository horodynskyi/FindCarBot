using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FindCarBot.Domain.Abstractions;
using FindCarBot.Domain.Models;
using FindCarBot.Domain.Utils;
using FindCarBot.IoC.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types.ReplyMarkups;

namespace FindCarBot.Domain.Services
{
    public class SearchService : AutoRiaService,ISearchService
    {
        private readonly IMemoryCache _cache;
        public SearchService(IConfiguration configuration, HttpClient httpClient, IOptions<AutoRiaOptions> options, IMemoryCache cache) 
            : base(httpClient, options)
        {
            _cache = cache;
        }

        private async Task<ReplyKeyboardMarkup> GetReplyMarkup<T>(T entity) where T:IEnumerable<BaseModel>
        {
            List<List<KeyboardButton>> keys = new List<List<KeyboardButton>>();
            for (int i = 0; i < entity.Count();)
            {
                var row = new List<KeyboardButton>();
                for (int j = 0; j < 3; j++, i++)
                {
                    if (entity.Count() - i ==0)
                        break;
                    row.Add(new KeyboardButton(entity.Skip(i).FirstOrDefault().Name)); 
                }
                keys.Add(row);
            }
            return new ReplyKeyboardMarkup
            {
                Keyboard = keys,
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };
        }
        public async Task<ReplyKeyboardMarkup> GetSearchButtons(BaseModel model)
        {
            var parameters = await GetParameters(model);
            if (parameters == null)
                return new ReplyKeyboardMarkup();
            return await GetReplyMarkup(parameters);
        }
        
        public async Task<ReplyKeyboardMarkup> GetSearchButtons(int value)
        {
            var parameters = await GetModelAuto(value);
            if (parameters == null)
                return new ReplyKeyboardMarkup();
            return await GetReplyMarkup(parameters);
        }
        
        public async Task<IEnumerable<AdInfoResponse>> CreateRequest(PickedParameters parameters)
        {
            var adsIds = await HttpClient.GetFromJsonAsync<AdIdsResponse>(Constants.Urls.GetAdIds(parameters,Options));
            List<AdInfoResponse> adInfos = new List<AdInfoResponse>();
            foreach (var item in adsIds.GetIds())
            {
                adInfos.Add(await HttpClient.GetFromJsonAsync<AdInfoResponse>(Constants.Urls.GetAdInfo(item,Options)));
            }
            return adInfos;
        }
    }
}