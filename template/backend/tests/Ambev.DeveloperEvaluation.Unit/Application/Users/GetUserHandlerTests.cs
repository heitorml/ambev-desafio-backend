using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users
{
    public class GetUserHandlerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly GetUserHandler _handler;

        public GetUserHandlerTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetUserHandler(_userRepository, _mapper);
        }

        [Fact(DisplayName = "Given valid user ID When getting user Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var userId = Guid.NewGuid();
            var command = new GetUserCommand(userId);
            var user = new User { Id = userId };
            var result = new GetUserResult { Id = userId };

            _userRepository.GetByIdAsync(userId, Arg.Any<CancellationToken>()).Returns(user);
            _mapper.Map<GetUserResult>(user).Returns(result);

            // When
            var getUserResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            getUserResult.Should().NotBeNull();
            getUserResult.Id.Should().Be(userId);
        }
    }
}
