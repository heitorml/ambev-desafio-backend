using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Auth
{
    public class AuthenticateUserValidatorTests
    {
        private readonly AuthenticateUserValidator _validator;

        public AuthenticateUserValidatorTests()
        {
            _validator = new AuthenticateUserValidator();
        }

        [Fact(DisplayName = "Given valid credentials When validating Then should not have errors")]
        public void Validate_ValidCommand_ShouldNotHaveErrors()
        {
            // Given
            var command = new AuthenticateUserCommand { Email = "test@test.com", Password = "password123" };

            // When
            var result = _validator.TestValidate(command);

            // Then
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory(DisplayName = "Given invalid email When validating Then should have error")]
        [InlineData("")]
        [InlineData("invalid-email")]
        public void Validate_InvalidEmail_ShouldHaveError(string email)
        {
            // Given
            var command = new AuthenticateUserCommand { Email = email, Password = "password123" };

            // When
            var result = _validator.TestValidate(command);

            // Then
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }
    }
}
