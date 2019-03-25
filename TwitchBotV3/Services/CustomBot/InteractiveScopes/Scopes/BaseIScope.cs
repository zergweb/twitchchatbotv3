using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchBotV3.Model.ChatCommands;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace TwitchBotV3.Services.CustomBot.InteractiveScopes.Scopes
{
    public class BaseIScope : InteractiveScope
    {
        public Dictionary<string, BaseChatCommand> Commands { get; set; }

        public override void InvokeScope(OnMessageReceivedArgs e, TwitchClient client)
        {
            if (e.ChatMessage.Message.StartsWith("!", StringComparison.InvariantCultureIgnoreCase))
            {
                BaseChatCommand comm = Commands[e.ChatMessage.Message.ToString().Trim().Substring(1)];
                if (comm != null)
                {
                    comm.InvokeCommand(e, client);
                }
            }
        }
    }
}
