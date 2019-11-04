using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Identity
{
    public interface IUserPhoneNumberStoreEx<TUser> : IUserPhoneNumberStore<TUser>
        where TUser : class
    {
        Task<TUser> FindByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);
    }
}