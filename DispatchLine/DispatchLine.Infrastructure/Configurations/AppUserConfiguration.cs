using DispatchLine.Domain.Constants;
using DispatchLine.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DispatchLine.Infrastructure.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(ConfigurationConsts.MaxFirstNameLength);
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(ConfigurationConsts.MaxFamilyNameLength);
        builder.Property(u => u.PartyIdentification).IsRequired().HasMaxLength(ConfigurationConsts.MaxPartyIdentificationLength);
        builder.Property(u => u.Address).IsRequired().HasMaxLength(ConfigurationConsts.MaxFullAddressLength);
        builder.Property(u => u.CityName).IsRequired().HasMaxLength(ConfigurationConsts.MaxCityNameLength);
        builder.Property(x => x.EmergencyContactNumber).HasMaxLength(ConfigurationConsts.MaxPhoneNumberLength);
        builder.Property(u => u.RefreshToken).HasMaxLength(ConfigurationConsts.MaxRefreshTokenLength);
        builder.Property(x => x.DepartmentId).IsRequired();
        
        builder.HasOne(u => u.Department)
            .WithMany(d => d.Users)
            .HasForeignKey(u => u.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}