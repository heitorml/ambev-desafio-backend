using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(product => product.Id).NotEmpty();
            RuleFor(product => product.ProductName).NotEmpty().MaximumLength(255);
            RuleFor(product => product.UnitPrice).GreaterThan(0);
            RuleFor(product => product.Quantity).GreaterThanOrEqualTo(0);
        }
    }
}
