using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartHandler : IRequestHandler<CreateCartCommand, CreateCartResult>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateCartHandler(
            ICartRepository cartRepository, 
            IMapper mapper,
            IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<CreateCartResult> Handle(CreateCartCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateCartValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var productIds = command.Products.Select(p => p.Id).ToList();
            var products = await _productRepository.GetByIdsAsync(productIds, cancellationToken);

            var cart = _mapper.Map<Cart>(command, opt => opt.Items["Products"] = products);

            var rulesDiscount = RulesDiscountConfiguration.GetRules();
            var discountService = new DiscountService(rulesDiscount);

            discountService.ApplyDiscounts(cart.Products.ToList());

            cart.Calculate();
            cart.Status = CartStatus.Active;

            var createdcart = await _cartRepository.CreateAsync(cart, cancellationToken);
            var result = _mapper.Map<CreateCartResult>(createdcart);
            return result;
        }

    }
}
