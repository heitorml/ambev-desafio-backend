using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    public class GetSaleValidator : AbstractValidator<GetSaleRequest>
    {
        public GetSaleValidator()
        {
            RuleFor(sale => sale.Id).NotEmpty().NotNull();
        }
    }
}
