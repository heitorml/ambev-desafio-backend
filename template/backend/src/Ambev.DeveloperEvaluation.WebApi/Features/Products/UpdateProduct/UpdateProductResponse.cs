namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct
{
    public class UpdateProductResponse
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
    }
}
