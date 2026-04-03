using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly IMapper _mapper;
        private readonly ISaleRepository _saleRepository;

        public UpdateSaleHandler(IMapper mapper, ISaleRepository saleRepository)
        {
            _mapper = mapper;
            _saleRepository = saleRepository;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {

            var sale = await _saleRepository.GetByIdAsync(request.SalesId, cancellationToken);
            if (sale == null)
                return new UpdateSaleResult
                {
                    SaleId = request.SalesId,
                    Success = false
                };


            var validator = new UpdateSaleValidador();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var saleUpadate = _mapper.Map<Sale>(request);

            var rulesDiscount = RulesDiscountConfiguration.GetRules();
            var discountService = new DiscountService(rulesDiscount);

            discountService.ApplyDiscounts(saleUpadate.Products.ToList());

            var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
            var result = _mapper.Map<UpdateSaleResult>(createdSale);
            return result;
        }
    }
}
