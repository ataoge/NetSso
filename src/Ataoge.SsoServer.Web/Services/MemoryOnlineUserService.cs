using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Ataoge.SsoServer.Web.Services
{
     public class MemoryOnlineUserService : IOnlineUserService
    {
        public MemoryOnlineUserService()
        {

        }

        private ConcurrentDictionary<string, int> _onlineUsers = new ConcurrentDictionary<string, int>();
        private ConcurrentDictionary<string, IDictionary<string, long>> _onlineClients = new ConcurrentDictionary<string, IDictionary<string,long>>();

        public void Add(string userId, string client, bool refresh = true)
        {
      
            if (_onlineClients.ContainsKey(userId))
            {
                var clientDicts = _onlineClients[userId];
                if (clientDicts.ContainsKey(client))
                {
                    if (!refresh)
                    {
                        return;
                    }
                    clientDicts[client] = DateTimeOffset.Now.Ticks;
                    return;
                }
                else
                {
                    clientDicts[client] = DateTimeOffset.Now.Ticks;
                    var count = _onlineUsers[userId];
                    _onlineUsers[userId] = count + 1;
                    return;
                }
            }
            else
            {
                var clientDicts = new Dictionary<string, long>();
                clientDicts[client] = DateTimeOffset.Now.Ticks;
                _onlineClients.TryAdd(userId, clientDicts);
                _onlineUsers.TryAdd(userId, 1);
                return;
            }
       }

        public bool Exists(string userId, string client = null)
        {
            if (client == null)
                return _onlineUsers.ContainsKey(userId);
            else
            {
                if (_onlineClients.ContainsKey(userId))
                {
                    var clientDicts = _onlineClients[userId];
                    return clientDicts.ContainsKey(client);
                }
            }
            return false;
        }

        public string[] Get(string userId)
        {
            if (!_onlineClients.ContainsKey(userId))
                return null;
            
            var clientDicts = _onlineClients[userId];
            return clientDicts.Keys.ToArray();
        }

        public string[] GetAll()
        {
            return _onlineUsers.Keys.ToArray();
        }

        public bool Refresh(string userId, string client)
        {
            if (_onlineClients.ContainsKey(userId))
            {
                IDictionary<string, long> clientDicts;
                _onlineClients.TryGetValue(userId, out clientDicts);
                if (clientDicts.ContainsKey(client))
                {
                    clientDicts[client] = DateTimeOffset.Now.Ticks;
                    return true;
                }
            }
            return false;
        }

        public void Remove(string userId, string client)
        {
            if (string.IsNullOrEmpty(client))
            {
                IDictionary<string, long> clientDicts;
                _onlineClients.TryRemove(userId, out clientDicts);
                int count;
                _onlineUsers.TryRemove(userId, out count);
            }
            else
            {
                IDictionary<string, long> clientDicts;
                _onlineClients.TryGetValue(userId, out clientDicts);
                if (clientDicts!= null && clientDicts.ContainsKey(client))
                {
                    clientDicts.Remove(client);
                    int count = _onlineUsers[userId];
                    if (count > 1)
                    {
                        _onlineUsers.TryUpdate(userId, count-1, count);
                    }
                    else
                    {
                        _onlineClients.TryRemove(userId, out clientDicts);
                        _onlineUsers.TryRemove(userId, out count);
                    }
                }
            
            }
        }

        public void Update(int expireMinute)
        {
            IList<string> userFromRemove = new List<string>();
            foreach(var keyValueClient in _onlineClients)
            {
                IList<string> keyFromRemove = new List<string>();
                foreach(var aClient in keyValueClient.Value)
                {
                    TimeSpan timeSpan = new TimeSpan(expireMinute / 60, expireMinute % 60, 0);
                    if (DateTimeOffset.Now -  new DateTimeOffset(aClient.Value ,TimeSpan.Zero) > timeSpan)
                    {
                        keyFromRemove.Add(aClient.Key);
                    }
                }

                if (keyFromRemove.Count > 0)
                {
                    foreach(var key in keyFromRemove)
                    {
                        if (keyValueClient.Value.Remove(key))
                        {
                            int count = _onlineUsers[keyValueClient.Key];
                            _onlineUsers[keyValueClient.Key] = count -1;
                        }
                        
                    }

                    if (keyValueClient.Value.Count() < 1)
                    {
                        userFromRemove.Add(keyValueClient.Key);
                    }
                }
                
            }

            if (userFromRemove.Count > 0)
            {
                foreach(var key in userFromRemove)
                {
                    IDictionary<string, long> clientDicts;
                    _onlineClients.Remove(key, out clientDicts);
                    int count;
                    _onlineUsers.Remove(key, out count);
                }
            }
        }
    }
}