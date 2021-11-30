using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using FindCarBot.Domain.Abstractions;
using FindCarBot.Domain.Models;
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
            : base(configuration,httpClient, options)
        {
            _cache = cache;
        }
        
        
        public async Task<ReplyKeyboardMarkup> GetSearchButtons<T>(T entity) where T : class
        {
            
            var parameters = await GetParameters(entity);
            var result = parameters.OrderBy(x => x.Value);
            List<List<KeyboardButton>> keys = new List<List<KeyboardButton>>();
            for (int i = 0; i < parameters.Count();)
            {
                var row = new List<KeyboardButton>();
                for (int j = 0; j < 3; j++, i++)
                {
                    if (parameters.Count() - i ==0)
                        break;
                    row.Add(new KeyboardButton(parameters.Skip(i).FirstOrDefault().Name)); 
                }
                keys.Add(row);
            }
            return new ReplyKeyboardMarkup
            {
              Keyboard = keys,
              ResizeKeyboard = true
            };
        }

        public async Task<JsonObject> CreateRequest(PickedParameters parameters)
        {
            var str = $"{Options.Url}/search/?api_key={Options.Token}";
            return await HttpClient.GetFromJsonAsync<JsonObject>(str);
        }
    }
}