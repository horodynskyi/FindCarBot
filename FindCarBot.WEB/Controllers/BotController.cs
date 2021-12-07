using System.Threading.Tasks;
using FindCarBot.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FindCarBot.WEB.Controllers
{
    [ApiController]
    [Route("api/message/update")]
    public class BotController : Controller
    {
        private readonly ITelegramBotClient _client;
        private readonly ICommandService _commandService;
        private readonly IHandleService _handleService;
        
        public BotController(ICommandService commandService, ITelegramBotClient client, IHandleService handleService)
        {
            _commandService = commandService;
            _client = client;
            _handleService = handleService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Started");
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Update update)
        {
            if (update == null) return Ok();

            var message = update.Message;
          // await _handleService.Execute(message);
            if (_commandService.Contains(message,_client))
            {
                await _commandService.Execute(message);
                return Ok();
            }
            else if (await _handleService.Contains(message))
            {
                await _handleService.Execute(message);
                return Ok();
            }
            return Ok();
                
        }
    }
}