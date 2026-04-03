using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class cartsModifiedEvent
    {
        public Cart cart { get; }

        public cartsModifiedEvent(Cart cart)
        {
            cart = cart;
        }
    }
}