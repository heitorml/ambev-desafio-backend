using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartValidator : AbstractValidator<CreateCartCommand>
    {
        public CreateCartValidator()
        {
            RuleFor(cart => cart.Branch).NotEmpty().Length(3, 50);
            RuleFor(cart => cart.UserId).NotEmpty();

            RuleFor(cart => cart.Products).NotEmpty();
        }
    }
}
