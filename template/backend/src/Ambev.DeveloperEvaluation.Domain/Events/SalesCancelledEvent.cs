using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SalesCancelledEvent
    {
        public Sale Sale { get; }

        public SalesCancelledEvent(Sale sale)
        {
            Sale = sale;
        }       
    }
}