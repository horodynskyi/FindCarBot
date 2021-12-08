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
        private readonly ICallBackService _callBackService;
        
        public BotController(ICommandService commandService, ITelegramBotClient client, IHandleService handleService, ICallBackService callBackService)
        {
            _commandService = commandService;
            _client = client;
            _handleService = handleService;
            _callBackService = callBackService;
        }
        [HttpGet ("/carinfo")]
        public async Task<IActionResult> GetCarInfo()
        {
            return Ok("Got it");
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
            var callback = update.CallbackQuery;
            
            if (message != null)
            {
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
            }
            else if (callback != null)
            {
                await _callBackService.ProccesCallback(_client,callback);
                return Ok();
            }
          
            return Ok();
        }
    }
}