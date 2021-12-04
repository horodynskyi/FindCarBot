using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using FindCarBot.Domain.Abstractions;
using FindCarBot.Domain.Models;
using FindCarBot.IoC.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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
        
        public async Task<ReplyKeyboardMarkup> GetSearchButtons(BaseModel model)
        {
            var parameters = await GetParameters(model);
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

        public async Task<ReplyKeyboardMarkup> GetSearchButtons(int value)
        {
            var parameters = await GetModelAuto(value);
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
        
        public async Task<Response> CreateRequest()
        {
            var url =
                $"{Options.Url}/search?api_key={Options.Token}&category_id=1&brandOrigin[1]=392&s_yers[1]=2012&po_yers[1]=2016&bodystyle%5B4%5D=2&marka_id%5B0%5D=79&model_id%5B0%5D=0&currency=1&type%5B0%5D=1&gearbox%5B0%5D=1&power_name=1&countpage=50&with_photo=1";
            var str = $"{Options.Url}/search/?api_key={Options.Token}";
            var res = await HttpClient.GetFromJsonAsync<Response>(url);

            return res;
        }
    }
}