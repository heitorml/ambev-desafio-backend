using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart
{
    public class UpdateCartRequest
    {
        public Guid Id { get; set; }
        public string Branch { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public CartStatus Status { get; set; }
        public List<UpdateCartProductRequest> Products { get; set; } = new();
    }

    public class UpdateCartProductRequest
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
