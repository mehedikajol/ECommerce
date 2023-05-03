using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.EntityConfigurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(250).HasDefaultValue("");
        builder.Property(p => p.Description).HasMaxLength(1000).HasDefaultValue("");
        builder.Property(p => p.SKU).IsRequired().HasMaxLength(15).HasDefaultValue("");
        builder.Property(p => p.Price).IsRequired();
    }
}
