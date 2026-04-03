using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts
{
    public class ListProductsHandler : IRequestHandler<ListProductsCommand, PagedResult<ListProductsResult>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ListProductsHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ListProductsResult>> Handle(ListProductsCommand request, CancellationToken cancellationToken)
        {
            var validator = new ListProductsCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var products = _productRepository.GetAllAsync(cancellationToken);

            if (request.Id != Guid.Empty)
                products = products.Where(p => p != null && p.Id == request.Id);

            if (!string.IsNullOrWhiteSpace(request.ProductName))
                products = products.Where(p => p != null && p.ProductName.Contains(request.ProductName));

            var totalItems = await products.CountAsync(cancellationToken);

            var paginatedProducts = await products
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<ListProductsResult>
            {
                Items = _mapper.Map<List<ListProductsResult>>(paginatedProducts),
                TotalCount = totalItems,
                CurrentPage = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
