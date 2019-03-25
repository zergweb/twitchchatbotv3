using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TwitchBotV3.Services.CustomBot.VotingEvent;
using TwitchBotV3.Services.CustomBot.VotingEvent.ResultEvents;

namespace TwitchBotV3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VotingController : Controller
    {

        private VotingManager vm;
        public VotingController(VotingManager _vm)
        {
            vm = _vm;
        }
        [HttpPost("/startvoting")]
        public IActionResult StartVoting([FromBody]VotingPattern vot)
        {
            var v = vot;
            vm.StartVoting(vot);
            return Ok();
        }
        

        [HttpPost]
        public IActionResult PauseVoting()
        {
            vm.PauseVoting();
            return Ok();
        }
        [HttpPost]
        public IActionResult ContinueVoting()
        {
            vm.ContinueVoting();
            return Ok();
        }
        [HttpPost]
        public IActionResult CancelVoting()
        {
            vm.CancelVoting();
            return Ok();
        }

        [HttpGet("/testvoting")]
        public IActionResult TestVoting()
        {           
            var voting = vm.GetVoting();
            return Ok(JsonConvert.SerializeObject(voting, Formatting.Indented));
        }

    }
}