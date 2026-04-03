using Ambev.DeveloperEvaluation.Common.Pagination;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    public class ListSalesCommand : IRequest<PagedResult<ListSalesResult>>
    {
        public Guid Id { get; }

        public Guid UserId { get; set; }

        public int PageSize { get; set; } = 10;

        public int PageNumber { get; set; } = 1;

        public ListSalesCommand(Guid id, Guid userId )
        {
            Id = id;
            UserId = userId;
        }

    }
}
