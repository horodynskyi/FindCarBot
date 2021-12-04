using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using FindCarBot.Domain.Models;
using FindCarBot.Domain.Services;
using Telegram.Bot.Types.ReplyMarkups;

namespace FindCarBot.Domain.Abstractions
{
    public interface ISearchService
    {
        Task<ReplyKeyboardMarkup> GetSearchButtons(BaseModel model);
        Task<ReplyKeyboardMarkup> GetSearchButtons(int value);
        Task<Result> CreateRequest();
    }
}