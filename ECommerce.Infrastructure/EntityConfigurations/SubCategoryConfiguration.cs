using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.EntityConfigurations
{
    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.Property(sc => sc.Name).IsRequired().HasMaxLength(50).HasDefaultValue("");
            builder.Property(sc => sc.Description).IsRequired().HasMaxLength(250).HasDefaultValue("");
        }
    }
}
