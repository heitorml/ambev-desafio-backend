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

        public Guid UserId { get; set; }

        public CartStatus Status { get; set; }

        public List<ProductResult> Products { get; set; } = new List<ProductResult>();
    }

    public class ProductResult
    {
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal? Discount { get; set; }

        public string ProductName { get; set; } = string.Empty;
    }
}
