using System.Collections.Generic;

namespace FindCarBot.Domain.Abstractions
{
    public interface ISearchService
    {
        List<TelegramCommand> GetAutoAtributes();
    }
}