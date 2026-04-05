using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using MassTransit;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class CreateProductHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly CreateProductHandler _handler;

        public CreateProductHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _bus = Substitute.For<IBus>();
            _handler = new CreateProductHandler(_productRepository, _mapper, _bus);
        }

        [Fact(DisplayName = "Given valid product data When creating product Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = new CreateProductCommand
            {
                ProductName = "Product Test",
                UnitPrice = 10,
                Quantity = 5
            };

            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = command.ProductName,
                UnitPrice = command.UnitPrice
            };

            var result = new CreateProductResult
            {
                Id = product.Id,
                ProductName = product.ProductName,
                UnitPrice = product.UnitPrice
            };

            _mapper.Map<Product>(command).Returns(product);
            _mapper.Map<CreateProductResult>(product).Returns(result);

            _productRepository.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>())
                .Returns(product);

            // When
            var createProductResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            createProductResult.Should().NotBeNull();
            createProductResult.Id.Should().Be(product.Id);
            createProductResult.TotalPrice.Should().Be(50);
            await _productRepository.Received(1).CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given invalid product data When creating product Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Given
            var command = new CreateProductCommand(); // Empty command fails validation

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        }
    }
}
