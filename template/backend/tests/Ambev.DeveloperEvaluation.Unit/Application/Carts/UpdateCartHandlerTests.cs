using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using MassTransit;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts
{
    public class UpdateCartHandlerTests
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly UpdateCartHandler _handler;

        public UpdateCartHandlerTests()
        {
            _cartRepository = Substitute.For<ICartRepository>();
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _bus = Substitute.For<IBus>();
            _handler = new UpdateCartHandler(_mapper, _cartRepository, _productRepository, _bus);
        }

        [Fact(DisplayName = "Given valid cart data When updating cart Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = UpdateCartHandlerTestData.GenerateValidCommand();
            var cart = new Cart
            {
                Id = command.Id,
                UserId = command.UserId,
                Branch = command.Branch
            };

            var result = new UpdateCartResult { Id = cart.Id, Success = true };

            _cartRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(cart);
            _mapper.Map<UpdateCartResult>(cart).Returns(result);

            // Mock the void Map call
            _mapper.When(x => x.Map(Arg.Any<object>(), Arg.Any<object>(), Arg.Any<Action<IMappingOperationOptions<object, object>>>()))
                   .Do(_ => { });

            // When
            var updateCartResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            updateCartResult.Should().NotBeNull();
            updateCartResult.Success.Should().BeTrue();
        }

        [Fact(DisplayName = "Given non-existent cart When updating cart Then returns failure response")]
        public async Task Handle_NonExistentCart_ReturnsFailureResponse()
        {
            // Given
            var command = UpdateCartHandlerTestData.GenerateValidCommand();
            _cartRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((Cart)null);

            // When
            var updateCartResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            updateCartResult.Should().NotBeNull();
            updateCartResult.Success.Should().BeFalse();
        }
    }
}
