using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Product : BaseEntity
{
    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice { get; set; }

    public decimal? Discount { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public Product()
    {
        CreatedAt = DateTime.UtcNow;
    }
}
