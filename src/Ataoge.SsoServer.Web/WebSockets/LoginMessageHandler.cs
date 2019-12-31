using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Ataoge.SsoServer.WebSockets
{
    public class LoginMessageHandler : WebSocketHandler
    {
        private ConcurrentDictionary<string, string> _clients = new ConcurrentDictionary<string, string>();
        
        public LoginMessageHandler(WebSocketConnectionManager webSocketConnectionManager, ILoggerFactory loggerFactory) : base(webSocketConnectionManager)
        {
            _logger = loggerFactory.CreateLogger<LoginMessageHandler>();
        }

        private readonly ILogger _logger;

        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);

            var socketId = WebSocketConnectionManager.GetId(socket);
            //await SendMessageToAllAsync($"{socketId} is now connected");
            var msg = new MessageInfo() { Sender = "Server", Receiver = socketId, Type = "connected"};
            await SendMessageAsync(socket, JsonConvert.SerializeObject(msg));
        }

        public override async Task OnDisconnected(WebSocket socket)
        {
            try
            {
                var socketId = WebSocketConnectionManager.GetId(socket);
                var key = _clients.FirstOrDefault(t => t.Value == socketId).Key;
                string client;
                _clients.TryRemove(key, out client);
                _logger.LogInformation($"{client} has Disconnected");
                await base.OnDisconnected(socket);
            }
            catch(Exception)
            {

            }
            
        }

        public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var socketId = WebSocketConnectionManager.GetId(socket);
            //var message = $"{socketId} said: {Encoding.UTF8.GetString(buffer, 0, result.Count)}";
            var json = Encoding.UTF8.GetString(buffer, 0, result.Count);
            var msg = await Task.Run( () => JsonConvert.DeserializeObject<MessageInfo>(json));
            if (msg.Type == "client")
            {
                //var uri = new Uri(msg.Message);
                var validation = msg.Message.Length == 36;
                var clientId = msg.Message;
                 _clients.TryAdd(clientId, msg.Sender);
            }

            //await SendMessageToAllAsync(message);
            //throw new NotImplementedException();
        }

         public async Task SendLoginMessageAsync(string clientId, string token)
         {
            var receiver  =  _clients.FirstOrDefault(p => p.Key == clientId).Value;
            var msg = new MessageInfo() {Sender = "Server", Receiver = receiver, Type = "loginToken", Message = token };
            string removed;
            _clients.TryRemove(clientId, out removed);
            await base.SendMessageAsync(removed, JsonConvert.SerializeObject(msg));
         }

         public IList<string> GetClients()
         {
            return base.WebSocketConnectionManager.GetAll()?.Keys?.ToList();
         }
    }
}