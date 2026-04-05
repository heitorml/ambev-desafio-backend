namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts
{
    public class ListProductsResponse
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
    }
}
