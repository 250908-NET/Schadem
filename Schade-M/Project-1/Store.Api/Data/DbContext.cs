using Microsoft.EntityFrameworkCore;
using Store.Models;

namespace Store.Data;

public class StoreDbContext : DbContext
{
    // Fields
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    
    // Methods
    // You still need a constructor! -- constructor made

    public StoreDbContext( DbContextOptions<StoreDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>()
        .HasMany(o => o.Products)
        .WithMany(p => p.Orders);
    }
}

