using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Client;

namespace TwitchBotV3.Services.CustomBot.VotingEvent.ResultEvents
{
    public interface IVotingResultEvent
    {    
        void InvokeEvent(Bot bot, Voting voting);
    }
}
