using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FindCarBot.Domain.Abstractions;
using FindCarBot.Domain.Models;
using Microsoft.Extensions.Configuration;
using FindCarBot.IoC.Options;
using Microsoft.Extensions.Options;


namespace FindCarBot.Domain.Services
{
    public class AutoRiaService:IAutoRiaService
    {
        private readonly AutoRiaOptions _options; 
        private readonly HttpClient _httpClient;

        public AutoRiaService(IConfiguration configuration, HttpClient httpClient, IOptions<AutoRiaOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }
        
        public async Task<List<Mark>> GetMarks()
        {
             return  await  _httpClient.GetFromJsonAsync<List<Mark>>($"{_options.Url}/categories/1/marks?api_key={_options.Token}");
        }

        public async Task<List<BodyStyle>> GetBodyStyles()
        {
            return  await  _httpClient.GetFromJsonAsync<List<BodyStyle>>($"{_options.Url}/categories/1/bodystyles?api_key={_options.Token}");
        }

        public async Task<List<Fuel>> GetFuelTypes()
        {
            return  await  _httpClient.GetFromJsonAsync<List<Fuel>>($"{_options.Url}/type?api_key={_options.Token}");
        }

        public async Task<IEnumerable<GearBox>> GetGearBoxes()
        {
            return  await  _httpClient.GetFromJsonAsync<List<GearBox>>($"{_options.Url}/categories/1/gearboxes?api_key={_options.Token}");
        }
        
        public async Task<List<DriverType>> GetDriverTypes()
        {
            return  await  _httpClient.GetFromJsonAsync<List<DriverType>>($"{_options.Url}/categories/1/driverTypes?api_key={_options.Token}");
        }
        
    }
}