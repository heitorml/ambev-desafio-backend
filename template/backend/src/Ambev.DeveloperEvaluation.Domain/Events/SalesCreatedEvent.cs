using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SalesCreatedEvent
    {
        public Sale Sale { get; }

        public SalesCreatedEvent(Sale sale)
        {
            Sale = sale;
        }
    }
}