using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DAL;

public static class RelationMapping
{
    public static void MappRelationships(this ModelBuilder builder)
    {
        builder.Entity<Product>().
                   HasOne(i => i._Category)
                   .WithMany(i => i._Product)
                   .HasForeignKey(i => i.Category_Id)
                   .IsRequired().OnDelete(DeleteBehavior.Cascade);
    }
}
