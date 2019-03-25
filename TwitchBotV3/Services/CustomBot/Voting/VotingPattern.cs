using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitchBotV3.Services.CustomBot.VotingEvent
{
    public class VotingPattern
    {
        public Voting Voting { get; set; }
        public List<String> ResultEventsNames { get; set; }
    }
}
