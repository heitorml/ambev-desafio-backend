namespace Ambev.DeveloperEvaluation.Domain.Events.Sales
{
    public class SaleCancelledEvent : BaseEventProperties
    {

        public Guid CartId { get; set; }
    }
}