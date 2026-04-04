using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class CartProducts : BaseEntity
    {
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal? Discount { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public Guid CartId { get; set; }

        public Guid ProductId { get; set; }

        public Cart Cart { get; set; }

        public Product Product { get; set; }

        public CartProducts()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public decimal CalculateTotalPrice() {

            return (Quantity * UnitPrice);
        }
    }
}
