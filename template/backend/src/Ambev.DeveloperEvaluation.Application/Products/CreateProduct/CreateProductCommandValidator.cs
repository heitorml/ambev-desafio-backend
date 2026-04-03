using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(product => product.ProductName).NotEmpty().MaximumLength(255);
        RuleFor(product => product.UnitPrice).GreaterThan(0);
        RuleFor(product => product.Quantity).GreaterThanOrEqualTo(0);
    }
}
