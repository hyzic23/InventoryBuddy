using System.ComponentModel.DataAnnotations;

namespace InventoryBuddy.Api.Dtos;

public class PurchaseDto
{
    [Range(1, int.MaxValue, ErrorMessage = "Purchase quantity must be at least 1.")]
    public int Quantity { get; set; }
}