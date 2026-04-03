using Ambev.DeveloperEvaluation.Application.Carts.ListCart;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart
{
    public class GetCartResult
    {
        public Guid cartsId { get; set; }

        public decimal TotalCartAmount { get; set; }

        public string Branch { get; set; } = string.Empty;

        public long Quantity { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }

        public CartStatus Status { get; set; }

        public List<ProductResult> Products { get; set; } = new List<ProductResult>();
    }
}
