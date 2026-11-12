using InventoryBuddy.Api.Dtos;
using InventoryBuddy.Api.Models;
using InventoryBuddy.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryBuddy.Api.Controllers;

[ApiController]
[Route("api/[controller]")] 
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var products = await _productService.GetProductsAsync(pageNumber, pageSize);
        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(ProductDto productDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var product = await _productService.CreateProductAsync(productDto);
        return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
    }

    [HttpPost("{id}/purchase")]
    public async Task<IActionResult> PurchaseProduct(int id, PurchaseDto purchaseDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var success = await _productService.PurchaseProductAsync(id, purchaseDto.Quantity);
        if (!success) return BadRequest("Insufficient stock or concurrency conflict.");
        
        return Ok("Purchase successful.");
    }
    
}