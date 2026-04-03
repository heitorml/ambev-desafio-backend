using Ambev.DeveloperEvaluation.Common.Pagination;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts
{
    public class ListProductsCommand : IRequest<PagedResult<ListProductsResult>>
    {
        public Guid Id { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public int PageSize { get; set; } = 10;

        public int PageNumber { get; set; } = 1;

        public ListProductsCommand(Guid id, string productName)
        {
            Id = id;
            ProductName = productName;
        }
    }
}
