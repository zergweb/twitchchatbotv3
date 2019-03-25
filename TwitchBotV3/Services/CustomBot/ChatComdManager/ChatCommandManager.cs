using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchBotV3.Model;
using TwitchBotV3.Model.ChatCommands;
using TwitchBotV3.Model.Repositories;
using TwitchBotV3.Services.CustomBot;

namespace TwitchBotV3.Services.ChatComManager
{
    public class ChatCommandManager
    {
        private Dictionary<string,BaseChatCommand> commands;
        private readonly ICommonChatCommandRepository<CommonChatCommand> CCCRepository;
        private Bot bot;
        public ChatCommandManager(Bot _bot, ICommonChatCommandRepository<CommonChatCommand> cccRepository)
        {        
            bot = _bot;
            CCCRepository = cccRepository;
            LoadCommand();
        }
        public void LoadCommand()
        {
            var coms = CCCRepository.GetAll();
            commands = new Dictionary<string, BaseChatCommand>();
            foreach(var com in coms)
            {
                commands.Add(com.CommandName, com);
            }
        }
        public void SetCommand()
        {
            LoadCommand();
            bot.SetChatCommand(commands);
        }
        public Dictionary<string, BaseChatCommand> GetChatCommands()
        {
            return this.commands;
        }
        public void AddCommonChatCommand(CommonChatCommand cm) {
            CCCRepository.Add(cm);
            SetCommand();
        }
        public void DeleteCommonChatCommand(string name)
        {
            CCCRepository.DeleteByName(name);
            SetCommand();
        }
        public void UpdateCommonCommand(CommonChatCommand cm)
        {
            CCCRepository.Update(cm);
            SetCommand();
        }
        public IEnumerable<CommonChatCommand> GetAllCommonChatCommands()
        {
            return CCCRepository.GetAll();
        }
        
    }
}
