using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TwitchBotV3.Model;
using TwitchBotV3.Model.ChatCommands;
using TwitchBotV3.Model.Repositories;
using TwitchBotV3.Services;
using TwitchBotV3.Services.Auth;
using TwitchBotV3.Services.ChatComManager;
using TwitchBotV3.Services.CustomBot;
using TwitchBotV3.Services.CustomBot.InteractiveScopes;
using TwitchBotV3.Services.CustomBot.VotingEvent;
using TwitchBotV3.Services.WebSocketService;

namespace TwitchBotV3
{
    public class AutofacModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new TestService()).As<ITestService>().InstancePerLifetimeScope();
            builder.RegisterType<TestService>().As<ITestService>().InstancePerLifetimeScope();
            builder.RegisterType<WSConnectionService>().SingleInstance();          
            builder.RegisterType<Bot>().SingleInstance(); 
            builder.RegisterType<ChatCommandManager>().InstancePerDependency();
            builder.RegisterType<LoggerMessageHandler>().InstancePerDependency();
            builder.RegisterType<ScopesManager>().InstancePerDependency();
            builder.RegisterType<PersonRepository>().As<IPersonRepository<Person>>().InstancePerDependency();
            builder.RegisterType<CommonChatCommandRespository>().As<ICommonChatCommandRepository<CommonChatCommand>>().InstancePerDependency();
            builder.RegisterType<JwtAuthService>().As<IJwtAuthService>().InstancePerLifetimeScope();
            builder.RegisterType<VotingManager>().SingleInstance();
        }
    }
}
