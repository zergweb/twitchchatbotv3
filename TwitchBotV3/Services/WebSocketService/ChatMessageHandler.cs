using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBotV3.Services.WebSocketService
{
    public class ChatMessageHandler : WebSocketHandler
    {
        public ChatMessageHandler(WSConnectionService webSocketConnectionManager) : base(webSocketConnectionManager)
        {
        }
        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);         
            var socketId = WSConnection.GetId(socket);
            await SendMessageToAllAsync($"{socketId} is now connected");
        }
        public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var socketId = WSConnection.GetId(socket);
            var message = $"{socketId} said: {Encoding.UTF8.GetString(buffer, 0, result.Count)}";
            await SendMessageToAllAsync(message);
        }
    }
}
