using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.ORM.Repositories
{
    public class ProductRepositoryTests
    {
        private readonly DefaultContext _context;
        private readonly ProductRepository _repository;
        private readonly IDistributedCache _cache;

        public ProductRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DefaultContext(options);
            _cache = Substitute.For<IDistributedCache>();
            _repository = new ProductRepository(_context, _cache);
        }

        [Fact(DisplayName = "Given new product When creating Then should save to database")]
        public async Task CreateAsync_ValidProduct_SavesToDatabase()
        {
            // Given
            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "Test Product",
                UnitPrice = 10.5m
            };

            // When
            var createdProduct = await _repository.CreateAsync(product);

            // Then
            createdProduct.Should().NotBeNull();
            var dbProduct = await _context.Products.FindAsync(product.Id);
            dbProduct.Should().NotBeNull();
            dbProduct.ProductName.Should().Be(product.ProductName);
        }

        [Fact(DisplayName = "Given existing product When getting by ID Then returns product")]
        public async Task GetByIdAsync_ExistingProduct_ReturnsProduct()
        {
            // Given
            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "Test Product",
                UnitPrice = 10.5m
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            // When
            var foundProduct = await _repository.GetByIdAsync(product.Id);

            // Then
            foundProduct.Should().NotBeNull();
            foundProduct!.Id.Should().Be(product.Id);
        }

        [Fact(DisplayName = "Given existing product When deleting Then should remove from database")]
        public async Task DeleteAsync_ExistingProduct_RemovesFromDatabase()
        {
            // Given
            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "Test Product",
                UnitPrice = 10.5m
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            // When
            var result = await _repository.DeleteAsync(product.Id);

            // Then
            result.Should().BeTrue();
            var dbProduct = await _context.Products.FindAsync(product.Id);
            dbProduct.Should().BeNull();

            // Verify cache removed
            await _cache.Received(1).RemoveAsync(Arg.Is<string>(k => k.Contains(product.Id.ToString())), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given multiple products When getting by IDs Then returns list of products")]
        public async Task GetByIdsAsync_MultipleProducts_ReturnsProducts()
        {
            // Given
            var product1 = new Product 
            { 
                Id = Guid.NewGuid(),
                ProductName = "P1",
                UnitPrice = 1 
            };
            
            var product2 = new Product 
            { 
                Id = Guid.NewGuid(), 
                ProductName = "P2", 
                UnitPrice = 2 
            };

            await _context.Products.AddRangeAsync(product1, product2);
            await _context.SaveChangesAsync();

            var ids = new List<Guid> { product1.Id, product2.Id };

            // When
            var products = await _repository.GetByIdsAsync(ids, CancellationToken.None);

            // Then
            products.Should().HaveCount(2);
            products.Should().Contain(p => p.Id == product1.Id);
            products.Should().Contain(p => p.Id == product2.Id);
        }
    }
}
