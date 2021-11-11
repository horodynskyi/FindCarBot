using System.Collections.Generic;

namespace FindCarBot.Domain.Abstractions
{
    public interface ICommandService
    {
        List<TelegramCommand> Get();
    }
}