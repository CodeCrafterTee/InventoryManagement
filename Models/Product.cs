// Models/Product.cs
namespace InventoryManagement.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public bool IsLowStock(int threshold = 5) => Quantity <= threshold;
}