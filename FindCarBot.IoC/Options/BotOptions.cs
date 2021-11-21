using System;

namespace FindCarBot.IoC.Options
{
    public class BotOptions
    {
        public const string Bot = "TelegramBot";

        public string Token { get; set; } = String.Empty;
        public string Url { get; set; } = String.Empty;
    }
}