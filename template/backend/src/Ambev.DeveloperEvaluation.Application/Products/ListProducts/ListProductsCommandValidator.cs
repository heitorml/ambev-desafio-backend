using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts
{
    public class ListProductsCommandValidator : AbstractValidator<ListProductsCommand>
    {
        public ListProductsCommandValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
        }
    }
}
