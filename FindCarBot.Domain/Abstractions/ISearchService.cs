using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using FindCarBot.Domain.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace FindCarBot.Domain.Abstractions
{
    public interface ISearchService
    {
        Task<ReplyKeyboardMarkup> GetSearchButtons<T>(T model) where T : class;
        Task<JsonObject> CreateRequest(PickedParameters parameters);
    }
}