using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sales
{
    public class SaleModifiedEvent : BaseEventProperties
    {
        public decimal TotalCartAmount { get; set; } = 0M;

        public string Branch { get; set; } = string.Empty;

        public long Quantity { get; set; }

        public DateTime CreatedAt { get; set; }

        public CartStatus Status { get; set; }

        public Guid CartId { get; set; }
    }
}