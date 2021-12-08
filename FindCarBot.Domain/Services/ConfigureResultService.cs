using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindCarBot.Domain.Abstractions;
using FindCarBot.Domain.Utils;
using FindCarBot.IoC.Options;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace FindCarBot.Domain.Services
{
    public class ConfigureResultService:IConfigureResultService
    {
        protected readonly AutoRiaOptions Options;
        public ConfigureResultService(IOptions<AutoRiaOptions> options)
        {
            Options = options.Value;
        }
        protected InlineKeyboardMarkup CreateReplyMarkup(string linkToView,string text,string callBackData,string smile)
        {
            
            return  new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithUrl(text, linkToView),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(smile,callBackData),
                },
               
            });
        }
        public async Task Result(ITelegramBotClient client, IDistributedCache cache, long chatId, IEnumerable<AdInfoResponse> adInfo)
        {
            //client.EditMessageReplyMarkupAsync();
            if (adInfo.Count() == 0)
            {
                await client.SendTextMessageAsync(chatId, "Sorry, I can't find cars by your parameters");
            }
            else
            {
                int msgId = Convert.ToInt32(await cache.GetStringAsync($"message/result/{chatId}/1")) ;
                if (msgId == 0)
                {
                    int autoId = 1;
                    foreach (var item in adInfo.Take(3))
                    {
                        var callBackData = $"message/result/{chatId}/{autoId}";
                        var text = $"{item.MarkName}/{item.ModelName}";
                        await cache.SetStringAsync($"{chatId}/{autoId}", JsonConvert.SerializeObject(item));
                        var message_obj = await client.SendPhotoAsync(chatId, item.GetPhoto() ,item.ToString(),
                            replyMarkup:CreateReplyMarkup(item.GetLink(Options), text,callBackData,"\U0001F49C"));
                        await cache.SetStringAsync($"message/result/{chatId}/{autoId++}",message_obj.MessageId.ToString());

                    }
                    await cache.SetStringAsync($"{chatId.ToString()}/list",JsonConvert.SerializeObject(adInfo.Skip(3)));
                }
                else
                {
                    int autoId = 1;
                    foreach (var item in adInfo.Take(3))
                    {
                        var photo = new InputMediaPhoto()
                        {
                            Media = item.GetPhoto(),
                            Caption = item.ToString()
                        };
                        await cache.SetStringAsync($"{chatId}/{autoId}", JsonConvert.SerializeObject(item));
                        var callBackData = $"{chatId}/{autoId}";
                        var text = $"{item.MarkName}/{item.ModelName}";
                        await client.EditMessageMediaAsync(chatId,msgId,media:photo,
                            CreateReplyMarkup(item.GetLink(Options),text,callBackData,"\U0001F49C"));
                        await cache.RemoveAsync($"message/result/{chatId}/{autoId}");
                        await cache.SetStringAsync($"message/result/{chatId}/{autoId++}",msgId.ToString());

                    }
                    await cache.SetStringAsync($"{chatId.ToString()}/list",JsonConvert.SerializeObject(adInfo.Skip(3)));
                }
                
            }
            var next = new InlineKeyboardMarkup(new[]
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData("\U000023E9","next"), 
                }
            });
            await client.SendTextMessageAsync(chatId,"Next 3 cars", replyMarkup: next);
        }

        public async Task GetBag(ITelegramBotClient client, long chatId,IEnumerable<AdInfoResponse> adsInfo)
        {
            var bagId = 0;
            
            foreach (var item in adsInfo.Take(3))
            {
                var callBackData = $"bag/remove/{bagId++}";
                var text = $"{item.MarkName}/{item.ModelName}";
                await client.SendPhotoAsync(chatId, item.GetPhoto() ,item.ToString(),
                    replyMarkup:CreateReplyMarkup(item.GetLink(Options), text,callBackData,"\U0000274C"));
            }
        }
    }
}