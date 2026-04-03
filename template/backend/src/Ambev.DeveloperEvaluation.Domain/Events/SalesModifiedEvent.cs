using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SalesModifiedEvent
    {
        public Sale Sale { get; }

        public SalesModifiedEvent(Sale sale)
        {
            Sale = sale;
        }
    }
}