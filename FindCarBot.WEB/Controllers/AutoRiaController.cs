using System;
using System.Threading.Tasks;
using FindCarBot.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace FindCarBot.WEB.Controllers
{
    [ApiController]
    [Route("api/autoria/auto")]
    public class AutoRiaController:ControllerBase
    {
        private readonly IAutoRiaService _riaService;

        public AutoRiaController(IAutoRiaService riaService)
        {
            _riaService = riaService;
        }

        [HttpGet("marks")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _riaService.GetMarks());
        }
        
        [HttpGet("bodystyles")]
        public async Task<IActionResult> GetBodyStyles()
        {
            
            return Ok(await _riaService.GetBodyStyles());
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