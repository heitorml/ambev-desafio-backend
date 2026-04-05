namespace Ambev.DeveloperEvaluation.Domain.Events.Products
{
    public class ProductModifiedEvent : BaseEventProperties
    {
        public decimal UnitPrice { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public Guid ProductId { get; set; }
    }
}
