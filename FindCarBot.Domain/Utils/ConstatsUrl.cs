using System.Dynamic;
using FindCarBot.Domain.Models;
using FindCarBot.IoC.Options;
using Microsoft.Extensions.Options;

namespace FindCarBot.Domain.Utils
{
    public static class Constants
    {
        public static class Urls
        {
            public static string GetAdIds(PickedParameters model, AutoRiaOptions options)
            {
                return $"{options.Url}search?api_key={options.Token}" +
                       $"&category_id=1" +
                       $"&brandOrigin[0]={model.Manufacture.Value}" +
                       $"&s_yers[1]={model.Dates.StartYears}" +
                       $"&po_yers[1]={model.Dates.EndYears}" +
                       $"&bodystyle[0]={model.BodyStyle.Value}" +
                       $"price_ot = {model.PriceRange.StartPrice}" +
                       $"price_do = {model.PriceRange.EndPrice}" +
                       $"&currency=1" +
                       $"&type[0]={model.Fuel.Value}" +
                       $"&gearbox[0]={model.GearBox.Value}" +
                       $"&power_name=1" +
                       $"&countpage=50" +
                       $"&with_photo=1";
            }

            public static string GetAdInfo(string Id,AutoRiaOptions options)
            {
                return $"{options.Url}/info?api_key={options.Token}&auto_id={Id}";
            }
        }
    }
}