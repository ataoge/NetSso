using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.AspNetCore.Identity.EntityFrameworkCore
{
    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    public class IdentityDbContext : IdentityDbContext<EfIdentityUser<string>, EfIdentityRole>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IdentityDbContext"/>.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public IdentityDbContext(DbContextOptions options) : base(options)
        {

        }

       
    }

    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    /// <typeparam name="TUser">The type of the user objects.</typeparam>
    public class IdentityDbContext<TUser> : IdentityDbContext<TUser, EfIdentityRole> 
        where TUser : EfIdentityUser<string>
    {
            /// <summary>
        /// Initializes a new instance of <see cref="IdentityDbContext"/>.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public IdentityDbContext(DbContextOptions options) : base(options)
        {

        }
    }

    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    /// <typeparam name="TUser">The type of the user objects.</typeparam>
    public class IdentityDbContext<TUser, TRole> : IdentityDbContext<TUser, TRole, string> 
        where TUser : EfIdentityUser<string>
        where TRole : EfIdentityRole
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IdentityDbContext"/>.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public IdentityDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TRole>(b =>
            {
                b.Property(u => u.DisplayName).HasColumnName(base.ConvertName(nameof(EfIdentityRole.DisplayName))).HasMaxLength(256);
                b.Property(u => u.Pid).HasColumnName(base.ConvertName(nameof(EfIdentityRole.Pid)));

                b.Property(u => u.RoleType).HasColumnName(base.ConvertName(nameof(EfIdentityRole.RoleType)));
                b.Property(u => u.SIndex).HasColumnName(base.ConvertName(nameof(EfIdentityRole.SIndex)));
               
            });
        }


    }

    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    /// <typeparam name="TUser">The type of user objects.</typeparam>
    /// <typeparam name="TRole">The type of role objects.</typeparam>
    /// <typeparam name="TKey">The type of the primary key for users and roles.</typeparam>
    public class IdentityDbContext<TUser, TRole, TKey> : IdentityDbContext<TUser, TRole, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>, IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>>
        where TUser : EfIdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Initializes a new instance of the db context.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public IdentityDbContext(DbContextOptions options) : base(options)
        {

        }

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
    /// <typeparam name="TRole">The type of role objects.</typeparam>
    /// <typeparam name="TKey">The type of the primary key for users and roles.</typeparam>
    /// <typeparam name="TUserClaim">The type of the user claim object.</typeparam>
    /// <typeparam name="TUserRole">The type of the user role object.</typeparam>
    /// <typeparam name="TUserLogin">The type of the user login object.</typeparam>
    /// <typeparam name="TRoleClaim">The type of the role claim object.</typeparam>
    /// <typeparam name="TUserToken">The type of the user token object.</typeparam>
    public abstract class IdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken> : IdentityUserContext<TUser, TKey, TUserClaim, TUserLogin, TUserToken>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
        where TUserClaim : IdentityUserClaim<TKey>
        where TUserRole : IdentityUserRole<TKey>
        where TUserLogin : IdentityUserLogin<TKey>
        where TRoleClaim : IdentityRoleClaim<TKey>
        where TUserToken : IdentityUserToken<TKey>
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        protected IdentityDbContext(DbContextOptions options) : base(options)
        {
          
        }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of User roles.
        /// </summary>
        public DbSet<TUserRole> UserRoles { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of roles.
        /// </summary>
        public DbSet<TRole> Roles { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> of role claims.
        /// </summary>
        public DbSet<TRoleClaim> RoleClaims { get; set; }

        /// <summary>
        /// Configures the schema needed for the identity framework.
        /// </summary>
        /// <param name="builder">
        /// The builder being used to construct the model for this context.
        /// </param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TUser>(b =>
            {
                b.HasMany<TUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
            });

            builder.Entity<TRole>(b =>
            {
                b.HasKey(r => r.Id);
                b.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();
                b.ToTable(base.ConvertName("AspNetRoles"));
                b.Property(r => r.Id).HasColumnName(base.ConvertName(nameof(IdentityRole<TKey>.Id)));
                b.Property(r => r.ConcurrencyStamp).HasColumnName(base.ConvertName(nameof(IdentityRole<TKey>.ConcurrencyStamp))).IsConcurrencyToken();

                b.Property(u => u.Name).HasColumnName(base.ConvertName(nameof(IdentityRole<TKey>.Name))).HasMaxLength(256);
                b.Property(u => u.NormalizedName).HasColumnName(base.ConvertName(nameof(IdentityRole<TKey>.NormalizedName))).HasMaxLength(256);
               
                b.HasMany<TUserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();
                b.HasMany<TRoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
            });


            builder.ApplyConfiguration<TRoleClaim>(new IdentityRoleClaimConfiguration<TRoleClaim, TKey>(this));
            //builder.Entity<TRoleClaim>(b =>
            //{
            //    b.HasKey(rc => rc.Id);
            //    b.ToTable("AspNetRoleClaims");
            //});

            builder.ApplyConfiguration<TUserRole>(new IdentityUserRoleConfiguration<TUserRole,TKey>(this));
            //builder.Entity<TUserRole>(b =>
            //{
            //    b.HasKey(r => new { r.UserId, r.RoleId });
            //    b.ToTable("AspNetUserRoles");
            //});
        }
    }
}