using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitchBotV3.Services
{
    public class TestService : ITestService
    {
        public String GetData()
        {
            return "Data from service";
        }
    }
}
