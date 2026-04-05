using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using MassTransit;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users
{
    public class DeleteUserHandlerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IBus _bus;
        private readonly DeleteUserHandler _handler;

        public DeleteUserHandlerTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _bus = Substitute.For<IBus>();
            _handler = new DeleteUserHandler(_userRepository, _bus);
        }

        [Fact(DisplayName = "Given valid user ID When deleting user Then returns true")]
        public async Task Handle_ValidRequest_ReturnsTrue()
        {
            // Given
            var userId = Guid.NewGuid();
            var command = new DeleteUserCommand(userId);
            _userRepository.DeleteAsync(userId, Arg.Any<CancellationToken>()).Returns(true);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Success.Should().BeTrue();
            await _userRepository.Received(1).DeleteAsync(userId, Arg.Any<CancellationToken>());
        }
    }
}
