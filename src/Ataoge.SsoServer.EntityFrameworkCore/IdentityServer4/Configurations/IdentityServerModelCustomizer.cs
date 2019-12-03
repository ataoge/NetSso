using Ataoge.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace IdentityServer4.EntityFramework.Configurations
{
    public class IdentityServerModelCustomizer : RelationalModelCustomizer
    {
        public IdentityServerModelCustomizer(ModelCustomizerDependencies dependencies)
            : base(dependencies)
        {
            
        }
     
        public override void Customize(ModelBuilder modelBuilder, DbContext context)
        {
            if  (context is AtaogeDbContext)
            {
                AtaogeDbContext dbContext = context as AtaogeDbContext;
                new OperationalContextModelBuilder().BuildModel(modelBuilder, dbContext);
                new ResourcesContextModelBuilder().BuildModel(modelBuilder, dbContext);
                new ClientsContextModelBuilder().BuildModel(modelBuilder, dbContext);
            }
            base.Customize(modelBuilder, context);
        }

       
    }
}