using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistance.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(nameof(Product), tb =>
        {
            tb.HasComment("Products catalog.");
            tb.HasCheckConstraint("CK_Product_Price_NonNegative", "[Price] >= 0");
            tb.HasCheckConstraint("CK_Product_Quantity_NonNegative", "[Quantity] >= 0");
        });

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).ValueGeneratedOnAdd();

        builder.Property(p => p.Name).IsRequired().HasMaxLength(200);

        builder.Property(p => p.Description).HasMaxLength(2000).IsUnicode(true);

        builder.Property(p => p.Price).IsRequired().HasPrecision(18, 2); 

        builder.Property(p => p.Quantity).IsRequired().HasDefaultValue(0);

        builder.HasIndex(p => p.Name).IsUnique().HasDatabaseName("UX_Product_Name");
    }
}
