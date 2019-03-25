using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchBotV3.Services.CustomBot.InteractiveScopes;
using TwitchBotV3.Services.CustomBot.InteractiveScopes.Scopes;
using TwitchBotV3.Services.CustomBot.VotingEvent.ResultEvents;

namespace TwitchBotV3.Services.CustomBot.VotingEvent
{
    public class VotingManager
    {
        public String VotingScopeName="voting";
        private ScopesManager sm;
        private Bot bot;
        private Voting voting;
        private VotingScope votingScope;
        private String Drawer = "==========================================";
        private String sDrawer = "------------------------------------------";
        public VotingManager(ScopesManager _sm, Bot _bot)
        {
            sm = _sm;
            bot = _bot;
        }
        public void StartVoting(VotingPattern pattern)
        {
            //CreateFakeVoting();
            CreateVoting(pattern);
            votingScope = new VotingScope { voting=this.voting, Name=VotingScopeName };
            var strBuilder = new StringBuilder(Drawer).Append(voting.ObjectOfVoting)
                .Append(sDrawer).Append("Варианты:").Append(sDrawer);
            for(int i=1;i<voting.Options.Count;i++)
            {
                var opt=voting.Options.ElementAt(i);
                strBuilder.Append(i+")").Append(opt.Value).Append(" - ").Append(opt.Key).Append(sDrawer);
            }
            bot.AddInteractiveScope(votingScope.Name, votingScope);
            bot.SendMessage(Drawer+" "+strBuilder);
            
        }
        public void PauseVoting()
        {
            if (votingScope != null) bot.RemoveInteractiveScope(votingScope.Name);         
            bot.SendMessage(Drawer+" Голосование приостановленно "+Drawer);
            new BaseResultEvent().InvokeEvent(bot, voting);
        }
        public void ContinueVoting()
        {
            bot.AddInteractiveScope(votingScope.Name, votingScope);
            bot.SendMessage(Drawer + " Голосование продолженно " + Drawer);
        }
        public void CancelVoting()
        {
           if (votingScope != null) bot.RemoveInteractiveScope(votingScope.Name);
           foreach(var resultEvent in voting.ResultEvents){
                resultEvent.InvokeEvent(bot, voting);
            }
            votingScope = null;
            voting = null;
        }
        //public void PrintVotingResults()
        //{
        //    var sBuilder = new StringBuilder(Drawer).Append(" ");
        //    sBuilder.Append(voting.ObjectOfVoting).Append(" Результаты голосования: ").Append(Drawer);
        //    foreach(var opt in voting.Options)
        //    {
        //        sBuilder.Append(opt.Value)
        //            .Append(" - ")
        //            .Append(voting.Votes.Where(x => x.Value == opt.Key).Count().ToString())
        //            .Append(" голосов ")
        //            .Append(Drawer);
        //    }
        //    bot.SendMessage(sBuilder.ToString());
        //}
        private void CreateVoting(VotingPattern pattern)
        {
            voting = pattern.Voting;
            foreach(var typeName in pattern.ResultEventsNames)
            {
                Type t = Type.GetType(typeName);
                if (t != null)
                {                    
                    voting.ResultEvents.Add((IVotingResultEvent)Activator.CreateInstance(t));
                }
            }
        }

        private void CreateFakeVoting()
        {
            var options = new Dictionary<string, string>
            {
                ["opt1"] = "options1 value",
                ["opt2"] = "options2 value"
            };
            voting = new Voting
            {
                
                Options = options,
                ObjectOfVoting = "тестовое голосование",
                ResultEvents= new List<IVotingResultEvent> { new BaseResultEvent() },
                Votes= new Dictionary<string, string>()
            };
        }
        public Voting GetVoting()
        {
            return this.voting;
        }
    }  
}
