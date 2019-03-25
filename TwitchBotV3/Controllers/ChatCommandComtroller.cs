using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchBotV3.Model.ChatCommands;
using TwitchBotV3.Services.ChatComManager;

namespace TwitchBotV3.Controllers
{
    public class ChatCommandComtroller : Controller
    {
        private readonly ChatCommandManager ccm;

        public ChatCommandComtroller(ChatCommandManager _ccm)
        {
            ccm = _ccm;
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult AddCommonCommand([FromBody]CommonChatCommand comm)
        {
            ccm.AddCommonChatCommand(comm);
            return Ok("add command " + comm.CommandName);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteCommonCommand(string comm)
        {
            ccm.DeleteCommonChatCommand(comm);
            return Ok("command " + comm + "deleted");
        }
        [HttpPut]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateCommonCommand(CommonChatCommand comm)
        {
            ccm.UpdateCommonCommand(comm);
            return Ok("command "+comm+"is updated");
        }
    }
}
