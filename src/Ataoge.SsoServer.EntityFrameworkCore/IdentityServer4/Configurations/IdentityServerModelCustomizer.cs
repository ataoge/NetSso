using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace IdentityServer4.EntityFramework.Configurations
{
    public class IdentityServerModelCustomizer : RelationalModelCustomizer
    {
        public IdentityServerModelCustomizer([NotNullAttribute] ModelCustomizerDependencies dependencies) : base(dependencies)
        {
        }

        /// <summary>
		/// Configures the entities by first getting the collection of <see cref="IEntityTypeConfiguration" /> from the <see cref="Provider" />.
		/// Then proceeds with the default implementation of <see cref="ModelCustomizer" />.
		/// </summary>
		/// <param name="modelBuilder">The builder being used to construct the model.</param>
		/// <param name="dbContext">The context instance that the model is being created for.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="modelBuilder" /> or <paramref name="dbContext" /> is null.</exception>
		public override void Customize(ModelBuilder modelBuilder, DbContext dbContext )
		{
            base.Customize(modelBuilder, dbContext);
        }
    }
}