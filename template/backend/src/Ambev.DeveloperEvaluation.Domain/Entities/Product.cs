using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Product : BaseEntity
{
    public decimal UnitPrice { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public Product()
    {
        CreatedAt = DateTime.UtcNow;
    }
}
