using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Identity
{
    /// <summary>
    /// Provides a custom user store that overrides password related methods to valid the user's password against LDAP.
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public class LdapUserManager<TUser, TKey> : AspNetUserManager<TUser>
        where TUser : EfIdentityUser<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        public LdapUserManager(IUserStore<TUser> store, 
                IOptions<IdentityOptions> optionsAccessor, 
                IPasswordHasher<TUser> passwordHasher, 
                IEnumerable<IUserValidator<TUser>> userValidators, 
                IEnumerable<IPasswordValidator<TUser>> passwordValidators, 
                ILookupNormalizer keyNormalizer, 
                IdentityErrorDescriber errors, 
                IServiceProvider services, 
                ILogger<UserManager<TUser>> logger) 
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

       
    }
}