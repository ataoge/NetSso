using System;
using Ataoge.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microsoft.AspNetCore.Identity.EntityFrameworkCore.Configurations
{
    public class IdentityUserClaimConfiguration<TEntity, TKey> : AtaogeEntityTypeConfiguration<TEntity>
        where TEntity : IdentityUserClaim<TKey>
        where TKey : IEquatable<TKey>
    {
        public IdentityUserClaimConfiguration(IAtaogeDbContext dbContext) : base(dbContext)
        {
        }

        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(uc => uc.Id);
            builder.Property(uc => uc.Id).HasColumnName(base.ConvertName(nameof(IdentityUserClaim<TKey>.Id)));
            builder.Property(uc => uc.UserId).HasColumnName(base.ConvertName(nameof(IdentityUserClaim<TKey>.UserId)));
            builder.Property(uc => uc.ClaimType).HasColumnName(base.ConvertName(nameof(IdentityUserClaim<TKey>.ClaimType)));
            builder.Property(uc => uc.ClaimValue).HasColumnName(base.ConvertName(nameof(IdentityUserClaim<TKey>.ClaimValue)));
            builder.ToTable(base.ConvertName("AspNetUserClaims"));
        }
    }
}