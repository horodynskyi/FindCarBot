using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using FindCarBot.Domain.Models;
using FindCarBot.Domain.Services;
using FindCarBot.Domain.Utils;
using FindCarBot.IoC.Options;
using Telegram.Bot.Types.ReplyMarkups;

namespace FindCarBot.Domain.Abstractions
{
    public interface ISearchService
    {
        Task<ReplyKeyboardMarkup> GetSearchButtons(BaseModel model);
        Task<ReplyKeyboardMarkup> GetSearchButtons(int value);
        Task<IEnumerable<AdInfoResponse>> CreateRequest(PickedParameters parameters);
    }
}