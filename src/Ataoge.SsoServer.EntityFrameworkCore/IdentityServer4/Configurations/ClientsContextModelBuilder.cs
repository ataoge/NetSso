using System;
using Ataoge.EntityFrameworkCore;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace IdentityServer4.EntityFramework.Configurations
{
    public class ClientsContextModelBuilder : IEntityFrameworkModelBuilder
    {
        private ConfigurationStoreOptions GetStoreOptions(AtaogeDbContext dbContext)
        {
            ConfigurationStoreOptions storeOptions = null;
            try
            {
                storeOptions = dbContext.GetService<ConfigurationStoreOptions>();
            }
            catch (InvalidOperationException) {

            }
            return storeOptions;
        }
        
        public void BuildModel(ModelBuilder modelBuilder, AtaogeDbContext dbContext)
        {
            var  storeOptions = GetStoreOptions(dbContext);
            if (storeOptions == null) return;

            if (!string.IsNullOrWhiteSpace(storeOptions.DefaultSchema)) modelBuilder.HasDefaultSchema(storeOptions.DefaultSchema);
            
            modelBuilder.Entity<Client>(client =>
            {
                client.ToTable(dbContext.ConvertName(storeOptions.Client.Name));
                client.HasKey(x => x.Id);

                client.Property(x => x.Id).HasColumnName(dbContext.ConvertName(nameof(Client.Id)));
                client.Property(x => x.ClientId).HasColumnName(dbContext.ConvertName(nameof(Client.ClientId))).HasMaxLength(200).IsRequired();
                client.Property(x => x.ProtocolType).HasColumnName(dbContext.ConvertName(nameof(Client.ProtocolType))).HasMaxLength(200).IsRequired();
                client.Property(x => x.ClientName).HasColumnName(dbContext.ConvertName(nameof(Client.ClientName))).HasMaxLength(200);
                client.Property(x => x.ClientUri).HasColumnName(dbContext.ConvertName(nameof(Client.ClientUri))).HasMaxLength(2000);
                client.Property(x => x.LogoUri).HasColumnName(dbContext.ConvertName(nameof(Client.LogoUri))).HasMaxLength(2000);
                client.Property(x => x.Description).HasColumnName(dbContext.ConvertName(nameof(Client.Description))).HasMaxLength(1000);
                client.Property(x => x.FrontChannelLogoutUri).HasColumnName(dbContext.ConvertName(nameof(Client.FrontChannelLogoutUri))).HasMaxLength(2000);
                client.Property(x => x.BackChannelLogoutUri).HasColumnName(dbContext.ConvertName(nameof(Client.BackChannelLogoutUri))).HasMaxLength(2000);
                client.Property(x => x.ClientClaimsPrefix).HasColumnName(dbContext.ConvertName(nameof(Client.ClientClaimsPrefix))).HasMaxLength(200);
                client.Property(x => x.PairWiseSubjectSalt).HasColumnName(dbContext.ConvertName(nameof(Client.PairWiseSubjectSalt))).HasMaxLength(200);
                client.Property(x => x.UserCodeType).HasColumnName(dbContext.ConvertName(nameof(Client.UserCodeType))).HasMaxLength(100);

                client.Property(x => x.AbsoluteRefreshTokenLifetime).HasColumnName(dbContext.ConvertName(nameof(Client.AbsoluteRefreshTokenLifetime)));
                client.Property(x => x.AccessTokenLifetime).HasColumnName(dbContext.ConvertName(nameof(Client.AccessTokenLifetime)));
                client.Property(x => x.AccessTokenType).HasColumnName(dbContext.ConvertName(nameof(Client.AccessTokenType)));
                client.Property(x => x.AllowAccessTokensViaBrowser).HasColumnName(dbContext.ConvertName(nameof(Client.AllowAccessTokensViaBrowser)));
                client.Property(x => x.AllowOfflineAccess).HasColumnName(dbContext.ConvertName(nameof(Client.AllowOfflineAccess)));
                client.Property(x => x.AllowPlainTextPkce).HasColumnName(dbContext.ConvertName(nameof(Client.AllowPlainTextPkce)));
                client.Property(x => x.AllowRememberConsent).HasColumnName(dbContext.ConvertName(nameof(Client.AllowRememberConsent)));
                client.Property(x => x.AlwaysIncludeUserClaimsInIdToken).HasColumnName(dbContext.ConvertName(nameof(Client.AlwaysIncludeUserClaimsInIdToken)));
                client.Property(x => x.AlwaysSendClientClaims).HasColumnName(dbContext.ConvertName(nameof(Client.AlwaysSendClientClaims)));
                client.Property(x => x.AuthorizationCodeLifetime).HasColumnName(dbContext.ConvertName(nameof(Client.AuthorizationCodeLifetime)));
                client.Property(x => x.BackChannelLogoutSessionRequired).HasColumnName(dbContext.ConvertName(nameof(Client.BackChannelLogoutSessionRequired)));
                client.Property(x => x.ConsentLifetime).HasColumnName(dbContext.ConvertName(nameof(Client.ConsentLifetime)));
                client.Property(x => x.EnableLocalLogin).HasColumnName(dbContext.ConvertName(nameof(Client.EnableLocalLogin)));
                client.Property(x => x.Enabled).HasColumnName(dbContext.ConvertName(nameof(Client.Enabled)));
                client.Property(x => x.FrontChannelLogoutSessionRequired).HasColumnName(dbContext.ConvertName(nameof(Client.FrontChannelLogoutSessionRequired)));
                client.Property(x => x.IdentityTokenLifetime).HasColumnName(dbContext.ConvertName(nameof(Client.IdentityTokenLifetime)));
                client.Property(x => x.IncludeJwtId).HasColumnName(dbContext.ConvertName(nameof(Client.IncludeJwtId)));
                client.Property(x => x.RefreshTokenExpiration).HasColumnName(dbContext.ConvertName(nameof(Client.RefreshTokenExpiration)));
                client.Property(x => x.RefreshTokenUsage).HasColumnName(dbContext.ConvertName(nameof(Client.RefreshTokenUsage)));
                client.Property(x => x.RequireClientSecret).HasColumnName(dbContext.ConvertName(nameof(Client.RequireClientSecret)));
                client.Property(x => x.RequireConsent).HasColumnName(dbContext.ConvertName(nameof(Client.RequireConsent)));
                client.Property(x => x.RequirePkce).HasColumnName(dbContext.ConvertName(nameof(Client.RequirePkce)));
                client.Property(x => x.SlidingRefreshTokenLifetime).HasColumnName(dbContext.ConvertName(nameof(Client.SlidingRefreshTokenLifetime)));
                client.Property(x => x.UpdateAccessTokenClaimsOnRefresh).HasColumnName(dbContext.ConvertName(nameof(Client.UpdateAccessTokenClaimsOnRefresh)));

                client.Property(x => x.Created).HasColumnName(dbContext.ConvertName(nameof(Client.Created)));
                client.Property(x => x.Updated).HasColumnName(dbContext.ConvertName(nameof(Client.Updated)));
                client.Property(x => x.LastAccessed).HasColumnName(dbContext.ConvertName(nameof(Client.LastAccessed)));
                client.Property(x => x.UserSsoLifetime).HasColumnName(dbContext.ConvertName(nameof(Client.UserSsoLifetime)));
                client.Property(x => x.UserCodeType).HasColumnName(dbContext.ConvertName(nameof(Client.UserCodeType))).HasMaxLength(100);
                client.Property(x => x.DeviceCodeLifetime).HasColumnName(dbContext.ConvertName(nameof(Client.DeviceCodeLifetime)));
                client.Property(x => x.NonEditable).HasColumnName(dbContext.ConvertName(nameof(Client.NonEditable)));


                client.HasIndex(x => x.ClientId).IsUnique();

                client.HasMany(x => x.AllowedGrantTypes).WithOne(x => x.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
                client.HasMany(x => x.RedirectUris).WithOne(x => x.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
                client.HasMany(x => x.PostLogoutRedirectUris).WithOne(x => x.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
                client.HasMany(x => x.AllowedScopes).WithOne(x => x.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
                client.HasMany(x => x.ClientSecrets).WithOne(x => x.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
                client.HasMany(x => x.Claims).WithOne(x => x.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
                client.HasMany(x => x.IdentityProviderRestrictions).WithOne(x => x.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
                client.HasMany(x => x.AllowedCorsOrigins).WithOne(x => x.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
                client.HasMany(x => x.Properties).WithOne(x => x.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ClientGrantType>(grantType =>
            {
                grantType.ToTable(dbContext.ConvertName(storeOptions.ClientGrantType.Name));
                grantType.HasKey(x => x.Id);

                grantType.Property(x => x.Id).HasColumnName(dbContext.ConvertName(nameof(ClientGrantType.Id)));
                grantType.Property(x => x.ClientId).HasColumnName(dbContext.ConvertName(nameof(ClientGrantType.ClientId)));
                grantType.Property(x => x.GrantType).HasColumnName(dbContext.ConvertName(nameof(ClientGrantType.GrantType))).HasMaxLength(250).IsRequired();
            });

            modelBuilder.Entity<ClientRedirectUri>(redirectUri =>
            {
                redirectUri.ToTable(dbContext.ConvertName(storeOptions.ClientRedirectUri.Name));
                redirectUri.HasKey(x => x.Id);

                redirectUri.Property(x => x.Id).HasColumnName(dbContext.ConvertName(nameof(ClientRedirectUri.Id)));
                redirectUri.Property(x => x.ClientId).HasColumnName(dbContext.ConvertName(nameof(ClientRedirectUri.ClientId)));
                redirectUri.Property(x => x.RedirectUri).HasColumnName(dbContext.ConvertName(nameof(ClientRedirectUri.RedirectUri))).HasMaxLength(2000).IsRequired();
            });

            modelBuilder.Entity<ClientPostLogoutRedirectUri>(postLogoutRedirectUri =>
            {
                postLogoutRedirectUri.ToTable(dbContext.ConvertName(storeOptions.ClientPostLogoutRedirectUri.Name));
                postLogoutRedirectUri.HasKey(x => x.Id);

                postLogoutRedirectUri.Property(x => x.Id).HasColumnName(dbContext.ConvertName(nameof(ClientPostLogoutRedirectUri.Id)));
                postLogoutRedirectUri.Property(x => x.ClientId).HasColumnName(dbContext.ConvertName(nameof(ClientPostLogoutRedirectUri.ClientId)));
                postLogoutRedirectUri.Property(x => x.PostLogoutRedirectUri).HasColumnName(dbContext.ConvertName(nameof(ClientPostLogoutRedirectUri.PostLogoutRedirectUri))).HasMaxLength(2000).IsRequired();
            });

            modelBuilder.Entity<ClientScope>(scope =>
            {
                scope.ToTable(dbContext.ConvertName(storeOptions.ClientScopes.Name));
                scope.HasKey(x => x.Id);

                scope.Property(x => x.Id).HasColumnName(dbContext.ConvertName(nameof(ClientScope.Id)));
                scope.Property(x => x.ClientId).HasColumnName(dbContext.ConvertName(nameof(ClientScope.ClientId)));
                scope.Property(x => x.Scope).HasColumnName(dbContext.ConvertName(nameof(ClientScope.Scope))).HasMaxLength(200).IsRequired();
            });

            modelBuilder.Entity<ClientSecret>(secret =>
            {
                secret.ToTable(dbContext.ConvertName(storeOptions.ClientSecret.Name));
                secret.HasKey(x => x.Id);

                secret.Property(x => x.Id).HasColumnName(dbContext.ConvertName(nameof(ClientSecret.Id)));
                secret.Property(x => x.ClientId).HasColumnName(dbContext.ConvertName(nameof(ClientSecret.ClientId)));
                secret.Property(x => x.Expiration).HasColumnName(dbContext.ConvertName(nameof(ClientSecret.Expiration)));

                secret.Property(x => x.Value).HasColumnName(dbContext.ConvertName(nameof(ClientSecret.Value))).HasMaxLength(2000).IsRequired();
                secret.Property(x => x.Type).HasColumnName(dbContext.ConvertName(nameof(ClientSecret.Type))).HasMaxLength(250);
                secret.Property(x => x.Description).HasColumnName(dbContext.ConvertName(nameof(ClientSecret.Description))).HasMaxLength(2000);
                secret.Property(x => x.Created).HasColumnName(dbContext.ConvertName(nameof(ClientSecret.Created)));
            });

            modelBuilder.Entity<ClientClaim>(claim =>
            {
                claim.ToTable(dbContext.ConvertName(storeOptions.ClientClaim.Name));
                claim.HasKey(x => x.Id);

                claim.Property(x => x.Id).HasColumnName(dbContext.ConvertName(nameof(ClientClaim.Id)));
                claim.Property(x => x.ClientId).HasColumnName(dbContext.ConvertName(nameof(ClientClaim.ClientId)));
                claim.Property(x => x.Type).HasColumnName(dbContext.ConvertName(nameof(ClientClaim.Type))).HasMaxLength(250).IsRequired();
                claim.Property(x => x.Value).HasColumnName(dbContext.ConvertName(nameof(ClientClaim.Value))).HasMaxLength(250).IsRequired();
            });

            modelBuilder.Entity<ClientIdPRestriction>(idPRestriction =>
            {
                idPRestriction.ToTable(dbContext.ConvertName(storeOptions.ClientIdPRestriction.Name));
                idPRestriction.HasKey(x => x.Id);

                idPRestriction.Property(x => x.Id).HasColumnName(dbContext.ConvertName(nameof(ClientIdPRestriction.Id)));
                idPRestriction.Property(x => x.ClientId).HasColumnName(dbContext.ConvertName(nameof(ClientIdPRestriction.ClientId)));
                idPRestriction.Property(x => x.Provider).HasColumnName(dbContext.ConvertName(nameof(ClientIdPRestriction.Provider))).HasMaxLength(200).IsRequired();
            });

            modelBuilder.Entity<ClientCorsOrigin>(corsOrigin =>
            {
                corsOrigin.ToTable(dbContext.ConvertName(storeOptions.ClientCorsOrigin.Name));
                corsOrigin.HasKey(x => x.Id);

                corsOrigin.Property(x => x.Id).HasColumnName(dbContext.ConvertName(nameof(ClientCorsOrigin.Id)));
                corsOrigin.Property(x => x.ClientId).HasColumnName(dbContext.ConvertName(nameof(ClientCorsOrigin.ClientId)));
                corsOrigin.Property(x => x.Origin).HasColumnName(dbContext.ConvertName(nameof(ClientCorsOrigin.Origin))).HasMaxLength(150).IsRequired();
            });

            modelBuilder.Entity<ClientProperty>(property =>
            {
                property.ToTable(dbContext.ConvertName(storeOptions.ClientProperty.Name));
                property.HasKey(x => x.Id);

                property.Property(x => x.Id).HasColumnName(dbContext.ConvertName(nameof(ClientProperty.Id)));
                property.Property(x => x.ClientId).HasColumnName(dbContext.ConvertName(nameof(ClientProperty.ClientId)));
                property.Property(x => x.Key).HasColumnName(dbContext.ConvertName(nameof(ClientProperty.Key))).HasMaxLength(250).IsRequired();
                property.Property(x => x.Value).HasColumnName(dbContext.ConvertName(nameof(ClientProperty.Value))).HasMaxLength(2000).IsRequired();
            });
        }
    }
}