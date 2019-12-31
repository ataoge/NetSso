using Newtonsoft.Json;

namespace Ataoge.SsoServer.WebSockets
{
    public class MessageInfo
    {
        [JsonProperty("sender")]
        public string Sender {get; set;}

        [JsonProperty("receiver")]
        public string Receiver {get; set;}

        [JsonProperty("type")]
        public string Type {get; set;}

        [JsonProperty("message")]
        public string Message {get; set;}
    }
}