namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequest
    {
        public string Branch { get; set; } = string.Empty;

        public int UserId { get; set; }

        public List<ProductResquest> Products { get; set; } = new List<ProductResquest>();
    }

    public class ProductResquest
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public string ProductName { get; set; } = string.Empty;
    }
}
