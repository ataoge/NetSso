using System;
using Ataoge.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microsoft.AspNetCore.Identity.EntityFrameworkCore.Configurations
{
    public class IdentityRoleClaimConfiguration<TEntity, TKey> : AtaogeEntityTypeConfiguration<TEntity>
        where TEntity : IdentityRoleClaim<TKey>
        where TKey : IEquatable<TKey>
    {
        public IdentityRoleClaimConfiguration(IAtaogeDbContext dbContext) : base(dbContext)
        {
        }

        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(uc => uc.Id);
            builder.Property(uc => uc.Id).HasColumnName(base.ConvertName(nameof(IdentityRoleClaim<TKey>.Id)));
            builder.Property(uc => uc.RoleId).HasColumnName(base.ConvertName(nameof(IdentityRoleClaim<TKey>.RoleId)));
            builder.Property(uc => uc.ClaimType).HasColumnName(base.ConvertName(nameof(IdentityRoleClaim<TKey>.ClaimType)));
            builder.Property(uc => uc.ClaimValue).HasColumnName(base.ConvertName(nameof(IdentityRoleClaim<TKey>.ClaimValue)));
            builder.ToTable(base.ConvertName("AspNetRoleClaims"));
        }
    }

}