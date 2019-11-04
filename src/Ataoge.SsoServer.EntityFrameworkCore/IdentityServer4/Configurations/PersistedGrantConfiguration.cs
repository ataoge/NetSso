using Ataoge.EntityFrameworkCore;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4.EntityFramework.Configurations
{
    public class PersistedGrantConfiguration : AtaogeEntityTypeConfiguration<PersistedGrant>
    {
        public PersistedGrantConfiguration(IAtaogeDbContext dbContext, OperationalStoreOptions options) : base(dbContext)
        {
            _options = options;
        }

        private OperationalStoreOptions _options;

        public override void Configure(EntityTypeBuilder<PersistedGrant> builder)
        {
            builder.ToTable(base.ConvertName(_options.PersistedGrants.Name));

            builder.Property(x => x.Key).HasColumnName(base.ConvertName(nameof(PersistedGrant.Key))).HasMaxLength(200).ValueGeneratedNever();
            builder.Property(x => x.Type).HasColumnName(base.ConvertName(nameof(PersistedGrant.Type))).HasMaxLength(50).IsRequired();
            builder.Property(x => x.SubjectId).HasColumnName(base.ConvertName(nameof(PersistedGrant.SubjectId))).HasMaxLength(200);
            builder.Property(x => x.ClientId).HasColumnName(base.ConvertName(nameof(PersistedGrant.ClientId))).HasMaxLength(200).IsRequired();
            builder.Property(x => x.CreationTime).HasColumnName(base.ConvertName(nameof(PersistedGrant.CreationTime))).IsRequired();
            builder.Property(x => x.Expiration).HasColumnName(base.ConvertName(nameof(PersistedGrant.Expiration)));
            // 50000 chosen to be explicit to allow enough size to avoid truncation, yet stay beneath the MySql row size limit of ~65K
            // apparently anything over 4K converts to nvarchar(max) on SqlServer
            builder.Property(x => x.Data).HasColumnName(base.ConvertName(nameof(PersistedGrant.Data))).HasMaxLength(50000).IsRequired();

            builder.HasKey(x => x.Key);

            builder.HasIndex(x => new { x.SubjectId, x.ClientId, x.Type });
        }
    }
}