using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    public class ListSalesResult
    {
        public Guid SalesId { get; set; }

        public decimal TotalSaleAmount { get; set; }

        public string Branch { get; set; } = string.Empty;

        public long Quantity { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }

        public SalesStatus Status { get; set; }

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
