using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class cartsCreatedEvent
    {
        public Cart cart { get; }

        public cartsCreatedEvent(Cart cart)
        {
            cart = cart;
        }
    }
}