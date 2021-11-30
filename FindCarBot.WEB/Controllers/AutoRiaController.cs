using System;
using System.Linq;
using System.Threading.Tasks;
using FindCarBot.Domain.Abstractions;
using FindCarBot.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace FindCarBot.WEB.Controllers
{
    [ApiController]
    [Route("api/autoria/auto")]
    public class AutoRiaController:ControllerBase
    {
        private readonly IAutoRiaService _riaService;
        private readonly ISearchService _service;

        public AutoRiaController(IAutoRiaService riaService, ISearchService service)
        {
            _riaService = riaService;
            _service = service;
        }

        [HttpGet("marks")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _riaService.GetMarks());
        }
        
        [HttpGet("tests")]
        public async Task<IActionResult> GetTest()
        {
            var result = await _riaService.GetParameters(Mark.CreateInstance());
            var str = result.FirstOrDefault(x => x.Name == "BMW");
            return Ok($"{str.Name} + || {str.Value}");
        }
        
        [HttpGet("bodystyles")]
        public async Task<IActionResult> GetBodyStyles()
        {
            var result = await _riaService.GetBodyStyles();
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