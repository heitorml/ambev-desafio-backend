using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleValidator()
        {
            RuleFor(sale => sale.Branch).NotEmpty().Length(3, 50);
            RuleFor(sale => sale.UserId).NotEmpty();

            RuleFor(sale => sale.Products).NotEmpty();
        }
    }
}
