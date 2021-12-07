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
        
        [HttpGet("countries")]
        public async Task<IActionResult> GetManufacture()
        {
            return Ok(await _riaService.GetManufacture());
        }
        
        [HttpGet("tests")]
        public async Task<IActionResult> GetTest()
        {
            var result = await _service.CreateRequest(new PickedParameters());
            return Ok(result);
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