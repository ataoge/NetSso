using System;
using System.Collections.Generic;

namespace Ataoge.SsoServer.WebSockets
{
    public class LoginManager
    {
        public LoginManager()
        {

        }

        private Dictionary<string, string> _userTokens = new Dictionary<string, string>();

        public string GetTempToken(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return null;
            var token = Guid.NewGuid().ToString();
            if (_userTokens.TryAdd(token, userName))
                return token;
            return null;
        }

        public string GetUserName(string token)
        {
            if (string.IsNullOrEmpty(token) || !_userTokens.ContainsKey(token))
                return null;
            string userName;
            if (_userTokens.Remove(token, out userName))
            {
                return userName;
            }
            return null;
        }
    }
}