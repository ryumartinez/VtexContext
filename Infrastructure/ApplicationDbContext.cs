using Infrastructure.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Festejo> Festejos { get; set; }
    
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
