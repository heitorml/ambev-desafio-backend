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
    public class UserRepositoryTests
    {
        private readonly DefaultContext _context;
        private readonly UserRepository _repository;
        private readonly IDistributedCache _cache;

        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DefaultContext(options);
            _cache = Substitute.For<IDistributedCache>();
            _repository = new UserRepository(_context, _cache);
        }

        [Fact(DisplayName = "Given new user When creating Then should save to database")]
        public async Task CreateAsync_ValidUser_SavesToDatabase()
        {
            // Given
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "testuser",
                Email = "test@example.com",
                Password = "Password123!",
                Role = UserRole.Customer,
                Status = UserStatus.Active
            };

            // When
            var createdUser = await _repository.CreateAsync(user);

            // Then
            createdUser.Should().NotBeNull();
            var dbUser = await _context.Users.FindAsync(user.Id);
            dbUser.Should().NotBeNull();
            dbUser.Username.Should().Be(user.Username);
        }

        [Fact(DisplayName = "Given existing user When getting by ID Then returns user")]
        public async Task GetByIdAsync_ExistingUser_ReturnsUser()
        {
            // Given
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "testuser",
                Email = "test@example.com",
                Password = "Password123!",
                Role = UserRole.Customer,
                Status = UserStatus.Active
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // When
            var foundUser = await _repository.GetByIdAsync(user.Id);

            // Then
            foundUser.Should().NotBeNull();
            foundUser!.Id.Should().Be(user.Id);
        }

        [Fact(DisplayName = "Given existing user When getting by email Then returns user")]
        public async Task GetByEmailAsync_ExistingUser_ReturnsUser()
        {
            // Given
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "testuser",
                Email = "test@example.com",
                Password = "Password123!",
                Role = UserRole.Customer,
                Status = UserStatus.Active
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // When
            var foundUser = await _repository.GetByEmailAsync(user.Email);

            // Then
            foundUser.Should().NotBeNull();
            foundUser!.Email.Should().Be(user.Email);
        }

        [Fact(DisplayName = "Given existing user When deleting Then should remove from database")]
        public async Task DeleteAsync_ExistingUser_RemovesFromDatabase()
        {
            // Given
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "testuser",
                Email = "test@example.com",
                Password = "Password123!",
                Role = UserRole.Customer,
                Status = UserStatus.Active
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // When
            var result = await _repository.DeleteAsync(user.Id);

            // Then
            result.Should().BeTrue();
            var dbUser = await _context.Users.FindAsync(user.Id);
            dbUser.Should().BeNull();

            // Verify cache removed
            await _cache.Received(1).RemoveAsync(Arg.Is<string>(k => k.Contains(user.Id.ToString())), Arg.Any<CancellationToken>());
        }
    }
}
