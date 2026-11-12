using InventoryBuddy.Api.Data;
using InventoryBuddy.Api.Dtos;
using InventoryBuddy.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryBuddy.Api.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(int pageNumber, int pageSize)
    {
        return await _context.Products
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<Product> CreateProductAsync(ProductDto productDto)
    {
        var product = new Product
        {
            Name = productDto.Name,
            Price = productDto.Price,
            StockQty = productDto.StockQuantity
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> PurchaseProductAsync(int id, int quantity)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null || product.StockQty < quantity)
            return false;

        product.StockQty -= quantity;
        
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            return false; // Handled concurrency conflict
        }
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}