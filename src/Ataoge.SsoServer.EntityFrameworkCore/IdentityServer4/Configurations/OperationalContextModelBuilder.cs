using System;
using Ataoge.EntityFrameworkCore;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace IdentityServer4.EntityFramework.Configurations
{
    public class OperationalContextModelBuilder : IEntityFrameworkModelBuilder
    {
  
        private OperationalStoreOptions GetStoreOptions(AtaogeDbContext dbContext)
        {
            OperationalStoreOptions storeOptions = null;
            try
            {
                storeOptions = dbContext.GetService<OperationalStoreOptions>();
            }
            catch (InvalidOperationException) {

            }
            return storeOptions;
        }

        public void BuildModel(ModelBuilder modelBuilder, AtaogeDbContext dbContext)
        {
            OperationalStoreOptions options = GetStoreOptions(dbContext);
         
            
            if (options != null && options.EnableTokenCleanup)
            {
                modelBuilder.ApplyConfiguration(new PersistedGrantConfiguration(dbContext, options));
                modelBuilder.ApplyConfiguration(new DeviceFlowCodesConfiguration(dbContext, options));
            }
            else
            {
                modelBuilder.Entity<PersistedGrant>(builder => {
                    builder.HasKey(x => x.Key);
                });
                modelBuilder.Entity<DeviceFlowCodes>(builder => {
                    builder.HasKey(x => x.UserCode);
                });
            }
        }
    }
}