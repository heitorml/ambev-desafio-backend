using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Auth
{
    public class AuthenticateUserHandlerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly AuthenticateUserHandler _handler;

        public AuthenticateUserHandlerTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _passwordHasher = Substitute.For<IPasswordHasher>();
            _jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
            _handler = new AuthenticateUserHandler(_userRepository, _passwordHasher, _jwtTokenGenerator);
        }

        [Fact(DisplayName = "Given valid credentials When authenticating Then returns success response with token")]
        public async Task Handle_ValidCredentials_ReturnsToken()
        {
            // Given
            var command = new AuthenticateUserCommand 
            { 
                Email = "test@test.com", 
                Password = "password"
            };

            var user = new User 
            {
                Email = command.Email, 
                Password = "hashedPassword", 
                Username = "test", 
                Role = UserRole.Customer,
                Status = UserStatus.Active 
            };
            var token = "generated-jwt-token";

            _userRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>()).Returns(user);
            _passwordHasher.VerifyPassword(command.Password, user.Password).Returns(true);
            _jwtTokenGenerator.GenerateToken(user).Returns(token);

            // When
            var authResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            authResult.Should().NotBeNull();
            authResult.Token.Should().Be(token);
        }
    }
}
