using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using MassTransit;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class DeleteProductHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly IBus _bus;
        private readonly DeleteProductHandler _handler;

        public DeleteProductHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _bus = Substitute.For<IBus>();
            _handler = new DeleteProductHandler(_productRepository, _bus);
        }

        [Fact(DisplayName = "Given valid product ID When deleting product Then returns true")]
        public async Task Handle_ValidRequest_ReturnsTrue()
        {
            // Given
            var productId = Guid.NewGuid();
            var command = new DeleteProductCommand(productId);
            _productRepository.DeleteAsync(productId, Arg.Any<CancellationToken>()).Returns(true);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Success.Should().BeTrue();
            await _productRepository.Received(1).DeleteAsync(productId, Arg.Any<CancellationToken>());
        }
    }
}
