using System;
using Ataoge.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microsoft.AspNetCore.Identity.EntityFrameworkCore.Configurations
{
    public class IdentityUserTokenConfiguration<TEntity, TKey> : AtaogeEntityTypeConfiguration<TEntity>
        where TEntity : IdentityUserToken<TKey>
        where TKey : IEquatable<TKey>
    {
        public IdentityUserTokenConfiguration(IAtaogeDbContext dbContext) : base(dbContext)
        {
        }

        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(l => new { l.UserId, l.LoginProvider, l.Name });

            builder.Property(ut => ut.UserId).HasColumnName(base.ConvertName(nameof(IdentityUserToken<TKey>.UserId)));
            builder.Property(ut => ut.LoginProvider).HasColumnName(base.ConvertName(nameof(IdentityUserToken<TKey>.LoginProvider)));
            builder.Property(ut => ut.Name).HasColumnName(base.ConvertName(nameof(IdentityUserToken<TKey>.Name)));
            builder.Property(ut => ut.Value).HasColumnName(base.ConvertName(nameof(IdentityUserToken<TKey>.Value)));
            
            builder.ToTable(base.ConvertName("AspNetUserTokens"));
        }
    }
}