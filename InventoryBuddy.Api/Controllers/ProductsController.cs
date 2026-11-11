using InventoryBuddy.Api.Data;
using InventoryBuddy.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryBuddy.Api.Controllers;

[ApiController]
[Route("api/[controller]")] 
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public ProductsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await _dbContext.Products.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Product>> AddProduct(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProducts), new { id = product.Id}, product);
    }
    
    [HttpPost("{id}/purchase")]
    public async Task<IActionResult> PurchaseProduct(int id, int quantity)
    {
        var product = await _dbContext.Products.FindAsync(id);
        if (product == null) return NotFound();
        
        product.StockQty -= quantity; 
        
        await _dbContext.SaveChangesAsync();
        return Ok(product);
    }
    
}