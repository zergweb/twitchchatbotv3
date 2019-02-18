using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TwitchBotV3.Services.WebSocketService;

namespace TwitchBotV3.Logger
{
    public class WSLogger : ILogger
    {
        private ChatMessageHandler WSHandler;
        private object _lock = new object();
        public WSLogger(ChatMessageHandler ws)
        {
            WSHandler=ws;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            //return logLevel == LogLevel.Trace;
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                WSHandler.SendMessageToAllAsync(formatter(state, exception));
                //lock (_lock)
                //{
                //    File.AppendAllText(filePath, formatter(state, exception) + Environment.NewLine);
                //}
            }
        }
    }
}
