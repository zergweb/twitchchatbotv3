using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchBotV3.Services.CustomBot.InteractiveScopes.Scopes;
using TwitchBotV3.Services.ChatComManager;

namespace TwitchBotV3.Services.CustomBot.InteractiveScopes
{
    public class ScopesManager
    {
        private Bot bot;
        private Dictionary<string, InteractiveScope> InteractiveScopes;
        private ChatCommandManager chatCommandManager;
        public ScopesManager(Bot _bot, ChatCommandManager _ccm)
        {
            this.bot = _bot;
            chatCommandManager = _ccm;
            CreateScopes();
        }
        public void SetScopes()
        {
            bot.SetInteractiveScopes(InteractiveScopes);
        }
        private void CreateScopes()
        {
            InteractiveScopes = new Dictionary<string, InteractiveScope>();
            var scope1 = new BaseIScope { Name = "basescope", Commands = chatCommandManager.GetChatCommands() };
            InteractiveScopes.Add(scope1.Name, scope1);
        }
    }
}
