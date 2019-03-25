using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchBotV3.Services.CustomBot.VotingEvent;
namespace TwitchBotV3.Services.CustomBot.InteractiveScopes.Scopes
{
    public class VotingScope : InteractiveScope
    {
        public Voting voting;
        public override void InvokeScope(OnMessageReceivedArgs e, TwitchClient client)
        {
            if (voting.Options.Any(x => x.Key==e.ChatMessage.Message.ToString().Trim()))
            {
                if (!voting.Votes.Any(x => x.Key.Contains(e.ChatMessage.DisplayName))) {
                    voting.Votes[e.ChatMessage.DisplayName] = e.ChatMessage.Message.ToString().Trim();
                }
            }          
        }
    }
}
