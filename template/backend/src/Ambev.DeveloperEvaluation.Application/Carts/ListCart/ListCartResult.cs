using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCart
{
    public class ListCartResult
    {
        public Guid Id { get; set; }

        public decimal TotalCartAmount { get; set; }

        public string Branch { get; set; } = string.Empty;

        public long Quantity { get; set; }

        public DateTime CreatedAt { get; set; }

        public UserResult User { get; set; }
        
        public Guid UserId { get; set; }

        public CartStatus Status { get; set; }

        public List<CartProductResult> Products { get; set; } = new List<CartProductResult>();
    }

    public class CartProductResult
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal? Discount { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public decimal TotalPrice { get; set; }
    }
}
