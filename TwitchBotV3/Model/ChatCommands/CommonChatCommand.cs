using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace TwitchBotV3.Model.ChatCommands
{
    public class CommonChatCommand : BaseChatCommand
    {
        public override void InvokeCommand(OnMessageReceivedArgs e, TwitchClient client)
        {
            base.InvokeCommand(e, client);
        }
    }
}
