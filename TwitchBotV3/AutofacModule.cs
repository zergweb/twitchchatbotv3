using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TwitchBotV3.Model;
using TwitchBotV3.Model.Repositories;
using TwitchBotV3.Services;
using TwitchBotV3.Services.Auth;

namespace TwitchBotV3
{
    public class AutofacModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // builder.Register(c => new TestService()).As<ITestService>().InstancePerLifetimeScope();
            builder.RegisterType<TestService>().As<ITestService>().InstancePerLifetimeScope();
            builder.RegisterType<TestRepository>().As<IEntityRepository<Person>>().InstancePerLifetimeScope();
            builder.RegisterType<JwtAuthService>().As<IJwtAuthService>().InstancePerLifetimeScope();

        }
    }
}
