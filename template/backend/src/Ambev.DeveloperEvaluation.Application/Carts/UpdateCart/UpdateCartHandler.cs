using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using AutoMapper;
using FluentValidation;
using MassTransit;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, UpdateCartResult>
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBus _bus;

        public UpdateCartHandler(
            IMapper mapper,
            ICartRepository cartRepository, 
            IProductRepository productRepository, 
            IBus bus)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _bus = bus;
        }

        public async Task<UpdateCartResult> Handle(
            UpdateCartCommand request, 
            CancellationToken cancellationToken)
        {

            var cart = await _cartRepository.GetByIdAsync(request.Id, cancellationToken);
            if (cart == null)
                return new UpdateCartResult{ Id = request.Id, Success = false};

            var validator = new UpdateCartValidador();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var productIds = request.Products.Select(p => p.Id).ToList();
            var products = await _productRepository.GetByIdsAsync(productIds, cancellationToken);
            
            _mapper.Map(request, cart, opt => opt.Items["Products"] = products);

            var rulesDiscount = RulesDiscountConfiguration.GetRules();
            var discountService = new DiscountService(rulesDiscount);

            discountService.ApplyDiscounts(cart.Products.ToList());
            cart.Calculate();

            await _cartRepository.UpdateAsync(cart, cancellationToken);
            var result = _mapper.Map<UpdateCartResult>(cart);
            result.Success = true;

            return result;
        }
    }
}
