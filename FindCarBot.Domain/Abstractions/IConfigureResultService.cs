using System.Collections.Generic;
using System.Threading.Tasks;
using FindCarBot.Domain.Utils;
using Microsoft.Extensions.Caching.Distributed;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FindCarBot.Domain.Abstractions
{
    public interface IConfigureResultService
    {
        Task Result(ITelegramBotClient client, IDistributedCache cache ,long chatId, IEnumerable<AdInfoResponse> adInfo);
        Task GetBag(ITelegramBotClient client,long chatId,IEnumerable<AdInfoResponse> adsInfo);
    }
}