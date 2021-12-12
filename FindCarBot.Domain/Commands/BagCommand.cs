using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindCarBot.Domain.Abstractions;
using FindCarBot.Domain.Utils;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FindCarBot.Domain.Commands;

public class BagCommand:TelegramCommand
{
    public override string Name => @"/bag";
    private readonly IDistributedCache _cache;
    private readonly IConfigureResultService _resultService;

    public BagCommand(IDistributedCache cache, IConfigureResultService resultService)
    {
        _cache = cache;
        _resultService = resultService;
    }

    public override async Task Execute(Message message, ITelegramBotClient client)
    {
        var ser = await _cache.GetStringAsync($"bag/{message.From.Id}");
        if (!JsonConvert.DeserializeObject<IEnumerable<AdInfoResponse>>(ser).Any())
        {
            await client.SendTextMessageAsync(message.Chat.Id,"I can't find your bag.\nSomething wrong or bag is empty");
        }
        else
        {
            var adsInfo = JsonConvert.DeserializeObject<IEnumerable<AdInfoResponse>>(ser); 
            await _resultService.GetBag(client, message.Chat.Id, adsInfo);
        }
        
    }

    public override bool Contains(Message message)
    {
        if (message.Type != MessageType.Text)
            return false;
        return message.Text.Contains(Name);
    }
}