using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class cartsCancelledEvent
    {
        public Cart cart { get; }

        public cartsCancelledEvent(Cart cart)
        {
            cart = cart;
        }       
    }
}