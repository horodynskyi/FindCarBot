using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FindCarBot.Domain.Abstractions;
using FindCarBot.Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace FindCarBot.Domain.Services
{
    public class AutoRiaService:IAutoRiaService
    {
        private readonly string _token;
        private readonly string _url;
        private readonly HttpClient _httpClient;

        public AutoRiaService(IConfiguration configuration, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _token = configuration["AutoRiaToken"];
            _url = configuration["AutoRiaUrl"];
        }
        
        public async Task<List<Mark>> GetMarks()
        {
             return  await  _httpClient.GetFromJsonAsync<List<Mark>>($"{_url}/categories/?api_key={_token}");
        }

        public async Task<List<BodyStyle>> GetBodyStyles()
        {
            return  await  _httpClient.GetFromJsonAsync<List<BodyStyle>>($"{_url}/categories/1/bodystyles?api_key={_token}");
        }

        public async Task<List<Fuel>> GetFuelTypes()
        {
            return  await  _httpClient.GetFromJsonAsync<List<Fuel>>($"{_url}/type?api_key={_token}");
        }

        public async Task<List<GearBox>> GetGearBoxes()
        {
            return  await  _httpClient.GetFromJsonAsync<List<GearBox>>($"{_url}/categories/1/gearboxes?api_key={_token}");
        }
        
        public async Task<List<DriverType>> GetDriverTypes()
        {
            return  await  _httpClient.GetFromJsonAsync<List<DriverType>>($"{_url}/categories/1/driverTypes?api_key={_token}");
        }
        
    }
}