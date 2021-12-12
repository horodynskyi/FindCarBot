using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindCarBot.Domain.Abstractions;
using FindCarBot.Domain.Models;
using FindCarBot.Domain.Utils;
using FindCarBot.IoC.Options;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FindCarBot.Domain.Services;

public class CallBackService:ConfigureResultService,ICallBackService
{
    private readonly IDistributedCache _cache;
    private readonly DistributedCacheEntryOptions _options;
    public CallBackService(IDistributedCache cache,IOptions<AutoRiaOptions> options) : base(options)
    {
        _options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMonths(1))
            .SetSlidingExpiration(TimeSpan.FromMinutes(10));
        _cache = cache;
    }
    private async Task Pagination(ITelegramBotClient client,long id)
    {
        var ser = await _cache.GetStringAsync($"{id.ToString()}/list");
        var adInfo = JsonConvert.DeserializeObject<IEnumerable<AdInfoResponse>>(ser);
        await Result(client, _cache, id, adInfo);
    }

    private bool IsCallBack(CallbackQuery callbackQuery)
    {
        for (int i = 1; i <= 3; i++)
        {
            if (callbackQuery.Data == $"message/result/{callbackQuery.From.Id}/{i}")
            {
                return true;
            }
        }
        return false;
    }
    private async Task AddToBag(CallbackQuery callbackQuery)
    {
        var str = callbackQuery.Data.Split("/");
        if (str != null)
        {
            var ind = str.LastOrDefault();
            var adInfo = JsonConvert.DeserializeObject<AdInfoResponse>(await _cache.GetStringAsync($"{callbackQuery.From.Id}/{ind}"));
            await _cache.RemoveAsync(callbackQuery.Data);
            List<AdInfoResponse> autoBag;
            var autoBagSer = await _cache.GetStringAsync($"bag/{callbackQuery.From.Id}");
            if (autoBagSer == null)
            {
                autoBag = new List<AdInfoResponse>();
            }
            else
            {
                autoBag = JsonConvert.DeserializeObject<List<AdInfoResponse>>(autoBagSer);
                await _cache.RemoveAsync($"bag/{callbackQuery.From.Id}");
            }
            autoBag.Add(adInfo);
            await _cache.SetStringAsync($"bag/{callbackQuery.From.Id}",JsonConvert.SerializeObject(autoBag));
        }
     
    }
    private async Task RemoveFromBag(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var str = callbackQuery.Data.Split("/");
        if (str != null)
        {
            var ind = str.LastOrDefault();
            var bagInfoSer = await _cache.GetStringAsync($"bag/{callbackQuery.From.Id}");
            if (bagInfoSer != null)
            {
                var bagInfo = JsonConvert.DeserializeObject<List<AdInfoResponse>>(bagInfoSer);
                bagInfo.Remove(bagInfo.ElementAt(Convert.ToInt32(ind)));
                await _cache.RemoveAsync($"bag/{callbackQuery.From.Id}");
                await _cache.SetStringAsync($"bag/{callbackQuery.From.Id}",JsonConvert.SerializeObject(bagInfo));
                await client.DeleteMessageAsync(callbackQuery.From.Id, callbackQuery.Message.MessageId);
            }
        }
    }
    public async Task ProccesCallback(ITelegramBotClient client, CallbackQuery callbackQuery)
    {
        if (callbackQuery.Data == "next")
        {
            await Pagination(client, callbackQuery.From.Id);
            await client.AnswerCallbackQueryAsync(callbackQuery.Id,cacheTime:1);
        }
        if (IsCallBack(callbackQuery))
        {
            await AddToBag(callbackQuery);
            await client.AnswerCallbackQueryAsync(callbackQuery.Id,"Added to bag",true,cacheTime:1);
        }

        if (callbackQuery.Data.Contains($"bag/{callbackQuery.From.Id}"))
        {
            await RemoveFromBag(callbackQuery, client);
        }
        else await client.AnswerCallbackQueryAsync(callbackQuery.Id);
    }
}