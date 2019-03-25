using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchBotV3.Services.WebSocketService;

namespace TwitchBotV3.Logger
{
    public class WSLoggerProvider : ILoggerProvider
    {
        private LoggerMessageHandler ws;
        public WSLoggerProvider(LoggerMessageHandler wsHandler)
        {
            ws = wsHandler;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new WSLogger(ws);
        }

        public void Dispose()
        {
        }
    }
}
