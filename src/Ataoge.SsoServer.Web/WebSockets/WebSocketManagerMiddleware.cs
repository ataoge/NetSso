using System;
using System.IO;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Ataoge.SsoServer.WebSockets
{
    public class WebSocketManagerMiddleware
    {
        private readonly RequestDelegate _next;
        private WebSocketHandler _webSocketHandler { get; set; }

        private readonly ILogger _logger; 

        public WebSocketManagerMiddleware(RequestDelegate next, 
                                          WebSocketHandler webSocketHandler,
                                          ILogger<WebSocketManagerMiddleware> logger)
        {
            _next = next;
            _webSocketHandler = webSocketHandler;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if(!context.WebSockets.IsWebSocketRequest) {
                await _next.Invoke(context);
                return;
            }
            
            CancellationToken ct = context.RequestAborted;
            var socket = await context.WebSockets.AcceptWebSocketAsync();
            await _webSocketHandler.OnConnected(socket);
            
            await Receive(socket, async(result, buffer) =>
            {
                if(result.MessageType == WebSocketMessageType.Text)
                {
                    await _webSocketHandler.ReceiveAsync(socket, result, buffer);
                    return;
                }

                else if(result.MessageType == WebSocketMessageType.Close)
                {
                    await _webSocketHandler.OnDisconnected(socket);
                    return;
                }

            }, ct);

            //await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", ct);
            socket.Dispose();
            
            //TODO - investigate the Kestrel exception thrown when this is the last middleware
            //await _next.Invoke(context);
        }

        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage, CancellationToken ct = default(CancellationToken))
        {
            var bufferSize = 1024 * 4;
            
            while(socket.State == WebSocketState.Open)
            {
                using (var ms = new MemoryStream())
                {
                    try
                    {
                        WebSocketReceiveResult result;

                        do
                        {
                            ct.ThrowIfCancellationRequested();
                            var buffer = new ArraySegment<byte>(new byte[bufferSize]);

                            result = await socket.ReceiveAsync(buffer: buffer,
                                                                cancellationToken: ct);
                            ms.Write(buffer.Array, buffer.Offset, result.Count);


                        }
                        while (!result.EndOfMessage);
                        ms.Seek(0, SeekOrigin.Begin);


                        // Encoding UTF8: https://tools.ietf.org/html/rfc6455#section-5.6
                        //using (var reader = new StreamReader(ms, Encoding.UTF8))
                        //{
                        //    return await reader.ReadToEndAsync();
                        //}

                        handleMessage(result, ms.ToArray());
                    }
                    catch (WebSocketException ex)
                    {
                        _logger.LogWarning(ex.Message);
                    }
                }              
            }
        }
    }
}