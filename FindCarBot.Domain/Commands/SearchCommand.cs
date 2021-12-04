using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FindCarBot.Domain.Abstractions;
using FindCarBot.Domain.Enums;
using FindCarBot.Domain.Models;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FindCarBot.Domain.Commands
{
    public class SearchCommand:TelegramCommand
    {
        public override string Name => @"Search";
        private readonly ISearchService _searchService;
        private ITelegramBotClient _client;
        private long _chatId;
       
        public SearchCommand(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            _client = client;
            _chatId = message.Chat.Id; 
            var keyBoard = await _searchService.GetSearchButtons(new Manufacture());
            await client.SendTextMessageAsync(_chatId, "Select country of manufacture: ", 
                ParseMode.Html, replyMarkup: keyBoard);
            
        }
        
        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;
            return message.Text.Contains(Name);
        }
    }
}