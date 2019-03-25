using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client;

namespace TwitchBotV3.Services.CustomBot.VotingEvent.ResultEvents
{
    public class BaseResultEvent : IVotingResultEvent
    {
        private String Drawer = "==========================================";
        private Voting voting;
        private Bot bot;
        public void InvokeEvent(Bot _bot, Voting _voting)
        {
            bot = _bot;
            voting = _voting;
            PrintVotingResults();
        }      
        private void PrintVotingResults()
        {
            var sBuilder = new StringBuilder(Drawer).Append(" ");
            sBuilder.Append(voting.ObjectOfVoting).Append(" Результаты голосования: ").Append(Drawer);
            foreach (var opt in voting.Options)
            {
                sBuilder.Append(opt.Value)
                    .Append(" - ")
                    .Append(voting.Votes.Where(x => x.Value == opt.Key).Count().ToString())
                    .Append(" голосов ")
                    .Append(Drawer);
            }
            bot.SendMessage(sBuilder.ToString());
        }
    }
}
