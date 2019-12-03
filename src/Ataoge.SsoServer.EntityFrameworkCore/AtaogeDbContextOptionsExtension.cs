using System.Collections.Generic;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Ataoge.EntityFrameworkCore
{
    public class AtaogeDbContextOptionsExtension : IDbContextOptionsExtension
    {

        public AtaogeDbContextOptionsExtension()
        {
            //OperationalStoreOptions = new OperationalStoreOptions();
            //ConfigurationStoreOptions = new ConfigurationStoreOptions();
        }

        protected AtaogeDbContextOptionsExtension(AtaogeDbContextOptionsExtension copyFrom)
        {
            this.UseInnerModel = copyFrom.UseInnerModel;
            //this.OperationalStoreOptions = copyFrom.OperationalStoreOptions;
            //this.ConfigurationStoreOptions = copyFrom.ConfigurationStoreOptions;
        }

        public virtual AtaogeDbContextOptionsExtension Clone()
        {
            return new AtaogeDbContextOptionsExtension(this);
        }

        private DbContextOptionsExtensionInfo _info;

        public virtual DbContextOptionsExtensionInfo Info
            => _info ??= new ExtensionInfo(this);

        public bool UseInnerModel {get; set;}

        public virtual void ApplyServices(IServiceCollection services)
        {
           //services.AddSingleton<AtaogeDbContextOptionsExtension>(this);
           //services.AddSingleton<OperationalStoreOptions>(this.OperationalStoreOptions);
           //services.AddSingleton<ConfigurationStoreOptions>(this.ConfigurationStoreOptions);
        }

        public virtual  void Validate(IDbContextOptions options)
        {
            
        }

        public virtual  AtaogeDbContextOptionsExtension WithInnerModel(bool userInnerModel)
        {
            var clone = Clone();
            clone.UseInnerModel = userInnerModel;
            return clone;
        }

       // public OperationalStoreOptions OperationalStoreOptions {get; set;}

       /* public virtual  AtaogeDbContextOptionsExtension WithOperationalStoreOptions(OperationalStoreOptions operationalStoreOptions)
        {
            var clone = Clone();
            clone.OperationalStoreOptions = operationalStoreOptions;
            return clone;
        } */

        public bool UserOperationalStore {get; set;}


        public bool UseConfigurationStore {get; set;}

        /*
        public ConfigurationStoreOptions ConfigurationStoreOptions {get; set;}

        public virtual  AtaogeDbContextOptionsExtension WithConfigurationStoreOptions(ConfigurationStoreOptions configurationStoreOptions)
        {
            var clone = Clone();
            clone.ConfigurationStoreOptions = configurationStoreOptions;
            return clone;
        }
        */

        private sealed class ExtensionInfo : DbContextOptionsExtensionInfo
        {
            public ExtensionInfo(IDbContextOptionsExtension extension)
                : base(extension)
            {
            }

            public override bool IsDatabaseProvider => false;

            public override string LogFragment => "";

            public override long GetServiceProviderHashCode() => 0;

            public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
            {
            }
        }


    }
    
}