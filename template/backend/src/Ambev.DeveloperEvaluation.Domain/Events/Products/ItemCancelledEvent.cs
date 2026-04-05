namespace Ambev.DeveloperEvaluation.Domain.Events.Products
{
    public class ItemCancelledEvent : BaseEventProperties
    {
        public Guid ProductId { get; set; }
    }
}