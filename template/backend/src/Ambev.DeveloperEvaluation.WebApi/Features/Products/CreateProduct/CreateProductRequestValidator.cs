using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(product => product.ProductName).NotEmpty().MaximumLength(255);
            RuleFor(product => product.UnitPrice).GreaterThan(0);
            RuleFor(product => product.Quantity).GreaterThanOrEqualTo(0);
        }
    }
}
