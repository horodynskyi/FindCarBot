using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FindCarBot.Domain.Abstractions;
using FindCarBot.Domain.Models;
using FindCarBot.IoC.Options;
using Microsoft.Extensions.Options;


namespace FindCarBot.Domain.Services
{
    public class AutoRiaService : IAutoRiaService
    {
        protected readonly AutoRiaOptions Options;
        protected readonly HttpClient HttpClient;

        public AutoRiaService(HttpClient httpClient, IOptions<AutoRiaOptions> options)
        {
            HttpClient = httpClient;
            Options = options.Value;
        }

        public async Task<IEnumerable<Mark>> GetMarks()
        {
            return await HttpClient.GetFromJsonAsync<IEnumerable<Mark>>(
                $"{Options.Url}/categories/1/marks?api_key={Options.Token}");
        }

        public async Task<IEnumerable<BodyStyle>> GetBodyStyles()
        {
            return await HttpClient.GetFromJsonAsync<IEnumerable<BodyStyle>>(
                $"{Options.Url}/categories/1/bodystyles?api_key={Options.Token}");
        }

        public async Task<IEnumerable<Fuel>> GetFuelTypes()
        {
            return await HttpClient.GetFromJsonAsync<IEnumerable<Fuel>>(
                $"{Options.Url}/type?api_key={Options.Token}");
        }

        public async Task<IEnumerable<GearBox>> GetGearBoxes()
        {
            return await HttpClient.GetFromJsonAsync<IEnumerable<GearBox>>(
                $"{Options.Url}/categories/1/gearboxes?api_key={Options.Token}");
        }

        public async Task<IEnumerable<DriverType>> GetDriverTypes()
        {
            return await HttpClient.GetFromJsonAsync<IEnumerable<DriverType>>(
                $"{Options.Url}/categories/1/driverTypes?api_key={Options.Token}");
        }

        public async Task<IEnumerable<Manufacture>> GetManufacture()
        {
            
            return await HttpClient.GetFromJsonAsync<IEnumerable<Manufacture>>(
                $"{Options.Url}/countries?api_key={Options.Token}");
        }

        public async Task<IEnumerable<ModelAuto>> GetModelAuto(int value)
        {
            return await HttpClient.GetFromJsonAsync<IEnumerable<ModelAuto>>(
                $"{Options.Url}/categories/1/marks/{value}/models?api_key={Options.Token}");
        }

        public async Task<IEnumerable<BaseModel>> GetParameters<T>(T entity)
        {
            
            switch (entity)
            {
                case GearBox:
                    return await GetGearBoxes();
                case DriverType:
                    return await GetDriverTypes();
                case Fuel:
                    return await GetFuelTypes();
                case BodyStyle:
                    return await GetBodyStyles();
                case Mark:
                    return await GetMarks();
                case Manufacture:
                    return await GetManufacture();
                default: return Array.Empty<BaseModel>();
            }
        }
    }
}