using Microsoft.EntityFrameworkCore;

namespace CloudNativeEcommerce.ApiService;

public class EcommerceDbContext : DbContext
{
    public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Laptop", Price = 999.99m, Description = "High-performance laptop", Stock = 10 },
            new Product { Id = 2, Name = "Mouse", Price = 29.99m, Description = "Wireless mouse", Stock = 50 },
            new Product { Id = 3, Name = "Keyboard", Price = 79.99m, Description = "Mechanical keyboard", Stock = 25 }
        );
    }
}
