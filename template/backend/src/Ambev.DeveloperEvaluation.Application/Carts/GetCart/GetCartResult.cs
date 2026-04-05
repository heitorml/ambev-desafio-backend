using Ambev.DeveloperEvaluation.Application.Carts.ListCart;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart
{
    public class GetCartResult
    {
        public Guid Id { get; set; }

        public decimal TotalCartAmount { get; set; }

        public string Branch { get; set; } = string.Empty;

        public long Quantity { get; set; }

        public DateTime CreatedAt { get; set; }

        public UserResult User { get; set; }

        public CartStatus Status { get; set; }

        public List<CartProductResult> Products { get; set; } = new List<CartProductResult>();
    }
}
