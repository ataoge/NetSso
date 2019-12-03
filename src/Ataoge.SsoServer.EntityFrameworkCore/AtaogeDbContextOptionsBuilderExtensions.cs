using System;
using Ataoge.EntityFrameworkCore;
using IdentityServer4.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Microsoft.EntityFrameworkCore
{
    public static class AtaogeDbContextOptionsBuilderExtension
    {
        public static DbContextOptionsBuilder UseIdentityServer(this DbContextOptionsBuilder optionsBuilder, Action<AtaogeDbContextOptionsExtension> options)
        {
            var infrastructure = ((IDbContextOptionsBuilderInfrastructure)optionsBuilder);
            var contextOptions = optionsBuilder.Options;
            
            var ataogeOptionsExtension = optionsBuilder.Options.FindExtension<AtaogeDbContextOptionsExtension>() ?? new AtaogeDbContextOptionsExtension();

            
            infrastructure.AddOrUpdateExtension(ataogeOptionsExtension);
            
            
            if (options != null)
            {
                options(ataogeOptionsExtension);
            }

            if (ataogeOptionsExtension.UseInnerModel)
            {
                
                optionsBuilder.ReplaceService<IModelCustomizer, IdentityServerModelCustomizer>();
            }
            return optionsBuilder;
        }
    }
}