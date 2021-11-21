using System;

namespace FindCarBot.IoC.Options
{
    public class AutoRiaOptions
    {
        public const string AutoRia = "AutoRia";

        public string Token { get; set; } = String.Empty;
        public string Url { get; set; } = String.Empty;
    }
}