namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart
{
    public class CreateCartRequest
    {
        public string Branch { get; set; } = string.Empty;

        public Guid UserId { get; set; }

        public List<ProductResquest> Products { get; set; } = new List<ProductResquest>();
    }

    public class ProductResquest
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }
    }
}
