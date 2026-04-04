using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleModifiedEvent
    {
        public Cart cart { get; }

        public SaleModifiedEvent(Cart cart)
        {
            cart = cart;
        }
    }
}