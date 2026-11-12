using InventoryBuddy.Api.Dtos;
using InventoryBuddy.Api.Models;

namespace InventoryBuddy.Api.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetProductsAsync(int pageNumber, int pageSize);
    Task<Product?> GetProductByIdAsync(int id);
    Task<Product> CreateProductAsync(ProductDto productDto);
    Task<bool> PurchaseProductAsync(int id, int quantity);
    Task<bool> DeleteProductAsync(int id);
}