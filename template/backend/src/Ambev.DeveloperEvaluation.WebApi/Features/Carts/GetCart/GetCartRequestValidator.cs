using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart
{
    public class GetCartRequestValidator : AbstractValidator<GetCartRequest>
    {
        public GetCartRequestValidator()
        {
            RuleFor(Cart => Cart.Id).NotEmpty().NotNull();
        }
    }
}
