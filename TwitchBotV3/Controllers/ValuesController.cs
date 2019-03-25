using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TwitchBotV3.Model;
using TwitchBotV3.Model.Repositories;
using TwitchBotV3.Services;
using TwitchBotV3.Services.CustomBot.VotingEvent.ResultEvents;

namespace TwitchBotV3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        ITestService t;
        
        IServiceProvider serviceProvider;
        public IConfiguration Configuration { get; }
        public ValuesController(ITestService test, IConfiguration conf, IServiceProvider _serviceProvider)
        {
            t = test;
            Configuration = conf;
            
            serviceProvider = _serviceProvider;
        }
        [Authorize]
        [HttpPost("/testdata")]
        public ActionResult<IEnumerable<string>> Get()
        {
            var str = Configuration.GetSection("SqlConnections:LocalMysql").Value;
            return new string[] {t.GetData(), str };
        }
        //[HttpGet("/type")]
        //public IActionResult TypeGet()
        //{
        //    Type t = Type.GetType("BaseResultEvent");
        //    if (t != null)
        //    {
        //        var g = (IVotingResultEvent)Activator.CreateInstance(t);
        //    }
        //    Activator.CreateInstance();
        //    return Ok();
        //}
        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
