using System;
using System.Reflection;
using Ataoge.AspNetCore.Identity.EntityFrameworkCore;
using Ataoge.EntityFrameworkCore;
using Ataoge.SsoServer.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Contains extension methods to <see cref="IdentityBuilder"/> for adding entity framework stores.
    /// </summary>
    public static class IdentityEntityFrameworkBuilderExtensions
    {
        /// <summary>
        /// Adds an Entity Framework implementation of identity information stores.
        /// </summary>
        /// <typeparam name="TContext">The Entity Framework database context to use.</typeparam>
        /// <param name="builder">The <see cref="IdentityBuilder"/> instance this method extends.</param>
        /// <returns>The <see cref="IdentityBuilder"/> instance this method extends.</returns>
        public static IdentityBuilder AddAtaogeEfStores<TContext>(this IdentityBuilder builder)
            where TContext : AtaogeDbContext
        {
            AddStores(builder.Services, builder.UserType, builder.RoleType, typeof(TContext));
            return builder;
        }

        private static void AddStores(IServiceCollection services, Type userType, Type roleType, Type contextType)
        {
            var identityUserType = FindGenericBaseType(userType, typeof(EfIdentityUser<>));
            if (identityUserType == null)
            {
                throw new InvalidOperationException(Resources.NotIdentityUser);
            }

            var keyType = identityUserType.GenericTypeArguments[0];
            if (roleType != null)
            {
                var identityRoleType = FindGenericBaseType(roleType, typeof(IdentityRole<>));
                if (identityRoleType == null)
                {
                    throw new InvalidOperationException(Resources.NotIdentityRole);
                }

                Type userStoreType = null;
                Type roleStoreType = null;
                var identityContext = FindGenericBaseType(contextType, typeof(IdentityDbContext<,,,,,,,>));
                 if (identityContext == null)
                {
                    if (keyType == typeof(string))  //字符型
                    {
                        // If its a custom DbContext, we can only add the default POCOs
                        userStoreType = typeof(UserStore<,,,>).MakeGenericType(userType, roleType, contextType, keyType);
                        roleStoreType = typeof(RoleStore<,,>).MakeGenericType(roleType, contextType, keyType);
                    }
                    else
                    {
                        userStoreType = typeof(EfUserStore<,,,>).MakeGenericType(userType, roleType, contextType, keyType);
                        roleStoreType = typeof(EfRoleStore<,,>).MakeGenericType(roleType, contextType, keyType);
                    }
                }
                else
                {
                    if (keyType == typeof(string))  //字符型
                    {
                        userStoreType = typeof(UserStore<,,,,,,,,>).MakeGenericType(userType, roleType, contextType,
                            identityContext.GenericTypeArguments[2],
                            identityContext.GenericTypeArguments[3],
                            identityContext.GenericTypeArguments[4],
                            identityContext.GenericTypeArguments[5],
                            identityContext.GenericTypeArguments[7],
                            identityContext.GenericTypeArguments[6]);
                        roleStoreType = typeof(RoleStore<,,,,>).MakeGenericType(roleType, contextType,
                            identityContext.GenericTypeArguments[2],
                            identityContext.GenericTypeArguments[4],
                            identityContext.GenericTypeArguments[6]);
                    }
                    else
                    {
                         userStoreType = typeof(EfUserStore<,,,,,,,,>).MakeGenericType(userType, roleType, contextType,
                            identityContext.GenericTypeArguments[2],
                            identityContext.GenericTypeArguments[3],
                            identityContext.GenericTypeArguments[4],
                            identityContext.GenericTypeArguments[5],
                            identityContext.GenericTypeArguments[7],
                            identityContext.GenericTypeArguments[6]);
                        roleStoreType = typeof(EfRoleStore<,,,,>).MakeGenericType(roleType, contextType,
                            identityContext.GenericTypeArguments[2],
                            identityContext.GenericTypeArguments[4],
                            identityContext.GenericTypeArguments[6]);
                    }
                }
                services.TryAddScoped(typeof(IUserStore<>).MakeGenericType(userType), userStoreType);
                services.TryAddScoped(typeof(IRoleStore<>).MakeGenericType(roleType), roleStoreType);
            }
            else
            {
                // No Roles
                Type userStoreType = null;
                var identityContext = FindGenericBaseType(contextType, typeof(IdentityUserContext<,,,,>));
                if (identityContext == null)
                {
                    if (keyType == typeof(string))  //字符型
                    {
                        // If its a custom DbContext, we can only add the default POCOs
                        userStoreType = typeof(UserStore<,,,>).MakeGenericType(userType, roleType, contextType, keyType);
                    }
                    else
                    {
                        userStoreType = typeof(EfUserStore<,,,>).MakeGenericType(userType, roleType, contextType, keyType);
                    }
                }
                else
                {
                    if (keyType == typeof(string))
                    {
                        userStoreType = typeof(UserOnlyStore<,,,,,>).MakeGenericType(userType, roleType, contextType,
                            identityContext.GenericTypeArguments[1],
                            identityContext.GenericTypeArguments[2],
                            identityContext.GenericTypeArguments[3],
                            identityContext.GenericTypeArguments[4]);
                    }
                    else
                    {
                        userStoreType = typeof(EfUserOnlyStore<,,,,,>).MakeGenericType(userType, roleType, contextType,
                            identityContext.GenericTypeArguments[1],
                            identityContext.GenericTypeArguments[2],
                            identityContext.GenericTypeArguments[3],
                            identityContext.GenericTypeArguments[4]);   
                    }

                }
                services.TryAddScoped(typeof(IUserStore<>).MakeGenericType(userType), userStoreType);
            }
            
        }

        private static TypeInfo FindGenericBaseType(Type currentType, Type genericBaseType)
        {
            var type = currentType;
            while (type != null)
            {
                var typeInfo = type.GetTypeInfo();
                var genericType = type.IsGenericType ? type.GetGenericTypeDefinition() : null;
                if (genericType != null && genericType == genericBaseType)
                {
                    return typeInfo;
                }
                type = type.BaseType;
            }
            return null;
        }
    }
}