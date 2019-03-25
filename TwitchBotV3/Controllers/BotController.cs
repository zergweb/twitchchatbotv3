using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using TwitchBotV3.Model.ChatCommands;
using TwitchBotV3.Services.ChatComManager;
using TwitchBotV3.Services.CustomBot;
using TwitchBotV3.Services.CustomBot.InteractiveScopes;
using TwitchBotV3.Services.CustomBot.VotingEvent;

namespace TwitchBotV3.Controllers
{
    [Route("api/bot/[action]")]
    public class BotController : Controller
    {
        private Bot bot;
        private ScopesManager sManager;
        private readonly VotingManager vManager;
        private readonly ChatCommandManager ccm;
        public BotController(Bot _bot, ScopesManager _sManager, VotingManager _vManager, ChatCommandManager _ccm)
        {
            bot = _bot;
            sManager = _sManager;
            vManager = _vManager;
            //userManager = _userManager;
            ccm = _ccm;
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult SetCommand()
        {
            sManager.SetScopes();
            return Ok("set command");
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Connect()
        {
            bot.Connect();
            sManager.SetScopes();
            return Ok("connected is");
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult GetStatus()
        {          
            return Ok(JsonConvert.SerializeObject(bot.GetStatus(), Formatting.Indented));
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Disconnect()
        {
            bot.Disconnect();
            return Ok("disconnected");
        }
        [HttpPost]
        public IActionResult SendMessage()
        {
            var mess = " текст \n текст";
            bot.SendMessage(mess);
            return Ok(mess);
        }


        
        
    }
}
