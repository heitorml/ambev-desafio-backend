using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCreatedEvent
    {
        public Cart cart { get; }

        public SaleCreatedEvent(Cart cart)
        {
            cart = cart;
        }
    }
}