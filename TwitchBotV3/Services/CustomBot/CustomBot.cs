using System.Collections.Generic;
using TwitchLib.Client;
using TwitchLib.Client.Models;

using TwitchBotV3.Model;
using TwitchBotV3.Model.ChatCommands;
using Microsoft.Extensions.Configuration;
using TwitchBotV3.Services.CustomBot.InteractiveScopes.Scopes;

namespace TwitchBotV3.Services.CustomBot
{
    public partial class Bot
    {
        public  TwitchClient client;
        private IConfiguration config;
        private Dictionary<string, BaseChatCommand> commands;
        private Dictionary<string, InteractiveScope> InteractiveScopes;
        public Bot(IConfiguration _config)
        {
            config = _config;
            ConnectionCredentials credentials = new ConnectionCredentials(config["TwitchBotOpt:BotName"], config["TwitchBotOpt:AccessToken"]);
            client = new TwitchClient();
            client.Initialize(credentials, config["TwitchBotOpt:ChannelName"]);
           // client.OnConnected += OnConnected;
            client.OnJoinedChannel +=onJoinedChannel;
            client.OnMessageReceived += onMessageReceived;
          //client.OnWhisperReceived += OnWhisperReceived;
          //client.OnNewSubscriber += OnNewSubscriber;
        }
        public void SendMessage(string message)
        {
            client.SendMessage(config["TwitchBotOpt:ChannelName"], message);
          
        }
        public void Connect()
        {
            if (!client.IsConnected)
            {
                client.Connect();
            }        
        }
        public void Disconnect()
        {
            if (client.IsConnected)
            {
                client.Disconnect();
            }         
        }
        public BotStatus GetStatus()
        {           
            var v = client.JoinedChannels;
            var listChannels = new List<string>();
            foreach(var i in v)
            {
                listChannels.Add(i.Channel);
            }
            return new BotStatus
            {
                IsConnected = client.IsConnected,
                Name = config["TwitchBotOpt:BotName"],
                Channels=listChannels
            };
        }
        public void SetChatCommand(Dictionary<string, BaseChatCommand> comm)
        {
            commands = comm;
        }
        public void SetInteractiveScopes(Dictionary<string, InteractiveScope> Scopes)
        {
            this.InteractiveScopes = Scopes;
        }
        public void AddInteractiveScope(string key , InteractiveScope scope)
        {
            this.InteractiveScopes.Add(key, scope);
        }
        public bool RemoveInteractiveScope(string key)
        {
            return this.InteractiveScopes.Remove(key);
        }
    }
  

}
