using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitchBotV3.Services.Auth;

namespace TwitchBotV3.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public class AuthDataBody
        {
            public string login { get; set; }
            public string pass { get; set; }
        }
        private IJwtAuthService authManager;
        public AuthController(IJwtAuthService auth)
        {
            authManager = auth;
        }
        [HttpPost("/login")]
        public IActionResult Login([FromBody] AuthDataBody data)
        {
            var r = authManager.GetToken(data.login, data.pass);
            if (r != null) { return Ok(r); } else { return NotFound(); }                        
        }
        [Authorize]
        [HttpGet("/isauth")]
        public IActionResult IsAuth()
        {
            return Ok();
        }
    }
}