using Ambev.DeveloperEvaluation.Application.Carts.ListCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts
{
    public class ListCartHandlerTests
    {
        private readonly DefaultContext _context;
        private readonly CartRepository _repository;
        private readonly IMapper _mapper;
        private readonly ListCartHandler _handler;

        public ListCartHandlerTests()
        {
            var options = new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DefaultContext(options);
            _repository = new CartRepository(_context);
            _mapper = Substitute.For<IMapper>();
            _handler = new ListCartHandler(_repository, _mapper);
        }

        [Fact(DisplayName = "Given valid list request When listing carts Then returns paginated results")]
        public async Task Handle_ValidRequest_ReturnsPaginatedResults()
        {
            // Given
            var command = new ListCartCommand { PageNumber = 1, PageSize = 10 };
            var cart = new Cart { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), Branch = "Test" };
            _context.Carts.Add(cart);
            _context.SaveChanges();

            var results = new List<ListCartResult> { new ListCartResult { Id = cart.Id } };
            _mapper.Map<List<ListCartResult>>(Arg.Any<List<Cart>>()).Returns(results);

            // When
            var pagedResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            pagedResult.Should().NotBeNull();
            pagedResult.Items.Should().HaveCount(1);
            pagedResult.TotalCount.Should().Be(1);
        }
    }
}
