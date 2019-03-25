using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchBotV3.Services.WebSocketService;

namespace TwitchBotV3.Logger
{
    public static class LoggerExtensions
    {
        public static ILoggerFactory WebSocketLogger(this ILoggerFactory factory, LoggerMessageHandler wsHandler
                                        )
        {
            factory.AddProvider(new WSLoggerProvider(wsHandler));
            return factory;
        }
    }
}
