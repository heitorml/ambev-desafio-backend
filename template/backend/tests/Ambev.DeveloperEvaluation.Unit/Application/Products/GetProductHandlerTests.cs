using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class GetProductHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly GetProductHandler _handler;

        public GetProductHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetProductHandler(_productRepository, _mapper);
        }

        [Fact(DisplayName = "Given valid product ID When getting product Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var productId = Guid.NewGuid();
            var command = new GetProductCommand(productId);
            var product = new Product { Id = productId };
            var result = new GetProductResult { Id = productId };

            _productRepository.GetByIdAsync(productId, Arg.Any<CancellationToken>()).Returns(product);
            _mapper.Map<GetProductResult>(product).Returns(result);

            // When
            var getProductResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            getProductResult.Should().NotBeNull();
            getProductResult.Id.Should().Be(productId);
        }
    }
}
