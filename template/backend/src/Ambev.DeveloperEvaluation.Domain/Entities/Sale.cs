using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{
    public decimal TotalSaleAmount { get; set; }
 
    public string Branch { get; set; } = string.Empty;
    
    public long Quantity { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public Guid UserId { get; set; }

    public User User { get; set; } = new User();

    public SalesStatus Status { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
    
    public Sale()
    {
        CreatedAt = DateTime.UtcNow;
    }
}
