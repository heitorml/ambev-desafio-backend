using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts
{
    public class UpdateCartValidatorTests
    {
        private readonly UpdateCartValidador _validator;

        public UpdateCartValidatorTests()
        {
            _validator = new UpdateCartValidador();
        }

        [Fact(DisplayName = "Given valid cart data When validating Then should not have errors")]
        public void Validate_ValidCommand_ShouldNotHaveErrors()
        {
            // Given
            var command = UpdateCartHandlerTestData.GenerateValidCommand();

            // When
            var result = _validator.TestValidate(command);

            // Then
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory(DisplayName = "Given invalid branch name When validating Then should have error")]
        [InlineData("")]
        [InlineData("br")]
        public void Validate_InvalidBranch_ShouldHaveError(string branch)
        {
            // Given
            var command = UpdateCartHandlerTestData.GenerateValidCommand();
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
            var command = UpdateCartHandlerTestData.GenerateValidCommand();
            command.Products = new List<UpdateCartItemCommand>();

            // When
            var result = _validator.TestValidate(command);

            // Then
            result.ShouldHaveValidationErrorFor(x => x.Products);
        }

        [Fact(DisplayName = "Given more than 20 identical product IDs When validating Then should have error")]
        public void Validate_MoreThan20IdenticalProducts_ShouldHaveError()
        {
            // Given
            var command = UpdateCartHandlerTestData.GenerateValidCommand();
            var productId = Guid.NewGuid();
            var products = new List<UpdateCartItemCommand>();
            for (int i = 0; i < 21; i++)
            {
                products.Add(new UpdateCartItemCommand { Id = productId, Quantity = 1 });
            }
            command.Products = products;

            // When
            var result = _validator.TestValidate(command);

            // Then
            result.ShouldHaveValidationErrorFor(x => x.Products);
        }
    }
}
