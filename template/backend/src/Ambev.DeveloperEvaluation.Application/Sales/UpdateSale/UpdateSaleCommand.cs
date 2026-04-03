using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        public Guid SalesId { get; set; }
        
        public Guid UserId { get; set; }

        public string Branch { get; set; } = string.Empty;

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
