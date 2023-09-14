
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CORE.DAL;

public class MyContext: DbContext
{
    public MyContext(DbContextOptions options) : base(options) { }
   
   
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      
       // modelBuilder.MappRelationships();
        base.OnModelCreating(modelBuilder);
    }

 }
