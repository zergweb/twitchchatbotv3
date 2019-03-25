
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchBotV3.Model;
using TwitchBotV3.Model.ChatCommands;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;

namespace TwitchBotV3.Services.CustomBot
{
    public partial class Bot
    {
        private BaseChatCommand GetCommand(String message)
        {
            return commands[message.Trim().Substring(1)];
        }
        public  void onMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            //if (e.ChatMessage.Message.StartsWith("!", StringComparison.InvariantCultureIgnoreCase))
            //{
            //    BaseChatCommand comm = GetCommand(e.ChatMessage.Message.ToString());
            //    if (comm != null)
            //    {
            //        comm.InvokeCommand(sender, e, client);
            //    }
            //}
            foreach(var scope in InteractiveScopes)
            {
                scope.Value.InvokeScope(e, client);
            }

            //if (e.ChatMessage.Message.Contains("!main"))
            //{
             //   Person p = userManager.RegisterNewPerson(e.ChatMessage.Username);
             //   if (p != null)
              //  {
                   // client.SendWhisper(e.ChatMessage.Username, "name:" + p.Name + " pass:" + p.Password);
              //  }
             //   else
              //  {
             //       client.SendWhisper(e.ChatMessage.Username, "что то пошло не так");
              //  }

            //}
            
        }
    }
}
