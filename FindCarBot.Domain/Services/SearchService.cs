using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindCarBot.Domain.Abstractions;
using FindCarBot.Domain.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace FindCarBot.Domain.Services
{
    public class SearchService : ISearchService 
    {
        private readonly IAutoRiaService _riaService;

        public SearchService(IAutoRiaService riaService)
        {
            _riaService = riaService;
        }

        public async Task<ReplyKeyboardMarkup> GetSearchButtons(BaseModel model)
        {
            var result = await _riaService.GetGearBoxes();
            
            
            return new ReplyKeyboardMarkup
            {
              
            };
        }
        
        
    }
}