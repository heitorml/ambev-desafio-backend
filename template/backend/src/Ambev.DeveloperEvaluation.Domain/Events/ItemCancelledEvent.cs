using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class ItemCancelledEvent
    {
        public Product Product { get; }

        public ItemCancelledEvent(Product product)
        {
            Product = product;
        }
    }
}