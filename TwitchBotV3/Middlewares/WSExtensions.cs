using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchBotV3.Services.WebSocketService;

namespace TwitchBotV3.Middlewares
{
    public static class WSExtensions
    {
        public static IApplicationBuilder MapWebSocket(this IApplicationBuilder app,
                                                              PathString path,
                                                              WebSocketHandler handler)
        {
            return app.Map(path, (_app) => _app.UseMiddleware<WSMiddleware>(handler));
        }
    }
}
