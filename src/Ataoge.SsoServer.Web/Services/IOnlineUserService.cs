namespace Ataoge.SsoServer.Web.Services
{
    public interface IOnlineUserService
    {
        void Add(string userId, string client, bool refresh = true);

        string[] Get(string userId);

        void Remove(string userId, string client);

        bool Exists(string userId, string client = null);

        string[] GetAll();

        bool Refresh(string userId, string client);

        void Update(int expireMinute);
    }
}