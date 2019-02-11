using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Login([FromBody] AuthDataBody data)
        {
            var r = await authManager.GetToken(data.login, data.pass);
            if (r != null) { return Ok(r); } else { return NotFound(); }                        
        }
    }
}