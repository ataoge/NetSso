using System;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.AspNetCore.Identity.EntityFrameworkCore
{
    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    /// <typeparam name="TUser">The type of the user objects.</typeparam>
    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    /// <typeparam name="TUser">The type of user objects.</typeparam>
    /// <typeparam name="TRole">The type of role objects.</typeparam>
    /// <typeparam name="TKey">The type of the primary key for users and roles.</typeparam>
    public class EfIdentityDbContext<TUser, TRole, TKey> : IdentityDbContext<TUser, TRole, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>, IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>>
        where TUser : EfIdentityUser<TKey>
        where TRole : EfIdentityRole<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        /// <summary>
        /// Initializes a new instance of the db context.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public EfIdentityDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TRole>(b =>
            {
                b.Property(u => u.DisplayName).HasColumnName(base.ConvertName(nameof(EfIdentityRole<TKey>.DisplayName))).HasMaxLength(256);
                b.Property(u => u.Pid).HasColumnName(base.ConvertName(nameof(EfIdentityRole<TKey>.Pid)));

                b.Property(u => u.RoleType).HasColumnName(base.ConvertName(nameof(EfIdentityRole<TKey>.RoleType)));
                b.Property(u => u.SIndex).HasColumnName(base.ConvertName(nameof(EfIdentityRole<TKey>.SIndex)));
               
            });

            
            builder.Entity<TUser>(b =>
            {
                b.Property(u => u.FamilyName).HasColumnName(base.ConvertName(nameof(EfIdentityUser<TKey>.FamilyName)));
                b.Property(u => u.GivenName).HasColumnName(base.ConvertName(nameof(EfIdentityUser<TKey>.GivenName)));
            });
         
        }


    }

    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    /// <typeparam name="TUser">The type of the user objects.</typeparam>
    public class EfIdentityDbContext<TUser, TRole> : EfIdentityDbContext<TUser, TRole, int> 
        where TUser : EfIdentityUser<int>
        where TRole : EfIdentityRole<int>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IdentityDbContext"/>.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public EfIdentityDbContext(DbContextOptions options) : base(options)
        {

        }

        
    }
}