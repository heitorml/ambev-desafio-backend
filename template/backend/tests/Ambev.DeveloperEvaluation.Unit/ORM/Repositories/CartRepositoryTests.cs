using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.ORM.Repositories
{
    public class CartRepositoryTests
    {
        private readonly DefaultContext _context;
        private readonly CartRepository _repository;
        private readonly IDistributedCache _cache;

        public CartRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DefaultContext(options);
            _cache = Substitute.For<IDistributedCache>();
            _repository = new CartRepository(_context, _cache);
        }

        [Fact(DisplayName = "Given new cart When creating Then should save to database")]
        public async Task CreateAsync_ValidCart_SavesToDatabase()
        {
            // Given
            var cart = new Cart 
            { 
                Id = Guid.NewGuid(), 
                UserId = Guid.NewGuid(),
                Branch = "Test" 
            };

            // When
            var createdCart = await _repository.CreateAsync(cart);

            // Then
            createdCart.Should().NotBeNull();
            var dbCart = await _context.Carts.FindAsync(cart.Id);
            dbCart.Should().NotBeNull();
            dbCart.Branch.Should().Be(cart.Branch);
        }

        [Fact(DisplayName = "Given existing cart When getting by ID Then returns cart with products")]
        public async Task GetByIdAsync_ExistingCart_ReturnsCartWithProducts()
        {
            // Given
            var userId = Guid.NewGuid();
            var user = new User 
            {
                Id = userId,
                Email = "test@test.com", 
                Username = "test", 
                Password = "password",
                Role = UserRole.Customer,
                Status = UserStatus.Active 
            };
            await _context.Users.AddAsync(user);

            var cart = new Cart 
            { 
                Id = Guid.NewGuid(), 
                UserId = userId, 
                Branch = "Test" 
            };

            var product = new CartProducts 
            { 
                Id = Guid.NewGuid(),
                CartId = cart.Id, 
                ProductId = Guid.NewGuid(), 
                Quantity = 1 
            };

            cart.Products.Add(product);
            
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();

            // When
            var foundCart = await _repository.GetByIdAsync(cart.Id);

            // Then
            foundCart.Should().NotBeNull();
            foundCart.Products.Should().NotBeEmpty();
        }

        [Fact(DisplayName = "Given existing cart When deleting Then should remove from database and invalidate cache")]
        public async Task DeleteAsync_ExistingCart_RemovesFromDatabase()
        {
            // Given
            var userId = Guid.NewGuid();
            var user = new User 
            {
                Id = userId,
                Email = "test@test.com", 
                Username = "test", 
                Password = "password",
                Role = UserRole.Customer,
                Status = UserStatus.Active 
            };
            await _context.Users.AddAsync(user);

            var cart = new Cart { Id = Guid.NewGuid(), UserId = userId, Branch = "Test" };
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();

            // When
            var result = await _repository.DeleteAsync(cart.Id);

            // Then
            result.Should().BeTrue();
            var dbCart = await _context.Carts.FindAsync(cart.Id);
            dbCart.Should().BeNull();

            // Verify cache removed
            await _cache.Received(1).RemoveAsync(Arg.Is<string>(k => k.Contains(cart.Id.ToString())), Arg.Any<CancellationToken>());
        }
    }
}
