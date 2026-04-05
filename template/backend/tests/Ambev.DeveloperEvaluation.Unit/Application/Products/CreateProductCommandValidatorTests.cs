using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class CreateProductCommandValidatorTests
    {
        private readonly CreateProductCommandValidator _validator;

        public CreateProductCommandValidatorTests()
        {
            _validator = new CreateProductCommandValidator();
        }

        [Fact(DisplayName = "Given valid product data When validating Then should not have errors")]
        public void Validate_ValidCommand_ShouldNotHaveErrors()
        {
            // Given
            var command = ProductHandlerTestData.GenerateValidCreateCommand();

            // When
            var result = _validator.TestValidate(command);

            // Then
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact(DisplayName = "Given empty name When validating Then should have error")]
        public void Validate_EmptyName_ShouldHaveError()
        {
            // Given
            var command = ProductHandlerTestData.GenerateValidCreateCommand();
            command.ProductName = "";

            // When
            var result = _validator.TestValidate(command);

            // Then
            result.ShouldHaveValidationErrorFor(x => x.ProductName);
        }

        [Fact(DisplayName = "Given zero or negative price When validating Then should have error")]
        public void Validate_InvalidPrice_ShouldHaveError()
        {
            // Given
            var command = ProductHandlerTestData.GenerateValidCreateCommand();
            command.UnitPrice = 0;

            // When
            var result = _validator.TestValidate(command);

            // Then
            result.ShouldHaveValidationErrorFor(x => x.UnitPrice);
        }
    }
}
