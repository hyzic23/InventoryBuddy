using System.ComponentModel.DataAnnotations;

namespace InventoryBuddy.Api.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } =  string.Empty;
    public decimal Price { get; set; }
    public int StockQty { get; set; }
    // Concurrency token
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
}