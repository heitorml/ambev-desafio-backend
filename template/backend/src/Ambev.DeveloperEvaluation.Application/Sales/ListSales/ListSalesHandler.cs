using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    public class ListSalesHandler : IRequestHandler<ListSalesCommand, PagedResult<ListSalesResult>>
    {
        private readonly ISaleRepository _salesRepository;
        private readonly IMapper _mapper;

        public ListSalesHandler(ISaleRepository salesRepository, IMapper mapper)
        {
            _salesRepository = salesRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ListSalesResult>> Handle(ListSalesCommand request, CancellationToken cancellationToken)
        {
            var sales =  _salesRepository.GetAll(cancellationToken);               
            
            if (request.Id != Guid.Empty)
                sales = sales.Where(s => s.Id == request.Id);

            if (request.UserId != Guid.Empty)
                sales = sales.Where(s => s.UserId == request.UserId);

            var totalItens = sales.Count();

            var listSales = _mapper.Map<List<ListSalesResult>?>(sales);

            var itensPagina = await sales
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PagedResult<ListSalesResult>
            {
                Items = listSales,
                TotalCount = totalItens,
                CurrentPage = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
