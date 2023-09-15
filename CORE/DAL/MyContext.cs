
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CORE.DAL;

public class MyContext: DbContext
{
    public MyContext(DbContextOptions options) : base(options) { }

    public DbSet< Category> categories { get; set; }
    public DbSet<Product> products { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new CategoryConfiguration().Configure(modelBuilder.Entity<Category>());
        new ProductConfiguration().Configure(modelBuilder.Entity<Product>());
        modelBuilder.MappRelationships();
        base.OnModelCreating(modelBuilder);
    }

 }
