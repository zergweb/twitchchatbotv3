using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Client.Events;

namespace TwitchBotV3.Services.CustomBot
{
    public partial class Bot
    {
        public void onJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            client.SendMessage(e.Channel, "Legendary bot is connected on Legendary chat");
        }
    }

}
