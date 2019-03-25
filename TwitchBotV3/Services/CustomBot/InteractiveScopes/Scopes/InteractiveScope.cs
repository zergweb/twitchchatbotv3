using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace TwitchBotV3.Services.CustomBot.InteractiveScopes.Scopes
{
    public abstract class InteractiveScope
    {
        public String Name { get; set; }
        public abstract void InvokeScope(OnMessageReceivedArgs e, TwitchClient client);      
    }
}
