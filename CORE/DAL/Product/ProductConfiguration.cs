using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CORE.DAL;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(i => i.Name).IsRequired();
        builder.Property(i => i.Category_Id).IsRequired();
    }
}

