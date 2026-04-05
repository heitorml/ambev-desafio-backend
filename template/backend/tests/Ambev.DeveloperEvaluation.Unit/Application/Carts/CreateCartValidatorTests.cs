using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts
{
    public class CreateCartValidatorTests
    {
        private readonly CreateCartValidator _validator;

        public CreateCartValidatorTests()
        {
            _validator = new CreateCartValidator();
        }

        [Fact(DisplayName = "Given valid cart data When validating Then should not have errors")]
        public void Validate_ValidCommand_ShouldNotHaveErrors()
        {
            // Given
            var command = CreateCartHandlerTestData.GenerateValidCommand();

            // When
            var result = _validator.TestValidate(command);

            // Then
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory(DisplayName = "Given invalid branch name When validating Then should have error")]
        [InlineData("")]
        [InlineData("br")]
        [InlineData("This branch name is definitely way too long to be valid according to the rules")]
        public void Validate_InvalidBranch_ShouldHaveError(string branch)
        {
            // Given
            var command = CreateCartHandlerTestData.GenerateValidCommand();
            command.Branch = branch;

            // When
            var result = _validator.TestValidate(command);

            // Then
            result.ShouldHaveValidationErrorFor(x => x.Branch);
        }

        [Fact(DisplayName = "Given empty products list When validating Then should have error")]
        public void Validate_EmptyProducts_ShouldHaveError()
        {
            // Given
            var command = CreateCartHandlerTestData.GenerateValidCommand();
            command.Products = new List<CreateCartItemCommand>();

            // When
            var result = _validator.TestValidate(command);

            // Then
            result.ShouldHaveValidationErrorFor(x => x.Products);
        }
    }
}
