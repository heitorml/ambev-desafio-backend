using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Cart : BaseEntity
{
    public decimal TotalCartAmount { get; set; } = 0M;
 
    public string Branch { get; set; } = string.Empty;
    
    public long Quantity { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public Guid UserId { get; set; }

    public User User { get; set; }

    public CartStatus Status { get; set; }

    public ICollection<CartProducts> Products { get; set; } = new List<CartProducts>();
    
    public Cart()
    {
        CreatedAt = DateTime.UtcNow;
    }

    public void Calculate() 
    {
        TotalCartAmount  = Products.Sum(o => o.TotalPrice);
        Quantity = Products.Sum(o => o.Quantity);
    }
}
