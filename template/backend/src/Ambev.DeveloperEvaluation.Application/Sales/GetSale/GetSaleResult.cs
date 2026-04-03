using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleResult
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
}
