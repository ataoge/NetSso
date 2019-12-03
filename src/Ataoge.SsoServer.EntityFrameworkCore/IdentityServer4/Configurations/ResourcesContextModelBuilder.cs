using System;
using Ataoge.EntityFrameworkCore;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace IdentityServer4.EntityFramework.Configurations
{
    public class ResourcesContextModelBuilder : IEntityFrameworkModelBuilder
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

            modelBuilder.Entity<IdentityResource>(identityResource =>
            {
                identityResource.ToTable(dbContext.ConvertName(storeOptions.IdentityResource.Name)).HasKey(x => x.Id);

                identityResource.Property(x => x.Id).HasColumnName(dbContext.ConvertName(nameof(IdentityResource.Id)));
                identityResource.Property(x => x.Emphasize).HasColumnName(dbContext.ConvertName(nameof(IdentityResource.Emphasize)));
                identityResource.Property(x => x.Enabled).HasColumnName(dbContext.ConvertName(nameof(IdentityResource.Enabled)));
                identityResource.Property(x => x.Required).HasColumnName(dbContext.ConvertName(nameof(IdentityResource.Required)));
                identityResource.Property(x => x.ShowInDiscoveryDocument).HasColumnName(dbContext.ConvertName(nameof(IdentityResource.ShowInDiscoveryDocument)));


                identityResource.Property(x => x.Name).HasColumnName(dbContext.ConvertName(nameof(IdentityResource.Name))).HasMaxLength(200).IsRequired();
                identityResource.Property(x => x.DisplayName).HasColumnName(dbContext.ConvertName(nameof(IdentityResource.DisplayName))).HasMaxLength(200);
                identityResource.Property(x => x.Description).HasColumnName(dbContext.ConvertName(nameof(IdentityResource.Description))).HasMaxLength(1000);

                identityResource.Property(x => x.Created).HasColumnName(dbContext.ConvertName(nameof(IdentityResource.Created)));
                identityResource.Property(x => x.Updated).HasColumnName(dbContext.ConvertName(nameof(IdentityResource.Updated)));
                identityResource.Property(x => x.NonEditable).HasColumnName(dbContext.ConvertName(nameof(IdentityResource.NonEditable)));


                identityResource.HasIndex(x => x.Name).IsUnique();

                identityResource.HasMany(x => x.UserClaims).WithOne(x => x.IdentityResource).HasForeignKey(x => x.IdentityResourceId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                identityResource.HasMany(x => x.Properties).WithOne(x => x.IdentityResource).HasForeignKey(x => x.IdentityResourceId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<IdentityClaim>(claim =>
            {
                claim.ToTable(dbContext.ConvertName(storeOptions.IdentityClaim.Name)).HasKey(x => x.Id);

                claim.Property(x => x.Id).HasColumnName(dbContext.ConvertName(nameof(IdentityClaim.Id)));
                claim.Property(x => x.IdentityResourceId).HasColumnName(dbContext.ConvertName(nameof(IdentityClaim.IdentityResourceId)));
                claim.Property(x => x.Type).HasColumnName(dbContext.ConvertName(nameof(IdentityClaim.Type))).HasMaxLength(200).IsRequired();
            });

            modelBuilder.Entity<IdentityResourceProperty>(property =>
            {
                property.ToTable(storeOptions.IdentityResourceProperty.Name).HasKey(t => t.Id);

                property.Property(x => x.Id).HasColumnName(dbContext.ConvertName(nameof(Property.Id)));
                property.Property(x => x.Key).HasColumnName(dbContext.ConvertName(nameof(Property.Key))).HasMaxLength(250).IsRequired();
                property.Property(x => x.Value).HasColumnName(dbContext.ConvertName(nameof(Property.Value))).HasMaxLength(2000).IsRequired();

                property.Property(x => x.IdentityResourceId).HasColumnName(dbContext.ConvertName(nameof(IdentityResourceProperty.IdentityResourceId)));
            });



            modelBuilder.Entity<ApiResource>(apiResource =>
            {
                apiResource.ToTable(dbContext.ConvertName(storeOptions.ApiResource.Name)).HasKey(x => x.Id);

                apiResource.Property(x => x.Id).HasColumnName(dbContext.ConvertName(nameof(ApiResource.Id)));
                apiResource.Property(x => x.Enabled).HasColumnName(dbContext.ConvertName(nameof(ApiResource.Enabled)));

                apiResource.Property(x => x.Name).HasColumnName(dbContext.ConvertName(nameof(ApiResource.Name))).HasMaxLength(200).IsRequired();
                apiResource.Property(x => x.DisplayName).HasColumnName(dbContext.ConvertName(nameof(ApiResource.DisplayName))).HasMaxLength(200);
                apiResource.Property(x => x.Description).HasColumnName(dbContext.ConvertName(nameof(ApiResource.Description))).HasMaxLength(1000);

                apiResource.Property(x => x.Created).HasColumnName(dbContext.ConvertName(nameof(ApiResource.Created)));
                apiResource.Property(x => x.Updated).HasColumnName(dbContext.ConvertName(nameof(ApiResource.Updated)));
                apiResource.Property(x => x.LastAccessed).HasColumnName(dbContext.ConvertName(nameof(ApiResource.LastAccessed)));
                apiResource.Property(x => x.NonEditable).HasColumnName(dbContext.ConvertName(nameof(ApiResource.NonEditable)));

                apiResource.HasIndex(x => x.Name).IsUnique();

                apiResource.HasMany(x => x.Secrets).WithOne(x => x.ApiResource).HasForeignKey(x => x.ApiResourceId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                apiResource.HasMany(x => x.Scopes).WithOne(x => x.ApiResource).HasForeignKey(x => x.ApiResourceId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                apiResource.HasMany(x => x.UserClaims).WithOne(x => x.ApiResource).HasForeignKey(x => x.ApiResourceId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                apiResource.HasMany(x => x.Properties).WithOne(x => x.ApiResource).HasForeignKey(x => x.ApiResourceId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ApiSecret>(apiSecret =>
            {
                apiSecret.ToTable(dbContext.ConvertName(storeOptions.ApiSecret.Name)).HasKey(x => x.Id);

                apiSecret.Property(x => x.Id).HasColumnName(dbContext.ConvertName(nameof(ApiSecret.Id)));
                apiSecret.Property(x => x.ApiResourceId).HasColumnName(dbContext.ConvertName(nameof(ApiSecret.ApiResourceId)));
                apiSecret.Property(x => x.Expiration).HasColumnName(dbContext.ConvertName(nameof(ApiSecret.Expiration)));

                apiSecret.Property(x => x.Description).HasColumnName(dbContext.ConvertName(nameof(ApiSecret.Description))).HasMaxLength(1000);
                apiSecret.Property(x => x.Value).HasColumnName(dbContext.ConvertName(nameof(ApiSecret.Value))).HasMaxLength(2000);
                apiSecret.Property(x => x.Type).HasColumnName(dbContext.ConvertName(nameof(ApiSecret.Type))).HasMaxLength(250);
                apiSecret.Property(x => x.Created).HasColumnName(dbContext.ConvertName(nameof(ApiSecret.Created)));
            });

            modelBuilder.Entity<ApiResourceClaim>(apiClaim =>
            {
                apiClaim.ToTable(dbContext.ConvertName(storeOptions.ApiClaim.Name)).HasKey(x => x.Id);

                apiClaim.Property(x => x.Id).HasColumnName(dbContext.ConvertName(nameof(ApiResourceClaim.Id)));
                apiClaim.Property(x => x.ApiResourceId).HasColumnName(dbContext.ConvertName(nameof(ApiResourceClaim.ApiResourceId)));

                apiClaim.Property(x => x.Type).HasColumnName(dbContext.ConvertName(nameof(ApiResourceClaim.Type))).HasMaxLength(200).IsRequired();
            });

            modelBuilder.Entity<ApiScope>(apiScope =>
            {
                apiScope.ToTable(dbContext.ConvertName(storeOptions.ApiScope.Name)).HasKey(x => x.Id);

                apiScope.Property(x => x.Id).HasColumnName(dbContext.ConvertName(nameof(ApiScope.Id)));
                apiScope.Property(x => x.ApiResourceId).HasColumnName(dbContext.ConvertName(nameof(ApiScope.ApiResourceId)));
                apiScope.Property(x => x.Emphasize).HasColumnName(dbContext.ConvertName(nameof(ApiScope.Emphasize)));
                apiScope.Property(x => x.Required).HasColumnName(dbContext.ConvertName(nameof(ApiScope.Required)));
                apiScope.Property(x => x.ShowInDiscoveryDocument).HasColumnName(dbContext.ConvertName(nameof(ApiScope.ShowInDiscoveryDocument)));



                apiScope.Property(x => x.Name).HasColumnName(dbContext.ConvertName(nameof(ApiScope.Name))).HasMaxLength(200).IsRequired();
                apiScope.Property(x => x.DisplayName).HasColumnName(dbContext.ConvertName(nameof(ApiScope.DisplayName))).HasMaxLength(200);
                apiScope.Property(x => x.Description).HasColumnName(dbContext.ConvertName(nameof(ApiScope.Description))).HasMaxLength(1000);

                apiScope.HasIndex(x => x.Name).IsUnique();

                apiScope.HasMany(x => x.UserClaims).WithOne(x => x.ApiScope).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ApiScopeClaim>(apiScopeClaim =>
            {
                apiScopeClaim.ToTable(dbContext.ConvertName(storeOptions.ApiScopeClaim.Name)).HasKey(x => x.Id);

                apiScopeClaim.Property(x => x.Id).HasColumnName(dbContext.ConvertName(nameof(ApiScopeClaim.Id)));
                apiScopeClaim.Property(x => x.ApiScopeId).HasColumnName(dbContext.ConvertName(nameof(ApiScopeClaim.ApiScopeId)));

                apiScopeClaim.Property(x => x.Type).HasColumnName(dbContext.ConvertName(nameof(ApiScopeClaim.Type))).HasMaxLength(200).IsRequired();
            });

            modelBuilder.Entity<ApiResourceProperty>(property =>
            {
                property.ToTable(dbContext.ConvertName(storeOptions.ApiResourceProperty.Name)).HasKey(t => t.Id);
                
                property.Property(x => x.Id).HasColumnName(dbContext.ConvertName(nameof(Property.Id)));
                property.Property(x => x.Key).HasColumnName(nameof(Property.Key)).HasMaxLength(250).IsRequired();
                property.Property(x => x.Value).HasColumnName(nameof(Property.Value)).HasMaxLength(2000).IsRequired();

                property.Property(x => x.ApiResourceId).HasColumnName(dbContext.ConvertName(nameof(ApiResourceProperty.ApiResourceId)));
            });

           
        }
    }
}