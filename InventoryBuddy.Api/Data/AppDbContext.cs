using InventoryBuddy.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryBuddy.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Product> Products { get; set; }
}