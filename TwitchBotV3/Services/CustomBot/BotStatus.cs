using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitchBotV3.Services.CustomBot
{
    public class BotStatus
    {
        public String Name { get; set; }
        public bool IsConnected { get; set; }
        public IEnumerable<String> Channels { get; set; }
    }
}
