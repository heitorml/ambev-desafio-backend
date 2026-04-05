using Ambev.DeveloperEvaluation.Common.Pagination;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCart
{
    public class ListCartCommand : IRequest<PagedResult<ListCartResult>>
    {
        public Guid Id { get; }

        public Guid UserId { get; set; }

        public int PageSize { get; set; } = 10;

        public int PageNumber { get; set; } = 1;

        public ListCartCommand() { }

        public ListCartCommand(Guid id, Guid userId )
        {
            Id = id;
            UserId = userId;
        }

    }
}
