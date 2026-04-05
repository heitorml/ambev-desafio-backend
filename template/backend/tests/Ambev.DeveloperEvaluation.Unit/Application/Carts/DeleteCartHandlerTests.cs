using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using MassTransit;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts
{
    public class DeleteCartHandlerTests
    {
        private readonly ICartRepository _cartRepository;
        private readonly IBus _bus;
        private readonly DeleteCartHandler _handler;

        public DeleteCartHandlerTests()
        {
            _cartRepository = Substitute.For<ICartRepository>();
            _bus = Substitute.For<IBus>();
            _handler = new DeleteCartHandler(_cartRepository, _bus);
        }

        [Fact(DisplayName = "Given valid cart ID When deleting cart Then returns success true")]
        public async Task Handle_ValidRequest_ReturnsSuccessTrue()
        {
            // Given
            var cartId = Guid.NewGuid();
            var command = new DeleteCartCommand(cartId);
            _cartRepository.DeleteAsync(cartId, Arg.Any<CancellationToken>()).Returns(true);

            // When
            var deleteCartResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            deleteCartResult.Success.Should().BeTrue();
            await _cartRepository.Received(1).DeleteAsync(cartId, Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given non-existent cart ID When deleting cart Then returns success false")]
        public async Task Handle_NonExistentCart_ReturnsSuccessFalse()
        {
            // Given
            var cartId = Guid.NewGuid();
            var command = new DeleteCartCommand(cartId);
            _cartRepository.DeleteAsync(cartId, Arg.Any<CancellationToken>()).Returns(false);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}
