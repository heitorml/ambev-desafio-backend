using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(product => product.Id).NotEmpty();
        RuleFor(product => product.ProductName).NotEmpty().MaximumLength(255);
        RuleFor(product => product.UnitPrice).GreaterThan(0);
        RuleFor(product => product.Quantity).GreaterThanOrEqualTo(0);
    }
}
