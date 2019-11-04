using System;
using Ataoge.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microsoft.AspNetCore.Identity.EntityFrameworkCore.Configurations
{
    public class IdentityUserLoginConfiguration<TEntity, TKey> : AtaogeEntityTypeConfiguration<TEntity>
        where TEntity : IdentityUserLogin<TKey>
        where TKey : IEquatable<TKey>
    {
        public IdentityUserLoginConfiguration(IAtaogeDbContext dbContext) : base(dbContext)
        {
        }

        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(l => new { l.LoginProvider, l.ProviderKey });
           
            builder.Property(ul => ul.UserId).HasColumnName(base.ConvertName(nameof(IdentityUserLogin<TKey>.UserId)));
            builder.Property(ul => ul.LoginProvider).HasColumnName(base.ConvertName(nameof(IdentityUserLogin<TKey>.LoginProvider)));
            builder.Property(ul => ul.ProviderKey).HasColumnName(base.ConvertName(nameof(IdentityUserLogin<TKey>.ProviderKey)));
            builder.Property(ul => ul.ProviderDisplayName).HasColumnName(base.ConvertName(nameof(IdentityUserLogin<TKey>.ProviderDisplayName)));
            builder.ToTable(base.ConvertName("AspNetUserLogins"));
        }
    }
}