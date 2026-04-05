using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using MassTransit;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class UpdateProductHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly UpdateProductHandler _handler;

        public UpdateProductHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _bus = Substitute.For<IBus>();
            _handler = new UpdateProductHandler(_productRepository, _mapper, _bus);
        }

        [Fact(DisplayName = "Given valid product data When updating product Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = ProductHandlerTestData.GenerateValidUpdateCommand();
            var product = new Product { Id = command.Id, ProductName = command.ProductName, UnitPrice = command.UnitPrice };
            var result = new UpdateProductResult { Id = product.Id };

            _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(product);
            _mapper.Map<UpdateProductResult>(product).Returns(result);

            // When
            var updateProductResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            updateProductResult.Should().NotBeNull();
        }
    }
}
