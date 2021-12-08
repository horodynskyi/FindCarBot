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
            if (callbackQuery.Data ==$"message/result/{callbackQuery.From.Id}/{i}")
            {
                return true;
            }
        }
        return false;
    }
    private async Task AddToBag(CallbackQuery callbackQuery)
    {
        var ser = await _cache.GetStringAsync(callbackQuery.Data);
        var adInfo = JsonConvert.DeserializeObject<AdInfoResponse>(ser);
        await _cache.RemoveAsync(callbackQuery.Data);
        var autoBag = JsonConvert.DeserializeObject<List<AdInfoResponse>>(
                await _cache.GetStringAsync($"bag/{callbackQuery.From.Id}")) ?? new List<AdInfoResponse>();
        await _cache.RemoveAsync($"bag/{callbackQuery.From.Id}");
        autoBag.Add(adInfo);
        await _cache.SetStringAsync($"bag/{callbackQuery.From.Id}",JsonConvert.SerializeObject(autoBag));
    }
    public async Task ProccesCallback(ITelegramBotClient client, CallbackQuery callbackQuery)
    {
        if (callbackQuery.Data == "next")
        {
            await Pagination(client, callbackQuery.From.Id);
            await client.AnswerCallbackQueryAsync(callbackQuery.Id);
        }
        if (IsCallBack(callbackQuery))
        {
            await AddToBag(callbackQuery);
            await client.AnswerCallbackQueryAsync(callbackQuery.Id,"Added to bag");
        }
        else await client.AnswerCallbackQueryAsync(callbackQuery.Id);
    }
}