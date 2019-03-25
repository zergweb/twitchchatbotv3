using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchBotV3.Services.CustomBot.VotingEvent.ResultEvents;

namespace TwitchBotV3.Services.CustomBot.VotingEvent
{
    public class Voting
    {
       // public String ChannelName { get; set; }
        //public String Key { get; set; }
        public String ObjectOfVoting { get; set; }     
        public Dictionary<string,string> Options { get; set; }
        public Dictionary<string,string> Votes { get; set; }
        public List<IVotingResultEvent> ResultEvents { get; set; }
    }
}
