using InventoryBuddy.Api.Data;
using InventoryBuddy.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryBuddy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        return await _context.Categories.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory(Category category)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return Ok(category);
    }
}