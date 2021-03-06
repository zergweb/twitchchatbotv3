﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchBotV3.Services.WebSocketService
{
    public abstract class WebSocketHandler
    {     
            protected WSConnectionService WSConnection { get; set; }
            public WebSocketHandler(WSConnectionService webSocketConnection)
            {
                WSConnection = webSocketConnection;
            }
            public virtual async Task OnConnected(WebSocket socket)
            {
                WSConnection.AddSocket(socket);
            }
            public virtual async Task OnDisconnected(WebSocket socket)
            {
                await WSConnection.RemoveSocket(WSConnection.GetId(socket));
            }
            public async Task SendMessageAsync(WebSocket socket, string message)
            {
                if (socket.State != WebSocketState.Open)
                    return;

                await socket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(message),
                                                                      offset: 0,
                                                                      count: message.Length),
                                       messageType: WebSocketMessageType.Text,
                                       endOfMessage: true,
                                       cancellationToken: CancellationToken.None);
            }
            public async Task SendMessageAsync(string socketId, string message)
            {
                await SendMessageAsync(WSConnection.GetSocketById(socketId), message);
            }
            public async Task SendMessageToAllAsync(string message)
            {
                foreach (var pair in WSConnection.GetAll())
                {
                    if (pair.Value.State == WebSocketState.Open)
                        await SendMessageAsync(pair.Value, message);
                }
            }
            public abstract Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
        }
}
