using System;
using Ataoge.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microsoft.AspNetCore.Identity.EntityFrameworkCore.Configurations
{
    public class IdentityUserRoleConfiguration<TEntity, TKey> : AtaogeEntityTypeConfiguration<TEntity>
        where TEntity : IdentityUserRole<TKey>
        where TKey : IEquatable<TKey>
    {
        public IdentityUserRoleConfiguration(IAtaogeDbContext dbContext) : base(dbContext)
        {
        }

        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(l => new { l.UserId, l.RoleId });
           
            builder.Property(ur => ur.UserId).HasColumnName(base.ConvertName(nameof(IdentityUserRole<TKey>.UserId)));
            builder.Property(ur => ur.RoleId).HasColumnName(base.ConvertName(nameof(IdentityUserRole<TKey>.RoleId)));
       
            builder.ToTable(base.ConvertName("AspNetUserRoles"));
        }
    }
}