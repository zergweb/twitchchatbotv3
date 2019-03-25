using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace TwitchBotV3.Model.ChatCommands
{
    public abstract class BaseChatCommand
    {
        public int Id { get; set; }
        public String CommandName { get; set; }
        public String Responce { get; set; }
        public String Type { get; set; }
        public virtual void InvokeCommand(OnMessageReceivedArgs e, TwitchClient client)
        {
            client.SendMessage(e.ChatMessage.Channel, Responce);
        }
    }
}
