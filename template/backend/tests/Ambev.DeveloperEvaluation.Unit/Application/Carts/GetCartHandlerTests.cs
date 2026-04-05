using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts
{
    public class GetCartHandlerTests
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly GetCartHandler _handler;

        public GetCartHandlerTests()
        {
            _cartRepository = Substitute.For<ICartRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetCartHandler(_cartRepository, _mapper);
        }

        [Fact(DisplayName = "Given valid cart ID When getting cart Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var cartId = Guid.NewGuid();
            var command = new GetCartCommand(cartId);
            var cart = new Cart { Id = cartId };
            var result = new GetCartResult { Id = cartId };

            _cartRepository.GetByIdAsync(cartId, Arg.Any<CancellationToken>()).Returns(cart);
            _mapper.Map<GetCartResult>(cart).Returns(result);

            // When
            var getCartResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            getCartResult.Should().NotBeNull();
            getCartResult.Id.Should().Be(cartId);
            await _cartRepository.Received(1).GetByIdAsync(cartId, Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given non-existent cart ID When getting cart Then throws KeyNotFoundException")]
        public async Task Handle_NonExistentCart_ThrowsKeyNotFoundException()
        {
            // Given
            var cartId = Guid.NewGuid();
            var command = new GetCartCommand(cartId);
            _cartRepository.GetByIdAsync(cartId, Arg.Any<CancellationToken>()).Returns((Cart)null);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}
