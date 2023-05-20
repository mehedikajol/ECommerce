using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.EntityConfigurations;

internal class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.Property(up => up.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(up => up.LastName).IsRequired().HasMaxLength(50);
        builder.Property(up => up.Email).IsRequired().HasMaxLength(50);
        builder.Property(up => up.Phone).HasMaxLength(50).HasDefaultValue("");
        builder.Property(up => up.Address).HasMaxLength(250).HasDefaultValue("");
        builder.Property(up => up.ProfilePictureUrl).HasMaxLength(200).HasDefaultValue("");
    }
}
