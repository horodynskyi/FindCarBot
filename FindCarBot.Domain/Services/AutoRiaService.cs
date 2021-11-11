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

        public async Task GetAllAutoAttributes(string token)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Mark>> GetTypesOfAuto()
        {
             return  await  _httpClient.GetFromJsonAsync<List<Mark>>($"{_url}/categories/?api_key={_token}");
        }
    }
}