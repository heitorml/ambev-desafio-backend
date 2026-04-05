using Ambev.DeveloperEvaluation.Application.Products.ListProducts;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class ListProductsHandlerTests
    {
        private readonly DefaultContext _context;
        private readonly ProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ListProductsHandler _handler;

        public ListProductsHandlerTests()
        {
            var options = new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DefaultContext(options);
            _repository = new ProductRepository(_context, Substitute.For<IDistributedCache>());
            _mapper = Substitute.For<IMapper>();
            _handler = new ListProductsHandler(_repository, _mapper);
        }

        [Fact(DisplayName = "Given valid list request When listing products Then returns paginated results")]
        public async Task Handle_ValidRequest_ReturnsPaginatedResults()
        {
            // Given
            var command = new ListProductsCommand 
            { 
                PageNumber = 1, 
                PageSize = 10 
            };
            
            var product = new Product 
            { 
                Id = Guid.NewGuid(),
                ProductName = "Test Product",
                UnitPrice = 10 
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            var results = new List<ListProductsResult> { new ListProductsResult { Id = product.Id } };
            _mapper.Map<List<ListProductsResult>>(Arg.Any<List<Product>>()).Returns(results);

            // When
            var pagedResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            pagedResult.Should().NotBeNull();
            pagedResult.Items.Should().HaveCount(1);
            pagedResult.TotalCount.Should().Be(1);
        }
    }
}
