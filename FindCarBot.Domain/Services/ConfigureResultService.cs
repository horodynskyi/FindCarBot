using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindCarBot.Domain.Abstractions;
using FindCarBot.Domain.Models;
using FindCarBot.Domain.Utils;
using FindCarBot.IoC.Options;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FindCarBot.Domain.Services
{
    public class ConfigureResultService:IConfigureResultService
    {
        private readonly AutoRiaOptions _options;
        public ConfigureResultService(IOptions<AutoRiaOptions> options)
        {
            _options = options.Value;
        }
        private InlineKeyboardMarkup CreateReplyMarkup(string linkToView,string mark,string model)
        {
            return  new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithUrl($"{mark}/{model}", linkToView)
                } 
            });
        }
        public async Task Result(ITelegramBotClient client, long chatId,IEnumerable<AdInfoResponse> adInfo)
        {
            if (adInfo.Count() == 0)
            {
                await client.SendTextMessageAsync(chatId, "Sorry, I can't find cars by your parameters");
            }
            else 
                foreach (var item in adInfo)
                {
                    await client.SendPhotoAsync(chatId, item.GetPhoto() ,item.ToString(),replyMarkup:CreateReplyMarkup(item.GetLink(_options),item.MarkName,item.ModelName));
                }
            
        }
    }
}