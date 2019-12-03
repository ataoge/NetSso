using System;
using Ataoge.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore.Configurations;
using Microsoft.EntityFrameworkCore;


namespace Ataoge.AspNetCore.Identity.EntityFrameworkCore
{
    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    /// <typeparam name="TUser">The type of the user objects.</typeparam>
    public class IdentityUserContext<TUser> : IdentityUserContext<TUser, string> where TUser : EfIdentityUser<string>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IdentityUserContext{TUser}"/>.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public IdentityUserContext(DbContextOptions options) : base(options) { }
    }

    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    /// <typeparam name="TUser">The type of user objects.</typeparam>
    /// <typeparam name="TKey">The type of the primary key for users and roles.</typeparam>
    public class IdentityUserContext<TUser, TKey> : IdentityUserContext<TUser, TKey, IdentityUserClaim<TKey>, IdentityUserLogin<TKey>, IdentityUserToken<TKey>>
        where TUser : EfIdentityUser<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Initializes a new instance of the db context.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public IdentityUserContext(DbContextOptions options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

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
    /// <typeparam name="TUser">The type of user objects.</typeparam>
    /// <typeparam name="TKey">The type of the primary key for users and roles.</typeparam>
    /// <typeparam name="TUserClaim">The type of the user claim object.</typeparam>
    /// <typeparam name="TUserLogin">The type of the user login object.</typeparam>
    /// <typeparam name="TUserToken">The type of the user token object.</typeparam>
    public abstract class IdentityUserContext<TUser, TKey, TUserClaim, TUserLogin, TUserToken> : AtaogeDbContext
        where TUser : IdentityUser<TKey>
        where TKey : IEquatable<TKey>
        where TUserClaim : IdentityUserClaim<TKey>
        where TUserLogin : IdentityUserLogin<TKey>
        where TUserToken : IdentityUserToken<TKey>
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        protected IdentityUserContext(DbContextOptions options) : base(options)
        {
            
        }


        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of Users.
        /// </summary>
        public DbSet<TUser> Users { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of User claims.
        /// </summary>
        public DbSet<TUserClaim> UserClaims { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of User logins.
        /// </summary>
        public DbSet<TUserLogin> UserLogins { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of User tokens.
        /// </summary>
        public DbSet<TUserToken> UserTokens { get; set; }

        /// <summary>
        /// Configures the schema needed for the identity framework.
        /// </summary>
        /// <param name="builder">
        /// The builder being used to construct the model for this context.
        /// </param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TUser>(b =>
            {
                b.HasKey(u => u.Id);
                b.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
                b.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");
                b.ToTable(base.ConvertName("AspNetUsers"));
                b.Property(u => u.Id).HasColumnName(base.ConvertName(nameof(IdentityUser<TKey>.Id)));
                b.Property(u => u.ConcurrencyStamp).HasColumnName(base.ConvertName(nameof(IdentityUser<TKey>.ConcurrencyStamp))).IsConcurrencyToken();

                b.Property(u => u.UserName).HasColumnName(base.ConvertName(nameof(IdentityUser<TKey>.UserName))).HasMaxLength(256);
                b.Property(u => u.NormalizedUserName).HasColumnName(base.ConvertName(nameof(IdentityUser<TKey>.NormalizedUserName))).HasMaxLength(256);
                b.Property(u => u.Email).HasColumnName(base.ConvertName(nameof(IdentityUser<TKey>.Email))).HasMaxLength(256);
                b.Property(u => u.NormalizedEmail).HasColumnName(base.ConvertName(nameof(IdentityUser<TKey>.NormalizedEmail))).HasMaxLength(256);
                
                b.Property(u => u.TwoFactorEnabled).HasColumnName(base.ConvertName(nameof(IdentityUser<TKey>.TwoFactorEnabled)));
                b.Property(u => u.EmailConfirmed).HasColumnName(base.ConvertName(nameof(IdentityUser<TKey>.EmailConfirmed)));
                b.Property(u => u.SecurityStamp).HasColumnName(base.ConvertName(nameof(IdentityUser<TKey>.SecurityStamp)));
                b.Property(u => u.AccessFailedCount).HasColumnName(base.ConvertName(nameof(IdentityUser<TKey>.AccessFailedCount)));
                b.Property(u => u.PasswordHash).HasColumnName(base.ConvertName(nameof(IdentityUser<TKey>.PasswordHash)));
                b.Property(u => u.LockoutEnabled).HasColumnName(base.ConvertName(nameof(IdentityUser<TKey>.LockoutEnabled)));
                b.Property(u => u.LockoutEnd).HasColumnName(base.ConvertName(nameof(IdentityUser<TKey>.LockoutEnd)));
                b.Property(u => u.PhoneNumber).HasColumnName(base.ConvertName(nameof(IdentityUser<TKey>.PhoneNumber)));
                b.Property(u => u.PhoneNumberConfirmed).HasColumnName(base.ConvertName(nameof(IdentityUser<TKey>.PhoneNumberConfirmed)));

                // Replace with b.HasMany<IdentityUserClaim>().
                b.HasMany<TUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
                b.HasMany<TUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
                b.HasMany<TUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();
            });

            //builder.Entity<TUserClaim>(b =>
            //{
            //    b.HasKey(uc => uc.Id);
            //    b.ToTable("AspNetUserClaims");
            //});

            builder.ApplyConfiguration<TUserClaim>(new IdentityUserClaimConfiguration<TUserClaim,TKey>(this));

            //builder.Entity<TUserLogin>(b =>
            //{
            //    b.HasKey(l => new { l.LoginProvider, l.ProviderKey });
            //    b.ToTable("AspNetUserLogins");
            //});

            builder.ApplyConfiguration<TUserLogin>(new IdentityUserLoginConfiguration<TUserLogin,TKey>(this));


            /*builder.Entity<TUserToken>(b => 
            {
                b.HasKey(l => new { l.UserId, l.LoginProvider, l.Name });
                b.ToTable("AspNetUserTokens");
            });*/

            builder.ApplyConfiguration<TUserToken>(new IdentityUserTokenConfiguration<TUserToken,TKey>(this));
        }
    }
}