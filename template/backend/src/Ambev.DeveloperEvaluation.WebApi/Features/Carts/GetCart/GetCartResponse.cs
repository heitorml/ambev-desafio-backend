using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart
{
    public class GetCartResponse
    {
        public Guid CartsId { get; set; }

        public decimal TotalCartAmount { get; set; }

        public string Branch { get; set; } = string.Empty;

        public long Quantity { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }

        public CartStatus Status { get; set; }

        public List<CartProductResponse> Products { get; set; } = new List<CartProductResponse>();
    }

    public class CartProductResponse
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? Discount { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
    }
}
