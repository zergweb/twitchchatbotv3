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
namespace TwitchBotV3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        ITestService t;
        IEntityRepository<Person> rep;
        public IConfiguration Configuration { get; }
        public ValuesController(ITestService test, IConfiguration conf, IEntityRepository<Person> _rep)
        {
            t = test;
            Configuration = conf;
            rep = _rep;
        }
        [Authorize]
        [HttpPost("/testdata")]
        public ActionResult<IEnumerable<string>> Get()
        {
            var str = Configuration.GetSection("SqlConnections:LocalMysql").Value;
            return new string[] {t.GetData(), str, rep.Get(1).Name };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

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
