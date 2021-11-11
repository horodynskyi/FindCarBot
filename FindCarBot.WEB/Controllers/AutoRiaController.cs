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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _riaService.GetTypesOfAuto());
        }
    }
}