using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.ListCarts
{
    public class ListCartsResponse
    {
        public Guid Id { get; set; }

        public decimal TotalCartAmount { get; set; }

        public string Branch { get; set; } = string.Empty;

        public long Quantity { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }

        public CartStatus Status { get; set; }

        public List<ListProductsResponse> Products { get; set; } = new List<ListProductsResponse>();
    }
}
