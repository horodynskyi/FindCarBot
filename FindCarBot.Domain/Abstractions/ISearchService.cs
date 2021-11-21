﻿using System.Collections.Generic;
using System.Threading.Tasks;
using FindCarBot.Domain.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace FindCarBot.Domain.Abstractions
{
    public interface ISearchService
    {
        Task<ReplyKeyboardMarkup> GetSearchButtons(BaseModel model);
    }
}