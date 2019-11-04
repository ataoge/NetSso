using System;
using Ataoge.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.AspNetCore.Identity.EntityFrameworkCore
{
    /// <summary>
    /// Creates a new instance of a persistence store for roles.
    /// </summary>
    /// <typeparam name="TRole">The type of the class representing a role.</typeparam>
    /// <typeparam name="TContext">The type of the data context class used to access the store.</typeparam>
    /// <typeparam name="TKey">The type of the primary key for a role.</typeparam>
    /// <typeparam name="TUserRole">The type of the class representing a user role.</typeparam>
    /// <typeparam name="TRoleClaim">The type of the class representing a role claim.</typeparam>
    public class EfRoleStore<TRole, TContext, TKey, TUserRole, TRoleClaim> :
        RoleStore<TRole, TContext, TKey, TUserRole, TRoleClaim>
        where TRole : EfIdentityRole<TKey>
        where TKey : struct, IEquatable<TKey>
        where TContext : AtaogeDbContext
        where TUserRole : IdentityUserRole<TKey>, new()
        where TRoleClaim : IdentityRoleClaim<TKey>, new()
    {
        /// <summary>
        /// Constructs a new instance of <see cref="RoleStore{TRole, TContext, TKey}"/>.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/>.</param>
        /// <param name="describer">The <see cref="IdentityErrorDescriber"/>.</param>
        public EfRoleStore(TContext context, IdentityErrorDescriber describer = null) : base(context, describer) { }
    }

    /// <summary>
    /// Creates a new instance of a persistence store for roles.
    /// </summary>
    /// <typeparam name="TRole">The type of the class representing a role.</typeparam>
    /// <typeparam name="TContext">The type of the data context class used to access the store.</typeparam>
    /// <typeparam name="TKey">The type of the primary key for a role.</typeparam>
    public class EfRoleStore<TRole, TContext, TKey> : EfRoleStore<TRole, TContext, TKey, IdentityUserRole<TKey>, IdentityRoleClaim<TKey>>,
        IQueryableRoleStore<TRole>,
        IRoleClaimStore<TRole>
        where TRole : EfIdentityRole<TKey>
        where TKey : struct, IEquatable<TKey>
        where TContext : AtaogeDbContext
    {
        /// <summary>
        /// Constructs a new instance of <see cref="RoleStore{TRole, TContext, TKey}"/>.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/>.</param>
        /// <param name="describer">The <see cref="IdentityErrorDescriber"/>.</param>
        public EfRoleStore(TContext context, IdentityErrorDescriber describer = null) : base(context, describer) { }
    }

    /// <summary>
    /// Creates a new instance of a persistence store for roles.
    /// </summary>
    /// <typeparam name="TRole">The type of the class representing a role.</typeparam>
    /// <typeparam name="TContext">The type of the data context class used to access the store.</typeparam>
    public class EfRoleStore<TRole, TContext> : EfRoleStore<TRole, TContext, int>
        where TRole : EfIdentityRole<int>
        where TContext : AtaogeDbContext
    {
        /// <summary>
        /// Constructs a new instance of <see cref="RoleStore{TRole, TContext}"/>.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/>.</param>
        /// <param name="describer">The <see cref="IdentityErrorDescriber"/>.</param>
        public EfRoleStore(TContext context, IdentityErrorDescriber describer = null) : base(context, describer) { }
    }

    /// <summary>
    /// Creates a new instance of a persistence store for roles.
    /// </summary>
    /// <typeparam name="TRole">The type of the class representing a role</typeparam>
    public class EfRoleStore<TRole> : EfRoleStore<TRole, AtaogeDbContext, int>
        where TRole : EfIdentityRole<int>
    {
        /// <summary>
        /// Constructs a new instance of <see cref="RoleStore{TRole}"/>.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/>.</param>
        /// <param name="describer">The <see cref="IdentityErrorDescriber"/>.</param>
        public EfRoleStore(AtaogeDbContext context, IdentityErrorDescriber describer = null) : base(context, describer) { }
    }
}