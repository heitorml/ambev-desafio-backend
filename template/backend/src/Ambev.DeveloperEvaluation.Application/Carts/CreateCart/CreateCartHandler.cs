using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using AutoMapper;
using FluentValidation;
using MassTransit;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartHandler : IRequestHandler<CreateCartCommand, CreateCartResult>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public CreateCartHandler(
            ICartRepository cartRepository,
            IMapper mapper,
            IProductRepository productRepository,
            IBus bus)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _productRepository = productRepository;
            _bus = bus;
        }

        public async Task<CreateCartResult> Handle(CreateCartCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateCartValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            if (command.Products != null && command.Products.Any())
            {
                command.Products = command.Products
                    .GroupBy(p => p.Id)
                    .Select(g => new CreateCartItemCommand
                    {
                        Id = g.Key,
                        Quantity = g.Sum(x => x.Quantity),
                        UnitPrice = g.First().UnitPrice,
                        ProductName = g.First().ProductName
                    }).ToList();
            }

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

            await SendEvent(cart, cancellationToken);

            return result;
        }

        private async Task SendEvent(Cart cart, CancellationToken cancellationToken)
        {
            await _bus.Publish(new SaleCreatedEvent
            {

                UserId = cart.UserId,
                TotalCartAmount = cart.TotalCartAmount,
                Quantity = cart.Quantity,
                Status = cart.Status,
                CreatedAt = cart.CreatedAt,
                CartId = cart.Id
            },
            cancellationToken);
        }
    }
}
