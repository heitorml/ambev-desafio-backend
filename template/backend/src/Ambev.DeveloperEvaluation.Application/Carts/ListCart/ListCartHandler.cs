using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCart
{
    public class ListCartHandler : IRequestHandler<ListCartCommand, PagedResult<ListCartResult>>
    {
        private readonly ICartRepository _cartsRepository;
        private readonly IMapper _mapper;

        public ListCartHandler(ICartRepository cartsRepository, IMapper mapper)
        {
            _cartsRepository = cartsRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ListCartResult>> Handle(ListCartCommand request, CancellationToken cancellationToken)
        {
            var carts = _cartsRepository.GetAll(cancellationToken);

            if (request.Id != Guid.Empty)
                carts = carts.Where(s => s.Id == request.Id);

            if (request.UserId != Guid.Empty)
                carts = carts.Where(s => s.UserId == request.UserId);

            var totalItens = carts.Count();

            var itensPagina = await carts
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var ListCart = _mapper.Map<List<ListCartResult>>(itensPagina);

            return new PagedResult<ListCartResult>
            {
                Items = ListCart,
                TotalCount = totalItens,
                CurrentPage = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
