using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCancelledEvent
    {
        public Cart cart { get; }

        public SaleCancelledEvent(Cart cart)
        {
            cart = cart;
        }       
    }
}