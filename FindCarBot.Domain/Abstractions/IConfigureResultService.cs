using System.Collections.Generic;
using System.Threading.Tasks;
using FindCarBot.Domain.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FindCarBot.Domain.Abstractions
{
    public interface IConfigureResultService
    {
        Task Result(ITelegramBotClient client, long chatId, IEnumerable<AdInfoResponse> adInfo);
    }
}