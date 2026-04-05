using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using MassTransit;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts
{
    public class CreateCartHandlerTests
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly CreateCartHandler _handler;

        public CreateCartHandlerTests()
        {
            _cartRepository = Substitute.For<ICartRepository>();
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _bus = Substitute.For<IBus>();
            _handler = new CreateCartHandler(_cartRepository, _mapper, _productRepository, _bus);
        }

        [Fact(DisplayName = "Given valid cart data When creating cart Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = CreateCartHandlerTestData.GenerateValidCommand();
            var cart = new Cart
            {
                Id = Guid.NewGuid(),
                UserId = command.UserId,
                Branch = command.Branch
            };

            var result = new CreateCartResult { Id = cart.Id };

            _mapper.Map<Cart>(Arg.Any<object>(), Arg.Any<Action<IMappingOperationOptions<object, Cart>>>())
                .Returns(cart);
            _mapper.Map<CreateCartResult>(cart).Returns(result);
            _cartRepository.CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>()).Returns(cart);

            // When
            var createCartResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            createCartResult.Should().NotBeNull();
            createCartResult.Id.Should().Be(cart.Id);
            await _cartRepository.Received(1).CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>());
            await _bus.Received(1).Publish(Arg.Any<SaleCreatedEvent>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given invalid cart data When creating cart Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Given
            var command = new CreateCartCommand();

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        }
    }
}
