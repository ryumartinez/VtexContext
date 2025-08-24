using Infrastructure.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Festejo> Festejos { get; set; }
    
    public DbSet<Agasajado> Agasajados { get; set; }
    
    public DbSet<Product>  Products { get; set; }
    
    public DbSet<ProductCollection>  ProductCollections { get; set; }
    
    public DbSet<OwnershipRequest> OwnershipRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Optional: Fluent API for many-to-many between ApplicationUser and Festejo
        builder.Entity<ApplicationUser>()
            .HasMany(u => u.Festejos)
            .WithMany(f => f.Owners)
            .UsingEntity(j => j.ToTable("UserFestejos")); // Optional: name the join table
    }
}
