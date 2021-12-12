using System;
using System.Linq;
using System.Threading.Tasks;
using FindCarBot.Domain.Abstractions;
using FindCarBot.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace FindCarBot.WEB.Controllers
{
    [ApiController]
    [Route("api/autoria/auto")]
    public class AutoRiaController:ControllerBase
    {
        private readonly IAutoRiaService _riaService;
        private readonly ISearchService _service;
        private readonly IConnectionMultiplexer _server;

        public AutoRiaController(IAutoRiaService riaService, ISearchService service,IConnectionMultiplexer server)
        {
            _riaService = riaService;
            _service = service;
            _server = server;
        }

        [HttpGet("marks")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _riaService.GetMarks());
        }
        
        [HttpGet("countries")]
        public async Task<IActionResult> GetManufacture()
        {
            return Ok(await _riaService.GetManufacture());
        }
        
        [HttpGet("tests")]
        public async Task<IActionResult> GetTest()
        {
            var keys = _server.GetServer("localhost", 6379).Keys();
            var db = _server.GetDatabase();
            var keysList = keys.Select(key => (string) key).ToList();
            var result = _server.IsConnected;
            return Ok(keysList);
        }
        
        [HttpGet("bodystyles")]
        public async Task<IActionResult> GetBodyStyles()
        {
            var result = await _riaService.GetBodyStyles();
            return Ok(result.OrderBy(x=>x.Value));
        }
        [HttpGet("modelAuto")]
        public async Task<IActionResult> GetAutoModel()
        {
            var result = await _riaService.GetModelAuto(79);
            return Ok(result.OrderBy(x=>x.Value));
        }
        
        [HttpGet("fuel")]
        public async Task<IActionResult> GetTypeOfFuel()
        {
            return Ok(await _riaService.GetFuelTypes());
        }
        
        [HttpGet("gearboxes")]
        public async Task<IActionResult> GetTypeOfGearBox()
        {
            return Ok(await _riaService.GetGearBoxes());
        }
        
        [HttpGet("driverTypes")]
        public async Task<IActionResult> GetDriverTypes()
        {
            return Ok(await _riaService.GetDriverTypes());
        }
        
    }
}