using DispatchLine.Domain.Constants;
using DispatchLine.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DispatchLine.Infrastructure.Configurations;

public class AppRoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(r => r.Name).IsRequired().HasMaxLength(ConfigurationConsts.MaxTitleLength);
    }
}