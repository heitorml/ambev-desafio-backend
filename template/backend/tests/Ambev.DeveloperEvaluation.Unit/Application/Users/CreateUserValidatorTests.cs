using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users
{
    public class CreateUserValidatorTests
    {
        private readonly CreateUserCommandValidator _validator;

        public CreateUserValidatorTests()
        {
            _validator = new CreateUserCommandValidator();
        }

        [Fact(DisplayName = "Given valid user data When validating Then should not have errors")]
        public void Validate_ValidCommand_ShouldNotHaveErrors()
        {
            // Given
            var command = CreateUserHandlerTestData.GenerateValidCommand();

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
            var command = CreateUserHandlerTestData.GenerateValidCommand();
            command.Email = email;

            // When
            var result = _validator.TestValidate(command);

            // Then
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Theory(DisplayName = "Given invalid password When validating Then should have error")]
        [InlineData("")]
        [InlineData("123")]
        [InlineData("passwordwithoutupper")]
        public void Validate_InvalidPassword_ShouldHaveError(string password)
        {
            // Given
            var command = CreateUserHandlerTestData.GenerateValidCommand();
            command.Password = password;

            // When
            var result = _validator.TestValidate(command);

            // Then
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }
    }
}
