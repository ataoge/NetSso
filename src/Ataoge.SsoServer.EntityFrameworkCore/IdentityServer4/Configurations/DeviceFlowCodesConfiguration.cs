using Ataoge.EntityFrameworkCore;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4.EntityFramework.Configurations
{
    public class DeviceFlowCodesConfiguration : AtaogeEntityTypeConfiguration<DeviceFlowCodes>
    {
        public DeviceFlowCodesConfiguration(IAtaogeDbContext dbContext, OperationalStoreOptions options) : base(dbContext)
        {
            _options = options;
        }

        private OperationalStoreOptions _options;

        public override void Configure(EntityTypeBuilder<DeviceFlowCodes> builder)
        {
            builder.ToTable(base.ConvertName(_options.DeviceFlowCodes.Name));

            builder.Property(x => x.UserCode).HasColumnName(base.ConvertName(nameof(DeviceFlowCodes.UserCode))).HasMaxLength(200).ValueGeneratedNever();
            builder.Property(x => x.DeviceCode).HasColumnName(base.ConvertName(nameof(DeviceFlowCodes.DeviceCode))).HasMaxLength(50).IsRequired();
            builder.Property(x => x.SubjectId).HasColumnName(base.ConvertName(nameof(DeviceFlowCodes.SubjectId))).HasMaxLength(200);
            builder.Property(x => x.ClientId).HasColumnName(base.ConvertName(nameof(DeviceFlowCodes.ClientId))).HasMaxLength(200).IsRequired();
            builder.Property(x => x.CreationTime).HasColumnName(base.ConvertName(nameof(DeviceFlowCodes.CreationTime))).IsRequired();
            builder.Property(x => x.Expiration).HasColumnName(base.ConvertName(nameof(DeviceFlowCodes.Expiration)));
            // 50000 chosen to be explicit to allow enough size to avoid truncation, yet stay beneath the MySql row size limit of ~65K
            // apparently anything over 4K converts to nvarchar(max) on SqlServer
            builder.Property(x => x.Data).HasColumnName(base.ConvertName(nameof(DeviceFlowCodes.Data))).HasMaxLength(50000).IsRequired();

            builder.HasKey(x => x.UserCode);

            builder.HasIndex(x => new { x.SubjectId, x.ClientId, x.DeviceCode });
        }
    }
}
