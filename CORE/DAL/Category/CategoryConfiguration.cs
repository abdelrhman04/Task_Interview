using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DAL;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(i => i.Name).IsRequired();
    }
}

