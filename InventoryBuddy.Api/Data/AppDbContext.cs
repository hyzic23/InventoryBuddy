using InventoryBuddy.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryBuddy.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Adding a concurrency token to demonstrate realistic EF Core setup
        modelBuilder.Entity<Product>()
            .Property(p => p.RowVersion)
            .IsRowVersion();
    }
}